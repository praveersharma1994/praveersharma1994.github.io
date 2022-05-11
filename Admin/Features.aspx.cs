using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
public partial class admin_Features : System.Web.UI.Page
{
    FabAccessoriesEntities db = new FabAccessoriesEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            bindGrid();
    }

    private void bindGrid()
    {
        var cat = db.FeatureMasters.ToList();
        if (txtSearch.Text.Trim().Length > 0)
        {
            cat = cat.Where(r => r.FeatureName.ToLower().Contains(txtSearch.Text)).ToList();
        }



        var sub = from c in cat
                  select new { c.CreateDate, c.DisplayOrder, c.FeatureId, c.FeatureName };
        int pagesize = Convert.ToInt16(drpPagging.SelectedValue);
        grdList.PageSize = pagesize;
        grdList.DataSource = sub.ToList();
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

            var chkfeature = db.FeatureMasters.Where(r => r.FeatureName.ToLower() == txtfeature.Text.Trim().ToLower()).FirstOrDefault();

            if (chkfeature == null)
            {

                FeatureMaster cat = new FeatureMaster();
                cat.FeatureName = txtfeature.Text.Trim();
                cat.DisplayOrder = 0;
                cat.FeatureGroupId = 0;
                cat.CreateDate = System.DateTime.Now.Date;
                db.FeatureMasters.Add(cat);
                db.SaveChanges();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Feature has been saved successfully')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Feature is already exist')", true);
            }
        }
        else
        {
            Int16 id = Convert.ToInt16(hddId.Value);

            var chkfea = db.FeatureMasters.Where(r => r.FeatureName.ToLower() == txtfeature.Text.Trim() && r.FeatureId != id).FirstOrDefault();

            if (chkfea == null)
            {

                var cat = db.FeatureMasters.Where(r => r.FeatureId == id).FirstOrDefault();
                cat.FeatureName = txtfeature.Text;
                cat.DisplayOrder = 0;
                cat.FeatureGroupId = 0;
                cat.CreateDate = System.DateTime.Now.Date;
                db.SaveChanges();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Feature has been updated successfully')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Feature is already exist')", true);
            }
        }
        bindGrid();
        clear();
    }

    void clear()
    {
        txtSearch.Text = txtfeature.Text = hddId.Value = "";
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
            //var cat = db.FeatureMasters.Where(r => r.FeatureId == id).FirstOrDefault();
            //db.FeatureMasters.Remove(cat);
            //db.SaveChanges();
            bindGrid();
        }
    }

    private void getDetail(Int32 id)
    {
        var cat = db.FeatureMasters.Where(r => r.FeatureId == id).FirstOrDefault();
        if (cat != null)
        {
            txtfeature.Text = cat.FeatureName;
            hddId.Value = cat.FeatureId.ToString();
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