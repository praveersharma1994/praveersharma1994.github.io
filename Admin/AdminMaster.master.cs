using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class admin_AdminMaster : System.Web.UI.MasterPage
{
    FabAccessoriesEntities db = new FabAccessoriesEntities();

    protected void Page_Init(object sender, EventArgs e)
    {
        checkHttps();
    }

    void checkHttps()
    {



        string url = Request.Url.ToString();

        if (url.Contains("http://"))
        {
            if (!url.Contains("localhost"))
            {
                if (!url.Contains("www."))
                {
                   // url = url.Replace(".aspx", ".html");
                    Response.Redirect(url.Replace("http://", "https://www."));
                }
                else
                {
                   // url = url.Replace(".aspx", ".html");
                    Response.Redirect(url.Replace("http://", "https://"));
                }

            }
        }
        else if (url.Contains("https://sstyle"))
        {

            url = url.Replace("https://", "https://www.");
            //url = url.Replace(".aspx", ".html");
            Response.Redirect(url);
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["adminid"] != null)
        {
            lblusername.Text = Session["adname"].ToString();
        }
        else if (Session["UserLogin"] != null)
        {
            lblusername.Text = Session["adname"].ToString();
            aChangePwd.Visible = false;
        }
        else if (Session["OpLogin"] != null)
        {
            lblusername.Text = Session["adname"].ToString();
            aChangePwd.Visible = false;
        }
        else
        {
            Response.Redirect("../adminlogin.aspx");
        }
        Page.Header.DataBind();
    }


    protected void lnkLogout_Click(object sender, EventArgs e)
    {

        Session.Abandon();
        Session.Clear();
        Response.Redirect("../adminlogin.aspx");
    }
}
