using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class checkout : System.Web.UI.Page
{

    FabAccessoriesEntities db = new FabAccessoriesEntities();

    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserId"] == null || Session["LoginId"] == null)
            {
                blankdata();
            }
            else
            {
                getuserbilldetail();
            }

            Session["CouponPerc"] = null;
            Session["CouponCode"] = null;

            getcart();

            if (Convert.ToDecimal(ltrlNetTotal.Text.Trim()) >= 500)
            {
                btncheckout.Visible = true;
                lblcodmsg.Visible = false;
            }
            else
            {
                btncheckout.Visible = false;
                lblcodmsg.Visible = true;
            }
        }
    }

    public void blankdata()
    {
        txtsfname.Text = "";
        txtslname.Text = "";
        txtsaddress1.Text = "";
        //txtsstate.Text = "";
        drpsstate.SelectedValue = "0";
        txtscity.Text = "";
        drpscountry.SelectedIndex = 0;
        txtszip.Text = "";
        txtsmobile.Text = "";
        txtszip.Text = "";
        txtsemail.Text = "";
        txtsfname.Focus();
    }

    public void getuserbilldetail()
    {
        if (Session["UserId"] != null && Session["LoginId"] != null)
        {

            string email = Session["LoginId"].ToString().ToLower();

            var ulist = db.UserInfoes.Where(r => r.Email.ToLower() == email || r.Username.ToLower() == email).FirstOrDefault();

            if (ulist != null)
            {

                txtsfname.Text = ulist.FirstName;
                txtslname.Text = ulist.LastName;
                txtsaddress1.Text = ulist.Address;
                txtscity.Text = ulist.City;
                //txtsstate.Text = ulist.State;

                drpscountry.SelectedIndex = 0;
                txtszip.Text = ulist.Zip;
                txtsmobile.Text = ulist.ContactNo;
                txtsemail.Text = ulist.Email;



            }
        }
        else
        {
            blankdata();
        }
    }


    private void getcart()
    {
        var currenyVal = 1.00m;
        decimal tot = 0.00m;
        if (HttpContext.Current.Request.Cookies["fabcart"] == null)
        {
            Guid gui = Guid.NewGuid();
            var cookie = new HttpCookie("fabcart", gui.ToString())
            {
                Expires = DateTime.Now.AddDays(5)
            };
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        var cartp = HttpContext.Current.Request.Cookies["fabcart"].Value;
        //Literal lblCart = (Literal)this.Master.FindControl("ltritemcount");
        DataTable table = HttpContext.Current.Cache[cartp] as DataTable;
        if (table != null)
        {
            List<cart> crt = new List<cart>();
            foreach (DataRow cartRow in table.Rows)
            {
                cart c = new cart();
                c.Date = Convert.ToDateTime(cartRow["AddDate"].ToString());
                c.ext1 = cartRow["ext1"].ToString();
                c.ext2 = cartRow["ext2"].ToString();
                c.ext3 = cartRow["ext3"].ToString();
                c.remarks = cartRow["remarks"] == null ? "" : cartRow["remarks"].ToString() == "0" ? "" : cartRow["remarks"].ToString();
                c.ProductId = Convert.ToInt64(cartRow["ProductId"].ToString());
                var cartP = (from p in db.ProductMasters
                             join cat in db.CategoryMasters on p.CategoryId equals cat.CategoryId
                             join col in db.CollectionMasters on p.CollectionId equals col.CollectionId
                             join clr in db.ColorMasters on p.ColorId equals clr.ColorId into cm
                             from clr in cm.DefaultIfEmpty()
                             join sz in db.SizeMasters on p.SizeId equals sz.SizeId into sizem
                             from sz in sizem.DefaultIfEmpty()

                             where p.Id == c.ProductId
                             select new
                             {
                                 p.Id,
                                 p.SkuName,
                                 p.MRP,
                                 p.SRP,
                                 p.Description,
                                 p.Title,
                                 CollectionName = col.CollectionName,
                                 CategoryName = cat.CategoryName,
                                 price = p.MRP,
                                 saleprice = p.SRP,
                                 SizeName = sz.SizeName,
                                 ColorName = clr.ColorName,
                                 p.Image
                             }).ToList();
                var pCart = (from p in cartP

                             select new
                             {
                                 p.Id,
                                 p.SkuName,
                                 p.MRP,
                                 p.SRP,
                                 p.Description,
                                 p.Title,
                                 p.CollectionName,
                                 p.CategoryName,
                                 ProductPrice = Convert.ToDecimal(p.price) / currenyVal,
                                 discountprice = Convert.ToDecimal(Convert.ToDecimal(p.saleprice) / currenyVal).ToString("0.00"),
                                 p.Image
                                 //discountprice_old = Convert.ToDecimal((Convert.ToDecimal((Convert.ToDecimal(p.price) / currenyVal)) - Convert.ToDecimal((Convert.ToDecimal((Convert.ToDecimal(p.price) / currenyVal)) / 100) * Convert.ToDecimal(p.Discount)))).ToString("0.00")

                             }).FirstOrDefault();
                if (pCart != null)
                {
                    c.shortDesc = pCart.Title == null ? "" : pCart.Title;
                    c.unitPrice = Convert.ToDecimal(Convert.ToDecimal(pCart.ProductPrice).ToString("0.00"));
                    c.img = pCart.Image;
                    c.modelNo = pCart.SkuName;
                    c.ext1 = pCart.CollectionName;
                    c.ext2 = pCart.CategoryName;
                    c.discountprice = pCart.discountprice;

                }
                else
                {
                    c.shortDesc = "";
                    c.unitPrice = 0.00m;
                    c.oldPrice = 0.00m;
                    c.img = "noimg.jpg";
                    c.modelNo = "";
                    c.discountprice = "0.00";
                    c.modelNo = "";
                    c.ext1 = "";
                    c.ext2 = "";
                }
                c.Qty = Convert.ToInt16(cartRow["Qty"].ToString());

                //if (cartRow["remarks"] == null)
                //{
                //    c.remarks = "";
                //}
                //else
                //{
                //    c.remarks = cartRow["remarks"].ToString();
                //}

                c.Size = cartRow["size"].ToString();
                if (Convert.ToDecimal(c.unitPrice) > Convert.ToDecimal(c.discountprice))
                {
                    tot += Convert.ToDecimal(c.Qty) * Convert.ToDecimal(c.discountprice);
                }
                else
                {
                    tot += Convert.ToDecimal(c.Qty) * Convert.ToDecimal(c.unitPrice);
                }

                HttpContext.Current.Cache.Insert(cartp, table, null, DateTime.Now.AddDays(5), System.Web.Caching.Cache.NoSlidingExpiration);
                crt.Add(c);


            }

            ltrlSubTotal.Text = ltrlNetTotal.Text = Math.Round(Convert.ToDecimal((Convert.ToDecimal(tot)).ToString())).ToString("0.00");

        }
        else
        {
            Response.Redirect(Page.ResolveUrl("mycart.html"));
        }
    }

    public class cart
    {
        public Int64 ProductId { get; set; }
        public DateTime Date { get; set; }
        public int Qty { get; set; }
        public string Size { get; set; }
        public string ext1 { get; set; }
        public string ext2 { get; set; }
        public string ext3 { get; set; }
        public string shortDesc { get; set; }
        public string modelNo { get; set; }
        public string img { get; set; }
        public decimal unitPrice { get; set; }
        public decimal oldPrice { get; set; }
        public string discountprice { get; set; }
        public string qtystatus { get; set; }
        public string remarks { get; set; }
    }



    protected void btncheckout_Click(object sender, EventArgs e)
    {
        if (txtsfname.Text.Trim() != "" && txtslname.Text.Trim() != "" && txtsmobile.Text.Trim() != "" && txtsemail.Text.Trim() != "" && txtsaddress1.Text.Trim() != "" && drpsstate.SelectedValue != "0" && txtscity.Text.Trim() != "" && txtszip.Text.Trim() != "")
        {
            if (ltrlSubTotal.Text.Trim() == "")
            {
                ltrlSubTotal.Text = "0.00";
            }

            if (Convert.ToDecimal(ltrlSubTotal.Text.Trim()) <= 0)
            {
                Response.Redirect(Page.ResolveUrl("mycart.html"));
            }

            if (txtsmobile.Text.Trim().Length != 10)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Please fill valid mobile no. (10 digits only)')", true);
                return;
            }



            string email = txtsemail.Text.Trim();

            UserInfo newuser = new UserInfo();

            string userid = "";

            string OrderNo = "";
            Int64 ordId = 1;
            var ord = db.OrderTbls.OrderByDescending(r => r.Id).FirstOrDefault();
            if (ord != null)
            {
                ordId = ord.Id + 1;
            }

            OrderNo = "000" + ordId;
            if (ordId.ToString().Length < 5)
            {
                OrderNo = OrderNo.Substring(OrderNo.Length - 4);
            }

            var user = db.UserInfoes.FirstOrDefault(x => x.Email == email);
            if (user == null)
            {

                newuser.Address = txtsaddress1.Text.Trim();
                newuser.City = txtscity.Text.Trim();
                newuser.Company = "";
                newuser.ContactNo = txtsmobile.Text.Trim();
                newuser.ContactPerson = "";
                newuser.Country = drpscountry.SelectedItem.Text;
                newuser.CreateDate = DateTime.Now;
                newuser.Designation = "";
                newuser.Email = email;
                newuser.FirstName = txtsfname.Text.Trim();
                newuser.Gender = "Private";
                newuser.IsActive = true;
                newuser.LastName = txtslname.Text.Trim();

                newuser.Password = txtsfname.Text.Trim() + "@123";
                newuser.DeviceId = "";
                newuser.PlayerId = "";
                newuser.Registerfrom = "Website";
                newuser.State = drpsstate.SelectedValue;
                newuser.Username = email;
                newuser.Zip = txtszip.Text.Trim();
                db.UserInfoes.Add(newuser);
                db.SaveChanges();

                userid = newuser.Id.ToString();

                if (!db.NewsLetters.Any(r => r.EmailId == newuser.Email))
                {
                    var NewsChk = new NewsLetter();
                    NewsChk.EmailId = newuser.Email;
                    NewsChk.CreateDate = DateTime.Now;
                    NewsChk.Status = 1;
                    db.NewsLetters.Add(NewsChk);
                    db.SaveChanges();
                }

                sendmail sm = new sendmail();

                string body = sm.MailFormat(newuser.FirstName, newuser.Email, newuser.Password, "", "Registration");
                string adminbody = sm.MailFormat(newuser.FirstName, newuser.Email, newuser.Password, Encrypt(newuser.Id.ToString().Trim()), "adminregistraion");

                string adminrs = sm.SendMail(newuser.Email, newuser.Email, newuser.ContactNo, "New Registration on S Style Factory", adminbody, "Admin");
                string rs = sm.SendMail(newuser.Email, newuser.Email, newuser.ContactNo, "Thank you For Registration on S Style Factory", body, "Client");

            }
            else
            {
                userid = user.Id.ToString();
            }


            if (userid.ToString() != "")
            {
                OrderNo = "FFA" + OrderNo; string Checksum = "";
                if (userid.Trim().Length > 0)
                {


                    string Discountamount = "0.00";


                    if (Session["DiscountCouponAmt"] != null)
                    {
                        Discountamount = Session["DiscountCouponAmt"].ToString();
                    }

                    var ship = Convert.ToDecimal("0.00");
                    OrderTbl ordTbl = new OrderTbl();
                    ordTbl.OrderNo = OrderNo;
                    ordTbl.UserId = Convert.ToInt64(userid);
                    ordTbl.OrderTotal = Convert.ToDecimal(ltrlSubTotal.Text.Trim());
                    ordTbl.OrderCurrency = "INR";
                    ordTbl.ShippingAmt = ship;
                    ordTbl.CouponCode = Session["CouponCode"] == null ? "" : Session["CouponCode"].ToString();
                    ordTbl.CouponType = "";
                    ordTbl.CouponAmt = Convert.ToDecimal(Discountamount);
                    ordTbl.Comment = "";
                    ordTbl.status = "0";
                    ordTbl.OrderBy = "Website";
                    ordTbl.OrderDate = DateTime.Now;
                    //Discountamount = "0.00";
                    db.OrderTbls.Add(ordTbl);

                    OrderTracking ordTrack = new OrderTracking();
                    ordTrack.OrderNo = OrderNo;
                    ordTrack.OrderStatus = "Processed";
                    ordTrack.StatusDate = ordTrack.CreateDate = DateTime.Now;
                    ordTrack.DeliveredDate = Convert.ToDateTime("01/01/1900");
                    ordTrack.ExpectedDeliverDate = Convert.ToDateTime("01/01/1900");
                    ordTrack.ShippedDate = Convert.ToDateTime("01/01/1900");
                    db.OrderTrackings.Add(ordTrack);
                    db.SaveChanges();

                    decimal tot = 0.00m;
                    string cartPro = "";
                    if (OrderNo.Trim().Length > 0)
                    {

                        if (HttpContext.Current.Request.Cookies["fabcart"] == null)
                        {
                            Guid gui = Guid.NewGuid();
                            var cookie = new HttpCookie("fabcart", gui.ToString())
                            {
                                Expires = DateTime.Now.AddDays(5)
                            };
                            HttpContext.Current.Response.Cookies.Add(cookie);
                        }
                        var cartp = HttpContext.Current.Request.Cookies["fabcart"].Value;
                        //Literal lblCart = (Literal)this.Master.FindControl("ltritemcount");
                        DataTable table = HttpContext.Current.Cache[cartp] as DataTable;


                        if (table != null)
                        {
                            foreach (DataRow cartRow in table.Rows)
                            {
                                cart c = new cart();
                                c.Qty = Convert.ToInt16(cartRow["Qty"].ToString());
                                c.ProductId = Convert.ToInt64(cartRow["ProductId"].ToString());
                                decimal d = Convert.ToDecimal(c.ProductId);
                                string avlqty = "0";
                                c.remarks = cartRow["remarks"] == null ? "" : cartRow["remarks"].ToString() == "0" ? "" : cartRow["remarks"].ToString();
                                // var checkproduct = db.ProductMasters.Where(r => r.Id == d && r.ShowHide == 1).FirstOrDefault();

                                var checkproduct = (from p in db.ProductMasters
                                                    join cate in db.CategoryMasters on p.CategoryId equals cate.CategoryId
                                                    where p.Id == c.ProductId && p.ShowHide == 1
                                                    select new { p.Id, p.Image, p.Title, p.SkuName, p.MRP, p.SRP, ItemName = cate.CategoryName, cate.CategoryId }).FirstOrDefault();

                                if (checkproduct != null)
                                {


                                    OrderDetail orddetail = new OrderDetail();
                                    orddetail.OrderNo = OrderNo;
                                    orddetail.Comment = c.remarks;
                                    orddetail.ProductId = Convert.ToInt64(checkproduct.Id);
                                    orddetail.Img = checkproduct.Image;
                                    orddetail.ModelNo = checkproduct.SkuName;
                                    orddetail.Item = checkproduct.ItemName;
                                    orddetail.Price = Math.Round(Convert.ToDecimal(checkproduct.SRP));
                                    orddetail.Total = Math.Round(Convert.ToDecimal(Convert.ToDecimal(checkproduct.SRP)) * Convert.ToInt16(c.Qty));
                                    orddetail.ShortDesc = checkproduct.Title.ToString();
                                    orddetail.Qty = Convert.ToInt32(c.Qty);

                                    db.OrderDetails.Add(orddetail);
                                    tot += Math.Round(Convert.ToDecimal(orddetail.Total));
                                    db.SaveChanges();

                                    cartPro += " <tr><td><p><img style='float:left;'  src='https://www.sstylefactory.com/upload/product/small/'" + checkproduct.Image + "/>" + checkproduct.Title.ToString() + "</p> </td><td>" + checkproduct.SkuName + "</td><td> Rs. " + Math.Round(Convert.ToDecimal(checkproduct.SRP)) + "</td><td>" + c.Qty + "</td><td> Rs. " + orddetail.Total + "</td></tr>";
                                    cartPro += "<tr><td colspan='5'><hr style='margin:3px' /></td></tr>";

                                }
                            }
                        }


                        string cartProduct = "<table width='100%' style='font-family:Verdana;font-size:11px; border:solid 1px #ccc;'>";
                        cartProduct += "<tr><td style='width:40%'>  </td><td style='width:15%'>Model No</td><td style='width:15%'>Unit Price</td><td style='width:15%'>Quantity</td><td style='width:15%'>Total</td> </tr>";
                        cartProduct += "<tr><td colspan='5'><hr/></td></tr>";
                        cartProduct += cartPro;

                        //cartProduct += "<tr><td></td><td></td><td></td><td>Sub-Total:</td><td> Rs. " + ltrlSubTotal.Text.Trim() + "</td></tr>";
                        //cartProduct += "<tr><td></td><td></td><td></td><td>Shipping Charges:</td><td> Free</td></tr>";

                        if (Convert.ToDecimal(Discountamount) > 0)
                        {
                            cartProduct += "<tr><td></td><td></td><td></td><td>Discount:</td><td> Rs. " + Discountamount + "</td></tr>";
                        }
                        else
                        {
                            cartProduct += "<tr><td></td><td></td><td></td><td>Discount:</td><td> Rs. " + 0 + "</td></tr>";
                        }
                        //cartProduct += "<tr><td></td><td></td><td></td><td colspan='2'> <hr style='margin:3px' /></td></tr>";
                        //cartProduct += "<tr><td></td><td></td><td></td><td>Total Amt:</td><td> Rs. " + ltrlNetTotal.Text.Trim() + "</td></tr>";
                        // cartProduct += " </table>";

                        OrderBillingInfo ordBilling = new OrderBillingInfo();
                        ordBilling.Name = txtsfname.Text.Trim() + " " + txtslname.Text.Trim();
                        ordBilling.Email = txtsemail.Text.Trim();
                        ordBilling.ContactNo = txtsmobile.Text.Trim();
                        ordBilling.Address = txtsaddress1.Text.Trim();
                        ordBilling.Address2 = "";
                        ordBilling.City = txtscity.Text.Trim();
                        ordBilling.State = drpsstate.SelectedValue;
                        ordBilling.Country = drpscountry.SelectedItem.Text;
                        ordBilling.Zip = txtszip.Text.Trim();
                        ordBilling.CreateDate = DateTime.Now;
                        ordBilling.OrderNo = OrderNo;
                        db.OrderBillingInfoes.Add(ordBilling);
                        db.SaveChanges();

                        OrderShippingDetail ordShipping = new OrderShippingDetail();
                        ordShipping.Name = txtsfname.Text.Trim() + " " + txtslname.Text.Trim();
                        ordShipping.Email = txtsemail.Text.Trim();
                        ordShipping.ContactNo = txtsmobile.Text.Trim();
                        ordShipping.Address = txtsaddress1.Text.Trim();
                        ordShipping.Address2 = "";
                        ordShipping.City = txtscity.Text.Trim();
                        ordShipping.State = drpsstate.SelectedValue;
                        ordShipping.Country = drpscountry.SelectedItem.Text;
                        ordShipping.Zip = txtszip.Text.Trim();
                        ordShipping.CreateDate = DateTime.Now;
                        ordShipping.OrderNo = OrderNo;
                        db.OrderShippingDetails.Add(ordShipping);
                        db.SaveChanges();

                        OrderPaymentResponse payres = new OrderPaymentResponse();
                        payres.OrderId = OrderNo;
                        payres.PaymentId = "";
                        payres.PaymentMode = "COD";
                        payres.Status = "Unpaid";
                        payres.Message = "";
                        payres.Mode = "COD";
                        payres.CreateDate = DateTime.Now;
                        db.OrderPaymentResponses.Add(payres);
                        db.SaveChanges();




                        sendEmail(cartProduct, OrderNo, Convert.ToDecimal(ltrlSubTotal.Text.Trim()), ship.ToString(), "INR", Discountamount);


                        var cookie1 = new HttpCookie("fabcart", "")
                        {
                            Expires = DateTime.Now.AddDays(-1)
                        };
                        HttpContext.Current.Response.Cookies.Add(cookie1);
                        HttpContext.Current.Cache.Remove(cartp);


                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Thanks for your COD order. We will contact on your given mobile no. or email to confirm COD Order.'); window.location.href='index.html'; ", true);


                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Please try again !')", true);
                    }


                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Please try again !')", true);
                }


            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Invalid User !')", true);
            }

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Please fill all required informations')", true);
        }
    }

    private string Encrypt(string clearText)
    {
        string EncryptionKey = "fabfashion";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }


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

            msg = msg + "<tr><td colspan='2' style='border-top: 1px solid #ccc;text-align:left;'></td><td colspan='2' style='border-top: 1px solid #ccc;font-size: 13px;text-align:right;padding:5px;'>Sub Total (" + cur + "):</td><td style='border-top: 1px solid #ccc;font-size: 13px;padding:5px;' align='center'>" + amt + "</td></tr>";

            decimal Grandtotal = 0;

            if (Convert.ToDecimal(discamt) > 0)
            {
                Grandtotal = Convert.ToDecimal(amt) + Convert.ToDecimal(shippingamt) - Convert.ToDecimal(discamt);

                msg = msg + "<tr><td colspan='2' style='border-top: 1px solid #ccc;text-align:left;'></td><td colspan='2' style='border-top: 1px solid #ccc;font-size: 13px;text-align:right;padding:5px;'>Discount(" + cur + "):</td><td style='border-top: 1px solid #ccc;font-size: 13px;padding: 5px;' align='center'>" + discamt + "</td></tr>";
                msg = msg + "<tr><td colspan='2' style='border-top: 1px solid #ccc;text-align:left;'></td><td colspan='2' style='border-top: 1px solid #ccc;font-size: 13px;text-align:right;padding:5px;'>Grand Total(" + cur + "):</td><td style='border-top: 1px solid #ccc;font-size: 13px;padding: 5px;' align='center'>" + Grandtotal + "</td></tr>";
            }
            else
            {
                Grandtotal = Convert.ToDecimal(amt) + Convert.ToDecimal(shippingamt);
                msg = msg + "<tr><td colspan='2' style='border-top: 1px solid #ccc;text-align:left;'></td><td colspan='2' style='border-top: 1px solid #ccc;font-size: 13px;text-align:right;padding:5px;'>Grand Total(" + cur + "):</td><td style='border-top: 1px solid #ccc;font-size: 13px;padding: 5px;' align='center'>" + Grandtotal + "</td></tr>";
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
                string responseq = sm.SendMail(billingdetail.Name, billingdetail.Email, billingdetail.scontact, "Your Order (COD) at S Style Factory has been placed successfully.", body, "Client");
                string sendmailtoadmin = sm.SendMail(billingdetail.Name, billingdetail.Email, billingdetail.scontact, "Order on S Style Factory (COD)", body, "Admin");
            }
            catch (Exception ex)
            {
                Common.LogError(ex);
            }
        }
    }

    protected void btnpaytm_Click(object sender, EventArgs e)
    {
        if (txtsfname.Text.Trim() != "" && txtslname.Text.Trim() != "" && txtsmobile.Text.Trim() != "" && txtsemail.Text.Trim() != "" && txtsaddress1.Text.Trim() != "" && drpsstate.SelectedValue != "0" && txtscity.Text.Trim() != "" && txtszip.Text.Trim() != "")
        {
            if (ltrlSubTotal.Text.Trim() == "")
            {
                ltrlSubTotal.Text = "0.00";
            }

            if (Convert.ToDecimal(ltrlSubTotal.Text.Trim()) <= 0)
            {
                Response.Redirect(Page.ResolveUrl("mycart.html"));
            }

            if (txtsmobile.Text.Trim().Length != 10)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Please fill valid mobile no. (10 digits only)')", true);
                return;
            }



            string email = txtsemail.Text.Trim();

            UserInfo newuser = new UserInfo();

            string userid = "";

            string OrderNo = "";
            Int64 ordId = 1;
            var ord = db.OrderTbls.OrderByDescending(r => r.Id).FirstOrDefault();
            if (ord != null)
            {
                ordId = ord.Id + 1;
            }

            OrderNo = "000" + ordId;
            if (ordId.ToString().Length < 5)
            {
                OrderNo = OrderNo.Substring(OrderNo.Length - 4);
            }

            var user = db.UserInfoes.FirstOrDefault(x => x.Email == email);
            if (user == null)
            {

                newuser.Address = txtsaddress1.Text.Trim();
                newuser.City = txtscity.Text.Trim();
                newuser.Company = "";
                newuser.ContactNo = txtsmobile.Text.Trim();
                newuser.ContactPerson = "";
                newuser.Country = drpscountry.SelectedItem.Text;
                newuser.CreateDate = DateTime.Now;
                newuser.Designation = "";
                newuser.Email = email;
                newuser.FirstName = txtsfname.Text.Trim();
                newuser.Gender = "Private";
                newuser.IsActive = true;
                newuser.LastName = txtslname.Text.Trim();

                newuser.Password = txtsfname.Text.Trim() + "@123";
                newuser.DeviceId = "";
                newuser.PlayerId = "";
                newuser.Registerfrom = "Website";
                newuser.State = drpsstate.SelectedValue;
                newuser.Username = email;
                newuser.Zip = txtszip.Text.Trim();
                db.UserInfoes.Add(newuser);
                db.SaveChanges();

                userid = newuser.Id.ToString();

                if (!db.NewsLetters.Any(r => r.EmailId == newuser.Email))
                {
                    var NewsChk = new NewsLetter();
                    NewsChk.EmailId = newuser.Email;
                    NewsChk.CreateDate = DateTime.Now;
                    NewsChk.Status = 1;
                    db.NewsLetters.Add(NewsChk);
                    db.SaveChanges();
                }

                sendmail sm = new sendmail();

                string body = sm.MailFormat(newuser.FirstName, newuser.Email, newuser.Password, "", "Registration");
                string adminbody = sm.MailFormat(newuser.FirstName, newuser.Email, newuser.Password, Encrypt(newuser.Id.ToString().Trim()), "adminregistraion");

                string adminrs = sm.SendMail(newuser.Email, newuser.Email, newuser.ContactNo, "New Registration on S Style Factory", adminbody, "Admin");
                string rs = sm.SendMail(newuser.Email, newuser.Email, newuser.ContactNo, "Thank you For Registration on S Style Factory", body, "Client");

            }
            else
            {
                userid = user.Id.ToString();
            }

            Session["UserId"] = userid;

            if (userid.ToString() != "")
            {
                OrderNo = "FFA" + OrderNo; string Checksum = "";
                if (userid.Trim().Length > 0)
                {


                    string Discountamount = "0.00";


                    if (Session["DiscountCouponAmt"] != null)
                    {
                        Discountamount = Session["DiscountCouponAmt"].ToString();
                    }

                    var ship = Convert.ToDecimal("0.00");
                    OrderTbl ordTbl = new OrderTbl();
                    ordTbl.OrderNo = OrderNo;
                    ordTbl.UserId = Convert.ToInt64(userid);
                    ordTbl.OrderTotal = Convert.ToDecimal(ltrlSubTotal.Text.Trim());
                    ordTbl.OrderCurrency = "INR";
                    ordTbl.ShippingAmt = ship;
                    ordTbl.CouponCode = Session["CouponCode"] == null ? "" : Session["CouponCode"].ToString();
                    ordTbl.CouponType = "";
                    ordTbl.CouponAmt = Convert.ToDecimal(Discountamount);
                    ordTbl.Comment = "";
                    ordTbl.status = "0";
                    ordTbl.OrderBy = "Website";
                    ordTbl.OrderDate = DateTime.Now;
                    //Discountamount = "0.00";
                    db.OrderTbls.Add(ordTbl);

                    OrderTracking ordTrack = new OrderTracking();
                    ordTrack.OrderNo = OrderNo;
                    ordTrack.OrderStatus = "Processed";
                    ordTrack.StatusDate = ordTrack.CreateDate = DateTime.Now;
                    ordTrack.DeliveredDate = Convert.ToDateTime("01/01/1900");
                    ordTrack.ExpectedDeliverDate = Convert.ToDateTime("01/01/1900");
                    ordTrack.ShippedDate = Convert.ToDateTime("01/01/1900");
                    db.OrderTrackings.Add(ordTrack);
                    db.SaveChanges();

                    decimal tot = 0.00m;
                    string cartPro = "";
                    if (OrderNo.Trim().Length > 0)
                    {

                        if (HttpContext.Current.Request.Cookies["fabcart"] == null)
                        {
                            Guid gui = Guid.NewGuid();
                            var cookie = new HttpCookie("fabcart", gui.ToString())
                            {
                                Expires = DateTime.Now.AddDays(5)
                            };
                            HttpContext.Current.Response.Cookies.Add(cookie);
                        }
                        var cartp = HttpContext.Current.Request.Cookies["fabcart"].Value;
                        //Literal lblCart = (Literal)this.Master.FindControl("ltritemcount");
                        DataTable table = HttpContext.Current.Cache[cartp] as DataTable;


                        if (table != null)
                        {
                            foreach (DataRow cartRow in table.Rows)
                            {
                                cart c = new cart();
                                c.Qty = Convert.ToInt16(cartRow["Qty"].ToString());
                                c.ProductId = Convert.ToInt64(cartRow["ProductId"].ToString());
                                decimal d = Convert.ToDecimal(c.ProductId);
                                string avlqty = "0";
                                c.remarks = cartRow["remarks"] == null ? "" : cartRow["remarks"].ToString() == "0" ? "" : cartRow["remarks"].ToString();
                                // var checkproduct = db.ProductMasters.Where(r => r.Id == d && r.ShowHide == 1).FirstOrDefault();

                                var checkproduct = (from p in db.ProductMasters
                                                    join cate in db.CategoryMasters on p.CategoryId equals cate.CategoryId
                                                    where p.Id == c.ProductId && p.ShowHide == 1
                                                    select new { p.Id, p.Image, p.Title, p.SkuName, p.MRP, p.SRP, ItemName = cate.CategoryName, cate.CategoryId }).FirstOrDefault();

                                if (checkproduct != null)
                                {


                                    OrderDetail orddetail = new OrderDetail();
                                    orddetail.OrderNo = OrderNo;
                                    orddetail.Comment = c.remarks;
                                    orddetail.ProductId = Convert.ToInt64(checkproduct.Id);
                                    orddetail.Img = checkproduct.Image;
                                    orddetail.ModelNo = checkproduct.SkuName;
                                    orddetail.Item = checkproduct.ItemName;
                                    orddetail.Price = Math.Round(Convert.ToDecimal(checkproduct.SRP));
                                    orddetail.Total = Math.Round(Convert.ToDecimal(Convert.ToDecimal(checkproduct.SRP)) * Convert.ToInt16(c.Qty));
                                    orddetail.ShortDesc = checkproduct.Title.ToString();
                                    orddetail.Qty = Convert.ToInt32(c.Qty);

                                    db.OrderDetails.Add(orddetail);
                                    tot += Math.Round(Convert.ToDecimal(orddetail.Total));
                                    db.SaveChanges();

                                    cartPro += " <tr><td><p><img style='float:left;'  src='https://www.sstylefactory.com/upload/product/small/'" + checkproduct.Image + "/>" + checkproduct.Title.ToString() + "</p> </td><td>" + checkproduct.SkuName + "</td><td> Rs. " + Math.Round(Convert.ToDecimal(checkproduct.SRP)) + "</td><td>" + c.Qty + "</td><td> Rs. " + orddetail.Total + "</td></tr>";
                                    cartPro += "<tr><td colspan='5'><hr style='margin:3px' /></td></tr>";

                                }
                            }
                        }


                        string cartProduct = "<table width='100%' style='font-family:Verdana;font-size:11px; border:solid 1px #ccc;'>";
                        cartProduct += "<tr><td style='width:40%'>  </td><td style='width:15%'>Model No</td><td style='width:15%'>Unit Price</td><td style='width:15%'>Quantity</td><td style='width:15%'>Total</td> </tr>";
                        cartProduct += "<tr><td colspan='5'><hr/></td></tr>";
                        cartProduct += cartPro;

                        //cartProduct += "<tr><td></td><td></td><td></td><td>Sub-Total:</td><td> Rs. " + ltrlSubTotal.Text.Trim() + "</td></tr>";
                        //cartProduct += "<tr><td></td><td></td><td></td><td>Shipping Charges:</td><td> Free</td></tr>";

                        if (Convert.ToDecimal(Discountamount) > 0)
                        {
                            cartProduct += "<tr><td></td><td></td><td></td><td>Discount:</td><td> Rs. " + Discountamount + "</td></tr>";
                        }
                        else
                        {
                            cartProduct += "<tr><td></td><td></td><td></td><td>Discount:</td><td> Rs. " + 0 + "</td></tr>";
                        }
                        //cartProduct += "<tr><td></td><td></td><td></td><td colspan='2'> <hr style='margin:3px' /></td></tr>";
                        //cartProduct += "<tr><td></td><td></td><td></td><td>Total Amt:</td><td> Rs. " + ltrlNetTotal.Text.Trim() + "</td></tr>";
                        // cartProduct += " </table>";

                        OrderBillingInfo ordBilling = new OrderBillingInfo();
                        ordBilling.Name = txtsfname.Text.Trim() + " " + txtslname.Text.Trim();
                        ordBilling.Email = txtsemail.Text.Trim();
                        ordBilling.ContactNo = txtsmobile.Text.Trim();
                        ordBilling.Address = txtsaddress1.Text.Trim();
                        ordBilling.Address2 = "";
                        ordBilling.City = txtscity.Text.Trim();
                        ordBilling.State = drpsstate.SelectedValue;
                        ordBilling.Country = drpscountry.SelectedItem.Text;
                        ordBilling.Zip = txtszip.Text.Trim();
                        ordBilling.CreateDate = DateTime.Now;
                        ordBilling.OrderNo = OrderNo;
                        db.OrderBillingInfoes.Add(ordBilling);
                        db.SaveChanges();

                        OrderShippingDetail ordShipping = new OrderShippingDetail();
                        ordShipping.Name = txtsfname.Text.Trim() + " " + txtslname.Text.Trim();
                        ordShipping.Email = txtsemail.Text.Trim();
                        ordShipping.ContactNo = txtsmobile.Text.Trim();
                        ordShipping.Address = txtsaddress1.Text.Trim();
                        ordShipping.Address2 = "";
                        ordShipping.City = txtscity.Text.Trim();
                        ordShipping.State = drpsstate.SelectedValue;
                        ordShipping.Country = drpscountry.SelectedItem.Text;
                        ordShipping.Zip = txtszip.Text.Trim();
                        ordShipping.CreateDate = DateTime.Now;
                        ordShipping.OrderNo = OrderNo;
                        db.OrderShippingDetails.Add(ordShipping);
                        db.SaveChanges();

                        OrderPaymentResponse payres = new OrderPaymentResponse();
                        payres.OrderId = OrderNo;
                        payres.PaymentId = "";
                        payres.PaymentMode = "Paytm";
                        payres.Status = "Unpaid";
                        payres.Message = "";
                        payres.Mode = "Paytm";
                        payres.CreateDate = DateTime.Now;
                        db.OrderPaymentResponses.Add(payres);
                        db.SaveChanges();


                        

                        var cookie1 = new HttpCookie("fabcart", "")
                        {
                            Expires = DateTime.Now.AddDays(-1)
                        };
                        HttpContext.Current.Response.Cookies.Add(cookie1);
                        HttpContext.Current.Cache.Remove(cartp);


                        paytmcheckout(ltrlSubTotal.Text.Trim(), Discountamount, ship.ToString(), OrderNo);






                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Please try again !')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Please try again !')", true);
                }


            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Invalid User !')", true);
            }

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Please fill all required informations')", true);
        }
    }


    public void paytmcheckout(string amt, string discount, string shippingAmt, string OrderNo)
    {
        string usermail = "";
        string contno = "";
        string uid = "";
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

                totalamt = (Math.Floor(Math.Abs(Convert.ToDecimal(amt))) - Convert.ToDecimal(discount) + Math.Floor(Math.Abs(Convert.ToDecimal(shippingAmt)))).ToString();
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

        parameters.Add("CALLBACK_URL", "https://www.sstylefactory.com/pytmresponse.aspx");

        // parameters.Add("CALLBACK_URL", "http://localhost:7005/sstylefactory/pytmresponse.aspx");



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


    protected void btncoupon_Click(object sender, EventArgs e)
    {
        if (txtcouponcode.Text.Trim() != "")
        {
            FabAccessoriesEntities db = new FabAccessoriesEntities();
            string res = "";
            var dis = db.DiscountCoupons.Where(r => r.DiscountCode.ToLower() == txtcouponcode.Text.Trim().ToLower()).FirstOrDefault();
            if (dis != null)
            {
                if (dis.Status == "1")
                {

                    Session["CouponPerc"] = dis.DiscountPerc.ToString();
                    Session["CouponCode"] = txtcouponcode.Text.Trim();

                    //res = "{\"Response\" :" + dis.DiscountPerc.ToString() + ",\"res\":1,\"Message\":\"Coupon Code Applied\"}";

                    decimal tot = Convert.ToDecimal(ltrlSubTotal.Text.Trim());
                    decimal DisRs = 0.00M;
                    decimal dper = Convert.ToDecimal(Session["CouponPerc"]);
                    DisRs = Convert.ToDecimal(ltrlSubTotal.Text.Trim()) * (dper / 100);
                    Session["DiscountCouponAmt"] = Convert.ToDecimal(DisRs).ToString("0.00");
                    ltrdiscount.Text = Convert.ToDecimal(DisRs).ToString("0.00");
                    decimal total = Convert.ToDecimal(Convert.ToDecimal(ltrdiscount.Text).ToString("0.00"));
                    ltrlNetTotal.Text = (tot - DisRs).ToString("0.00");
                    // lblCouponStatus.Visible = true;
                    //lblCouponError.Visible = false;
                    // lblCouponStatus.Text = Session["CouponCode"].ToString() + " COUPON APPLIED!!!";

                    lblmsg.Text = "Coupon Applied";
                    lblmsg.Style.Add("color", "green");
                    txtcouponcode.Text = Session["CouponCode"].ToString();

                    dvcoupondisc.Visible = true;
                }
                else
                {

                    dvcoupondisc.Visible = false;

                    ltrlNetTotal.Text = ltrlSubTotal.Text.Trim();

                    Session["CouponPerc"] = null;
                    Session["CouponCode"] = Session["DiscountCouponAmt"] = null;
                    //res = "{\"Response\" :\"Coupon Code has been Expired\",\"res\":0,\"Message\":\"Coupon Code has been Expired\"}";
                    lblmsg.Text = "Coupon Code has been Expired";
                    lblmsg.Style.Add("color", "red");
                }
            }
            else
            {
                //res = "{\"Response\" :\"Coupon Code has been Expired\",\"res\":0,\"Message\":\"Invalid Coupon Code\"}";

                dvcoupondisc.Visible = false;

                ltrlNetTotal.Text = ltrlSubTotal.Text.Trim();

                Session["CouponPerc"] = null;
                Session["CouponCode"] = Session["DiscountCouponAmt"] = null;
                //res = "{\"Response\" :\"Coupon Code has been Expired\",\"res\":0,\"Message\":\"Coupon Code has been Expired\"}";
                lblmsg.Text = "Invalid Coupon Code";
                lblmsg.Style.Add("color", "red");
            }
        }
    }
}