using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

public partial class testimgother : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        DataTable dt = DataAccess.GetDataTable("select * from otherimage where Imagename!='' and Imagename is not null", CommandType.Text);

        if (dt.Rows.Count > 0)
        {

            for (int k = 0; k < dt.Rows.Count; k++)
            {

                if (File.Exists(Server.MapPath("~/upload/products/temp2/othersmall/" + dt.Rows[k]["imagename"].ToString())))
                {

                    string pathm = Server.MapPath("~/upload/products/othersmall/" + dt.Rows[k]["imagename"].ToString());
                    string small = Server.MapPath("~/upload/products/temp2/othersmall/" + dt.Rows[k]["imagename"].ToString());


                    // string pathl = Server.MapPath("~/upload/products/large/" + dt.Rows[k]["image"].ToString());
                    // string large = Server.MapPath("~/upload/products/temp2/large/" + dt.Rows[k]["image"].ToString());



                    
                    string watermarkImgSmall = Server.MapPath("~/images/fab-wtrMark2.png");

                    // CreateWaterMark1(large, watermarkImg, pathl);
                    //File.Delete(targetPath);
                    // GenerateMedium(0.4, strm, large);
                    CreateWaterMark1(small, watermarkImgSmall, pathm);
                }


                


            }

        }
    }

    void CreateWaterMark1(string mainImg, string watermark, string savePath)
    {
        System.Drawing.Image image = System.Drawing.Image.FromFile(@mainImg);//This is the background image 
        System.Drawing.Image logo = System.Drawing.Image.FromFile(@watermark); //This is your watermark 
        Graphics g = System.Drawing.Graphics.FromImage(image); //Create graphics object of the background image //So that you can draw your logo on it
        Bitmap TransparentLogo = new Bitmap(logo.Width, logo.Height); //Create a blank bitmap object //to which we //draw our transparent logo
        Graphics TGraphics = Graphics.FromImage(TransparentLogo);//Create a graphics object so that //we can draw //on the blank bitmap image object
        ColorMatrix ColorMatrix = new ColorMatrix(); //An image is represenred as a 5X4 matrix(i.e 4 //columns and 5 //rows) 
        ColorMatrix.Matrix33 = 1F;//the 3rd element of the 4th row represents the transparency 
        ImageAttributes ImgAttributes = new ImageAttributes();//an ImageAttributes object is used to set all //the alpha //values.This is done by initializing a color matrix and setting the alpha scaling value in the matrix.The address of //the color matrix is passed to the SetColorMatrix method of the //ImageAttributes object, and the //ImageAttributes object is passed to the DrawImage method of the Graphics object.
        ImgAttributes.SetColorMatrix(ColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap); TGraphics.DrawImage(logo, new Rectangle(0, 0, TransparentLogo.Width, TransparentLogo.Height), 0, 0, TransparentLogo.Width, TransparentLogo.Height, GraphicsUnit.Pixel, ImgAttributes);
        TGraphics.Dispose();
        g.DrawImage(TransparentLogo, 0, 1);

        // File.Delete(mainImg);
        image.Save(savePath, ImageFormat.Jpeg);
    }
    protected void btn2_Click(object sender, EventArgs e)
    {
        DataTable dt = DataAccess.GetDataTable("select * from otherimage where Imagename!='' and Imagename is not null", CommandType.Text);

        if (dt.Rows.Count > 0)
        {

            for (int k = 0; k < dt.Rows.Count; k++)
            {

               
                if (File.Exists(Server.MapPath("~/upload/products/temp2/otherlarge/" + dt.Rows[k]["imagename"].ToString())))
                {

                    //string pathm = Server.MapPath("~/upload/products/small/" + dt.Rows[k]["image"].ToString());
                    //string small = Server.MapPath("~/upload/products/temp2/small/" + dt.Rows[k]["image"].ToString());


                    string pathl = Server.MapPath("~/upload/products/otherlarge/" + dt.Rows[k]["imagename"].ToString());
                    string large = Server.MapPath("~/upload/products/temp2/otherlarge/" + dt.Rows[k]["imagename"].ToString());




                    string watermarkImg = Server.MapPath("~/images/fab-wtrMark1.png");


                    // CreateWaterMark1(large, watermarkImg, pathl);
                    //File.Delete(targetPath);
                    // GenerateMedium(0.4, strm, large);
                    CreateWaterMark1(large, watermarkImg, pathl);
                }


            }

        }
    }
}