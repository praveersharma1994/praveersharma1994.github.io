using System;
using System.Security.Cryptography;
using System.Collections.Generic;

public partial class GenerateChecksum : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string orderId = Request.QueryString["orderId"];
        string orderAmount = Request.QueryString["orderAmount"];
        string customerEmail = Request.QueryString["customerEmail"];
        string customerPhone = Request.QueryString["customerPhone"];
        CashFreeToken cft = new CashFreeToken();
        string t1 = cft.Main(orderId, orderAmount, customerEmail, customerPhone);
        Response.Write(t1);
    }


    public class CashFreeToken
    {
        private string CreateToken(string message, string secret)
        {
            secret = secret ?? "";
            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }

        public string Main(string orderId, string orderAmount, string customerEmail, string customerPhone)
        {
            String appId = "3226f3726891cb1b66f568746223";
            String secret = "396eac0e84e0397432c1885afa61ea3f083ae9f1";

            //---------  Testing --------------
            //String appId = "1322123034d868333dfcf7012231";
            //String secret = "2da31727ad90f5754a47e6c04a94638b125a7d89";

            String data = "appId=" + appId + "&orderId=" + orderId + "&orderAmount=" + orderAmount + "&customerEmail=" + customerEmail + "&customerPhone=" + customerPhone + "&orderCurrency=" + "INR";

            CashFreeToken n = new CashFreeToken();
            String signature = n.CreateToken(data, secret);
            return (signature);
        }
    }
}
