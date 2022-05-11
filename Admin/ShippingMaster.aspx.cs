using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
public partial class ShippingMaster : System.Web.UI.Page
{
    FabAccessoriesEntities db = new FabAccessoriesEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            bindGrid();
    }

    private void bindGrid()
    {
        var cat = db.ShipMasters.ToList();
        if (txtSearch.Text.Trim().Length > 0)
        {
            cat = cat.Where(r => r.ShipName.ToLower().Contains(txtSearch.Text)).ToList();
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
        if (txtUrl.Text.Trim().Length == 0)
        {
            txtUrl.Text = "#";
        }

        //DateTime dt = db.CreateQuery<DateTime>("CurrentDateTime()").AsEnumerable().FirstOrDefault();
        if (btnSave.Text.ToLower() == "submit")
        {
            ShipMaster cat = new ShipMaster();

            cat.ContactNo = txtContactNo.Text;
            cat.CreateDate = System.DateTime.Now.Date;

            cat.ShipName = txtCategory.Text;
            cat.ShipWebsite = txtWebsiteUrl.Text;
            cat.TrackUrl = txtUrl.Text;
            db.ShipMasters.Add(cat);
            db.SaveChanges();
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Ship Company has been saved successfully')", true);
        }
        else
        {
            Int16 id = Convert.ToInt16(hddId.Value);
            var cat = db.ShipMasters.Where(r => r.Id == id).FirstOrDefault();
            cat.ContactNo = txtContactNo.Text;
            cat.CreateDate = System.DateTime.Now.Date;

            cat.ShipName = txtCategory.Text;
            cat.ShipWebsite = txtWebsiteUrl.Text;
            cat.TrackUrl = txtUrl.Text;
            db.SaveChanges();
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Ship Company has been updated successfully')", true);
        }
        bindGrid();
        clear();
    }

    void clear()
    {
        txtUrl.Text = txtSearch.Text = txtContactNo.Text = txtCategory.Text = hddId.Value = txtWebsiteUrl.Text = "";
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
            var cat = db.Banners.Where(r => r.BannerId == id).FirstOrDefault();
            db.Banners.Remove(cat);
            db.SaveChanges();
            bindGrid();
        }
    }

    private void getDetail(Int32 id)
    {
        var cat = db.ShipMasters.Where(r => r.Id == id).FirstOrDefault();
        if (cat != null)
        {
            txtContactNo.Text = cat.ContactNo;
            txtCategory.Text = cat.ShipName;
            txtWebsiteUrl.Text = cat.ShipWebsite;
            txtUrl.Text = cat.TrackUrl;
            hddId.Value = id.ToString();
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