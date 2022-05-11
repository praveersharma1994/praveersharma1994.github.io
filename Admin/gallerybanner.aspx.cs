using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
public partial class admin_gallerybanner : System.Web.UI.Page
{
    FabAccessoriesEntities db = new FabAccessoriesEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            bindGrid();
    }

    private void bindGrid()
    {
        var cat = db.HomeGalleryBanners.ToList();
        if (txtSearch.Text.Trim().Length > 0)
        {
            cat = cat.Where(r => r.BannerUrl.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
        }

        grdList.DataSource = cat;
        grdList.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (fluUpload.HasFile)
        {
            string filename = System.DateTime.Now.Ticks.ToString().Substring(7, 5) + Path.GetExtension(fluUpload.FileName);
            string pathToSave = HttpContext.Current.Server.MapPath("~/upload/banner/") + filename;
            Stream strm = fluUpload.PostedFile.InputStream;
            var targetFile = pathToSave;

            GenerateThumbnails(0.4, strm, targetFile, 250, 360);

            hddImg.Value = filename;
        }
        if (btnSave.Text.ToLower() == "submit")
        {
            HomeGalleryBanner cat = new HomeGalleryBanner();
            cat.BannerImg = hddImg.Value;
            cat.BannerTitle = "";
            cat.CreateDate = System.DateTime.Now.Date;
            cat.DisplayOrder = 0;
            cat.BannerUrl = txturl.Text.Trim() == "" ? "#" : txturl.Text.Trim();
            db.HomeGalleryBanners.Add(cat);
            db.SaveChanges();
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Home gallery banner has been saved successfully');window.location.href='gallerybanner.aspx';", true);
        }
        else
        {
            Int16 id = Convert.ToInt16(hddId.Value);
            var cat = db.HomeGalleryBanners.Where(r => r.BannerId == id).FirstOrDefault();
            cat.BannerImg = hddImg.Value;
            cat.BannerTitle = "";
            cat.CreateDate = System.DateTime.Now.Date;
            cat.DisplayOrder = 0;
            cat.BannerUrl = txturl.Text.Trim() == "" ? "#" : txturl.Text.Trim();
            db.SaveChanges();
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Home gallery banner has been updated successfully');window.location.href='gallerybanner.aspx';", true);
        }
        bindGrid();
        clear();
    }

    protected void grdList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Int32 id = Convert.ToInt32(e.CommandArgument.ToString());
        if (e.CommandName.ToLower() == "edt")
        {
            getDetail(id);
        }
        if (e.CommandName.ToLower() == "del")
        {
            var cat = db.HomeGalleryBanners.Where(r => r.BannerId == id).FirstOrDefault();
            db.HomeGalleryBanners.Remove(cat);
            db.SaveChanges();
            bindGrid();
        }
    }

    private void getDetail(Int32 id)
    {
        var cat = db.HomeGalleryBanners.Where(r => r.BannerId == id).FirstOrDefault();
        if (cat != null)
        {
            txturl.Text = cat.BannerUrl;
            //txtCategory.Text = cat.BannerTitle;
            //txtDisplayOrder.Text = cat.DisplayOrder.ToString();
            //txtUrl.Text = cat.BannerUrl;
            hddId.Value = cat.BannerId.ToString();
            hddImg.Value = cat.BannerImg;
            btnSave.Text = "Update";
        }
    }

    protected void btnSeach_Click(object sender, EventArgs e)
    {
        bindGrid();
    }

    protected void grdList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdList.PageIndex = e.NewPageIndex;
        bindGrid();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }

    protected void drpPagging_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindGrid();
    }

    void clear()
    {
        txtSearch.Text = txturl.Text = hddId.Value = hddImg.Value = "";
        btnSave.Text = "Submit";
    }

    private void GenerateThumbnails(double scaleFactor, Stream sourcePath, string targetPath, int w, int h)
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

    protected void drpbanner_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindGrid();
    }
}