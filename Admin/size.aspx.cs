using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
public partial class admin_size : System.Web.UI.Page
{
    FabAccessoriesEntities db = new FabAccessoriesEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            bindGrid();
    }

    private void bindGrid()
    {
        var cat = db.SizeMasters.ToList();
        if (txtSearch.Text.Trim().Length > 0)
        {
            cat = cat.Where(r => r.SizeName.Contains(txtSearch.Text)).ToList();
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

            var chksize = db.SizeMasters.Where(r => r.SizeName == txtsize.Text.Trim()).FirstOrDefault();

            if (chksize == null)
            {
                SizeMaster cat = new SizeMaster();
                cat.SizeName = txtsize.Text;
                cat.Status = 1;
                cat.AdDate = System.DateTime.Now;
                db.SizeMasters.Add(cat);
                db.SaveChanges();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Size has been saved successfully')", true);
            }
            else
            {
                txtsize.Focus();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('This size is already added.')", true);
            }
        }
        else
        {
            Int16 id = Convert.ToInt16(hddId.Value);

            var cat = db.SizeMasters.Where(r => r.SizeName==txtsize.Text.Trim() && r.SizeId != id).FirstOrDefault();

            if (cat == null)
            {
                cat.SizeName = txtsize.Text;
                cat.Status = 1;
                cat.AdDate = System.DateTime.Now;
                db.SaveChanges();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Size has been updated successfully')", true);
            }
            else
            {
                txtsize.Focus();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('This size is already added.')", true);
            }
        }
        bindGrid();
        clear();
    }

    void clear()
    {
        txtSearch.Text = txtsize.Text = hddId.Value =  "";
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
            //var cat = db.SizeMasters.Where(r => r.SizeId == id).FirstOrDefault();
            //db.SizeMasters.Remove(cat);
            //db.SaveChanges();
            //bindGrid();
        }
    }

    private void getDetail(Int32 id)
    {
        var cat = db.SizeMasters.Where(r => r.SizeId == id).FirstOrDefault();
        if (cat != null)
        {
            txtsize.Text = cat.SizeName;
            hddId.Value = cat.SizeId.ToString();
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