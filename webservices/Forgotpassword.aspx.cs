using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class webserviceweb_Forgotpassword : System.Web.UI.Page
{
    string emailid;
    string response;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request["emailid"] != null)
        {
            emailid = Request["emailid"].ToString().Trim();
            forgorpass();
        }
        else
        {

        }
    }


    private void forgorpass()
    {
        FabAccessoriesEntities db = new FabAccessoriesEntities();
        var userdetail = db.UserInfoes.FirstOrDefault();
        if (emailid.Contains("@"))
        {
            userdetail = db.UserInfoes.Where(r => r.Email == emailid).FirstOrDefault();
        }
        else
        {
            userdetail = db.UserInfoes.Where(r => r.Username == emailid).FirstOrDefault();
        }
        if (userdetail != null)
        {
            if (userdetail.IsActive)
            {
                sendmail sm = new sendmail();
                string body = sm.MailFormat(userdetail.Username, userdetail.Username, userdetail.Password, "", "Forgotpassword");
                string responseq = sm.SendMail(userdetail.Username, userdetail.Email, userdetail.ContactNo, "Forgot Password", body, "Client");
                if (responseq.ToLower() == "sucess")
                {
                    response += "{\"response\":\"1\"}";
                }
                else
                {
                    response += "{\"response\":\"0\"}";
                }
            }
            else
            {
                response += "{\"response\":\"2\"}";
            }
        }
        else
        {
            response += "{\"response\":\"0\"}";
        }

        Response.Write(response);
    }
}