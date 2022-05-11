using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Web.Services;
public partial class OrderList : System.Web.UI.Page
{
    FabAccessoriesEntities db = new FabAccessoriesEntities();
    string OrderId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindGrid();
        }
    }

    private void bindGrid()
    {
        System.Globalization.DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
        dtFormat.ShortDatePattern = "dd/MM/yyyy";

        var cat = db.OrderTbls.ToList();
        if (txtSearch.Text.Trim().Length > 0)
        {
            cat = cat.Where(r => r.OrderNo.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
        }
        if (txtOrderDate.Text.Trim().Length > 0)
        {
            DateTime dtTo = Convert.ToDateTime(txtOrderDate.Text, dtFormat);
            cat = cat.Where(r => DateTime.Compare(r.OrderDate.Value.Date, dtTo.Date) == 0).ToList();
        }

        var sub = (from c in cat
                   join us in db.UserInfoes on c.UserId equals us.Id
                   join ordtrack in db.OrderTrackings on c.OrderNo equals ordtrack.OrderNo
                   join ordresp in db.OrderPaymentResponses on c.OrderNo equals ordresp.OrderId
                   select new
                   {
                       c.OrderNo,
                       c.OrderDate,
                       c.OrderTotal,
                       c.Id,
                       c.UserId,
                       c.ShippingAmt,
                       c.CouponAmt,
                       c.Comment,
                       c.OrderBy,
                       Name = us.FirstName + ' ' + us.LastName,
                       us.ContactNo,
                       us.Email,
                       us.Username,
                       c.status,
                       ordtrack.OrderStatus,
                       ordresp.PaymentMode
                       //hidestatus = (from ordhd in db.Orderhides where ordhd.OrderNo == ordtrack.OrderNo select ordhd.Status).Any() ? 0 : 1,
                   }).ToList();

        int pagesize = Convert.ToInt16(drpPagging.SelectedValue);
        grdList.PageSize = pagesize;
        if (txtUserName.Text.Trim().Length > 0)
        {
            sub = sub.Where(r => r.Username.ToLower().Contains(txtUserName.Text.ToLower())).ToList();
        }
        if (txtEmail.Text.Trim().Length > 0)
        {
            sub = sub.Where(r => r.Email.ToLower().Contains(txtEmail.Text.ToLower())).ToList();
        }
        if (GridViewSortDirection == SortDirection.Ascending)
        {
            grdList.DataSource = (sub.OrderBy(x => x.GetType().GetProperty(GridViewSortExpression).GetValue(x, null))).ToList().OrderByDescending(r => r.Id).ToList();
        }
        else
        {
            grdList.DataSource = (sub.OrderByDescending(x => x.GetType().GetProperty(GridViewSortExpression).GetValue(x, null))).ToList().OrderByDescending(r => r.Id).ToList();
        };
        grdList.DataBind();

    }

    protected void grdList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdList.PageIndex = e.NewPageIndex;
        bindGrid();
    }

    protected void grdList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToLower() == "edt")
        {
            Int32 id = Convert.ToInt32(e.CommandArgument.ToString());
            getDetail(id);
        }
    }

    private void getDetail(Int32 id)
    {

    }

    protected void lnkcancelorder_Click(object sender, EventArgs e)
    {
        LinkButton lnkcancel = (LinkButton)sender;

        GridViewRow gr = (GridViewRow)lnkcancel.NamingContainer;

        HiddenField ltrorder = (HiddenField)gr.FindControl("hddOrderNo");


        var cancelorder = db.OrderTrackings.Where(r => r.OrderNo == ltrorder.Value).FirstOrDefault();
        cancelorder.OrderStatus = "cancelled";
        //db.Orderhides.Add(dd);
        db.SaveChanges();
        getOrderDetail(ltrorder.Value);
        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Order Has been Cancelled')", true);
        bindGrid();

    }


    void getOrderDetail(string orid)
    {
        OrderId = orid;


        var orderM = from ordDetail in db.OrderDetails
                     join ord in db.OrderTbls on ordDetail.OrderNo equals ord.OrderNo

                     where (ordDetail.OrderNo == OrderId)
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
                         ProductTot = ordDetail.Total

                     };
        var orderDtl = orderM.ToList();
        var orderPay = (from p in db.OrderPaymentResponses
                        where p.OrderId == orid
                        select p).FirstOrDefault();
        OrderPaymentResponse response = new OrderPaymentResponse();
        var dds = (from p in db.OrderPaymentResponses
                   join q in db.OrderTbls on p.OrderId equals q.OrderNo
                   where p.OrderId == orid
                   orderby p.OrderId
                   select new
                   {
                       q.OrderTotal,
                       p.PaymentId,
                       p.PaymentMode,
                       p.Status,
                       p.Mode
                   });


        string shipamt = orderDtl.FirstOrDefault().ShippingAmt.ToString();
        string currency = orderDtl.FirstOrDefault().OrderCurrency.ToString();

        if (orderDtl != null)
        {

            foreach (var item in orderDtl)
            {
                var aa = db.ProductMasters.Where(r => r.SkuName == item.ModelNo).FirstOrDefault();

                if (aa != null)
                {
                    aa.StockPcs = (aa.StockPcs - item.Qty);
                    db.SaveChanges();
                }
            }

            decimal amount = 0;
            string cartpro = "";
            var orderdetail = db.OrderDetails.Where(r => r.OrderNo == OrderId).ToList();
            if (orderdetail != null)
            {

                string url = "http://www.fabfashionaccessories.com/upload/products/small";
                foreach (var itemdetail in orderdetail)
                {
                    amount += Convert.ToDecimal(itemdetail.Qty) * Convert.ToDecimal(itemdetail.Price);

                    cartpro += "<tr><td style='padding: 5px 10px;border-right: 1px solid #ccc; vertical-align: top;'><img style='float:left;' width='54px' height='54px'  src='" + url + itemdetail.Img.ToString() + "'/><p style='vertical-align: top;font-size: 13px;width: 65%;float: right;margin: 5px 0;'>" + itemdetail.ShortDesc + "</p> </td><td style='padding: 5px 10px;border-right: 1px solid #ccc; vertical-align: top;'><p style='vertical-align: top;font-size: 13px;'>" + itemdetail.ModelNo + "</p></td><td style='padding: 5px 10px;border-right: 1px solid #ccc; vertical-align: top;'><p style='vertical-align: top;font-size: 13px;'> Rs. " + itemdetail.Price + "</p></td><td style='padding: 5px 10px;border-right: 1px solid #ccc; vertical-align: top;'><p style='vertical-align: top;font-size: 13px;'>" + itemdetail.Qty + "</p></td><td style='padding: 5px 10px; vertical-align: top;'><p style='vertical-align: top;font-size: 14px;'> Rs. " + Convert.ToDecimal(itemdetail.Qty) * Convert.ToDecimal(itemdetail.Price) + "</p></td></tr>";

                }
            }
            sendEmail(cartpro, OrderId, amount, shipamt, currency, "0.00");
        }
    }



    void sendEmail(string cart, string orderno, decimal amt, string shippingamt, string cur, string discamt)
    {

        //        var billingdetail = (from b in db.OrderBillingInfoes
        //                             join s in db.OrderShippingDetails on b.OrderNo equals s.OrderNo
        //                             where b.OrderNo == OrderId
        //                             select new
        //                             {
        //                                 b.OrderNo,
        //                                 b.Name,
        //                                 b.State,
        //                                 b.Zip,
        //                                 b.Id,
        //                                 b.Email,
        //                                 b.Country,
        //                                 b.ContactNo,
        //                                 b.City,
        //                                 b.Address2,
        //                                 b.Address,
        //                                 sname = s.Name,
        //                                 sstate = s.State,
        //                                 scountry = s.Country,
        //                                 szip = s.Zip,
        //                                 semail = s.Email,
        //                                 saddress = s.Address,
        //                                 saddress2 = s.Address2,
        //                                 scontact = s.ContactNo,
        //                                 scity = s.City
        //                             }).FirstOrDefault();
        //        if (billingdetail != null)
        //        {
        //            string msg = "";


        //            msg = msg + "<table border='0' cellspacing='0' cellpadding='0' width='620' style='font-family: 'Open Sans', sans-serif;'><tr><td><div align='center'>";
        //            msg = msg + "<table border='0' cellspacing='0' cellpadding='0' width='620'>";
        //            msg = msg + "<tr><td></td></tr></table></div></td></tr><tr><td><div align='center'><table border='0' cellspacing='0' cellpadding='0' width='620'><tr><td></td></tr></table>";
        //            msg = msg + "</div></td></tr><tr><td valign='top'><div align='center'><table border='0' cellspacing='0' cellpadding='0' width='620'><tr>";
        //            msg = msg + "<td bgcolor='#ff4e63' style='border-bottom:3px solid #ccc; margin-bottom: 2px; margin-top: 0; text-align:center; padding-top: 10px; width: 620px;'><a href='http://www.jovifashion.com/' target='_blank'><img border='0' id='_x0000_i1025' src='http://www.jovifashion.com/images/logo.png' alt='Jovifashion' /></a></td>";
        //            msg = msg + "</tr><tr><td style='border-bottom: 2px dotted;'><p style='margin-bottom: 7px; margin-top: 0;  padding-top: 10px; width: 620px;' align='center'>";
        //            msg = msg + "<a style='font-size: 14px; text-decoration: none;color: #003659;padding: 0 8px;' href='http://www.jovifashion.com/tracking.html' target='_blank'><img src='http://www.jovifashion.com/images/track.png' style='padding: 0 5px; width: 20px; vertical-align: middle;' /></a> |";

        //            msg = msg + "<a style='font-size: 14px; text-decoration: none;color: #003659;padding: 0 8px;' href='http://www.jovifashion.com/shipping-handling.html' target='_blank'>FREE Shipping Sitewide * </a> |";
        //            msg = msg + "<a style='font-size: 14px; text-decoration: none;color: #003659;padding: 0 8px;' href='http://www.jovifashion.com/returns-refunds.html' target='_blank'>30 Days Return Policy</a> ";


        //            msg = msg + "</p></td></tr><tr>";

        //            msg = msg + "<td><table border='0' cellspacing='0' cellpadding='0' width='620' style='background:#f9f9f9;'><tr><td style='padding: 30px 12px;'>";

        //            msg = msg + "<p style='margin:0; padding:0px 0; font-weight:600;font-size:14px; padding:10px 0'>Hi " + billingdetail.Name + ",</p>";
        //            msg = msg + "<p style='margin:0; padding:0px 0; font-size:14px;'>Your Order Has been Canceled !</p><br />";

        //            msg = msg + "</td></tr></table></td></tr>";

        //            msg = msg + "<tr style='background-color:#f9f9f9'><td style='padding:10px 10px;'><p style='margin:0; padding: 20px 0 0 0; font-weight:600;font-size:12px;'>Please find below, the summary of your order " + orderno + "</p></td></tr>";
        //            msg = msg + " <tr><td style='background: #f9f9f9; padding-bottom: 10px;'><table border='0' cellspacing='0' cellpadding='0'  style='background:#f9f9f9; width: 600px; margin: 0 auto;    border: 1px solid #ccc;'>";

        //            msg = msg + "<tr style='font-size: 12px;text-align: left;'><th width='40%' style='padding: 5px 10px; border-right: 1px solid #ccc;border-bottom: 1px solid #ccc;'>Product Name</th> <th width='15%' style='padding: 5px 10px; border-right: 1px solid #ccc;border-bottom: 1px solid #ccc;'>Model</th><th width='15%' style='padding: 5px 10px; border-right: 1px solid #ccc;border-bottom: 1px solid #ccc;'>Price</th><th width='15%' style='padding: 5px 10px; border-right: 1px solid #ccc;border-bottom: 1px solid #ccc;'>Quantity</th><th width='15%' style='padding: 5px 10px; border-bottom: 1px solid #ccc;'>Total</th></tr>";


        //            msg = msg + cart;

        //            decimal Total = Convert.ToDecimal(amt) + Convert.ToDecimal(shippingamt) - Convert.ToDecimal(discamt);


        //            //            if (Total < 500)
        //            //            {
        //            //                msg = msg + "<tr><td colspan='2' style='border-top: 1px solid #ccc;text-align:left;'></td><td colspan='2' style='border-top: 1px solid #ccc;font-size: 13px;text-align:left;padding:5px;'>Sub Total (" + cur + "):</td><td style='border-top: 1px solid #ccc;font-size: 13px;padding:5px;' align='center'>" + amt + "</td></tr>";
        //            //            }
        //            //            else if (Total >= 500 && Total < 1000)
        //            //            {

        //            //                msg = msg + "<td colspan='2' rowspan='3' style='border-top: 1px solid #ccc; text-align: left;'><p>You will receive this free gift with your order</p><img src='http://www.jovifashion.com/upload/product/medium-asc1506010133ls-hot-retro-fashion-style-.jpg'></td> " +
        //            //"<td colspan='5' style='vertical-align: top;border-top: 1px solid #ccc;font-size: 13px;text-align:left;padding:5px;'><div style='margin-top: 1%;'> <table style=' float: right;width: 100%; text-align: right; font-size: 13px;'><tbody><tr><td>Sub Total(" + cur + ") :</td><td>" + amt + "</td></tr><tr> " +
        //            //"<td>COD Charges(" + cur + "):</td><td>" + shippingamt + "</td> </tr>";
        //            //            }
        //            //            else if (Total >= 1000)
        //            //            {
        //            //                msg = msg + "<td colspan='2' rowspan='3' style='border-top: 1px solid #ccc; text-align: left;'><p>You will receive this free gift with your order</p><img src='http://www.jovifashion.com/upload/product/medium-jea1506010014fj-feather-earrings-indian-.jpg'></td> " +
        //            //"<td colspan='5' style='vertical-align: top;'><div style='margin-top: 1%;'> <table style=' float: right;width: 100%; text-align: right;font-size: 13px;'><tbody><tr><td>Sub Total(" + cur + ") :</td><td>" + amt + "</td></tr><tr> " +
        //            //"<td>COD Charges(" + cur + "):</td><td>" + shippingamt + "</td> </tr>";
        //            //            }


        //            msg = msg +
        //"<td colspan='5' style='vertical-align: top;border-top: 1px solid #ccc;font-size: 13px;text-align:left;padding:5px;'><div style='margin-top: 1%;'> <table style=' float: right;width: 100%; text-align: right; font-size: 13px;'><tbody><tr><td>Sub Total(" + cur + ") :</td><td>" + amt + "</td></tr><tr> " +
        //"<td>COD Charges(" + cur + "):</td><td>" + shippingamt + "</td> </tr>";

        //            decimal Grandtotal = 0;

        //            if (Convert.ToDecimal(discamt) > 0)
        //            {
        //                Grandtotal = Convert.ToDecimal(amt) + Convert.ToDecimal(shippingamt) - Convert.ToDecimal(discamt);


        //                msg = msg + " <tr><td>Grand Total(" + cur + "):</td><td>" + discamt + "</td></tr>";
        //                msg = msg + " <tr><td>Grand Total(" + cur + "):</td><td>" + Grandtotal + "</td></tr></tbody></table></div> </td></tr></tbody>";
        //            }
        //            else
        //            {
        //                Grandtotal = Convert.ToDecimal(amt) + Convert.ToDecimal(shippingamt);
        //                msg = msg + " <tr><td>Grand Total(" + cur + "):</td><td>" + Grandtotal + "</td></tr></tbody></table></div> </td></tr></tbody>";

        //            }
        //            msg = msg + "</table></td></tr>";

        //            msg = msg + "<tr><td valign='top'><div style='border-bottom: 1px solid #CCC;  margin-bottom: 15px;  padding-bottom: 15px;' align='center'>";
        //            msg = msg + "<table border='0' cellspacing='0' cellpadding='0' width='620'>";
        //            msg = msg + "<tr><td width='34%'><div align='center'><table border='0' cellspacing='0' cellpadding='0' width='97%'><tr>";
        //            msg = msg + "<td><div style='border-bottom: 1px dashed #ccc;  padding-bottom: 10px;  margin-bottom: 10px;' align='center'>";
        //            msg = msg + "<table border='0' cellspacing='0' cellpadding='0' width='60%'><tr>";
        //            msg = msg + "<td width='40%'><p style='color: #044eaf;  font-weight: 600;  font-size: 14px;' align='center'><em>Connect with us:</em></p></td>";
        //            msg = msg + "<td width='15%' valign='bottom'><p align='center'><em><a href='https://www.facebook.com/jovifashiononline' target='_blank'><img src='http://jovifashion.com/newsletters/jovifashion1/images/facebook.jpg' alt='' id='_x0000_i1042' border='0' /></a></em></p></td>";
        //            msg = msg + "<td width='15%' valign='bottom'><p align='center'><em><a href='https://www.instagram.com/jovifashion' target='_blank'><img src='http://jovifashion.com/newsletters/jovifashion1/images/Instagram.jpg' alt='' id='_x0000_i1043' border='0' /></a></em></p></td>";
        //            msg = msg + "<td width='15%' valign='bottom'><p align='center'><em><a href='https://twitter.com/jovi_fashion' target='_blank'><img src='http://jovifashion.com/newsletters/jovifashion1/images/twitter.jpg' alt='' id='_x0000_i1044' border='0' /></a></em></p></td>";
        //            msg = msg + "<td width='15%' valign='bottom'><p align='center'><em><a href='https://plus.google.com/u/0/109967275054185899031/about' target='_blank'><img src='http://jovifashion.com/newsletters/jovifashion1/images/google.jpg' alt='' id='_x0000_i1045' border='0' /></a></em></p></td></tr></table></div></td></tr>";

        //            msg = msg + "<tr><td width='100%' colspan='2' valign='top'><p  style='color: #797979;  font-size: 14px;' align='center'><em>Email us at : <a style='color: #044eaf;  font-weight: 600;  font-size: 14px; text-decoration:none;' href='mailto:feedback@jovifashion.com'>feedback@jovifashion.com</a></em><em>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Call us at : </em><em style='color: #044eaf;  font-weight: 600;  font-size: 14px;'>+91-9636288883</em></p></td></tr>";

        //            msg = msg + "<tr><td colspan='2'><div align='center'><table border='0' cellspacing='0' cellpadding='0' width='95%'>";
        //            msg = msg + "<tr><td nowrap='nowrap'><p style='color: #797979;  font-size: 14px;'>Please add <a style='color: #555555;  font-weight: 500;  font-size: 14px;' href='mailto:feedback@jovifashion.com' target='_blank'>feedback@jovifashion.com</a> to <br />";

        //            msg = msg + "your address book to ensure delivery into your inbox.</p></td></tr></table></div></td></tr><tr><td><div align='center'>";
        //            msg = msg + "<table border='0' cellspacing='0' cellpadding='0' width='95%'><tr></tr></table></div></td><td></td></tr></table></div></td></tr></table></div></td></tr></table>";
        //            msg = msg + "</div></td></tr><tr><td><div align='center'><table border='0' cellspacing='0' cellpadding='0' width='620'><tr></tr></table></div></td></tr><tr><td><div align='center'></div></td></tr></table>";



        //            msg = msg + "<map name='Map' id='Map'>";
        //            msg = msg + "<area shape='rect' coords='234,219,347,261' href='http://www.jovifashion.com' target='_blank' />";
        //            msg = msg + "<area shape='rect' coords='240,410,346,452' href='https://play.google.com/store/apps/details?id=com.jovi.jovifashionapp&amp;hl=en' target='_blank' />";
        //            msg = msg + "<area shape='rect' coords='246,365,353,406' href='http://www.jovifashion.com' /></map>";


        //            string MailFrom = ConfigurationManager.AppSettings["MailFrom"].ToString();
        //            string Mailtoinfo = ConfigurationManager.AppSettings["MailTo"].ToString();
        //            //string Mailtoweb = ConfigurationManager.AppSettings["MailToWeb"].ToString();
        //            MailAddress address = new MailAddress(MailFrom, "JOVI Fashion Orders");
        //            MailAddress mailinfo = new MailAddress(Mailtoinfo);
        //            //MailAddress mailweb = new MailAddress(Mailtoweb);
        //            MailMessage message = new MailMessage();
        //            MailMessage msgcustomer = new MailMessage();

        //            message.From = address;

        //            message.To.Add(mailinfo);

        //            msgcustomer.From = address;

        //            msgcustomer.To.Add(billingdetail.Email);

        //            msgcustomer.Subject = "Your  Order at www.jovifashion.com is Canceled";
        //            msgcustomer.Body = msg;
        //            message.Body = msg;
        //            message.IsBodyHtml = true;
        //            msgcustomer.IsBodyHtml = true;
        //            SmtpClient sm = new SmtpClient();
        //            try
        //            {
        //                // sm.Send(msgcustomer);

        //            }
        //            catch
        //            {
        //            }
        //        }

    }




    protected void btnSeach_Click(object sender, EventArgs e)
    {
        bindGrid();
    }

    protected void drpPagging_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindGrid();
    }

    protected void grdList_Sorting(object sender, GridViewSortEventArgs e)
    {
        GridViewSortExpression = e.SortExpression;
        if (GridViewSortDirection == SortDirection.Ascending)
        {
            GridViewSortDirection = SortDirection.Descending;
        }
        else
        {
            GridViewSortDirection = SortDirection.Ascending;
        };
        bindGrid();

    }

    public SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState["sortDirection"] == null)
                ViewState["sortDirection"] = SortDirection.Ascending;

            return (SortDirection)ViewState["sortDirection"];
        }
        set { ViewState["sortDirection"] = value; }
    }

    public string GridViewSortExpression
    {
        get
        {
            return ViewState["GridViewSortExpression"] == null ? "Id" : ViewState["GridViewSortExpression"] as string;
        }
        set
        {
            ViewState["GridViewSortExpression"] = value;
        }
    }

    protected void grdList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //Repeater repPayment = (Repeater)e.Row.FindControl("repPayment");
        //HiddenField hddOrderNo = (HiddenField)e.Row.FindControl("hddOrderNo");

        //HiddenField hfhide = (HiddenField)e.Row.FindControl("hfhidestatus");
        //LinkButton lnkhide = (LinkButton)e.Row.FindControl("lnkhide");
        //LinkButton lnkretrive = (LinkButton)e.Row.FindControl("lnkretrive");

        //if (hddOrderNo != null)
        //{
        //    var pay = db.OrderPaymentResponses.Where(r => r.OrderId == hddOrderNo.Value).ToList();
        //    repPayment.DataSource = pay;
        //    repPayment.DataBind();
        //}

        //if (hfhide != null)
        //{

        //    if (hfhide.Value == "0")
        //    {
        //        lnkhide.Visible = false;
        //        lnkretrive.Visible = true;
        //    }
        //    else
        //    {
        //        lnkhide.Visible = true;
        //        lnkretrive.Visible = false;
        //    }
        //}

    }

    [WebMethod]
    public static string Updatestatus(string Orderid, string Status)
    {
        FabAccessoriesEntities db = new FabAccessoriesEntities();
        try
        {
            var getorder = db.OrderTbls.Where(r => r.OrderNo == Orderid).FirstOrDefault();

            if (getorder != null)
            {
                getorder.status = Status;
            }
            db.SaveChanges();
            return "Sucess";
        }
        catch (Exception e)
        {
            return "Failure";
        }
    }
    protected void lnkdeleteorder_Click(object sender, EventArgs e)
    {
        LinkButton lnkdelete = (LinkButton)sender;

        GridViewRow gr = (GridViewRow)lnkdelete.NamingContainer;

        HiddenField hddOrderNo = (HiddenField)gr.FindControl("hddOrderNo");

        int delorder = DataAccess.ExecuteQuery("delete from dbo.OrderTbl where orderno='" + hddOrderNo.Value + "'; delete from dbo.OrderDetail where OrderNo ='" + hddOrderNo.Value + "'; delete from dbo.OrderTracking where OrderNo ='" + hddOrderNo.Value + "'; delete from dbo.OrderShippingDetail where OrderNo ='" + hddOrderNo.Value + "'; delete from dbo.OrderPaymentResponse where OrderId ='" + hddOrderNo.Value + "'; delete from dbo.OrderBillingInfo where OrderNo ='" + hddOrderNo.Value + "';", CommandType.Text);

        if (delorder > 0)
        {
            bindGrid();
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Order has been deleted.');", true);
        }
    }
}