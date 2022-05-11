using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
public partial class admin_Material : System.Web.UI.Page
{
    FabAccessoriesEntities db = new FabAccessoriesEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            bindGrid();
    }

    private void bindGrid()
    {
        var cat = db.MaterialMasters.ToList();
        if (txtSearch.Text.Trim().Length > 0)
        {
            cat = cat.Where(r => r.Material.Contains(txtSearch.Text)).ToList();
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
       
        if (btnSave.Text.ToLower() == "submit")
        {
            MaterialMaster cat = new MaterialMaster();
            cat.Material = txtMaterial.Text;
            db.MaterialMasters.Add(cat);
            db.SaveChanges();
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Material has been saved successfully')", true);
        }
        else
        {
            Int16 id = Convert.ToInt16(hddId.Value);
            var cat = db.MaterialMasters.Where(r => r.MaterialId == id).FirstOrDefault();
            cat.Material = txtMaterial.Text;
            db.SaveChanges();
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Material has been updated successfully')", true);
        }
        bindGrid();
        clear();
    }

    void clear()
    {
        txtSearch.Text = txtMaterial.Text = hddId.Value =  "";
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
            var cat = db.MaterialMasters.Where(r => r.MaterialId == id).FirstOrDefault();
            db.MaterialMasters.Remove(cat);
            db.SaveChanges();
            bindGrid();
        }
    }

    private void getDetail(Int32 id)
    {
        var cat = db.MaterialMasters.Where(r => r.MaterialId == id).FirstOrDefault();
        if (cat != null)
        {
            txtMaterial.Text = cat.Material;
            hddId.Value = cat.MaterialId.ToString();
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