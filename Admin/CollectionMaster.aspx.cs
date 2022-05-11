using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
public partial class admin_Collection : System.Web.UI.Page
{
    FabAccessoriesEntities db = new FabAccessoriesEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            bindGrid();
    }

    private void bindGrid()
    {
        var cat = db.CollectionMasters.ToList();
        if (txtSearch.Text.Trim().Length > 0)
        {
            cat = cat.Where(r => r.CollectionName.Contains(txtSearch.Text)).ToList();
        }
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
        if (fluUpload.HasFile)
        {
            string filename = Common.url(txtCollection.Text) + Path.GetExtension(fluUpload.FileName);
            string pathToSave = HttpContext.Current.Server.MapPath("~/appcollectionimg/") + filename;
           
           // fluUpload.SaveAs(pathToSave);

            Stream strm = fluUpload.PostedFile.InputStream;
            var targetFile = pathToSave;
            GenerateThumbnails(0.4, strm, targetFile, 600, 600);

            hddImg.Value = filename;

            

        }
        if (btnSave.Text.ToLower() == "submit")
        {
            CollectionMaster cat = new CollectionMaster();
            cat.CollectionImg = hddImg.Value;
            cat.CollectionName = txtCollection.Text;
            cat.DisplayOrder = Convert.ToInt16(txtDisplayOrder.Text);
            db.CollectionMasters.Add(cat);
            db.SaveChanges();
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Collection has been saved successfully')", true);
        }
        else
        {
            Int16 id = Convert.ToInt16(hddId.Value);
            var cat = db.CollectionMasters.Where(r => r.CollectionId == id).FirstOrDefault();
            cat.CollectionImg = hddImg.Value;
            cat.CollectionName = txtCollection.Text;
            cat.DisplayOrder = Convert.ToInt16(txtDisplayOrder.Text);
            db.SaveChanges();
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Collection has been updated successfully')", true);
        }
        bindGrid();
        clear();
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

    void clear()
    {
        txtSearch.Text = txtDisplayOrder.Text = txtCollection.Text = hddId.Value = hddImg.Value = "";
        btnSave.Text = "Submit";
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
            var cat = db.CollectionMasters.Where(r => r.CollectionId == id).FirstOrDefault();
            db.CollectionMasters.Remove(cat);
            db.SaveChanges();
            bindGrid();
        }
    }

    private void getDetail(Int32 id)
    {
        var cat = db.CollectionMasters.Where(r => r.CollectionId == id).FirstOrDefault();
        if (cat != null)
        {
            txtCollection.Text = cat.CollectionName;
            txtDisplayOrder.Text = cat.DisplayOrder.ToString();
            hddId.Value = cat.CollectionId.ToString();
            hddImg.Value = cat.CollectionImg;
            btnSave.Text = "Update";
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
}