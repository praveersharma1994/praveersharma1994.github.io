using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
public partial class admin_Banner : System.Web.UI.Page
{
    FabAccessoriesEntities db = new FabAccessoriesEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            bindGrid();
    }

    private void bindGrid()
    {
        var cat = db.Banners.ToList();
        if (txtSearch.Text.Trim().Length > 0)
        {
            cat = cat.Where(r => r.BannerTitle.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
        }
      
        grdList.DataSource = cat;
        grdList.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (fluUpload.HasFile)
        {
            string filename = Common.url(txtCategory.Text, 25) + Path.GetExtension(fluUpload.FileName);
            string pathToSave = HttpContext.Current.Server.MapPath("~/upload/banner/") + filename;
            Stream strm = fluUpload.PostedFile.InputStream;
            var targetFile = pathToSave;
            
            GenerateThumbnails(0.4, strm, targetFile, 1024, 800);
            
            hddImg.Value = filename;
        }
        if (btnSave.Text.ToLower() == "submit")
        {
            Banner cat = new Banner();
            cat.BannerImg = hddImg.Value;
            cat.BannerTitle = txtCategory.Text;
            cat.CreateDate = System.DateTime.Now.Date;
            cat.DisplayOrder = 0;
            cat.BannerOf = "app";
            cat.BannerUrl = "";
            db.Banners.Add(cat);
            db.SaveChanges();
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Banner has been saved successfully');window.location.href='banner.aspx';", true);
        }
        else
        {
            Int16 id = Convert.ToInt16(hddId.Value);
            var cat = db.Banners.Where(r => r.BannerId == id).FirstOrDefault();
            cat.BannerImg = hddImg.Value;
            cat.BannerTitle = txtCategory.Text;
            cat.CreateDate = System.DateTime.Now.Date;
            cat.DisplayOrder = 0;
            cat.BannerOf = "app";
            cat.BannerUrl = "";
            db.SaveChanges();
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Banner has been updated successfully');window.location.href='banner.aspx';", true);
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
            var cat = db.Banners.Where(r => r.BannerId == id).FirstOrDefault();
            db.Banners.Remove(cat);
            db.SaveChanges();
            bindGrid();
        }
    }

    private void getDetail(Int32 id)
    {
        var cat = db.Banners.Where(r => r.BannerId == id).FirstOrDefault();
        if (cat != null)
        {
            txtCategory.Text = cat.BannerTitle;
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
        txtSearch.Text = txtCategory.Text = hddId.Value = hddImg.Value = "";
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