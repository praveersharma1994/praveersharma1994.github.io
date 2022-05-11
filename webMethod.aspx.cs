using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Net.Mail;
using System.IO;
public partial class webMethod : System.Web.UI.Page
{


    FabAccessoriesEntities db = new FabAccessoriesEntities();

    private readonly static string ConnString = ConfigurationManager.ConnectionStrings["JewelsConStr"].ConnectionString;
    private static SqlConnection dbConnection;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //[WebMethod]
    //public static string PlusViewed(string productId)
    //{
    //    string msg = "";
    //    try
    //    {
    //        Int64 pid = Convert.ToInt64(productId);
    //        JoviFashionEntities db = new JoviFashionEntities();
    //        var p = db.ProductMasters.Where(r => r.ProductId == pid).FirstOrDefault();
    //        if (p != null)
    //        {
    //            p.ViewedCount = p.ViewedCount + 1;
    //            db.SaveChanges();
    //        }
    //        msg = "Product has been like";
    //    }
    //    catch
    //    {
    //        msg = "";
    //    }
    //    return msg;
    //}

    [WebMethod]
    public static string AddToCart(string productId, string qty, string size, string remarks)
    {
        string msg = "";
        if (size == "0")
        {
            size = "";
        }
        try
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
            DataTable table = HttpContext.Current.Cache[cartp] as DataTable;
            if (table == null)
            {
                table = CreateDataTable();
            }
            DataRow[] drExists = table.Select("productId='" + productId + "'");
            if (drExists.Count() <= 0)
            {
                DataRow dr = table.NewRow();
                dr["ProductId"] = productId;
                dr["Qty"] = qty;
                dr["size"] = size;
                dr["remarks"] = remarks;
                dr["AddDate"] = DateTime.Now;
                dr["qtystatus"] = "";
                table.Rows.Add(dr);
            }
            HttpContext.Current.Cache.Insert(cartp, table, null, DateTime.Now.AddDays(5), System.Web.Caching.Cache.NoSlidingExpiration);
            msg = table.Rows.Count.ToString();
        }
        catch
        {
            msg = "";
        }
        return msg;
    }


    [WebMethod]
    public static string AddToWish(string productId, string qty, string size, string remarks)
    {
        string msg = "";
        if (size == "0")
        {
            size = "";
        }
        try
        {
            if (HttpContext.Current.Request.Cookies["fabwish"] == null)
            {
                Guid gui = Guid.NewGuid();
                var cookie = new HttpCookie("fabwish", gui.ToString())
                {
                    Expires = DateTime.Now.AddDays(5)
                };
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            var cartp = HttpContext.Current.Request.Cookies["fabwish"].Value;
            DataTable table = HttpContext.Current.Cache[cartp] as DataTable;
            if (table == null)
            {
                table = CreateDataTable();
            }
            DataRow[] drExists = table.Select("productId='" + productId + "'");
            if (drExists.Count() <= 0)
            {
                DataRow dr = table.NewRow();
                dr["ProductId"] = productId;
                dr["Qty"] = qty;
                dr["size"] = size;
                dr["remarks"] = remarks;
                dr["AddDate"] = DateTime.Now;
                dr["qtystatus"] = "";
                table.Rows.Add(dr);
            }
            HttpContext.Current.Cache.Insert(cartp, table, null, DateTime.Now.AddDays(5), System.Web.Caching.Cache.NoSlidingExpiration);
            msg = table.Rows.Count.ToString();
        }
        catch
        {
            msg = "";
        }
        return msg;
    }


    [WebMethod]
    public static string CountProductInCart()
    {
        string msg = "";
        try
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
            DataTable table = HttpContext.Current.Cache[cartp] as DataTable;
            if (table != null)
            {
                HttpContext.Current.Cache.Insert(cartp, table, null, DateTime.Now.AddDays(5), System.Web.Caching.Cache.NoSlidingExpiration);
                msg = table.Rows.Count.ToString();
            }
            else
            {
                msg = "0";
            }
        }
        catch
        {
            msg = "";
        }
        return msg;
    }

    public static DataTable CreateDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ProductId");
        dt.Columns.Add("Qty");
        dt.Columns.Add("size");
        dt.Columns.Add("ext1");
        dt.Columns.Add("ext2");
        dt.Columns.Add("ext3");
        dt.Columns.Add("AddDate");
        dt.Columns.Add("qtystatus");
        dt.Columns.Add("remarks");
        return dt;
    }

    //[WebMethod]
    //public static string NewsletterClose(string isShow)
    //{
    //    string msg = "";
    //    try
    //    {
    //        double show = 20;
    //        if (isShow.ToLower() == "true")
    //        {
    //            show = 1440;
    //        }
    //        if (HttpContext.Current.Request.Cookies["newsletter"] == null)
    //        {

    //            var cookie = new HttpCookie("newsletter", "newsletter")
    //            {

    //                Expires = DateTime.Now.AddMinutes(show)
    //            };
    //            HttpContext.Current.Response.Cookies.Add(cookie);
    //        }
    //        else
    //        {
    //            var cookie = new HttpCookie("newsletter", "newsletter")
    //            {

    //                Expires = DateTime.Now.AddMinutes(show)
    //            };
    //            HttpContext.Current.Response.Cookies.Add(cookie);
    //        }
    //    }
    //    catch
    //    {
    //        msg = "";
    //    }
    //    return msg;
    //}

    //[WebMethod]
    //public static string ShowCartSummery(string productId)
    //{
    //    FabAccessoriesEntities db = new FabAccessoriesEntities();
    //    var PorductId = Convert.ToInt64(productId);
    //    var proDu = (from productsss in db.ProductMasters
    //                 join cat in db.CategoryMasters on productsss.CategoryId equals cat.CategoryId
    //                 where productsss.Id == PorductId
    //                 select new { item = cat.CategoryName, productsss.Id, productsss.Title, productsss.SkuName }).FirstOrDefault();
    //    string url = ""; string pro = ""; string img = "";
    //    System.Web.UI.Page p = HttpContext.Current.Handler as System.Web.UI.Page;

    //    if (proDu != null)
    //    {
    //        //url = p.ResolveUrl("jewellery/" + CommanClass.url("item") + "/" + CommanClass.url(proDu.productDes.ToString(), 60) + "-" + proDu.productId.ToString() + ".html");
    //        //pro = proDu.item + "(" + proDu.ItemCode + ")";
    //        //img = ConfigurationManager.AppSettings["sampleDesignPath"] + "small/" + proDu.ItemCode + ".jpg";
    //    }
    //    return url + "," + pro + "," + img;
    //}

    //[WebMethod]
    //public static string AddTowish1(string productId)
    //{
    //    string str = "";

    //    AddtoWishList adw = new AddtoWishList();
    //    string msg = adw.addwishlist(productId);
    //    if (msg != "")
    //    {


    //        var cartp = HttpContext.Current.Request.Cookies["wishlist"].Value;
    //        if (cartp != null)
    //        {
    //            DataTable table = HttpContext.Current.Cache[cartp] as DataTable;
    //            if (table.Rows.Count > 0)
    //            {
    //                str = table.Rows.Count.ToString();
    //            }
    //        }
    //        else
    //        {
    //            str = "Error !";
    //        }

    //    }
    //    else
    //    {
    //        str = "Error !";
    //    }

    //    return str;
    //}

    //[WebMethod]
    //public static string[] GetFirstNames(string namePrefix)
    //{

    //    List<string> lstNames = new List<string>();

    //    SqlParameter param = new SqlParameter("@search", namePrefix + "%");
    //    string sqlQuery = "select distinct name from item where name like @search";

    //    DataTable dt = getsearch(sqlQuery, param);

    //    foreach (DataRow row in dt.Rows)
    //    {
    //        string name = Convert.ToString(row["name"]);
    //        lstNames.Add(name);
    //    }


    //    param = new SqlParameter("@search", namePrefix + "%");
    //    sqlQuery = "select distinct name from category where name like @search";

    //    dt = getsearch(sqlQuery, param);

    //    foreach (DataRow row in dt.Rows)
    //    {
    //        string name = Convert.ToString(row["name"]);
    //        lstNames.Add(name);
    //    }

    //    param = new SqlParameter("@search", namePrefix + "%");
    //    sqlQuery = "select distinct name from metal where name like @search";

    //    dt = getsearch(sqlQuery, param);

    //    foreach (DataRow row in dt.Rows)
    //    {
    //        string name = Convert.ToString(row["name"]);
    //        lstNames.Add(name);
    //    }

    //    param = new SqlParameter("@search", "%" + namePrefix + "%");
    //    sqlQuery = "select distinct case when len(productdes) > 80 then substring(productdes,1,80)  else productdes end as name from products where productdes like @search";

    //    dt = getsearch(sqlQuery, param);

    //    foreach (DataRow row in dt.Rows)
    //    {
    //        string name = Convert.ToString(row["name"]);
    //        lstNames.Add(name);
    //    }


    //    return lstNames.OrderByDescending(r => lstNames).Take(10).ToArray<string>();


    //}

    //private static DataTable getsearch(string query, SqlParameter param)
    //{
    //    DataTable dt = new DataTable();
    //    try
    //    {
    //        using (var command = CreateCommandWithOpenConnection())
    //        {
    //            command.CommandText = query.Trim();
    //            command.CommandType = CommandType.Text;
    //            if (param != null)
    //            {
    //                command.Parameters.Add(param);
    //            }
    //            dt.Load(command.ExecuteReader(CommandBehavior.CloseConnection));

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        CloseConnection();
    //        // dbalert(ex.Message + "<br><br>" + SQLQuery);
    //    }
    //    return dt;
    //}

    //private static void CloseConnection()
    //{
    //    if (dbConnection.State == ConnectionState.Open)
    //    {
    //        dbConnection.Close();
    //    }
    //}
    ////private static SqlDataAdapter dbDataAdapter;

    //private static SqlCommand CreateCommandWithOpenConnection()
    //{
    //    SqlCommand dbCommand = null;
    //    try
    //    {
    //        if (dbConnection == null)
    //        {
    //            dbConnection = new SqlConnection(ConnString);
    //        }

    //        //open the connection prior to check the connection state.
    //        if (dbConnection.State == ConnectionState.Closed)
    //        {
    //            dbConnection.Open();

    //        }
    //        //initialize the command object with no argument constructor.
    //        dbCommand = new SqlCommand();
    //        //set the connection to command object.
    //        dbCommand.Connection = dbConnection;
    //    }
    //    catch (Exception ex)
    //    {
    //        // dbalert(ex.Message);
    //    }
    //    return dbCommand;
    //}

    //[WebMethod]
    //public static string NewsLetter(string emailid)
    //{
    //    Entities db = new Entities();
    //    string success = "";
    //    try
    //    {
    //        var mailid = db.NewsLetters.Where(r => r.Email == emailid).FirstOrDefault();
    //        if (mailid == null)
    //        {

    //            int exec = DB.Business.SPs.InsertNewsLetter(0, emailid, "InsertNewsLetter").Execute();

    //            MailMessage messagecust = new MailMessage();

    //            MailAddress maddress = new MailAddress(ConfigurationManager.AppSettings["MailFrom"], "The Palace jewelry By Alankriti");
    //            messagecust.From = maddress;

    //            messagecust.To.Add(emailid);
    //            messagecust.Subject = "Thank You for Subscribing Newsletter on The palace Jewelry";
    //            var body = "";
    //            var filename = HttpContext.Current.Server.MapPath("~/emailer/NewsLetter.html");
    //            using (var objStreamReader = File.OpenText(filename))
    //            {
    //                body = objStreamReader.ReadToEnd();
    //            };

    //            body = body.Replace("##name##", emailid);

    //            messagecust.Body = body;
    //            messagecust.IsBodyHtml = true;
    //            SmtpClient smp = new SmtpClient();
    //            smp.Send(messagecust);
    //            success = "True";

    //        }
    //        else
    //        {
    //            success = "False";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        success = "a";

    //    }

    //    return success;
    //}

    //[WebMethod]
    //public static string ReferToFriend(string fromemail, string toemail, string yourname, string bodymessge, string url, string imgurl, string toemail1, string toemail2, string toemail3, string toemail4, string proname, string subscribe)
    //{
    //    string status = "";
    //    try
    //    {
    //        Entities db = new Entities();
    //        string id = "", desc = "";
    //        if (url.Contains("."))
    //        {
    //            if (url.Contains("-"))
    //            {
    //                id = url.Substring(url.LastIndexOf("-") + 1);
    //                id = id.Substring(0, id.LastIndexOf("."));
    //            }
    //        }

    //        Int32 pid = Convert.ToInt32(id);

    //        var pdetail = db.Products.Where(r => r.productId == pid).FirstOrDefault();
    //        if (pdetail != null)
    //        {
    //            desc = pdetail.productDes;
    //        }

    //        var checkcustomeroffer = db.ShareProducts.Where(r => r.mailFrom.Trim() == fromemail.Trim()).FirstOrDefault();
    //        if (checkcustomeroffer == null)
    //        {
    //            ShareProduct share = new ShareProduct();
    //            share.mailTo = toemail;
    //            share.mailFrom = fromemail;
    //            share.mailBcc = toemail;
    //            share.mailSubject = yourname + " wants to share this thepalacejewelry design with you.";
    //            share.mailMsg = bodymessge;
    //            share.mailUrl = url;
    //            share.createdOn = System.DateTime.Now;
    //            db.AddObject("ShareProducts", share);
    //            db.SaveChanges();
    //        }

    //        MailMessage messagecust = new MailMessage();
    //        messagecust.From = new MailAddress(ConfigurationManager.AppSettings["MailFrom"], "The Palace jewelry By Alankriti");
    //        messagecust.To.Add(toemail);
    //        if (toemail1 != "")
    //        {
    //            messagecust.Bcc.Add(toemail1);

    //        }
    //        if (toemail2 != "")
    //        {
    //            messagecust.Bcc.Add(toemail2);
    //        }
    //        if (toemail3 != "")
    //        {
    //            messagecust.Bcc.Add(toemail3);
    //        }
    //        if (toemail4 != "")
    //        {
    //            messagecust.Bcc.Add(toemail4);
    //        }

    //        messagecust.Subject = yourname + " wants to share this thepalacejewelry design with you.";
    //        var body = "";
    //        var filename = HttpContext.Current.Server.MapPath("~/emailer/ReferFriend.html");
    //        using (var objStreamReader = File.OpenText(filename))
    //        {
    //            body = objStreamReader.ReadToEnd();
    //        };

    //        body = body.Replace("##bodymessge##", bodymessge);
    //        body = body.Replace("##yourname##", yourname);
    //        body = body.Replace("##productname##", proname);
    //        body = body.Replace("##desc##", desc);
    //        body = body.Replace("##imageurl##", "http://www.thepalacejewelry.com/" + imgurl);
    //        body = body.Replace("##pageurl##", url);
    //        messagecust.Body = body;
    //        messagecust.IsBodyHtml = true;
    //        SmtpClient smcust = new SmtpClient();
    //        try
    //        {
    //            smcust.Send(messagecust);
    //            status = "1";
    //        }
    //        catch (Exception ex)
    //        {
    //            status = ex.ToString();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        status = ex.ToString();
    //    }
    //    return status;
    //}

    //[WebMethod]
    //public static string AddRatings(string PId, string Rating, string Username, string Useremail, string Reviews)
    //{
    //    string status = "0";

    //    if (PId != null && PId != "" && Rating != null && Rating != "" && Username != null && Username != "" && Useremail != null && Useremail != "" && Reviews != null && Reviews != "")
    //    {
    //        Entities db = new Entities();
    //        Testimonial user = new Testimonial();
    //        user.Productid = Convert.ToInt32(PId);
    //        if (Rating == "" || Rating == "0")
    //        {
    //            user.Rating = Rating;
    //        }
    //        else
    //        {
    //            user.Rating = Rating;
    //        }
    //        user.Name = Username;
    //        user.Email = Useremail;
    //        user.Description = Reviews;
    //        user.Testimonial1 = 0;
    //        user.Createdate = System.DateTime.Now.Date;
    //        user.status = "0";
    //        user.ImageName = "";
    //        user.TestominialsFrom = "Review";
    //        db.AddObject("Testimonials", user);
    //        db.SaveChanges();
    //        status = "1";
    //    }
    //    return status;
    //}

    //[WebMethod]
    //public static string chkbilling(string emailid)
    //{
    //    string success = "";
    //    if (HttpContext.Current.Session["username"] != null && HttpContext.Current.Session["userid"] != null)
    //    {
    //        success = "2";
    //    }
    //    else
    //    {
    //        Entities vr = new Entities();
    //        var BillingData = vr.UserInfoes.Where(v => v.Email.ToLower().Trim() == emailid.ToLower().Trim() && v.Password.Trim() != "sujeet@34#@$123").FirstOrDefault();
    //        if (BillingData != null)
    //        {
    //            success = "1";
    //        }
    //        else
    //        {
    //            success = "0";
    //        }
    //    }
    //    return success;
    //}

    //[WebMethod]
    //public static string chkuser(string emailid, string passkey)
    //{
    //    string success = "";

    //    Entities vr = new Entities();
    //    var BillingData = vr.UserInfoes.Where(v => v.Email.ToLower().Trim() == emailid.ToLower().Trim() && v.Password.Trim() == passkey.Trim()).FirstOrDefault();
    //    if (BillingData != null)
    //    {
    //        HttpContext.Current.Session["UserId"] = BillingData.UserID;
    //        HttpContext.Current.Session["LoginId"] = BillingData.LoginID;
    //        HttpContext.Current.Session["Password"] = BillingData.Password;
    //        HttpContext.Current.Session["FName"] = BillingData.FirstName;
    //        HttpContext.Current.Session["Name"] = BillingData.FirstName + " " + BillingData.LastName;
    //        HttpContext.Current.Session["Type"] = BillingData.Type;

    //        success = "1";
    //    }
    //    else
    //    {
    //        success = "0";
    //    }


    //    return success;
    //}

    [WebMethod]
    public static string ForgotPassword(string EmailId)
    {
        FabAccessoriesEntities db = new FabAccessoriesEntities();
        string status = "";

        var EmailChk = db.UserInfoes.Where(r => r.Email.ToLower().Trim() == EmailId.ToLower().Trim()).FirstOrDefault();
        if (EmailChk != null)
        {
            SendMail("", EmailChk.Email, "", "There was recentlty a request to recover the password for your account.", MailFormat(EmailChk.FirstName + " " + EmailChk.LastName, EmailId, EmailChk.Password, "ForgetPass"));
            status = "1";
        }
        else
        {
            status = "0";
        }

        return status;
    }

    private static string SendMail(string name, string email, string contact, string subject, string mailbody)
    {
        string res = "";
        MailMessage messagecust = new MailMessage();
        messagecust.From = new MailAddress(ConfigurationManager.AppSettings["MailFrom"], "S Style Factory");
        messagecust.To.Add(email.Trim());
        messagecust.Subject = subject;
        messagecust.Body = mailbody;
        messagecust.IsBodyHtml = true;
        SmtpClient smcust = new SmtpClient();
        try
        {
            smcust.Send(messagecust);
            res = "1";
        }
        catch (Exception ex)
        {
            res = ex.ToString();
        }


        return res;
    }

    public static string MailFormat(string name, string userName, string password, string type)
    {
        var filename = HttpContext.Current.Server.MapPath("~/emailer/forgetpass.html");
        var objStreamReader = File.OpenText(filename);
        var body = objStreamReader.ReadToEnd();
        body = body.Replace("##name##", name);
        body = body.Replace("##UserEmail##", userName);
        body = body.Replace("##Pass##", password);
        return body;
    }

    //[WebMethod]
    //public static string updatepassword(string oldpassword, string newpassword)
    //{

    //    Entities db = new Entities();
    //    string status = "";
    //    string userid = HttpContext.Current.Session["LoginId"].ToString();
    //    var changedata = db.UserInfoes.Where(r => r.LoginID == userid && r.Password == oldpassword).FirstOrDefault();
    //    if (changedata != null)
    //    {
    //        changedata.Password = newpassword;
    //        db.SaveChanges();
    //        status = "1";
    //    }
    //    else
    //    {
    //        status = "0";
    //    }

    //    return status;
    //}

    //[WebMethod]
    //public static string makepayment(string orderno)
    //{
    //    string status = "";
    //    string siteurl = "http://www.silgo.in/";
    //    decimal netamt = 0;
    //    int qty = 0;

    //    try
    //    {

    //        Entities db = new Entities();
    //        if (HttpContext.Current.Session["LoginId"] != null && HttpContext.Current.Session["LoginId"] != null)
    //        {
    //            string clientuserid = HttpContext.Current.Session["LoginId"].ToString();
    //            string ordnum = orderno;
    //            var shippedstatus = (from ordresp in db.OrderPaymentResponses
    //                                 where ordresp.Status != "success"
    //                                 join ordtb in db.OrderTbls on ordresp.OrderId.Trim() equals ordtb.OrderId.Trim()
    //                                 join ord in db.OrderDetails on ordtb.OrderId.Trim() equals ord.OrderId.Trim()
    //                                 join pr in db.Products on ord.ProductCode equals pr.ItemCode
    //                                 join it in db.Items on pr.ItemID equals it.ID
    //                                 select new
    //                                 {

    //                                     ord.Qty,
    //                                     ord.Total,
    //                                     ordtb.Discount,
    //                                     ordtb.TotallPrice,
    //                                     ordtb.shippingamt,
    //                                     pr.productDes,
    //                                     ordtb.OrderId,
    //                                     ord.Size,
    //                                     pr.ItemCode,
    //                                     ordresp.Status,
    //                                     pr.ItemID,
    //                                     it.Name,
    //                                     ordtb.UserId,
    //                                     pr.Barcode,
    //                                     pr.productId,
    //                                     pr.CategoryID,
    //                                     pr.productPrice,
    //                                     pr.grossWeight,
    //                                     pr.netWeight,
    //                                 }).Where(r => r.OrderId == ordnum && r.UserId == clientuserid).Distinct().ToList();


    //            if (shippedstatus.Count != 0)
    //            {
    //                //HttpContext.Current.Session["Orderid"] = orderno.Trim();

    //                foreach (var item in shippedstatus)
    //                {
    //                    if (HttpContext.Current.Request.Cookies["jcart"] == null)
    //                    {
    //                        Guid gui = Guid.NewGuid();
    //                        var cookie = new HttpCookie("jcart", gui.ToString())
    //                        {
    //                            Expires = DateTime.Now.AddDays(5)
    //                        };
    //                        HttpContext.Current.Response.Cookies.Add(cookie);
    //                    }
    //                    var cartp = HttpContext.Current.Request.Cookies["jcart"].Value;
    //                    DataTable table = HttpContext.Current.Cache[cartp] as DataTable;
    //                    if (table == null)
    //                    {
    //                        table = CreateDataTable();
    //                    }
    //                    DataRow[] drExists = table.Select("productId='" + item.productId + "'");
    //                    if (drExists.Count() <= 0)
    //                    {
    //                        DataRow dr = table.NewRow();
    //                        dr["ProductId"] = item.productId;
    //                        dr["Qty"] = item.Qty;
    //                        dr["size"] = item.Size;
    //                        dr["AddDate"] = DateTime.Now;
    //                        table.Rows.Add(dr);
    //                    }
    //                    HttpContext.Current.Cache.Insert(cartp, table, null, DateTime.Now.AddDays(5), System.Web.Caching.Cache.NoSlidingExpiration);

    //                    status = "1"; // 1 for Items Found
    //                    //}
    //                    //else
    //                    //{
    //                    //    var cat = db.OrderDetails.Where(r => r.ProductId == item.Id && r.OrderNo == orderno).FirstOrDefault();
    //                    //    db.OrderDetails.Remove(cat);
    //                    //    db.SaveChanges();
    //                    //}

    //                }
    //                //int ds = DB.Business.SPs.ApShippingInsertProcedure(orderno, "", "", clientuserid, "", 0, decimal.Parse("0.00"), decimal.Parse("0.00"), decimal.Parse("0.00"), 0, 0, 0, "", "", "", "", 0, decimal.Parse("0.00"), decimal.Parse("0.00"), "", "deleteorders").Execute();

    //                status = "1";
    //            }
    //            else
    //            {
    //                HttpContext.Current.Session["Orderid"] = "";
    //                status = "2";//2 for Nor  Order Found
    //            }

    //        }
    //        else
    //        {
    //            HttpContext.Current.Session["Orderid"] = "";
    //            status = "0";//0 for User session out
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        HelperMethod.LogError(ex);
    //    }

    //    return status;
    //}


    //[WebMethod]
    //public static string sendcouponmail(string email)
    //{
    //    string response = string.Empty;

    //    Entities db = new Entities();
    //    var checkemailexists = db.HomeCoupons.Where(r => r.email.ToLower().Trim() == email.ToLower().Trim()).FirstOrDefault();
    //    if (checkemailexists == null)
    //    {
    //        try
    //        {
    //            HomeCoupon hc = new HomeCoupon();
    //            hc.email = email;
    //            hc.isuser = false;
    //            sendmail(email);
    //            db.HomeCoupons.AddObject(hc);
    //            db.SaveChanges();
    //            response = "success";
    //        }
    //        catch (Exception ex)
    //        {
    //            response = "issue";
    //        }
    //    }
    //    else
    //    {
    //        response = "already";
    //    }
    //    return response;

    //}

    //public static void sendmail(string email)
    //{

    //    try
    //    {
    //        string Couponcode = "TPJ10";
    //        MailMessage messageadmin = new MailMessage();
    //        MailAddress maddress1 = new MailAddress(ConfigurationManager.AppSettings["MailFrom"], "The Palace Jewellery");
    //        messageadmin.From = maddress1;
    //        messageadmin.To.Add(email);
    //        // messageadmin.Bcc.Add(maddress1);
    //        //messageadmin.Bcc.Add("web@jewelsinfosystems.com");
    //        messageadmin.Subject = "Save 10%  on first order";
    //        var filename = HttpContext.Current.Server.MapPath("~/emailer/FirstCouponCode1.html");
    //        var objStreamReader = File.OpenText(filename);
    //        var body = objStreamReader.ReadToEnd();
    //        body = body.Replace("##name##", email);
    //        body = body.Replace("##coupon##", Couponcode);
    //        messageadmin.Body = body;
    //        messageadmin.IsBodyHtml = true;
    //        SmtpClient smtp = new SmtpClient();
    //        smtp.Send(messageadmin);
    //    }
    //    catch (Exception ex1)
    //    {

    //    }

    //}
}