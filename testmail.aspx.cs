using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Threading;

public partial class testmail : System.Web.UI.Page
{

    FabAccessoriesEntities db = new FabAccessoriesEntities();

    string OrderId = "FFA00070452";
    string OrderNo = "FFA00070452";

    protected void Page_Load(object sender, EventArgs e)
    {
        decimal tot = 0.00m;
        string cartPro = "";


        DataTable dtordr = DataAccess.GetDataTable("select * from OrderTbl where OrderNo='FFA00070452'", CommandType.Text);
        DataTable table = DataAccess.GetDataTable("select * from OrderDetail where OrderNo='FFA00070452'", CommandType.Text);


        if (OrderNo.Trim().Length > 0)
        {
            if (table != null)
            {
                foreach (DataRow cartRow in table.Rows)
                {

                    decimal ProductId = Convert.ToDecimal(cartRow["productid"].ToString());
                    //decimal d = Convert.ToDecimal(ProductId);
                    //string avlqty = "0";

                    // var checkproduct = db.ProductMasters.Where(r => r.Id == d && r.ShowHide == 1).FirstOrDefault();

                    var checkproduct = (from p in db.ProductMasters
                                        join cate in db.CategoryMasters on p.CategoryId equals cate.CategoryId
                                        where p.Id == ProductId && p.ShowHide == 1
                                        select new { p.Id, p.Image, p.Title, p.SkuName, ItemName = cate.CategoryName, cate.CategoryId }).FirstOrDefault();

                    if (checkproduct != null)
                    {

                        tot += Math.Round(Convert.ToDecimal(cartRow["total"].ToString()));


                        cartPro += " <tr><td><p><img style='float:left;'  src='https://www.fabfashionaccessories.com/upload/products/small/" + cartRow["img"].ToString() + "' />" + cartRow["shortdesc"].ToString() + "</p> </td><td>" + cartRow["modelno"].ToString() + "</td><td> Rs. " + cartRow["price"].ToString() + "</td><td>" + cartRow["qty"].ToString() + "</td><td> Rs. " + cartRow["total"].ToString() + "</td></tr>";
                        cartPro += "<tr><td colspan='5'><hr style='margin:3px' /></td></tr>";

                    }
                }
            }
            string cartProduct = "<table width='100%' style='font-family:Verdana;font-size:11px; border:solid 1px #ccc;'>";
            cartProduct += "<tr><td style='width:40%'>  </td><td style='width:15%'>Model No</td><td style='width:15%'>Unit Price</td><td style='width:15%'>Quantity</td><td style='width:15%'>Total</td> </tr>";
            cartProduct += "<tr><td colspan='5'><hr/></td></tr>";
            cartProduct += cartPro;

            cartProduct += "<tr><td></td><td></td><td></td><td>Sub-Total:</td><td> Rs. " + dtordr.Rows[0]["ordertotal"].ToString() + "</td></tr>";
            cartProduct += "<tr><td></td><td></td><td></td><td>Shipping Charges:</td><td> Rs. " + dtordr.Rows[0]["Shippingamt"].ToString() + "</td></tr>";

            if (Convert.ToDecimal(dtordr.Rows[0]["couponamt"].ToString()) > 0)
            {
                cartProduct += "<tr><td></td><td></td><td></td><td>Discount:</td><td> Rs. " + dtordr.Rows[0]["couponamt"].ToString() + "</td></tr>";
            }
            else
            {
                //cartProduct += "<tr><td></td><td></td><td></td><td>Discount:</td><td> Rs. " + 0 + "</td></tr>";
            }
            cartProduct += "<tr><td></td><td></td><td></td><td colspan='2'> <hr style='margin:3px' /></td></tr>";
            cartProduct += "<tr><td></td><td></td><td></td><td>Total Amt:</td><td> Rs. " + (Convert.ToDecimal(dtordr.Rows[0]["ordertotal"].ToString()) + Convert.ToDecimal(dtordr.Rows[0]["Shippingamt"].ToString()) - Convert.ToDecimal(dtordr.Rows[0]["couponamt"].ToString())).ToString("0.00") + "</td></tr>";
            cartProduct += " </table>";



            sendEmail(cartProduct, OrderNo, Convert.ToDecimal(dtordr.Rows[0]["ordertotal"].ToString()), dtordr.Rows[0]["Shippingamt"].ToString(), "INR", dtordr.Rows[0]["couponamt"].ToString());


        }
    }


    void sendEmail(string cart, string orderno, decimal amt, string shippingamt, string cur, string discamt)
    {
        var billingdetail = (from b in db.OrderBillingInfoes
                             join s in db.OrderShippingDetails on b.OrderNo equals s.OrderNo
                             where b.OrderNo == OrderId
                             select new
                             {
                                 b.OrderNo,
                                 b.Name,
                                 b.State,
                                 b.Zip,
                                 b.Id,
                                 b.Email,
                                 b.Country,
                                 b.ContactNo,
                                 b.City,
                                 b.Address2,
                                 b.Address,
                                 sname = s.Name,
                                 sstate = s.State,
                                 scountry = s.Country,
                                 szip = s.Zip,
                                 semail = s.Email,
                                 saddress = s.Address,
                                 saddress2 = s.Address2,
                                 scontact = s.ContactNo,
                                 scity = s.City
                             }).FirstOrDefault();
        if (billingdetail != null)
        {
            string msg = "";
            msg = msg + "";
           // msg = "<table cellpadding='5' cellspacing='0' width='100%'  style='border:1px solid #efefef;'><thead style='background-color:#efefef'><tr> <td style='width: 40%'></td><td style='width: 15%'>Model No</td><td style='width: 15%'>Unit Price</td><td style='width: 15%'>Quantity</td><td style='width: 15%'>Total</td> </tr></thead> <tbody>";

            msg = msg + cart;

            //decimal Total = Convert.ToDecimal(amt) + Convert.ToDecimal(shippingamt) - Convert.ToDecimal(discamt);

            //msg = msg + "<tr><td colspan='2' style='border-top: 1px solid #ccc;text-align:left;'></td><td colspan='2' style='border-top: 1px solid #ccc;font-size: 13px;text-align:right;padding:5px;'>Sub Total (" + cur + "):</td><td style='border-top: 1px solid #ccc;font-size: 13px;padding:5px;' align='center'>" + amt + "</td></tr>";

            //decimal Grandtotal = 0;

            //if (Convert.ToDecimal(discamt) > 0)
            //{
            //    Grandtotal = Convert.ToDecimal(amt) + Convert.ToDecimal(shippingamt) - Convert.ToDecimal(discamt);

            //    msg = msg + "<tr><td colspan='2' style='border-top: 1px solid #ccc;text-align:left;'></td><td colspan='2' style='border-top: 1px solid #ccc;font-size: 13px;text-align:right;padding:5px;'>Discount(" + cur + "):</td><td style='border-top: 1px solid #ccc;font-size: 13px;padding: 5px;' align='center'>" + discamt + "</td></tr>";
            //    msg = msg + "<tr><td colspan='2' style='border-top: 1px solid #ccc;text-align:left;'></td><td colspan='2' style='border-top: 1px solid #ccc;font-size: 13px;text-align:right;padding:5px;'>Grand Total(" + cur + "):</td><td style='border-top: 1px solid #ccc;font-size: 13px;padding: 5px;' align='center'>" + Grandtotal + "</td></tr>";
            //}
            //else
            //{
            //    Grandtotal = Convert.ToDecimal(amt) + Convert.ToDecimal(shippingamt);
            //    msg = msg + "<tr><td colspan='2' style='border-top: 1px solid #ccc;text-align:left;'></td><td colspan='2' style='border-top: 1px solid #ccc;font-size: 13px;text-align:right;padding:5px;'>Grand Total(" + cur + "):</td><td style='border-top: 1px solid #ccc;font-size: 13px;padding: 5px;' align='center'>" + Grandtotal + "</td></tr>";
            //}

            //msg += "</tbody></table>";

            msg = msg + "<div><table border='0' cellspacing='0' cellpadding='0' width='620' style='background:#f9f9f9;'><tbody><tr><td style='padding: 10px 12px;'>";

            msg = msg + "<p style='margin:0; padding:0px 0; font-weight:600;font-size:14px; padding:10px 0'>Delivery Address:</p>";

            msg = msg + "<p style='margin:0; padding:0px 0; font-weight:500;font-size:12px;'>" + billingdetail.sname + ", " + billingdetail.saddress + ", " + billingdetail.saddress2 + "<br />";

            msg = msg + billingdetail.scity + ", " + billingdetail.sstate + "<br />";

            msg = msg + billingdetail.scountry + ", " + billingdetail.szip + "<br />";

            msg = msg + "Contact No.: " + billingdetail.scontact + "<br />";

            msg = msg + "Email: " + billingdetail.semail + "<br />";

            msg = msg + "</p></td></tr>";
            msg += "</tbody></table></div>";

            sendmail sm = new sendmail();
            string body = "";
            String FILENAME = HttpContext.Current.Server.MapPath(@"~\emailer\order.html");
            StreamReader objStreamReader = File.OpenText(FILENAME);
            body = objStreamReader.ReadToEnd();
            body = body.Replace("##name##", (billingdetail.Name).First().ToString().ToUpper() + (billingdetail.Name).Substring(1));
            body = body.Replace("##orderno##", orderno);
            body = body.Replace("##itemdetail##", msg);

            try
            {
                string responseq = SendMail(billingdetail.Name, billingdetail.Email, billingdetail.scontact, "Your Order at Fab Fashion Accessories has been placed successfully.", body, "Client");
                string sendmailtoadmin = SendMail(billingdetail.Name, billingdetail.Email, billingdetail.scontact, "Order on Fab Fashion Accessories (Success)", body, "Admin");
            }
            catch (Exception ex)
            {
                Common.LogError(ex);
            }
        }
    }


    public string SendMail(string name, string email, string contact, string subject, string mailbody, string mailto)
    {
        string res = "";

        MailMessage messagecust = new MailMessage();
        messagecust.From = new MailAddress("apps@fabfashionaccessories.com", "Fab Fashion Accessories");

        if (mailto == "Admin")
        {
            messagecust.To.Add(new MailAddress("apps@fabfashionaccessories.com"));
            messagecust.Bcc.Add(new MailAddress("web@jewelsinfosystems.com"));
            messagecust.ReplyToList.Add(new MailAddress(email, name));
        }
        else if (mailto == "Client")
        {
            messagecust.To.Add(new MailAddress(email));
            messagecust.Bcc.Add(new MailAddress("web@jewelsinfosystems.com"));
            
            messagecust.ReplyToList.Add(new MailAddress("apps@fabfashionaccessories.com", "Fab Fashion Accessories"));
        }
        //else if (mailto == "all")
        //{
        //    messagecust.To.Add(new MailAddress(email));
        //    messagecust.Bcc.Add(new MailAddress("apps@fabfashionaccessories.com"));
           
        //    messagecust.ReplyToList.Add(new MailAddress("apps@fabfashionaccessories.com", "Fab Fashion Accessories"));
        //}

        messagecust.Subject = subject;
        messagecust.Body = mailbody;
        messagecust.IsBodyHtml = true;

        SmtpClient smcust = new SmtpClient();

        try
        {
            smcust.Send(messagecust);
            res = "Sucess";
        }
        catch (Exception ex)
        {
            res = ex.ToString();
        }
        //try
        //{
        //    Thread emails = new Thread(delegate()
        //    {
        //       smcust.Send(messagecust);

        //    });
        //    emails.IsBackground = true;
        //    emails.Start();
        //}
        //catch (Exception ex)
        //{
        //    //res = ex.ToString();
        //    Common.LogError(ex);
        //}
        res = "Sucess";
        return res;
    }

}