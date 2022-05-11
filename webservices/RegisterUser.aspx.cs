using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Mail;
using System.Configuration;
using System.Security.Cryptography;


public partial class webserviceweb_RegisterUser : System.Web.UI.Page
{
    private string _CompName = "";
    private string _ContactPerson = "";
    private string _mobile = "";
    private string _loginid = "";
    private string _Address = "";
    private string _city = "";
    private string _country = "";
    private string _username = "";
    private string _pwd = "";
    private string _DeviceId = "";
    private string _PlayerId = "";
    private string action = "";

    FabAccessoriesEntities db = new FabAccessoriesEntities();
    UserRegisterResponse response = new UserRegisterResponse();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["action"] != null) { action = Request["action"].ToString().Trim(); }
        if (action == "register")
        {
            register();
        }
        else if (action == "updateuser")
        {
            updateuser();
        }

    }

    private void register()
    {
        try
        {
            if (Request["compname"] != null && Request["contactperson"] != null && Request["mobile"] != null && Request["loginid"] != null && Request["address"] != null && Request["city"] != null && Request["country"] != null && Request["username"] != null && Request["pwd"] != null)
            {
                _CompName = Request["CompName"].Trim();
                _ContactPerson = Request["ContactPerson"].Trim();
                _mobile = Request["mobile"].Trim();
                _loginid = Request["loginid"].Trim();
                _Address = Request["address"].Trim();
                _city = Request["city"].Trim();
                _country = Request["country"].Trim();
                _username = Request["username"].Trim();
                _pwd = Request["pwd"].Trim();
                if (Request["deviceid"] != null) { _DeviceId = Request["deviceid"].ToString().Trim(); }
                if (Request["playerid"] != null) { _PlayerId = Request["playerid"].Trim();}
                if (string.IsNullOrEmpty(_loginid) || string.IsNullOrEmpty(_pwd) || string.IsNullOrEmpty(_username) || string.IsNullOrEmpty(_city) || string.IsNullOrEmpty(_mobile))
                {
                    response.Response = "0";
                    response.Message = "fields cannot be blank!";
                }
                else
                {
                    var user = db.UserInfoes.FirstOrDefault(x => x.Username == _username || x.Email == _loginid);
                    if (user != null)
                    {
                        response.Response = "0";
                        response.Message = "UserName/Email is alreday exists!";
                    }
                    else
                    {
                        UserInfo newuser = new UserInfo();
                        newuser.Address = _Address;
                        newuser.City = _city;
                        newuser.Company = _CompName;
                        newuser.ContactNo = _mobile;
                        newuser.ContactPerson = _ContactPerson;
                        newuser.Country = _country;
                        newuser.CreateDate = DateTime.Now;
                        newuser.Designation = "";
                        newuser.Email = _loginid;
                        newuser.FirstName = "";
                        newuser.Gender = "";
                        newuser.IsActive = true;
                        newuser.LastName = "";

                        newuser.Password = _pwd;
                        newuser.DeviceId = _DeviceId;
                        newuser.PlayerId = _PlayerId.Trim();
                        newuser.Registerfrom = "App";
                        newuser.State = "";
                        newuser.Username = _username;
                        newuser.Zip = "";
                        db.UserInfoes.Add(newuser);
                        db.SaveChanges();

                        if (!db.NewsLetters.Any(r => r.EmailId == newuser.Email))
                        {
                            var NewsChk = new NewsLetter();
                            NewsChk.EmailId = _loginid;
                            NewsChk.CreateDate = DateTime.Now;
                            NewsChk.Status = 1;
                            db.NewsLetters.Add(NewsChk);
                            db.SaveChanges();
                        }

                        sendmail sm = new sendmail();
                        //string body = sm.MailFormat(newuser.Username, newuser.Username, newuser.Password, "", "Registration");
                        //string rs = sm.SendMail(newuser.Username, newuser.Email, newuser.ContactNo, "Registartion", body, "all");
                        string body = sm.MailFormat(newuser.Username, newuser.Username, newuser.Password, "", "Registration");
                        string adminbody = sm.MailFormat(newuser.Email, newuser.Username, newuser.Password, Encrypt(newuser.Id.ToString().Trim()), "adminregistraion");
                        string adminrs = sm.SendMail(newuser.Username, newuser.Email, newuser.ContactNo, "Registration", adminbody, "Admin");
                        string rs = sm.SendMail(newuser.Username, newuser.Email, newuser.ContactNo, "Registration", body, "Client");
                        //string rs = "Sucess";
                        //string adminrs = "Sucess";
                        if (rs == "Sucess" && adminrs == "Sucess")
                        {
                            response.Response = "1";
                            response.UserId = newuser.Id.ToString();
                            response.UserName = newuser.Username.ToString();
                            response.Message = "success";
                            newuser.Mailstatus = 1;

                        }
                        else
                        {
                            response.Response = "0";
                            response.UserId = newuser.Id.ToString();
                            response.UserName = newuser.Username.ToString();
                            response.Message = "Mail is not send";
                        }
                    }
                }
            }
            else
            {
                response.Response = "0";
                response.Message = "Parameter not supplied!";
            }
        }
        catch (Exception ex)
        {
            response.Response = "0";
            response.Message = "Internal error!";
        }
        var msg = new JavaScriptSerializer().Serialize(response);

        Response.Write(msg);
    }

    private void updateuser()
    {
        if (Request["compname"] != null) { _CompName = Request["CompName"].ToString().Trim(); }
        if (Request["ContactPerson"] != null) { _ContactPerson = Request["ContactPerson"].ToString().Trim(); }
        if (Request["mobile"] != null) { _mobile = Request["mobile"].ToString().Trim(); }
        if (Request["loginid"] != null) { _loginid = Request["loginid"].ToString().Trim(); }
        if (Request["address"] != null) { _Address = Request["address"].ToString().Trim(); }
        if (Request["city"] != null) { _city = Request["city"].ToString().Trim(); }
        if (Request["country"] != null) { _country = Request["country"].ToString().Trim(); }
        if (Request["username"] != null) { _username = Request["username"].ToString().Trim(); }
        if (Request["pwd"] != null) { _pwd = Request["pwd"].ToString().Trim(); }
        if (string.IsNullOrEmpty(_username))
        {
            response.Response = "0";
            response.Message = "fields cannot be blank!";
        }
        else
        {
            var newuser = db.UserInfoes.FirstOrDefault(x => x.Username == _username && x.Email == _loginid);
            if (newuser != null)
            {
                newuser.Address = _Address;
                newuser.City = _city;
                newuser.Company = _CompName;
                newuser.ContactNo = _mobile;
                newuser.ContactPerson = _ContactPerson;
                newuser.Country = _country;
                // newuser.Email = _loginid;
                newuser.Password = _pwd;
                db.SaveChanges();

                response.Response = "1";
                response.UserId = newuser.Id.ToString();
                response.UserName = newuser.Username.Trim();
                response.Message = "success";
            }
            else
            {
                response.Response = "0";
                response.Message = "user is not exist!";
            }
        }
        var msg = new JavaScriptSerializer().Serialize(response);
        Response.Write(msg);
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

    public class UserRegisterResponse
    {
        public string Response { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
    }
}