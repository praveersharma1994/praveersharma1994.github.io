using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
public partial class admin_CategoryMaster : System.Web.UI.Page
{
    FabAccessoriesEntities db = new FabAccessoriesEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            bindGrid();
    }

    private void bindGrid()
    {
        var cat = db.CategoryMasters.ToList();
        if (txtSearch.Text.Trim().Length > 0)
        {
            cat = cat.Where(r => r.CategoryName.Contains(txtSearch.Text)).ToList();
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
            string filename = Common.url(txtCategory.Text, 25) + Path.GetExtension(fluUpload.FileName);

            var path = Path.Combine("~/upload/category/", filename);
            Stream strm = fluUpload.PostedFile.InputStream;
            var targetFile = HttpContext.Current.Server.MapPath(path);
            Common.img(0.4, strm, targetFile, 400, 400);
            //string pathToSave = HttpContext.Current.Server.MapPath("") + filename;
            //fluUpload.SaveAs(pathToSave);
            hddImg.Value = filename;
        }
        if (btnSave.Text.ToLower() == "submit")
        {
            CategoryMaster cat = new CategoryMaster();
            cat.CategoryImg = hddImg.Value;
            cat.CategoryName = txtCategory.Text;
            cat.DisplayOrder = Convert.ToInt16(txtDisplayOrder.Text);

            db.CategoryMasters.Add(cat);
            db.SaveChanges();
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Category has been saved successfully')", true);
        }
        else
        {
            Int16 id = Convert.ToInt16(hddId.Value);
            var cat = db.CategoryMasters.Where(r => r.CategoryId == id).FirstOrDefault();
            cat.CategoryImg = hddImg.Value;
            cat.CategoryName = txtCategory.Text;
            cat.DisplayOrder = Convert.ToInt16(txtDisplayOrder.Text);
            db.SaveChanges();
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Category has been updated successfully')", true);
        }
        bindGrid();
        clear();
    }

    void clear()
    {
        txtSearch.Text = txtDisplayOrder.Text = txtCategory.Text = hddId.Value = hddImg.Value = "";
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
            var cat = db.CategoryMasters.Where(r => r.CategoryId == id).FirstOrDefault();
            db.CategoryMasters.Remove(cat);
            db.SaveChanges();
            bindGrid();
        }
    }

    private void getDetail(Int32 id)
    {
        var cat = db.CategoryMasters.Where(r => r.CategoryId == id).FirstOrDefault();
        if (cat != null)
        {
            txtCategory.Text = cat.CategoryName;
            txtDisplayOrder.Text = cat.DisplayOrder.ToString();
            hddId.Value = cat.CategoryId.ToString();
            hddImg.Value = cat.CategoryImg;
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