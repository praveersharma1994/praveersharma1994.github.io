using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Summary description for CommanClass
/// </summary>
public class CommanClass
{
    public CommanClass()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static string GetRound(string amt)
    {
        string round = "";
        round = Math.Round(Convert.ToDecimal((Convert.ToInt16(Convert.ToDecimal(amt).ToString("0.00").Substring(
                                                                                          Convert.ToDecimal(amt).ToString("0.00").Length - 1)) == 0 ?
                                                                                          Math.Round(Convert.ToDecimal(Convert.ToDecimal(amt).ToString("0.0")), 1, MidpointRounding.AwayFromZero) :
                                                                                          Convert.ToDecimal(Convert.ToDecimal(amt.Substring(0, amt.IndexOf(".") + 2))) + 0.10m
                                                                                          )), 1, MidpointRounding.AwayFromZero) + "0";
        return round;
    }
    public static decimal GetRound(decimal amt)
    {
        string round = "0.00";
        round = Math.Round(Convert.ToDecimal((Convert.ToInt16(Convert.ToDecimal(amt.ToString()).ToString("0.00").Substring(
                                                                                          Convert.ToDecimal(amt).ToString("0.00").Length - 1)) == 0 ?
                                                                                          Math.Round(Convert.ToDecimal(Convert.ToDecimal(amt).ToString("0.0")), 1, MidpointRounding.AwayFromZero) :
                                                                                          Convert.ToDecimal(Convert.ToDecimal(amt.ToString().Substring(0, amt.ToString().IndexOf(".") + 2))) + 0.10m
                                                                                          )), 1, MidpointRounding.AwayFromZero) + "0";
        if (round.ToString() == "")
        {
            round = "0.00";
        }
        return Convert.ToDecimal(round);
    }
    public static DateTime GetServerDate()
    {
        DateTime date;

        date = System.DateTime.Now;
        //date = Convert.ToDateTime(DB.Business.SPs.GetCurrentDate("").ExecuteScalar());
        return date;
    }

    public static string urlWriting(string u)
    {
        u = Regex.Replace(u, @"[^0-9a-zA-Z-]+", "");
        return u.Trim().Replace(" ", "-");
    }
    public static string url(string u, int length)
    {
        u = u.ToLower().Trim();
        //u = u.Replace(",", "");
        //u = u.Replace('"', '&');
        //u = u.Replace("&", "");
        //u = u.Replace("'", "");
        //u = u.Replace("/", "");
        //u = u.Replace("%", "");
        //u = u.Replace(".", "");
        //u = u.Replace("_", "");
        //u = u.Replace("?", "");
        u = u.Replace(" ", "-");
        u = urlWriting(u);
        if (u.Length > length)
        {
            u = u.Substring(0, length - 1);
        }
        return u;
    }
    public static string url(string u)
    {
        u = u.ToLower().Trim();
        //u = u.Replace(",", "");
        //u = u.Replace('"', '&');
        //u = u.Replace("&", "");
        //u = u.Replace("'", "");
        //u = u.Replace("/", "");
        //u = u.Replace("%", "");
        //u = u.Replace(".", "");
        //u = u.Replace("_", "");
        //u = u.Replace("?", "");
        u = u.Replace(" ", "-");
        u = urlWriting(u);

        return u;
    }

    public static SortedDictionary<string, string> SortNameValueCollection(NameValueCollection nvc)
    {
        SortedDictionary<string, string> sortedDict = new SortedDictionary<string, string>();
        foreach (String key in nvc.AllKeys)
            sortedDict.Add(key, nvc[key]);
        return sortedDict;
    }
    public static string GetAppConfig(string Key)
    {
        return GetAppConfig(Key, "");
    }

    public static string GetAppConfig(string Key, string defaultValue)
    {
        string appConfigValue = "";
        string AppValue = System.Configuration.ConfigurationManager.AppSettings[Key];
        if (string.IsNullOrEmpty(AppValue))
        {
            if (!string.IsNullOrEmpty(defaultValue))
                appConfigValue = defaultValue;
            else
                appConfigValue = "";
        }
        else
        {
            appConfigValue = AppValue;
        }
        return appConfigValue;
    }

    //public static Crypto.Algorithm GetConfigAlgorithm(string key)
    //{
    //    return GetConfigAlgorithm(key, "");
    //}

    /// <summary>
    /// Supported Algorithm: SHA1, SHA256, SHA384, MD5 and SHA512
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    //public static Crypto.Algorithm GetConfigAlgorithm(string key, string defaultValue)
    //{
    //    string ConfigValue = GetAppConfig(key, defaultValue);
    //    Crypto.Algorithm algorithm = new Crypto.Algorithm();
    //    if (!string.IsNullOrEmpty(ConfigValue))
    //    {
    //        switch (ConfigValue.ToLower())
    //        {
    //            case "sha1":
    //                algorithm = Crypto.Algorithm.SHA1;
    //                break;
    //            case "sha256":
    //                algorithm = Crypto.Algorithm.SHA256;
    //                break;
    //            case "sha384":
    //                algorithm = Crypto.Algorithm.SHA384;
    //                break;
    //            case "sha512":
    //                algorithm = Crypto.Algorithm.SHA512;
    //                break;
    //            case "md5":
    //                algorithm = Crypto.Algorithm.MD5;
    //                break;
    //            default:
    //                throw new ArgumentException("Invalid algorithm configured in configuration", "Algorithm");
    //        }
    //    }
    //    else
    //        throw new ArgumentException("Invalid algorithm configured in configuration", "Algorithm");
    //    return algorithm;
    //}
}