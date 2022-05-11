using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
//using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ImgResize
/// </summary>
public class ImgResize
{
    public static double[] Big = { 1100, 1000 };
    public static double medium = 1.8;
    public static double small = 5;
    public ImgResize()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    /// <summary>
    /// Image Resize any height and width
    /// </summary>
    /// <param name="sourcePath">That image you want to save</param>
    /// <param name="targetPath">Where, you want to save image</param>
    /// <param name="h">new height of image</param>
    /// <param name="w">new width of image</param>
    public static void img(Stream sourcePath, string targetPath, int h, int w)
    {
        using (var image = System.Drawing.Image.FromStream(sourcePath))
        {
            var imgResize = new System.Drawing.Bitmap(w, h);
            var imgGraph = System.Drawing.Graphics.FromImage(imgResize);
            imgGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            imgGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            imgGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            var imageRectangle = new System.Drawing.Rectangle(0, 0, w, h);
            imgGraph.DrawImage(image, imageRectangle);
            imgResize.Save(targetPath, image.RawFormat);
        }
    }
    
    public static void img(Stream sourcePath, string targetPath, double Scalfactor)
    {
        using (var image = System.Drawing.Image.FromStream(sourcePath))
        {
            var newHeight = (int)(image.Height * Scalfactor);
            var newWidth = (int)(image.Width * Scalfactor);
            var imgResize = new System.Drawing.Bitmap(newWidth, newHeight);

            var imgGraph = System.Drawing.Graphics.FromImage(imgResize);
            imgGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            imgGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            imgGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            var imageRectangle = new System.Drawing.Rectangle(0, 0, newWidth, newHeight);
            imgGraph.DrawImage(image, imageRectangle);
            imgResize.Save(targetPath, image.RawFormat);
        }
    }
    
    public static void img(Stream sourcePath, string[] targetPath, double[] Scalfactor)
    {
        using (var image = System.Drawing.Image.FromStream(sourcePath))
        {
            for (int i = 0; i < targetPath.Length; i++)
            {
                string path = targetPath[i];
                double scal = Scalfactor[i];
                var newHeight = (int)(image.Height * scal);
                var newWidth = (int)(image.Width * scal);
                var imgResize = new System.Drawing.Bitmap(newWidth, newHeight);

                var imgGraph = System.Drawing.Graphics.FromImage(imgResize);
                imgGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                imgGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                imgGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                var imageRectangle = new System.Drawing.Rectangle(0, 0, newWidth, newHeight);
                imgGraph.DrawImage(image, imageRectangle);
                imgResize.Save(path, image.RawFormat);
            }
        }
    }
    
    public static void img(Stream sourcePath, string[] targetPath)
    {
        using (var image = System.Drawing.Image.FromStream(sourcePath))
        {
            double[] scalling = CalScalling(System.Drawing.Image.FromStream(sourcePath));
            for (int i = 0; i < targetPath.Length; i++)
            {
                string path = targetPath[i];
                double scal = scalling[i];
                var newHeight = (int)(image.Height * scal);
                var newWidth = (int)(image.Width * scal);
                var imgResize = new System.Drawing.Bitmap(newWidth, newHeight);
                var imgGraph = System.Drawing.Graphics.FromImage(imgResize);
                imgGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                imgGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                imgGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                var imageRectangle = new System.Drawing.Rectangle(0, 0, newWidth, newHeight);
                imgGraph.DrawImage(image, imageRectangle);
                imgResize.Save(path, image.RawFormat);
            }
        }
    }

    public static void img(Image sourcePath, string[] targetPath, double[] Scalfactor)
    {
        using (var image = sourcePath)
        {
            for (int i = 0; i < targetPath.Length; i++)
            {
                string path = targetPath[i];
                double scal = Scalfactor[i];
                var newHeight = (int)(image.Height * scal);
                var newWidth = (int)(image.Width * scal);
                var imgResize = new System.Drawing.Bitmap(newWidth, newHeight);

                var imgGraph = System.Drawing.Graphics.FromImage(imgResize);
                imgGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                imgGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                imgGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                var imageRectangle = new System.Drawing.Rectangle(0, 0, newWidth, newHeight);
                imgGraph.DrawImage(image, imageRectangle);
                imgResize.Save(path, image.RawFormat);
            }
        }
    }

    public static void img(Image sourcePath, string[] targetPath)
    {
        using (var image = sourcePath)
        {

            double[] scalling = CalScalling(sourcePath);
            for (int i = 0; i < targetPath.Length; i++)
            {
                string path = targetPath[i];
                double scal = scalling[i];
                var newHeight = (int)(image.Height * scal);
                var newWidth = (int)(image.Width * scal);
                var imgResize = new System.Drawing.Bitmap(newWidth, newHeight);
                var imgGraph = System.Drawing.Graphics.FromImage(imgResize);
                imgGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                imgGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                imgGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                var imageRectangle = new System.Drawing.Rectangle(0, 0, newWidth, newHeight);
                imgGraph.DrawImage(image, imageRectangle);
                imgResize.Save(path, image.RawFormat);
            }
        }
    }

    static double[] CalScalling(Image image)
    {
        double scalMedium = 0.00e;
        double scalBig = 0.00e;
        double height = image.Height;
        double width = image.Width;

        double scalHeight = 0.00e;
        if (Convert.ToInt32(Big[1]) < height) { scalHeight = Convert.ToInt32(Big[1]) / height; }
        else { scalHeight = height / height; }

        double scalWidth = 0.00e;
        if (Convert.ToInt32(Big[0]) < width) { scalWidth = Convert.ToInt32(Big[0]) / width; }
        else { scalWidth = width / width; }

        if (scalHeight < scalWidth) { scalBig = System.Convert.ToDouble(scalHeight.ToString("0.00")); }
        else { scalBig = System.Convert.ToDouble(scalWidth.ToString("0.00")); }

        scalMedium = scalBig / medium;
        double scalSmall = scalBig / small;
        double[] scalling = { scalBig, scalMedium, scalSmall };
        return scalling;
    }

    public static void img(Image sourcePath, string[] targetPath, string[] size)
    {
        using (var image = sourcePath)
        {

            double[] scalling = CalScalling(sourcePath, size);
            for (int i = 0; i < targetPath.Length; i++)
            {
                string path = targetPath[i];
                double scal = scalling[i];
                if (scal > 0)
                {
                    var newHeight = (int)(image.Height * scal);
                    var newWidth = (int)(image.Width * scal);
                    var imgResize = new System.Drawing.Bitmap(newWidth, newHeight);
                    var imgGraph = System.Drawing.Graphics.FromImage(imgResize);
                    imgGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    imgGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    imgGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    var imageRectangle = new System.Drawing.Rectangle(0, 0, newWidth, newHeight);
                    imgGraph.DrawImage(image, imageRectangle);
                    imgResize.Save(path, image.RawFormat);
                }
            }
        }
    }

    static double[] CalScalling(Image image, string[] size)
    {
        double scalSmall = 0.00e;
        double scalMedium = 0.00e;
        double scalBig = 0.00e;
        double height = image.Height;
        double width = image.Width;
        Int16 h = 0;
        Int16 w = 0;
        int i = 0;
        foreach (string ss in size)
        {
            string[] si = ss.Split('x');
            w = Convert.ToInt16(si[0].ToString());
            h = Convert.ToInt16(si[1].ToString());
            double scalHeight = 0.00e;
            if (Convert.ToInt32(h)<height) { scalHeight = Convert.ToInt32(h) / height; }
            else { scalHeight = 1.00; }

            double scalWidth = 0.00e;
            if (Convert.ToInt32(w)< width) { scalWidth = Convert.ToInt32(w) / width; }
            else { scalWidth = 1.00; }
            if (i == 0)
            {
                if (scalHeight < scalWidth) { scalBig = System.Convert.ToDouble(scalHeight.ToString("0.00")); }
                else { scalBig = System.Convert.ToDouble(scalWidth.ToString("0.00")); }
            }
            if (i == 1)
            {
                if (scalHeight < scalWidth) { scalMedium = System.Convert.ToDouble(scalHeight.ToString("0.00")); }
                else { scalMedium = System.Convert.ToDouble(scalWidth.ToString("0.00")); }
            }
            if (i == 2)
            {
                if (scalHeight < scalWidth) { scalSmall = System.Convert.ToDouble(scalHeight.ToString("0.00")); }
                else { scalSmall = System.Convert.ToDouble(scalWidth.ToString("0.00")); }
            }
            i++;
        }





        double[] scalling = { scalBig, scalMedium, scalSmall };
        return scalling;
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
}