using System;
using System.Collections.Generic;
using paytm;
using PaytmContant;
public partial class GenerateChecksum : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string MID = "JOVIVE05161633204151", ORDER_ID = "", CUST_ID = "", CHANNEL_ID = "WAP", INDUSTRY_TYPE_ID = "Retail109", WEBSITE = "JOVIWAP", TXN_AMOUNT = "", CALLBACK_URL = "";
            if (Request["ORDER_ID"] != null) { ORDER_ID = Request["ORDER_ID"].ToString(); }
            if (Request["CUST_ID"] != null) { CUST_ID = Request["CUST_ID"].ToString(); }
            if (Request["TXN_AMOUNT"] != null) { TXN_AMOUNT = Request["TXN_AMOUNT"].ToString(); }
            if (Request["CALLBACK_URL"] != null) { CALLBACK_URL = Request["CALLBACK_URL"].ToString(); }
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("MID", MID); //Provided by Paytm
            parameters.Add("ORDER_ID", ORDER_ID); //unique OrderId for every request
            parameters.Add("CUST_ID", CUST_ID); // unique customer identifier 
            parameters.Add("CHANNEL_ID", CHANNEL_ID); //Provided by Paytm
            parameters.Add("INDUSTRY_TYPE_ID", INDUSTRY_TYPE_ID); //Provided by Paytm
            parameters.Add("WEBSITE", WEBSITE); //Provided by Paytm
            parameters.Add("TXN_AMOUNT", TXN_AMOUNT); // transaction amount
            parameters.Add("CALLBACK_URL", CALLBACK_URL); //Provided by Paytm
            //parameters.Add("EMAIL", "abc@gmail.com"); // customer email id
            //parameters.Add("MOBILE_NO", "9999999999"); // customer 10 digit mobile no.
            string paytmChecksum = "";

            foreach (string key in parameters.Keys)
            {
                if (parameters[key].ToUpper().Contains("REFUND") || parameters[key].ToUpper().Contains("|"))
                {
                    parameters[key] = "";
                }
            }
            string merchantKey = "bd#lE#gNQVtP1#OX";
            paytmChecksum = CheckSum.generateCheckSum(merchantKey, parameters);
            Response.Write("{\"ORDER_ID\":\"" + parameters["ORDER_ID"] + "\",\"CHECKSUMHASH\":\"" + paytmChecksum + "\",\"paytm_STATUS\":\"1\"}");

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

    }
}
