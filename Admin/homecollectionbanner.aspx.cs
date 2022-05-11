using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Services;
using Newtonsoft.Json;

public partial class Admin_homecollectionbanner : System.Web.UI.Page
{
    FabAccessoriesEntities db = new FabAccessoriesEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            bindGrid();
            hddId.Value = "";
            binddropdown();
            RequiredFieldValidator1.Enabled = true;
        }
    }
    private void binddropdown()
    {
        try
        {
            var categorydata = db.CategoryMasters.Select(r => new { r.CategoryName, r.CategoryId }).ToList();
            if (categorydata.Count > 0)
            {
                drpcategory.DataSource = categorydata;
                drpcategory.DataTextField = "CategoryName";
                drpcategory.DataValueField = "CategoryId";
                drpcategory.DataBind();

            }
        }
        catch (Exception ex)
        {

        }
    }

    private void bindGrid()
    {
        var cat = DataAccess.GetDataTable("select * from CollectionBanner cb left join categorymaster cm on cm.categoryid=convert(int,cb.url)", CommandType.Text);//db.CollectionBanners.ToList();
        int pagesize = Convert.ToInt16(drpPagging.SelectedValue);
        grdList.PageSize = pagesize;
        grdList.DataSource = cat;
        grdList.DataBind();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //if (txtUrl.Text.Trim().Length == 0){txtUrl.Text = "#";}
        if (fluUpload.HasFile)
        {
            var dirpath = HttpContext.Current.Server.MapPath("~/upload/mobile/SpecialBanner");
            if (!Directory.Exists(dirpath))
            {
                Directory.CreateDirectory(dirpath);
            }
            string filename = Guid.NewGuid() + Path.GetExtension(fluUpload.FileName);
            string pathToSave = dirpath + "/" + filename;
            Stream strm = fluUpload.PostedFile.InputStream;
            var targetFile = pathToSave;

            //if (ddl_position.SelectedValue == "1" || ddl_position.SelectedValue == "4")
            //{
            //    GenerateThumbnails(0.4, strm, targetFile, 700, 300);
            //}
            //else if (ddl_position.SelectedValue == "2" || ddl_position.SelectedValue == "3")
            //{
            //    GenerateThumbnails(0.4, strm, targetFile, 700, 300);
            //}
            //else if (ddl_position.SelectedValue == "5" || ddl_position.SelectedValue == "8")
            //{
            //    GenerateThumbnails(0.4, strm, targetFile, 700, 300);
            //}
            //else if (ddl_position.SelectedValue == "6" || ddl_position.SelectedValue == "7")
            //{
            //}

            GenerateThumbnails(0.4, strm, targetFile, 700, 300);
            hddImg.Value = filename;
        }

        if (btnSave.Text.ToLower() != "submit")
        {
            int id = Convert.ToInt32(hddId.Value);
            var checkexists = db.CollectionBanners.Where(r => r.Url == drpcategory.SelectedItem.Value && r.Id != id).FirstOrDefault();
            if (checkexists == null)
            {

                var cat = db.CollectionBanners.FirstOrDefault(r => r.Id == id);
                cat.ImgName = hddImg.Value;
                cat.Title = drpcategory.SelectedItem.Text;
                cat.Position = "";
                cat.Url = drpcategory.SelectedItem.Value;
                cat.AdDate = System.DateTime.Now;

                db.SaveChanges();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Home Collection Banner has been updated successfully')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Collection  has been alredy in use')", true);
            }
        }
        else
        {
            var checkexists = db.CollectionBanners.Where(r => r.Url == drpcategory.SelectedItem.Value).FirstOrDefault();
            if (checkexists == null)
            {
                CollectionBanner hb = new CollectionBanner();
                hb.ImgName = hddImg.Value;
                hb.Title = drpcategory.SelectedItem.Text;
                hb.Position = "";
                hb.Url = drpcategory.SelectedItem.Value;
                hb.AdDate = System.DateTime.Now;
                db.CollectionBanners.Add(hb);
                db.SaveChanges();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Home Collection Banner has been added successfully')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Collection  has been alredy in use')", true);
            }

        }
        bindGrid();
        clear();
    }

    private string GetYouTubeID(string youTubeUrl)
    {
        //RegEx to Find YouTube ID
        Match regexMatch = Regex.Match(youTubeUrl, "^[^v]+v=(.{11}).*", RegexOptions.IgnoreCase);
        if (regexMatch.Success)
        {
            return regexMatch.Groups[1].Value;
        }
        return youTubeUrl;
    }

    void clear()
    {
        hddId.Value = hddImg.Value = "";
        drpcategory.SelectedIndex = 0;
        //ddl_position.SelectedIndex = 0; txtUrl.Text = 
        btnSave.Text = "Submit";
        RequiredFieldValidator1.Enabled = true;
    }

    protected void grdList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdList.PageIndex = e.NewPageIndex;
        bindGrid();
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
            var cat = db.CollectionBanners.Where(r => r.Id == id).FirstOrDefault();
            db.CollectionBanners.Remove(cat);
            db.SaveChanges();
            bindGrid();
        }
    }

    private void getDetail(Int32 id)
    {
        var cat = db.CollectionBanners.FirstOrDefault(r => r.Id == id);
        if (cat != null)
        {
            txtCategory.Text = cat.Title;
            hddId.Value = cat.Id.ToString();
            hddImg.Value = cat.ImgName;
            if (cat.Url != "")
            {
                drpcategory.SelectedValue = (Convert.ToString(cat.Url) == "" ? "0" : cat.Url);
            }
            //txtUrl.Text = cat.Url;
            //ddl_position.SelectedValue = cat.Position;

            btnSave.Text = "Update";
            btnSave.Enabled = true;
            RequiredFieldValidator1.Enabled = false;
        }
    }

    protected void btnSeach_Click(object sender, EventArgs e)
    {
        bindGrid();
    }

    protected void drpPagging_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindGrid();
    }

    private void GenerateThumbnails(double scaleFactor, Stream sourcePath, string targetPath, int w, int h)
    {
        using (var image = System.Drawing.Image.FromStream(sourcePath))
        {
            //var newWidth = (int)(image.Width * scaleFactor);
            //var newHeight = (int)(image.Height * scaleFactor);
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