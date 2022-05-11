using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ipaytm : System.Web.UI.Page
{
    FabAccessoriesEntities db = new FabAccessoriesEntities();
    string amount = "";
    string discountamt = "";
    string shippingamount = "";
    string iorderno = "";

    string contno = "";
    string uid = "";
    string usermail;
    // string totalamt = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            if (Request.QueryString["amount"] != null && Request.QueryString["discount"] != null && Request.QueryString["shippingAmt"] != null && Request.QueryString["OrderNo"] != null && Request.QueryString["usermail"] != null)
            {
                amount = Request.QueryString["amount"];
                discountamt = Request.QueryString["discount"];
                shippingamount = Request.QueryString["shippingAmt"];
                iorderno = Request.QueryString["OrderNo"];
                usermail = Request.QueryString["usermail"];

                if (Request.QueryString["contno"] != null)
                {
                    contno = Request.QueryString["contno"];

                }
                if (Request.QueryString["uid"] != null)
                {
                    uid = Request.QueryString["uid"];
                    Session["UserId"] = uid;

                }
                //if (Request.QueryString["OrderNo"] != null)
                //{

                //}



                if (amount != "" && discountamt != "" && shippingamount != "" && iorderno != "" && usermail != "")
                {
                    paytmcheckout(amount, discountamt, shippingamount, iorderno,usermail,contno,uid);
                }
                else
                {

                }
            }

        }

    }
    public void paytmcheckout(string amt, string discount, string shippingAmt, string OrderNo, string usermail1, string contno1, string uid1)
    {
        // string usermail = "";
        //string contno = "";
        //string uid = "";
        string totalamt = "";

        if (Session["UserId"] != null)
        {
            Int64 id = Convert.ToInt64(Session["UserId"].ToString());
            var user = db.UserInfoes.Where(r => r.Id == id).FirstOrDefault();
            if (user != null)
            {
                usermail = user.Email.ToLower();
                contno = user.ContactNo;
                uid = user.Id.ToString();

               // totalamt = (Math.Floor(Math.Abs(Convert.ToDecimal(amt))) - Convert.ToDecimal(discount) + Math.Floor(Math.Abs(Convert.ToDecimal(shippingAmt)))).ToString();

                totalamt = (Convert.ToDecimal(amt) - Convert.ToDecimal(discount) + Convert.ToDecimal(shippingAmt)).ToString("0.00");

                //totalamt = ltrlTotal.Text;
            }

            var chkpay = db.OrderPaymentResponses.Where(r => r.OrderId == OrderNo).FirstOrDefault();
            if (chkpay != null)
            {
                chkpay.CreateDate = System.DateTime.Now;
                chkpay.PaymentId = "";
                chkpay.PaymentMode = "PAYTM";
                chkpay.Status = "Unpaid";
                chkpay.Message = "";
                chkpay.Mode = "PAYTM";
                chkpay.CreateDate = System.DateTime.Now;
                db.SaveChanges();
            }
            else
            {
                OrderPaymentResponse pay = new OrderPaymentResponse();
                pay.OrderId = OrderNo;
                pay.PaymentId = "";
                pay.PaymentMode = "PAYTM";
                pay.Status = "Unpaid";
                pay.Message = "";
                pay.Mode = "PAYTM";
                pay.CreateDate = System.DateTime.Now;
                db.OrderPaymentResponses.Add(pay);
                db.SaveChanges();
            }





        }


        Session["ordId"] = OrderNo;
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        //parameters.Add("MID", "DfyEYC52335235080051");
        //parameters.Add("CHANNEL_ID", "WEB");
        //parameters.Add("INDUSTRY_TYPE_ID", "Retail");
        //parameters.Add("WEBSITE", "DEFAULT");
        //parameters.Add("EMAIL", usermail);
        //parameters.Add("MOBILE_NO", contno);
        //parameters.Add("CUST_ID", uid);
        //parameters.Add("ORDER_ID", OrderNo);
        //parameters.Add("TXN_AMOUNT", totalamt);
        parameters.Add("MID", "DfyEYC52335235080051");
        parameters.Add("CHANNEL_ID", "WEB");
        parameters.Add("INDUSTRY_TYPE_ID", "Retail");
        parameters.Add("WEBSITE", "DEFAULT");
        parameters.Add("EMAIL", usermail);
        parameters.Add("MOBILE_NO", contno);
        parameters.Add("CUST_ID", uid);
        parameters.Add("ORDER_ID", OrderNo + "@" + System.DateTime.Now.Ticks.ToString().Substring(6, 5));
        parameters.Add("TXN_AMOUNT", totalamt);

        parameters.Add("CALLBACK_URL", "https://www.sstylefactory.com/ipytmresponsesuccess.aspx");
       




        //This parameter is not mandatory. Use this to pass the callback url dynamically.
        //string merchantKey = "z!W6HJ9UxKF5t@jw";
        string merchantKey = "z!W6HJ9UxKF5t@jw";
        string checksum = paytm.CheckSum.generateCheckSum(merchantKey, parameters);

        //string paytmURL = "https://secure.paytm.in/oltp-web/processTransaction";
        string paytmURL = "https://securegw.paytm.in/theia/processTransaction";

        string outputHTML = "<html>";
        outputHTML += "<head>";
        outputHTML += "<title>Merchant Check Out Page</title>";
        outputHTML += "</head>";
        outputHTML += "<body>";
        outputHTML += "<center><h1>Please do not refresh this page...</h1></center>";
        outputHTML += "<form method='post' action='" + paytmURL + "' name='f1' id='PostForm'>";
        outputHTML += "<table border='1'>";
        outputHTML += "<tbody>";
        foreach (string key in parameters.Keys)
        {
            outputHTML += "<input type='hidden' name='" + key + "' value='" + parameters[key] + "'>";
        }
        outputHTML += "<input type='hidden' name='CHECKSUMHASH' value='" + checksum + "'>";
        outputHTML += "</tbody>";
        outputHTML += "</table>";
        outputHTML += "<script type='text/javascript'>";
        outputHTML += "document.f1.submit();";
        outputHTML += "</script>";
        outputHTML += "</form>";
        outputHTML += "</body>";
        outputHTML += "</html>";
        Response.Write(outputHTML);
    }
}