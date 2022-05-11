using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Drawing.Imaging;

public partial class test3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btncreate_Click(object sender, EventArgs e)
    {


        string mainImgpath = Server.MapPath("~/images/banner1.jpg");

        string mainImg = "banner1.jpg";

        int ss = mainImg.LastIndexOf('.');

        string ext = mainImg.Substring(ss);

        string savepath = Server.MapPath("~/images/sajid1" + ext);


        File.Copy(mainImgpath, savepath);


        


    }
}