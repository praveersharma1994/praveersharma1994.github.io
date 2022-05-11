using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net.Mail;
using System.Threading;
/// <summary>
/// Summary description for sendmail
/// </summary>
public class sendmail
{
    public sendmail()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string MailFormat(string name, string email, string contactno, string message, string mailto)
    {
        var body = "";
        if (mailto == "Admin")
        {
            var filename = HttpContext.Current.Server.MapPath(@"~\emailer\Contactus.html");
            var objStreamReader = File.OpenText(filename);
            body = objStreamReader.ReadToEnd();
            body = body.Replace("##name##", name.First().ToString().ToUpper() + (name).Substring(1));
            body = body.Replace("##UserEmail##", email);
            body = body.Replace("##Phone##", contactno);
            body = body.Replace("##Message##", message);
        }

        else if (mailto == "Forgotpassword")
        {
            var filename = HttpContext.Current.Server.MapPath(@"~\emailer\ForgetPass.html");
            var objStreamReader = File.OpenText(filename);
            body = objStreamReader.ReadToEnd();
            body = body.Replace("##name##", name.First().ToString().ToUpper() + (name).Substring(1));
            body = body.Replace("##UserEmail##", email);
            body = body.Replace("##Pass##", contactno);
        }

        else if (mailto == "Registration")
        {
            var filename = HttpContext.Current.Server.MapPath(@"~\emailer\registration.html");
            var objStreamReader = File.OpenText(filename);
            body = objStreamReader.ReadToEnd();
            body = body.Replace("##name##", name.First().ToString().ToUpper() + (name).Substring(1));
            body = body.Replace("##UserEmail##", email);
            body = body.Replace("##Pass##", contactno);
        }

        else if (mailto == "adminregistraion")
        {
            FabAccessoriesEntities db = new FabAccessoriesEntities();
            var data = db.UserInfoes.Where(r => r.Email == email).FirstOrDefault();
            var filename = HttpContext.Current.Server.MapPath(@"~\emailer\adminregistration.html");
            var objStreamReader = File.OpenText(filename);
            body = objStreamReader.ReadToEnd();
            body = body.Replace("##name##", name.First().ToString().ToUpper() + (name).Substring(1));
            body = body.Replace("##email##", email);
            body = body.Replace("##Username##", data.Username);
            body = body.Replace("##companyname##", data.Company == null ? "" : data.Company);
            body = body.Replace("##contactno##", data.ContactNo == null ? "" :data.ContactNo );
            body = body.Replace("##contactperson##", data.ContactPerson == null ? "" :data.ContactPerson );
            body = body.Replace("##address##", data.Address == null ? "" : data.Address);
            body = body.Replace("##city##", data.City == null ? "" :data.City);
            body = body.Replace("##country##", data.Country == null ? "" : data.Country);
        }

        else if (mailto == "Newelsletter")
        {
            var filename = HttpContext.Current.Server.MapPath(@"~\emailer\NewsLetter.html");
            var objStreamReader = File.OpenText(filename);
            body = objStreamReader.ReadToEnd();
            body = body.Replace("##name##", name.First().ToString().ToUpper() + (name).Substring(1));
        }

        return body;
    }

    public string SendMail(string name, string email, string contact, string subject, string mailbody, string mailto)
    {
        string res = "";

        MailMessage messagecust = new MailMessage();
        messagecust.From = new MailAddress("apps@fabfashionaccessories.com", "Fab Fashion Accessories");

        if (mailto == "Admin")
        {
            messagecust.To.Add(new MailAddress("apps@fabfashionaccessories.com"));
            messagecust.ReplyToList.Add(new MailAddress(email, name));
        }
        else if (mailto == "Client")
        {
            messagecust.To.Add(new MailAddress(email));
            //messagecust.Bcc.Add("web@jewelsinfosystems.com");
            messagecust.ReplyToList.Add(new MailAddress("apps@fabfashionaccessories.com", "Fab Fashion Accessories"));
        }
        else if (mailto == "all")
        {
            messagecust.To.Add(new MailAddress(email));
            messagecust.Bcc.Add(new MailAddress("apps@fabfashionaccessories.com"));
           // messagecust.Bcc.Add("web@jewelsinfosystems.com");
            messagecust.ReplyToList.Add(new MailAddress("apps@fabfashionaccessories.com", "Fab Fashion Accessories"));
        }

        messagecust.Subject = subject;
        messagecust.Body = mailbody;
        messagecust.IsBodyHtml = true;

        SmtpClient smcust = new SmtpClient();

        //try
        //{
        //    smcust.Send(messagecust);
        //    res = "Sucess";
        //}
        //catch (Exception ex)
        //{
        //    res = ex.ToString();
        //}
        try
        {
            Thread emails = new Thread(delegate()
            {
                smcust.Send(messagecust);
               
            });
            emails.IsBackground = true;
            emails.Start();
        }
        catch (Exception ex)
        {
            //res = ex.ToString();
            Common.LogError(ex);
        } 
        res = "Sucess";
        return res;
    }
}