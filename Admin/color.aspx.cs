using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
public partial class admin_color : System.Web.UI.Page
{
    FabAccessoriesEntities db = new FabAccessoriesEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            bindGrid();
    }

    private void bindGrid()
    {
        var cat = db.ColorMasters.ToList();
        if (txtSearch.Text.Trim().Length > 0)
        {
            cat = cat.Where(r => r.ColorName.Contains(txtSearch.Text)).ToList();
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

            var chksize = db.ColorMasters.Where(r => r.ColorName == txtcolor.Text.Trim()).FirstOrDefault();

            if (chksize == null)
            {
                ColorMaster cat = new ColorMaster();
                cat.ColorName = txtcolor.Text;
                cat.Status = 1;
                cat.AdDate = System.DateTime.Now;
                db.ColorMasters.Add(cat);
                db.SaveChanges();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Color has been saved successfully')", true);
            }
            else
            {
                txtcolor.Focus();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('This color is already added.')", true);
            }
        }
        else
        {
            Int16 id = Convert.ToInt16(hddId.Value);
            string colorname = txtcolor.Text.Trim().ToLower();

            var cat = db.ColorMasters.Where(r => r.ColorName.ToLower() == colorname && r.ColorId != id).FirstOrDefault();

            if (cat == null)
            {
                var cat1 = db.ColorMasters.Where(r => r.ColorId == id).FirstOrDefault();
                cat1.ColorName = txtcolor.Text;
                cat1.Status = 1;
                cat1.AdDate = System.DateTime.Now;
                db.SaveChanges();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Color has been updated successfully')", true);
            }
            else
            {
                txtcolor.Focus();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('This color is already added.')", true);
            }
        }
        bindGrid();
        clear();
    }

    void clear()
    {
        txtSearch.Text = txtcolor.Text = hddId.Value = "";
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
        var cat = db.ColorMasters.Where(r => r.ColorId == id).FirstOrDefault();
        if (cat != null)
        {
            txtcolor.Text = cat.ColorName;
            hddId.Value = cat.ColorId.ToString();
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