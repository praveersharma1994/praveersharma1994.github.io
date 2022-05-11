using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_shippedStatus : System.Web.UI.Page
{
    FabAccessoriesEntities db = new FabAccessoriesEntities();
    string momvalue = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getOrderDetail();
            getShippedBy();
        }
    }

    private void getOrderDetail()
    {
        if (Request["order"] != null)
        {
            ltrlOrdero.Text = Request["order"].ToString();
            var ord = (from or in db.OrderTbls
                       join use in db.UserInfoes on or.UserId equals use.Id
                       where or.OrderNo == ltrlOrdero.Text
                       select new { or.OrderDate, use.FirstName, use.Email }).FirstOrDefault();
            if (ord != null)
            {
                ltrlClient.Text = ord.FirstName + " - " + ord.Email;
                ViewState["email"] = ord.Email;
                ViewState["name"] = ord.FirstName;

                ltrlOrdDate.Text = ord.OrderDate.Value.Date.ToString("dd/MM/yyyy");
            }
            if (Request["d"] != null)
            {
                if (Request["d"] == "d")
                {
                    getShipDetail();
                }
            }
        }
    }

    private void getShipDetail()
    {
        var ordTrack = db.OrderTrackings.Where(r => r.OrderNo == ltrlOrdero.Text).FirstOrDefault();
        if (ordTrack != null)
        {
            txtTrackNo.Text = ordTrack.TarckNo;
            txtShippedDate.Text = Convert.ToDateTime(ordTrack.ShippedDate).ToString("dd/MM/yyyy");
            txtRemark.Text = ordTrack.Comments;
            txtExpDeliverDate.Text = Convert.ToDateTime(ordTrack.ExpectedDeliverDate).ToString("dd/MM/yyyy");
        }
    }

    private void getShippedBy()
    {
        var ship = db.ShipMasters.OrderBy(r => r.ShipName).ToList();
        drpShippedBy.DataSource = ship;
        drpShippedBy.DataTextField = "ShipName";
        drpShippedBy.DataValueField = "Id";
        drpShippedBy.DataBind();
    }

    protected void btnReview_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
        dtFormat.ShortDatePattern = "dd/MM/yyyy";
        int insert = 0;
        var ordTrack = db.OrderTrackings.Where(r => r.OrderNo == ltrlOrdero.Text).FirstOrDefault();
        if (ordTrack == null)
        {
            ordTrack = new OrderTracking();
            insert = 1;
        }
        ordTrack.OrderNo = ltrlOrdero.Text;
        ordTrack.Comments = txtRemark.Text;
        ordTrack.CreateDate = DateTime.Now;
        DateTime dtTo = Convert.ToDateTime(txtExpDeliverDate.Text, dtFormat);
        ordTrack.ExpectedDeliverDate = dtTo;
        ordTrack.OrderStatus = "Shipped";
        ordTrack.ShippedBy = drpShippedBy.SelectedItem.Text;
        dtTo = Convert.ToDateTime(txtShippedDate.Text, dtFormat);
        ordTrack.ShippedDate = dtTo;
        ordTrack.StatusDate = DateTime.Now;
        ordTrack.TarckNo = txtTrackNo.Text;
        if (insert == 1) { db.OrderTrackings.Add(ordTrack); }
        db.SaveChanges();

        var orderM = from ordDetail in db.OrderDetails
                     join ord in db.OrderTbls on ordDetail.OrderNo equals ord.OrderNo
                     where ordDetail.OrderNo == ltrlOrdero.Text
                     select new
                     {
                         ordDetail.Img,
                         ordDetail.ModelNo,
                         ordDetail.Item,
                         ord.ShippingAmt,
                         ordDetail.Price,
                         ordDetail.Total,
                         ordDetail.Qty,
                         ordDetail.Size,
                         ord.OrderCurrency,
                         ord.CouponAmt,
                         ord.OrderTotal,
                         // ProductTot = ordDetail.Total
                     };

        string discountamt = orderM.ToList().FirstOrDefault().CouponAmt.ToString();
        string shipamt = orderM.ToList().FirstOrDefault().ShippingAmt.ToString();
        string currency = orderM.ToList().FirstOrDefault().OrderCurrency.ToString();

        var orderstatus = db.OrderPaymentResponses.Where(r => r.OrderId == ltrlOrdero.Text && r.Status.ToLower() == "success").FirstOrDefault();
        if (orderstatus != null)
        {
            decimal amount = 0;
            string cartpro = "";
            var orderdetail = db.OrderDetails.Where(r => r.OrderNo == ltrlOrdero.Text).ToList();
            if (orderdetail != null)
            {
                //cartpro += "<tr><td style='width:40%'></td><td style='width:15%'>Model No</td><td style='width:15%'>Unit Price</td><td style='width:15%'>Quantity</td><td style='width:15%'>Total</td> </tr>";

                string url = "http://www.fabfashionaccessories.com/upload/products/small/";
                foreach (var itemdetail in orderdetail)
                {
                    amount += Convert.ToDecimal(itemdetail.Qty) * Convert.ToDecimal(itemdetail.Price);

                    //cartpro += "<tr><td ><p><img style='float:left;'  src='" + url + itemdetail.Img.ToString() + "'/>" + itemdetail.ShortDesc + "</p> </td><td>" + itemdetail.ModelNo + "</td><td> Rs. " + itemdetail.Price + "</td><td>" + itemdetail.Qty + "</td><td> Rs. " + Convert.ToDecimal(itemdetail.Qty) * Convert.ToDecimal(itemdetail.Price) + "</td></tr>";

                    cartpro += "<tr><td style='padding: 5px 10px;border-right: 1px solid #ccc; vertical-align: top;'><img style='float:left;' width='54px' height='54px'  src='" + url + itemdetail.Img.ToString() + "'/><p style='vertical-align: top;font-size: 13px;width: 65%;float: right;margin: 5px 0;'>" + itemdetail.ShortDesc + "</p> </td><td style='padding: 5px 10px;border-right: 1px solid #ccc; vertical-align: top;'><p style='vertical-align: top;font-size: 13px;'>" + itemdetail.ModelNo + "</p></td><td style='padding: 5px 10px;border-right: 1px solid #ccc; vertical-align: top;'><p style='vertical-align: top;font-size: 13px;'> Rs. " + itemdetail.Price + "</p></td><td style='padding: 5px 10px;border-right: 1px solid #ccc; vertical-align: top;'><p style='vertical-align: top;font-size: 13px;'>" + itemdetail.Qty + "</p></td><td style='padding: 5px 10px; vertical-align: top;'><p style='vertical-align: top;font-size: 14px;'> Rs. " + Convert.ToDecimal(itemdetail.Qty) * Convert.ToDecimal(itemdetail.Price) + "</p></td></tr>";
                    //if (itemdetail.ModelNo == "MOM15070105180")
                    //{
                    //    momvalue = "1";
                    //}
                    //if (itemdetail.ModelNo == "MOM15060105180")
                    //{
                    //    momvalue = "1";
                    //}
                }
            }

            sendEmail(ViewState["email"].ToString(), ViewState["name"].ToString(), ltrlOrdero.Text, cartpro, amount, shipamt, currency, discountamt);
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Shipping Detail has been updated')", true);
            txtExpDeliverDate.Text = txtRemark.Text = txtShippedDate.Text = txtTrackNo.Text = "";
        }
        // sendEmail(ViewState["email"].ToString(), ViewState["name"].ToString(), ltrlOrdero.Text);
    }

    void sendEmail(string email, string name, string orderno, string cart, decimal amt, string shippingamt, string cur, string discamt)
    {

        var billingdetail = (from b in db.OrderBillingInfoes
                             join s in db.OrderShippingDetails on b.OrderNo equals s.OrderNo
                             where b.OrderNo == orderno
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
            msg = msg + "<html><head><style></style><link href='https://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' type='text/css'></head><body><style type='text/css'>table>tr>td>p{margin:0;}</style>";
            msg = msg + "<table border='0' cellspacing='0' cellpadding='0' width='620' style='font-family: sans-serif;'><tr><td><div align='center'>";
            msg = msg + "<table border='0' cellspacing='0' cellpadding='0' width='620'>";
            msg = msg + "<tr><td></td></tr></table></div></td></tr><tr><td><div align='center'><table border='0' cellspacing='0' cellpadding='0' width='620'><tr><td></td></tr></table>";
            msg = msg + "</div></td></tr><tr><td valign='top' ><div align='center'><table border='0' cellspacing='0' cellpadding='0' width='620'><tr>";
            msg = msg + "<td style='text-align:center; ' bgcolor='#ff4e63'><p style='background-color: #000000; border-bottom:3px solid #ccc; margin-bottom: 2px;    margin-top: 0;  padding-top: 10px;   width: 620px;' align='center'><a href='http://www.fabfashionaccessories.com/' target='_blank'><img border='0' id='_x0000_i1025' src='http://www.fabfashionaccessories.com/images/fab-logo.png' alt='fabfashionaccessories' /></a></p></td>";
            msg = msg + "</tr><tr><td style='border-bottom: 2px dotted;'>";

            //msg = msg + "<p style='margin-bottom: 7px; margin-top: 0;  padding-top: 10px; width: 620px;' align='center'>";
            //msg = msg + "<a style='font-size: 14px; text-decoration: none;color: #003659;padding: 0 8px;' href='https://itunes.apple.com/us/app/fab-fashion-accessories/id1423106098?ls=1&mt=8' target='_blank'><img src='http://www.fabfashionaccessories.com/images/iosicon.gif' style='padding: 0 5px; width: 20px; vertical-align: middle; />Download iOS App</a>|";
            //msg = msg + "<a style='font-size: 14px; text-decoration: none;color: #003659;padding: 0 8px;' href='https://play.google.com/store/apps/details?id=com.fabfashions' target='_blank'><img src='http://www.fabfashionaccessories.com/images/Icons.gif' style='width: 20px;vertical-align: middle;' />Download Android App</a></p>";

            msg = msg + "</td></tr><tr>";

            msg = msg + "<td><table border='0' cellspacing='0' cellpadding='0' width='620' style='background:#f9f9f9;'><tr><td style='padding: 30px 12px;'>";
            msg = msg + "<table><tr><td>";
            msg = msg + "<p style='margin:0; padding:0px 0; font-weight:600;font-size:14px; padding:10px 0'>Dear " + name + ",</p>";
            msg = msg + "<p style='margin:0; padding:0px 0; font-size:14px; font-weight:600;'>Greetings from fabfashionaccessories.com</p><br />";

            msg = msg + "<p style='margin:0;padding:0px 0; font-weight:500;font-size:12px;'>fabfashionaccessories has shipped your order : <strong> " + orderno + "</strong> </p></td>";
            //if (momvalue.ToString() != "0")
            //{
            //    msg = msg += "  <td align='right' width='50%'>  <img src='http://www.jovifashion.com/images/momlogo.png' style='padding: 0 5px; vertical-align: middle;' /></td>";
            //}
            msg = msg += "</tr></table></td></tr>";
            msg = msg + "<tr><td style='background:#d2d2d2;padding: 15px 12px;display: block;margin-bottom: 10px;'>";
            msg = msg + "<p style='margin:0;padding:0px 0; font-weight:500;font-size:12px; text-align:center;'>You will receive your shipment by " + txtExpDeliverDate.Text.Trim() + " (Expected)</td></tr>";

            msg = msg + " <tr style='margin:10px 0;'><td style='background:#eee;padding: 15px 12px;'>";
            msg = msg + "<p style='margin:0;padding:0px 0; font-weight:500;font-size:12px; text-align:center;'>This shipment was sent through: <span style='font-weight:600'>" + drpShippedBy.SelectedItem.Text + "</span><br /><br />Shipment Tracking ID: <span style='font-weight:600'>" + txtTrackNo.Text.Trim() + "</span>";

            msg = msg + "<br />";
            msg = msg + "<br /></td></tr>";


            msg = msg + "<tr style='background-color:#f9f9f9'><td style='padding:10px 10px;'><p style='margin:0; padding: 20px 0 0 0; font-weight:600;font-size:12px;'>Please find below, the summary of your order : " + orderno + "</p></td></tr>";

            msg = msg + " <tr><td style='background: #f9f9f9; padding-bottom: 10px;'><table border='0' cellspacing='0' cellpadding='0'  style='background:#f9f9f9; width: 600px; margin: 0 auto;    border: 1px solid #ccc;'>";

            msg = msg + "<tr style='font-size: 12px;text-align: left;'><th width='40%' style='padding: 5px 10px; border-right: 1px solid #ccc;border-bottom: 1px solid #ccc;'>Product Name</th> <th width='15%' style='padding: 5px 10px; border-right: 1px solid #ccc;border-bottom: 1px solid #ccc;'>Model</th><th width='15%' style='padding: 5px 10px; border-right: 1px solid #ccc;border-bottom: 1px solid #ccc;'>Price</th><th width='15%' style='padding: 5px 10px; border-right: 1px solid #ccc;border-bottom: 1px solid #ccc;'>Quantity</th><th width='15%' style='padding: 5px 10px; border-bottom: 1px solid #ccc;'>Total</th></tr>";

            msg = msg + cart;

            msg = msg + "<tr><td colspan='2' style='border-top: 1px solid #ccc;text-align:left;'></td><td colspan='2' style='border-top: 1px solid #ccc;font-size: 13px;text-align:left; padding:5px;'>Sub Total (" + cur + "):</td><td style='border-top: 1px solid #ccc;font-size: 13px; padding:5px;' align='right'>" + amt + "</td></tr>";

            msg = msg + "<tr><td colspan='2' style='text-align:left;'></td><td colspan='2'  style='font-size: 13px;text-align:left;padding:5px;'>Shipping Amount (" + cur + "):</td><td style='font-size: 13px; padding:5px;' align='right'>" + shippingamt + "</td></tr>";


            decimal Grandtotal = 0;

            if (Convert.ToDecimal(discamt) > 0)
            {
                Grandtotal = Convert.ToDecimal(amt) + Convert.ToDecimal(shippingamt) - Convert.ToDecimal(discamt);

                msg = msg + "<tr><td colspan='2' style='text-align:left;'></td><td colspan='2' style='font-size: 13px;text-align:left; padding:5px;'>Discoont Amount (" + cur + "):</td><td style='font-size: 13px; padding:5px;' align='right'>" + discamt + "</td></tr>";

                msg = msg + "<tr><td colspan='2' style='text-align:left;'></td><td colspan='2' style='font-size: 13px;text-align:left; padding:5px;'>Grand Total (" + cur + "):</td><td style='font-size: 13px; padding:5px;' align='right'><strong>" + Grandtotal + "</strong></td></tr>";
            }
            else
            {
                Grandtotal = Convert.ToDecimal(amt) + Convert.ToDecimal(shippingamt);

                msg = msg + "<tr><td colspan='2' style='text-align:left;'></td><td colspan='2' style='font-size: 13px;text-align:left; padding:5px;'>Grand Total (" + cur + "):</td><td style='font-size: 13px; padding:5px;' align='right'><strong>" + Grandtotal + "</strong></td></tr>";

            }

            msg = msg + "</table></td></tr>";

            msg = msg + "<tr><td><table border='0' cellspacing='0' cellpadding='0' width='620' style='background:#f9f9f9;'><tr><td style='padding: 10px 12px;'>";

            msg = msg + "<p style='margin:0; padding:0px 0; font-weight:600;font-size:14px; padding:10px 0'>Delivery Address:</p>";

            msg = msg + "<p style='margin:0; padding:0px 0; font-weight:500;font-size:12px;'>" + billingdetail.sname + "<br />";
            msg = msg + billingdetail.saddress + "<br />";
            msg = msg + billingdetail.saddress2 + "<br />";
            msg = msg + billingdetail.scity + " <br />";
            msg = msg + billingdetail.sstate + "<br />";
            msg = msg + billingdetail.scountry + "<br />";
            msg = msg + billingdetail.szip + "<br />";
            msg = msg + "Contact No.: " + billingdetail.scontact + "<br />";
            msg = msg + "Email: " + billingdetail.semail + "<br />";

            msg = msg + "</p></td></tr>";


            msg = msg + "</table></td></tr>";
            msg = msg + "<tr><td valign='top'><div style='border-bottom: 1px solid #CCC;  margin-bottom: 15px;  padding-bottom: 15px;' align='center'>";
            msg = msg + "<table border='0' cellspacing='0' cellpadding='0' width='620'>";
            msg = msg + "<tr><td width='34%'><div align='center'><table border='0' cellspacing='0' cellpadding='0' width='97%'>";


            msg = msg + "<tr><td width='100%' colspan='2' valign='top'><p  style='color: #797979;  font-size: 14px; text-align:left; padding:0 15px;' align='center'><em>Email us at : <a style='color: #044eaf;  font-weight: 600;  font-size: 14px; text-decoration:none;' href='mailto:order@fabfashionaccessories.com'>order@fabfashionaccessories.com</a></em><em>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Call us at : </em><em style='color: #044eaf;  font-weight: 600;  font-size: 14px;'>+91-7506908279</em></p></td></tr>";

            msg = msg + "<tr><td colspan='2'>";

            msg = msg + "</td></tr>";
            msg = msg + "</table></div></td></tr></table></div></table>";
            msg = msg + "</div></td></tr></td></tr></table>";


            msg = msg + "</body></html>";

            // old mail formats................


            //msg = msg + "<table cellpadding='1' cellspacing='0' border='0' width='900px' style='font-family:Verdana;font-size:12px; border:solid 1px #ccc; padding:5px 15px 15px 15px; line-height:18px;'>";
            //msg = msg + "<tr style='padding-top: 10px;'><td align='left'><a href='http://www.jovifashion.com' style='text-decoration:none;'> <img src='http://www.jovifashion.com/images/orange-logo.png' /></a><div style=' float:right; font-weight:bold; font-size:18px;margin-top:10px'>Shipping Detail Of : " + orderno + "</div><br /></td></tr>";
            ////msg = msg + "<tr style='padding-top: 10px;'><td align='center' style='height:40px; vertical-align:middle;'><b>Completing Your Orders - It's Fast and Easy</b></td></tr>";
            //msg = msg + "<tr style='padding-top: 10px;'>";
            //msg = msg + "<td align='left' style='width:900px;padding-bottom:5px'>";
            //msg = msg + "Dear  " + name + ",<br /></td></tr>";
            //msg = msg + "<tr><td width='900px;' valign='top'>We have shipped your order. </td></tr>";
            //msg = msg + "<tr><td align='left'>Your Order reference no is  <b>" + orderno + "</b> and shipping details are as  below : </br></br></td></tr>";
            //msg = msg + "<tr><td align='left'> Order Date :  " + ltrlOrdDate.Text + "<br /> </td></tr>";
            //msg = msg + "<tr><td align='left'> Shipped by :  " + drpShippedBy.SelectedItem.Text + "<br /> </td></tr>";
            //msg = msg + "<tr><td align='left'> Track No. :  " + txtTrackNo.Text.Trim() + "<br /> </td></tr>";
            //msg = msg + "<tr><td align='left'> Shipped Date :  " + txtShippedDate.Text.Trim() + "<br /> </td></tr>";
            //msg = msg + "<tr><td align='left'> Exp. Deliver Date :  " + txtExpDeliverDate.Text.Trim() + "<br /><br /> </td></tr>";
            //msg = msg + "<tr><td align='left'>" + txtRemark.Text.Trim() + "<br /> </td></tr>";

            //msg = msg + "<tr><td align='left'><br/> </td></tr>";
            //// msg = msg + "<tr><td align='left'><table width='100%' style='font-family:Verdana;font-size:12px;'><tr><td><b>Billing Detail</b><br/>" + txtNameBilling.Text + " <br/>Email : " + txtEmailBilling.Text + "<br/> Contact No : " + txtTelephoneBilling.Text + "<br /> Address : " + txtAddress1Billing.Text + " " + txtAddress2Billing.Text + "<br/>" + txtCityBilling.Text + "," + drpStateBilling.SelectedItem.Text + "," + drpCountryBilling.SelectedItem.Text + "," + txtPostalCodeBilling.Text + "</td><td><b>Shipping  Detail</b><br/>" + txtNameShipping.Text + " <br/>Email : " + txtEmailShipping.Text + "<br/> Contact No : " + txtTelephoneShipping.Text + "<br /> Address : " + txtAddress1Shipping.Text + " " + txtAddress2Shipping.Text + "<br/>" + txtCityShipping.Text + "," + drpStateShipping.SelectedItem.Text + "," + drpCountryShipping.SelectedItem.Text + "," + txtPostCodeShipping.Text + "</td></tr></table>  </td></tr>";
            //msg = msg + "<tr><td align='left'> <br /><br /></td></tr>";
            //msg = msg + "<tr><td align='left'>For any further assistance, feel free to contact us.<br /></td></tr>";
            //msg = msg + "<tr><td align='left'>Regards,</td></tr>";
            //msg = msg + "<tr><td align='left'>JOVI fashion</td></tr>";
            ////msg = msg + "<tr><td align='left'>online@jewelsmanufacture.com</td></tr>";
            //msg = msg + "<tr><td align='left'>www.jovifashion.com<br /><br /></td></tr>";
            //msg = msg + "<tr><td align='center' style='padding:10px;border:solid 1px #cccccc;'>if you have any question / query, please email us at <a target='_blank' href='mailto:feedback@jovifashion.com'>feedback@jovifashion.com</a></td></tr>";
            //msg = msg + "</table>";
            //msg = msg + "</td></tr></table>";

            string MailFrom = ConfigurationManager.AppSettings["MailFrom"].ToString();
            string Mailtoinfo = ConfigurationManager.AppSettings["MailTo"].ToString();
            //string Mailtoweb = ConfigurationManager.AppSettings["MailToWeb"].ToString();
            MailAddress address = new MailAddress(MailFrom, "fabfashionaccessories.com");
            MailAddress mailinfo = new MailAddress(Mailtoinfo);
            //MailAddress mailweb = new MailAddress(Mailtoweb);
            MailMessage message = new MailMessage();
            message.From = address;
            // message.To.Add(new MailAddress("web@jewelsinfosystems.com"));
            message.To.Add(email);
            message.Bcc.Add(mailinfo);
            //message.Bcc.Add(mailweb);
            //message.CC.Add(address);
            message.Subject = "Shipment of Order " + ltrlOrdero.Text + " by www.fabfashionaccessories.com";
            message.Body = msg;
            message.IsBodyHtml = true;
            SmtpClient sm = new SmtpClient();
            try
            {
                sm.Send(message);
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "closeAll();alert('Your password has been sent given email !')", true);
                return;
            }
            catch
            {
            }
        }
    }

}