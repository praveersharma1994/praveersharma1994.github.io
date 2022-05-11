using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Data.SqlClient;
using System.Data;

using System.Configuration;
using System.Net.Mail;
using System.IO;

/// <summary>
/// Summary description for Common
/// </summary>
public class Common
{
   
    public static string ImgUrl = "http://www.jovifashion.com/upload/product/";
    public static string catalog = "http://www.jovifashion.com/upload/category/";
    public Common()
    {

    }
    public static string getnetwt(string gwt, string totdiawt, string totcswt)
    {
        string nwt = "0.000";

        decimal netwt = 0.000m;
        decimal Grosswt = 0.000m;

        totdiawt = (totdiawt=="" ? "0.00" : totdiawt);
        totcswt = (totcswt=="" ? "0.00" : totcswt);
        gwt = (gwt == "" ? "0.000" : gwt);


        Grosswt = Convert.ToDecimal(gwt);

        netwt = (Convert.ToDecimal(totdiawt) + Convert.ToDecimal(totcswt)) / Convert.ToDecimal("5.00");



        nwt = (Grosswt - netwt).ToString("0.000");



        return nwt;
    }


    public static string urlWriting(string u)
    {
        u = Regex.Replace(u, @"[^0-9a-zA-Z-]+", "");
        return u.Trim().Replace(" ", "-");
    }

    public static string url(string u, int length)
    {
        u = u.ToLower().Trim();
        u = u.Replace(" ", "-");
        u = u.Replace(".", "-");
        u = urlWriting(u);
        if (u.Length > length)
        {
            int index = u.IndexOf("-", length);
            if (index <= 0)
            {
                index = u.Length;
            }
            u = u.Substring(0, index);
        }
        u = u.Replace("--", "-");
        return u;
    }

    public static string url(string u)
    {
        u = u.ToLower().Trim();

        u = u.Replace(" ", "-");
        u = urlWriting(u);
        u = u.Replace("--", "-");
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

    public static string GetLatestVersion(string file)
    {
        var path = "";
        try
        {
            var physicalPath = HttpContext.Current.Server.MapPath(file);
            var version = "?v=" +
           new System.IO.FileInfo(physicalPath).LastWriteTime
           .ToString("yyyyMMddhhmmss");
            path = file + version;
        }
        catch (Exception ex)
        {

            //HelperMethod.LogError(ex);
        }
        return path;

    }

    public static string usertype
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["adminusertype"] != null)
                return (string)HttpContext.Current.Session["adminusertype"];
            else
                return "";
        }
        set
        {
            HttpContext.Current.Session["adminusertype"] = value;
        }
    }

    public static string userid
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["adminuserid"] != null)
                return (string)HttpContext.Current.Session["adminuserid"];
            else
                return "";
        }
        set
        {
            HttpContext.Current.Session["adminuserid"] = value;
        }
    }

    public static string username
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["adminusername"] != null)
                return (string)HttpContext.Current.Session["adminusername"];
            else
                return "";
        }
        set
        {
            HttpContext.Current.Session["adminusername"] = value;
        }
    }

    public static string loginid
    {
        get
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["adminloginid"] != null)
                return (string)HttpContext.Current.Session["adminloginid"];
            else
                return "";
        }
        set
        {
            HttpContext.Current.Session["adminloginid"] = value;
        }
    }

    public static void LogError(Exception ex)
    {
        string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
        message += Environment.NewLine;
        message += "-----------------------------------------------------------";
        message += Environment.NewLine;
        message += string.Format("Message: {0}", ex.Message);
        message += Environment.NewLine;
        message += string.Format("StackTrace: {0}", ex.StackTrace);
        message += Environment.NewLine;
        message += string.Format("Source: {0}", ex.Source);
        message += Environment.NewLine;
        message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
        message += Environment.NewLine;
        message += "-----------------------------------------------------------";
        message += Environment.NewLine;
        string path = HttpContext.Current.Server.MapPath("~/Error/logfile.txt");
        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine(message);
            writer.Close();
        }

    }

    public static void LogString(string e)
    {
        string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
        message += Environment.NewLine;
        message += "-----------------------------------------------------------";
        message += Environment.NewLine;
        message += string.Format("Message: {0}", e);
        message += Environment.NewLine;
        message += string.Format("StackTrace: {0}", e);
        message += Environment.NewLine;
        message += string.Format("Source: {0}", e);
        message += Environment.NewLine;
        message += string.Format("TargetSite: {0}", e);
        message += Environment.NewLine;
        message += "-----------------------------------------------------------";
        message += Environment.NewLine;
        string path = HttpContext.Current.Server.MapPath("~/Error/logfile.txt");
        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine(message);
            writer.Close();
        }

    }

    public static void img(double scaleFactor, System.IO.Stream sourcePath, string targetPath, int w, int h)
    {
        using (var image = System.Drawing.Image.FromStream(sourcePath))
        {
            var newWidth = (int)(w);
            var newHeight = (int)(h);
            var thumbnailImg = new System.Drawing.Bitmap(newWidth, newHeight);
            var thumbGraph = System.Drawing.Graphics.FromImage(thumbnailImg);
            thumbGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            thumbGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            thumbGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            var imageRectangle = new System.Drawing.Rectangle(0, 0, newWidth, newHeight);
            thumbGraph.DrawImage(image, imageRectangle);
            thumbnailImg.Save(targetPath, image.RawFormat);
        }
    }

    private static Random random = new Random();

    public static string GetRandomAlphaNumeric()
    {
        var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
        return new string(chars.Select(c => chars[random.Next(chars.Length)]).Take(4).ToArray());
    }

}