using paytm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ipytmresponsecancel : System.Web.UI.Page
{

    FabAccessoriesEntities db = new FabAccessoriesEntities();
    string OrderId = ""; decimal giftvalue = 0.00m, momvalue = 0.00m;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            //Session["ordId"] = "JF0008";
            if (Session["ordId"] != null)
            {
                OrderId = Session["ordId"].ToString();
                paymentdetail();
                getOrderDetail(OrderId);

            }

            Session["ordId"] = null;
        }
        else
        { }
    }

    public void paymentdetail()
    {
        String merchantKey = "z!W6HJ9UxKF5t@jw"; // Replace the with the Merchant Key provided by Paytm at the time of registration.

        Dictionary<string, string> parameters = new Dictionary<string, string>();
        string paytmChecksum = "";
        foreach (string key in Request.Form.Keys)
        {
            parameters.Add(key.Trim(), Request.Form[key].Trim());
        }

        if (parameters.ContainsKey("CHECKSUMHASH"))
        {
            paytmChecksum = parameters["CHECKSUMHASH"];
            parameters.Remove("CHECKSUMHASH");

        }



        if (parameters.Count != 0)
        {
            if (CheckSum.verifyCheckSum(merchantKey, parameters, paytmChecksum))
            {
                if (parameters["STATUS"] != "TXN_FAILURE")
                {
                    string txnid = parameters["TXNID"] as string;
                    UpdateOrderPaymentMode("PAYTM", 1, txnid);
                    // ClientScript.RegisterStartupScript(GetType(), "Message", "alert('PAID!!')", true);
                    //Response.Write("Paid");
                }
                else
                {
                    string txnid = parameters["BANKTXNID"] as string;
                    ltrlResponce.Text = parameters["RESPMSG"] as string;
                    UpdateOrderPaymentMode("PAYTM", 0, txnid);
                    // ClientScript.RegisterStartupScript(GetType(), "Message", "alert('UNPAID!!')", true);
                }
            }
            else
            {
                string txnid = parameters["BANKTXNID"] as string;
                ltrlResponce.Text = parameters["RESPMSG"] as string;
                UpdateOrderPaymentMode("PAYTM", 0, txnid);
                // ClientScript.RegisterStartupScript(GetType(), "Message", "alert('UNPAID!!')", true);
                //Response.Write("Unpaid");
            }
        }
    }

    public void UpdateOrderPaymentMode(string paymentMode, int y, string TxnId)
    {
        FabAccessoriesEntities db = new FabAccessoriesEntities();
        // OrderPaymentResponse pay = new OrderPaymentResponse();

        //var pay = db.OrderPaymentResponses.Where(r => r.OrderId == OrderId).FirstOrDefault();

        var pay = db.OrderPaymentResponses.Where(r => r.OrderId == OrderId).FirstOrDefault();

        ltrorderno.Text = OrderId;

        if (pay != null)
        {
            //pay.OrderId = Session["ordId"].ToString();
            //pay.PaymentMode = "PAYTM";
            //pay.CreateDate = System.DateTime.Now;
            //pay.PaymentId = TxnId;
            //pay.PayuMoneyId = TxnId;
            //if (y == 1)
            //{
            //    pay.Status = "success";
            //    pay.UnmappedStatus = "Transaction Successful";
            //}
            //else
            //{
            //    pay.Status = "Unpaid";
            //    pay.UnmappedStatus = "Transaction Failure";
            //}


            pay.OrderId = OrderId;
            pay.PaymentMode = pay.Mode = "PAYTM";

            pay.CreateDate = System.DateTime.Now;
            pay.PaymentId = TxnId;

            if (y == 1)
            {
                pay.Status = "success";

                // AddGoogleEcommerceTraking(OrderId);
            }
            else
            {
                pay.Status = "Unpaid";
            }


            var ord = db.OrderTbls.Where(r => r.OrderNo == OrderId).FirstOrDefault();

            if (ord != null && y == 1)
            {
                ord.status = "1";
            }



            db.SaveChanges();

        }
        else
        {

        }

    }

    void getOrderDetail(string orid)
    {
        // string orderNo = Request.Form["txnid"];
        FabAccessoriesEntities db = new FabAccessoriesEntities();
        string OrderId = orid;
        //if (Session["orderId"] != null)
        //{
        var orderM = from ordDetail in db.OrderDetails
                     join ord in db.OrderTbls on ordDetail.OrderNo equals ord.OrderNo
                     //join product in db.ProductMasters on ordDetail.ProductId equals product.ProductId
                     //join res in db.OrderPaymentResponses on ordDetail.OrderNo equals res.OrderId               
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
                         //orderdisc = (from disorder in db.DiscountDetails where disorder.OrderId == ord.OrderNo select disorder.CoupanAmount).FirstOrDefault(),
                         //orderTotal = (from ordTot in db.OrderDetails where ordTot.OrderNo == ord.OrderNo select ordTot.Total).Sum()
                     };

        var orderDtl = orderM.ToList();

        grvorddetail.DataSource = orderDtl;
        grvorddetail.DataBind();

        lbltotalamount.Text = "INR" + " " + orderDtl.FirstOrDefault().OrderTotal.ToString();
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
                       PaymentId = p.PaymentId,
                       PaymentMode = p.PaymentMode,
                       PayuMoneyId = p.PaymentId,
                       Status = p.Status,
                       Mode = p.Mode,
                       UnmappedStatus = ""
                   });

        string discamt = orderDtl.FirstOrDefault().CouponAmt.ToString();
        string shipamt = orderDtl.FirstOrDefault().ShippingAmt.ToString();
        string currency = orderDtl.FirstOrDefault().OrderCurrency.ToString();

        lbldiscountamt.Text = currency + " " + discamt;
        lblShipping.Text = currency + " " + shipamt;
        ltrlAmt.Text = lblgrand.Text = currency + " " + (orderDtl.FirstOrDefault().OrderTotal - orderDtl.FirstOrDefault().CouponAmt + orderDtl.FirstOrDefault().ShippingAmt).ToString();

        //dds.PaymentId=ltrlPaymentId.Text;
        //dds.Mode="";
        //dds.Status="Success";
        //dds.UnmappedStatus="";
        //dds.PayuMoneyId="";
        //dds.CreateDate=DateTime.Now;
        //dds.PaymentMode="HDFC";           
        db.SaveChanges();

        //// select top(1) loginid,goldwt,redemewt,goldrate,isnull(goldamt,'0.00') as goldamt, orderid, [status] from ordergolddiscount where orderid=@OrderId order by redemedate desc
        //// DataSet dds = DB.Business.SPs.SpPaymentResponce(0, orid, "", "", "", "", 0.00m, "", "", "", "", 0.0000m, DateTime.Now, "getdiscountamount").GetDataSet();

        //decimal discgoldamt = Convert.ToDecimal("0.00");

        //if (dds != null && dds.Tables[0].Rows.Count > 0)
        //{
        //    discgoldamt = Convert.ToDecimal(dds.Tables[0].Rows[0]["goldamt"].ToString());
        //}

        if (orderPay != null)
        {
            //ltrlAmt.Text =  (Convert.ToDecimal(orderPay.Amount)- discgoldamt).ToString();
            // ltrlAmt.Text = (Convert.ToDecimal(orderPay.)).ToString();
            ltrlTransactionId.Text = orderPay.PaymentId.ToString();
            ltrlResponce.Text += " - " + orderPay.Status.ToString();
            //  ltrlTransactionId.Text = orderPay.PayuMoneyId.ToString()!= null ? orderPay.PayuMoneyId.ToString()  : "";
        }

        //DataSet ds = DB.Business.SPs.SpPaymentResponce(0, orid, "", "", "", "", 0.00m, "", "", "", "", 0.0000m, DateTime.Now, "SelectByOrder").GetDataSet();
        //if (ds.Tables[0].Rows.Count > 0)
        //{

        //    int uppdt = DB.Business.SPs.SpPaymentResponce(0, orid, "", "", "", "", 0.00m, "", "", "", "", 0.0000m, DateTime.Now, "UpdateGolddiscstatus").Execute();

        //    if (ds.Tables[2].Rows.Count > 0)
        //    {

        //        ltrlAmt.Text = ds.Tables[2].Rows[0]["Currency_Symbol"].ToString() + " " + ltrlAmt.Text;
        //    }
        //    sendMail(ds.Tables[1], ds.Tables[2], ds.Tables[3], ds.Tables[4], 0);
        //}
        //}

        var orderstatus = db.OrderPaymentResponses.Where(r => r.OrderId == OrderId && r.Status.ToLower() == "success").FirstOrDefault();
        if (orderstatus != null)
        {
            //foreach (var item in orderDtl)
            //{



            //    var aa = db.ProductMasters.Where(r => r.SkuName == item.ModelNo).FirstOrDefault();

            //    string modno = aa.SkuName;
            //    int qty = Convert.ToInt32(item.Qty);

            //    int actqty = 0;
            //    if (aa.Qty < qty)
            //    {
            //        actqty = Convert.ToInt32(aa.Qty);
            //        aa.Qty = 0;
            //    }
            //    else
            //    {
            //        aa.Qty = (aa.Qty - item.Qty);
            //        actqty = qty;
            //    }
            //    int instockpos = DB.Business.SPs.SelectInsertUpdateStockPosting(Convert.ToInt64(aa.ProductId), modno, 0, actqty, OrderId, "", "order", DateTime.Now, "InsertStockPosting").Execute();
            //    if (aa != null)
            //    {
            //        db.SaveChanges();
            //    }




            //}

            decimal amount = 0;
            string cartpro = "";
            var orderdetail = db.OrderDetails.Where(r => r.OrderNo == OrderId).ToList();
            if (orderdetail != null)
            {
                //cartpro += "<tr><td style='width:40%'></td><td style='width:15%'>Model No</td><td style='width:15%'>Unit Price</td><td style='width:15%'>Quantity</td><td style='width:15%'>Total</td> </tr>";

                string url = "https://www.SStyleFactory.com/upload/products/small";
                foreach (var itemdetail in orderdetail)
                {
                    amount += Convert.ToDecimal(itemdetail.Qty) * Convert.ToDecimal(itemdetail.Price);

                    //cartpro += "<tr><td ><p><img style='float:left;'  src='"+url +itemdetail.Img.ToString() + "'/>" + itemdetail.ShortDesc + "</p> </td><td>" + itemdetail.ModelNo + "</td><td> Rs. " + itemdetail.Price + "</td><td>" + itemdetail.Qty + "</td><td> Rs. " + Convert.ToDecimal(itemdetail.Qty) * Convert.ToDecimal(itemdetail.Price) + "</td></tr>";

                    cartpro += " <tr><td><p><img style='float:left;'  src='https://www.SStyleFactory.com/upload/products/small/'" + itemdetail.Img.ToString() + "/>" + itemdetail.ShortDesc.ToString() + "</p> </td><td>" + itemdetail.ModelNo + "</td><td> INR " + Math.Round(Convert.ToDecimal(itemdetail.Price)) + "</td><td>" + itemdetail.Qty + "</td><td> INR " + Convert.ToDecimal(itemdetail.Qty) * Convert.ToDecimal(itemdetail.Price) + "</td></tr>";
                    cartpro += "<tr><td colspan='5'><hr style='margin:3px' /></td></tr>";

                    // cartpro += "<tr><td style='padding: 5px 10px;border-right: 1px solid #ccc; vertical-align: top;'><img style='float:left;' width='54px' height='54px'  src='" + url + itemdetail.Img.ToString() + "'/><p style='vertical-align: top;font-size: 13px;width: 65%;float: right;margin: 5px 0;'>" + itemdetail.ShortDesc + "</p> </td><td style='padding: 5px 10px;border-right: 1px solid #ccc; vertical-align: top;'><p style='vertical-align: top;font-size: 13px;'>" + itemdetail.ModelNo + "</p></td><td style='padding: 5px 10px;border-right: 1px solid #ccc; vertical-align: top;'><p style='vertical-align: top;font-size: 13px;'> Rs. " + itemdetail.Price + "</p></td><td style='padding: 5px 10px;border-right: 1px solid #ccc; vertical-align: top;'><p style='vertical-align: top;font-size: 13px;'>" + itemdetail.Qty + "</p></td><td style='padding: 5px 10px; vertical-align: top;'><p style='vertical-align: top;font-size: 14px;'> INR " + Convert.ToDecimal(itemdetail.Qty) * Convert.ToDecimal(itemdetail.Price) + "</p></td></tr>";

                }


                string cartProduct = "<table width='100%' style='font-family:Verdana;font-size:11px; border:solid 1px #ccc;'>";
                cartProduct += "<tr><td style='width:40%'>  </td><td style='width:15%'>Model No</td><td style='width:15%'>Unit Price</td><td style='width:15%'>Quantity</td><td style='width:15%'>Total</td> </tr>";
                cartProduct += "<tr><td colspan='5'><hr/></td></tr>";
                cartProduct += cartpro;

                if (Convert.ToDecimal(discamt) > 0)
                {
                    cartProduct += "<tr><td></td><td></td><td></td><td>Discount:</td><td> INR " + discamt + "</td></tr>";
                }
                else
                {
                    cartProduct += "<tr><td></td><td></td><td></td><td>Discount:</td><td> INR " + 0 + "</td></tr>";
                }


                decimal tamount = Convert.ToDecimal(amount) + Convert.ToDecimal(shipamt) - Convert.ToDecimal(discamt);
                giftvalue = tamount - momvalue;

                sendEmail(cartProduct, orid, amount, shipamt, currency, discamt);

            }

            //AddGoogleEcommerceTraking(orderDtl.FirstOrDefault().OrderCurrency, orid);
        }
        else
        {

        }
    }



    //void AddGoogleEcommerceTraking(string orderId)
    //{
    //    JoviFashionEntities db = new JoviFashionEntities();

    //    decimal gaShip = 0;
    //    decimal currPrice = 1;

    //    var uinfo = (from user in db.JoviFashionUserInfoes
    //                 join ord in db.JoviFashionOrderTbls on user.Id equals ord.UserId
    //                 where ord.OrderNo == orderId
    //                 select new { user.City, user.State, user.Country, ord.OrderTotal, ord.ShippingAmt }).FirstOrDefault();

    //    gaShip = Convert.ToDecimal(uinfo.ShippingAmt.ToString());
    //    Google.Transaction transaction = new Google.Transaction(orderId, "jovifashion.com", 0, Convert.ToDecimal(gaShip.ToString("0.00")), uinfo.City, uinfo.State, uinfo.Country);
    //    var itemDetail = (from ordDe in db.JoviFashionOrderDetails
    //                      join pro in db.ProductMasters on ordDe.ModelNo equals pro.ModelNo
    //                      //join itm in db.InnerSubCategories on pro.InnerSubcategoryId equals itm.InnerSubCategoryId
    //                      join subcat in db.SubCategoryMasters on pro.SubCategoryId equals subcat.SubCategoryId
    //                      select new { ordDe.OrderNo, ProductCode = pro.ModelNo, ordDe.Price, ordDe.Qty, itemName = subcat.SubCategoryName })
    //                         .Where(r => r.OrderNo == orderId);
    //    int id = 1;
    //    foreach (var item in itemDetail.ToList())
    //    {
    //        decimal gaPrice = 0;
    //        gaPrice = Convert.ToDecimal(item.Price / currPrice);
    //        transaction.Add(new Google.Item(item.ProductCode, item.itemName + "-" + item.ProductCode, item.itemName, Convert.ToDecimal(gaPrice.ToString("0.00")), Convert.ToInt32(item.Qty)));
    //    }


    //    Literal ltrlAnalytics = (Literal)this.Master.FindControl("ltrlAnalytics");

    //    MasterPage.analytics.AddTrans(transaction);

    //    ltrlAnalytics.Text = MasterPage.analytics.ToString();


    //}





    void sendEmail(string cart, string orderno, decimal amt, string shippingamt, string cur, string discamt)
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
            msg = msg + "";
            //msg = "<table cellpadding='5' cellspacing='0' width='100%'  style='border:1px solid #efefef;'><thead style='background-color:#efefef'><tr> <td style='width: 40%'></td><td style='width: 15%'>Model No</td><td style='width: 15%'>Unit Price</td><td style='width: 15%'>Quantity</td><td style='width: 15%'>Total</td> </tr></thead> <tbody>";

            msg = msg + cart;

            decimal Total = Convert.ToDecimal(amt) + Convert.ToDecimal(shippingamt) - Convert.ToDecimal(discamt);

            msg = msg + "<tr><td colspan='2' style='border-top: 1px solid #ccc;text-align:left;padding:5px;'></td><td colspan='2' style='border-top: 1px solid #ccc;font-size: 13px;text-align:right;padding:5px;'>Sub Total (" + cur + "):</td><td style='border-top: 1px solid #ccc;font-size: 13px;padding:5px;' align='center'>" + amt + "</td></tr>";

            decimal Grandtotal = 0;

            if (Convert.ToDecimal(discamt) > 0)
            {
                Grandtotal = Convert.ToDecimal(amt) + Convert.ToDecimal(shippingamt) - Convert.ToDecimal(discamt);

                msg = msg + "<tr><td colspan='2' style='border-top: 1px solid #ccc;text-align:left;padding:5px;'></td><td colspan='2' style='border-top: 1px solid #ccc;font-size: 13px;text-align:right;padding:5px;'>Discount(" + cur + "):</td><td style='border-top: 1px solid #ccc;font-size: 13px;padding: 5px;' align='center'>" + discamt + "</td></tr>";
                msg = msg + "<tr><td colspan='2' style='border-top: 1px solid #ccc;text-align:left;padding:5px;'></td><td colspan='2' style='border-top: 1px solid #ccc;font-size: 13px;text-align:right;padding:5px;'>Grand Total(" + cur + "):</td><td style='border-top: 1px solid #ccc;font-size: 13px;padding: 5px;' align='center'>" + Grandtotal + "</td></tr>";
            }
            else
            {
                Grandtotal = Convert.ToDecimal(amt) + Convert.ToDecimal(shippingamt);
                msg = msg + "<tr><td colspan='2' style='border-top: 1px solid #ccc;text-align:left;padding:5px;'></td><td colspan='2' style='border-top: 1px solid #ccc;font-size: 13px;text-align:right;padding:5px;'>Grand Total(" + cur + "):</td><td style='border-top: 1px solid #ccc;font-size: 13px;padding: 5px;' align='center'>" + Grandtotal + "</td></tr>";
            }

            msg += "</table>";

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
                string responseq = sm.SendMail(billingdetail.Name, billingdetail.Email, billingdetail.scontact, "Your Order (Success) at SStyleFactory has been placed successfully.", body, "Client");
                string sendmailtoadmin = sm.SendMail(billingdetail.Name, billingdetail.Email, billingdetail.scontact, "Order on SStyleFactory (Success)", body, "Admin");
            }
            catch (Exception ex)
            {
                Common.LogError(ex);
            }
        }
    }


}