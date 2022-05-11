using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
public partial class webservice_userlogin : System.Web.UI.Page
{
    string loginid = "";
    string pwd = "";
    string deviceid = "";
    private string _PlayerId = string.Empty;
    FabAccessoriesEntities db = new FabAccessoriesEntities();
    LoginResponse response = new LoginResponse();

    protected void Page_Load(object sender, EventArgs e)
    {
        string userdet = "";
        try
        {
            if (Request["loginid"] != null && Request["pwd"] != null)
            {
                loginid = Request["loginid"].ToString();
                pwd = Request["pwd"].ToString();
                if (Request["deviceid"] != null) { deviceid = Request["deviceid"].ToString().Trim(); }
                if (Request["playerid"] != null) { _PlayerId = Request["playerid"].ToString().Trim(); }
                if (string.IsNullOrEmpty(loginid) || string.IsNullOrEmpty(pwd))
                {
                    response.Response = "0";
                    response.Message = "loginid or password cannot be blank!";
                }
                else
                {
                    var user = db.UserInfoes.FirstOrDefault(x => x.Username == loginid);
                    if (user != null)
                    {
                        if (user.IsActive == true)
                        {
                            if (user.Password.Equals(pwd))
                            {
                                response.Response = "1";
                                response.UserId = user.Id.ToString();
                                response.UserName = user.Username.ToString();
                                response.ContactNo = user.ContactNo.ToString();
                                response.Email = user.Email.ToString();
                                response.City = user.City.ToString();
                                response.country = user.Country.ToString();
                                response.DeviceId = user.DeviceId == null ? "" : user.DeviceId.ToString();
                                response.Message = "success";

                                if (_PlayerId != null && _PlayerId != "")
                                {
                                    user.PlayerId = _PlayerId;
                                }

                                userdet = JsonConvert.SerializeObject(user);
                                if (user.DeviceId != null && user.DeviceId != "")
                                {
                                    if (!user.DeviceId.Contains(deviceid))
                                    {
                                        user.DeviceId = user.DeviceId + "," + deviceid;
                                    }
                                }
                                
                                db.SaveChanges();
                            }
                            else
                            {
                                response.Response = "0";
                                response.Message = "Invalid password!";
                            }
                        }
                        else
                        {
                            response.Response = "0";
                            response.Message = "user not active!";
                        }
                    }
                    else
                    {
                        response.Response = "0";
                        response.Message = "user does not exist!";
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

        Response.Write("[{\"Response\":[" + msg + "]" + ",\"userlist\":[" + userdet + "]}]");
    }

    public class LoginResponse
    {
        public string Response { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string ContactNo { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string country { get; set; }
        public string Message { get; set; }
        public string DeviceId { get; set; }
    }
}