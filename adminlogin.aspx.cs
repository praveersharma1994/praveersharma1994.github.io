using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class adminlogin : System.Web.UI.Page
{

    protected void Page_Init(object sender, EventArgs e)
    {
        checkHttps();
    }

    void checkHttps()
    {



        string url = Request.Url.AbsoluteUri;

        if (url.Contains("http://"))
        {
            if (!url.Contains("localhost"))
            {
                if (!url.Contains("www."))
                {
                    //url = url.Replace(".aspx", ".html");
                    Response.Redirect(url.Replace("http://", "https://www."));
                }
                else
                {
                    //url = url.Replace(".aspx", ".html");
                    Response.Redirect(url.Replace("http://", "https://"));
                }

            }
        }
        else if (url.Contains("https://sstylefactory"))
        {

            url = url.Replace("https://", "https://www.");
            //url = url.Replace(".aspx", ".html");
            Response.Redirect(url);
        }

    }


    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.DataBind();
        if (!Page.IsPostBack)
        {
            Session.Remove("adminid");
            Session.Remove("UserLogin");
            Session.Remove("aid");
            Session.Remove("Uid");
            txtLoginId.Focus();
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        FabAccessoriesEntities db = new FabAccessoriesEntities();

        string IPAdd = string.Empty;
       
        var dt1 = db.AdminLogins.Where(r => r.LoginId == txtLoginId.Text.Trim() && r.Password == txtPassword.Text.Trim()).FirstOrDefault();
        if (dt1 !=null)
        {
            Session["UserType"] = dt1.usertype.ToString();

           

            if (dt1.usertype.ToString() == "User")
            {
               
                Session["id"] = dt1.Id;
                Session["UserLogin"] = "User";
                Session["Uid"] = dt1.Id;
                Session["adname"] = "Welcome " + dt1.username.ToString() + " !";
                Session.Timeout = 600;
            }
            else if (dt1.usertype.ToString() == "Operator")
            {
                Session["id"] = dt1.Id;
                Session["OpLogin"] = "Operator";
                Session["Opid"] = dt1.Id;
                Session["adname"] = "Welcome " + dt1.username.ToString() + " !";
                Session.Timeout = 600;
            }
            else
            {
                Session["adminloginid"] = txtLoginId.Text.Trim();
                Session["id"] = dt1.Id;
                Session["adminid"] = "admin";
                Session["aid"] = dt1.Id;
                Session["adname"] = "Welcome " + dt1.username.ToString() + " !";
                Session.Timeout = 600;
            }
            Response.Redirect("~/Admin/AdminHome.aspx");
        }
        else
        {
            lblstatus.Text = "Login Id or Password Incorrect";
            txtPassword.Text = "";
            txtPassword.Text = "";
            lblstatus.Visible = true;
        }
        
    }
}