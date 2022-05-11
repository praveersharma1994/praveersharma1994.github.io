using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class Clientmaster : System.Web.UI.MasterPage
{

    FabAccessoriesEntities db = new FabAccessoriesEntities();

    //protected void Page_Init(object sender, EventArgs e)
    //{
    //    checkHttps();
    //}

    void checkHttps()
    {



        string url = Request.Url.AbsoluteUri;

        if (url.Contains("http://"))
        {
            if (!url.Contains("localhost"))
            {
                if (!url.Contains("www."))
                {
                    url = url.Replace(".aspx", ".html");
                    Response.Redirect(url.Replace("http://", "https://www."));
                }
                else
                {
                    url = url.Replace(".aspx", ".html");
                    Response.Redirect(url.Replace("http://", "https://"));
                }

            }
        }
        else if (url.Contains("https://sstylefactory"))
        {

            url = url.Replace("https://", "https://www.");
            url = url.Replace(".aspx", ".html");
            Response.Redirect(url);
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.DataBind();

        if (Session["UserId"] == null || Session["LoginId"] == null)
        {
            lnk_login.InnerText = "LOGIN / SIGN UP";

            usersignup.Visible = true;
            userlogin.Visible = true;
            logoutbtn.Visible = false;

            limyaccount.Visible = false;
            limyorder.Visible = false;

            //lnkmyaccount.Visible = false;
        }
        else
        {
            lnk_login.InnerText = Session["FName"].ToString();
            //lnkmyaccount.Visible = true;

            usersignup.Visible = false;
            userlogin.Visible = false;
            logoutbtn.Visible = true;

            limyaccount.Visible = true;
            limyorder.Visible = true;
        }

        if (HttpContext.Current.Request.Cookies["fabcart"] != null)
        {

            var cartp = HttpContext.Current.Request.Cookies["fabcart"].Value;
            DataTable table = HttpContext.Current.Cache[cartp] as DataTable;
            if (table != null)
            {
                HttpContext.Current.Cache.Insert(cartp, table, null, DateTime.Now.AddDays(5), System.Web.Caching.Cache.NoSlidingExpiration);
                lblcartitems.Text = table.Rows.Count.ToString();
            }
            else
            {
                lblcartitems.Text = "0";
            }
        }
        else
        {
            lblcartitems.Text = "0";
        }


        if (HttpContext.Current.Request.Cookies["fabwish"] != null)
        {

            var cartp = HttpContext.Current.Request.Cookies["fabwish"].Value;
            DataTable table = HttpContext.Current.Cache[cartp] as DataTable;
            if (table != null)
            {
                HttpContext.Current.Cache.Insert(cartp, table, null, DateTime.Now.AddDays(5), System.Web.Caching.Cache.NoSlidingExpiration);
                lblwishlistitems.Text = table.Rows.Count.ToString();
            }
            else
            {
                lblwishlistitems.Text = "0";
            }
        }
        else
        {
            lblwishlistitems.Text = "0";
        }


        if (!IsPostBack)
        {
            SeoData();
            bindmenu();
        }
    }


    void SeoData()
    {

        string pageurl = Request.Url.ToString();
        var content = db.Seos.Where(r => r.Url.ToLower() == pageurl.Replace(".aspx", ".html")).OrderByDescending(r => r.Id).FirstOrDefault();
        if (content != null)
        {
            HtmlHead hd = new HtmlHead();

            HtmlTitle htitle = new HtmlTitle();
            HtmlMeta meta1 = new HtmlMeta();
            HtmlMeta meta2 = new HtmlMeta();

            HtmlLink metalink = new HtmlLink();

            htitle.Text = content.Title;

            Head1.Controls.Add(htitle);

            meta1.Attributes.Add("name", "keyword");
            meta1.Attributes.Add("content", content.keyword);

            Head1.Controls.Add(meta1);

            meta2.Attributes.Add("name", "description");
            meta2.Attributes.Add("content", content.Description);

            Head1.Controls.Add(meta2);


            metalink.Attributes.Add("rel", "canonical");
            if (pageurl.Contains("index.aspx"))
            {
                metalink.Href = pageurl.Replace("index.aspx", "");
            }
            else
            {
                metalink.Href = pageurl.Replace(".aspx", ".html");
            }
            Head1.Controls.Add(metalink);

        }
        else
        {
            HtmlLink metalink = new HtmlLink();
            metalink.Attributes.Add("rel", "canonical");
            if (pageurl.Contains("index.aspx"))
            {
                metalink.Href = pageurl.Replace("index.aspx", "");
            }
            else
            {
                metalink.Href = pageurl.Replace(".aspx", ".html");
            }
            Head1.Controls.Add(metalink);
        }

    }


    public void bindmenu()
    {
        DataTable dt = DataAccess.GetDataTable("select top 10 * from dbo.CollectionMaster order by DisplayOrder,CollectionName", CommandType.Text);

        if (dt.Rows.Count > 0)
        {
            rptmenu.DataSource = dt;
            rptmenu.DataBind();
        }


    }
    protected void btnregister_Click(object sender, EventArgs e)
    {
        if (txtfirstname.Text.Trim() != "" && txtlastname.Text.Trim() != "" && txtemail.Text.Trim() != "" && txtmobileno.Text.Trim() != "" && txtaddress.Text.Trim() != "" && txtcity.Text.Trim() != "" && drpsstate.SelectedValue != "0" && txtpassword.Text.Trim() != "" && txtconfirmpass.Text.Trim() != "")
        {
            if (txtpassword.Text.Trim().ToLower() == txtconfirmpass.Text.Trim().ToLower())
            {
                var chkuser = db.UserInfoes.Where(r => r.Email.ToLower() == txtemail.Text.Trim().ToLower()).FirstOrDefault();

                if (chkuser == null)
                {
                    UserInfo uinfo = new UserInfo();

                    uinfo.FirstName = txtfirstname.Text.Trim();
                    uinfo.LastName = txtlastname.Text.Trim();
                    uinfo.Email = txtemail.Text.Trim();
                    uinfo.ContactNo = txtmobileno.Text.Trim();
                    uinfo.Address = txtaddress.Text.Trim();
                    uinfo.City = txtcity.Text.Trim();
                    uinfo.State = drpsstate.SelectedValue;
                    uinfo.Country = drpcountry.SelectedItem.Text;
                    uinfo.Password = txtconfirmpass.Text.Trim();

                    db.UserInfoes.Add(uinfo);
                    db.SaveChanges();


                    sendmail sm = new sendmail();

                    string body = sm.MailFormat(uinfo.FirstName, uinfo.Email, uinfo.Password, "", "Registration");
                    string adminbody = sm.MailFormat(uinfo.FirstName, uinfo.Email, uinfo.Password, Encrypt(uinfo.Id.ToString().Trim()), "adminregistraion");

                    string adminrs = sm.SendMail(uinfo.Email, uinfo.Email, uinfo.ContactNo, "New Registration on sstylefactory ", adminbody, "Admin");
                    string rs = sm.SendMail(uinfo.Email, uinfo.Email, uinfo.ContactNo, "Thank you For Registration on sstylefactory", body, "Client");

                    txtfirstname.Text = txtlastname.Text = txtemail.Text = txtmobileno.Text = txtaddress.Text = txtcity.Text = txtconfirmpass.Text = txtpassword.Text = "";

                    drpsstate.SelectedValue = "0";

                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Registration has been successfully done on our website. Thank You.')", true);
                    Response.Redirect( Page.ResolveUrl("index.html"));

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Email id already registered, please register with other email id.')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Both password did not match')", true);
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
    protected void btnlogin_Click(object sender, EventArgs e)
    {
        if (txtloginemail.Text.Trim() != "" && txtloginpass.Text.Trim() != "")
        {
            var chkuser = db.UserInfoes.Where(r => r.Email.ToLower() == txtloginemail.Text.Trim() && r.Password == txtloginpass.Text.Trim()).FirstOrDefault();
            if (chkuser != null)
            {
                // create user cookie data

                Session["UserId"] = chkuser.Id.ToString();
                Session["LoginId"] = chkuser.Email.ToString();
                Session["FName"] = chkuser.FirstName.ToString();
                Session["Name"] = chkuser.FirstName.ToString() + " " + chkuser.LastName;

                Response.Redirect(Request.Url.ToString());

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Please enter valid email id & password')", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Please fill all required informations')", true);
        }
    }
    protected void lnklogout_Click(object sender, EventArgs e)
    {
        Session["UserId"] = null;
        Session["LoginId"] = null;
        Session.Abandon();
        Session.Clear();
        Response.Redirect(Page.ResolveUrl("index.html"));
    }
    protected void lnksearch_Click(object sender, EventArgs e)
    {
        if (txtsearch.Text.Trim() != "")
        {
            Response.Redirect(Page.ResolveUrl("search/" + Common.url(txtsearch.Text.Trim()) + ".html"));
            //Response.Redirect(Page.ResolveUrl("search/" + txtsearch.Text.Replace("&", "-emp-").Replace('?', ' ').Trim() + ".html"));
        }
    }
    protected void btnsubscribe_Click(object sender, EventArgs e)
    {
        if (txtnewsletteremail.Text.Trim() != "")
        {

            var chknews = db.NewsLetters.Where(r => r.EmailId == txtnewsletteremail.Text.Trim()).FirstOrDefault();

            if (chknews == null)
            {
                var NewsChk = new NewsLetter();
                NewsChk.EmailId = txtnewsletteremail.Text.Trim();
                NewsChk.CreateDate = DateTime.Now;
                NewsChk.Status = 1;
                db.NewsLetters.Add(NewsChk);
                db.SaveChanges();

                txtnewsletteremail.Text = "";

                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Your Email has been added successfully.')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Your Email is already registered with us.')", true);
            }

            
        }
    }
    protected void btnforgotpassword_Click(object sender, EventArgs e)
    {
        if (txtforgotmail.Text.Trim() != "")
        {

        }
    }
}
