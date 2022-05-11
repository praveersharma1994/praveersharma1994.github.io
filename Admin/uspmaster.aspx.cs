using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
public partial class uspmaster : System.Web.UI.Page
{
    FabAccessoriesEntities db = new FabAccessoriesEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            bindGrid();
    }

    private void bindGrid()
    {
        var cat = db.USPDetails.ToList();
        if (txtSearch.Text.Trim().Length > 0)
        {
            cat = cat.Where(r => r.UspTitle.Contains(txtSearch.Text)).ToList();
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

            Guid rd = Guid.NewGuid();
           

            string filename = rd + Path.GetExtension(fluUpload.FileName);
            string pathToSave = HttpContext.Current.Server.MapPath("~/upload/usp/") + filename;

            fluUpload.SaveAs(pathToSave);
            hddImg.Value = filename;
        }
        if (btnSave.Text.ToLower() == "submit")
        {

            var chkusp = db.USPDetails.Where(r => r.UspId > 0).ToList();

            if (chkusp.Count < 4)
            {

                USPDetail cat = new USPDetail();
                cat.UspTitle = txttitle.Text.Trim();
                cat.UspSubTitle = txtsubtitle.Text.Trim();
                cat.Url = txturl.Text.Trim() == "" ? "#" : txturl.Text.Trim();
                cat.IconImageName = hddImg.Value;
                //cat.CollectionName = txtCollection.Text;
                cat.DisplayOrder = Convert.ToInt16(txtDisplayOrder.Text);
                db.USPDetails.Add(cat);
                db.SaveChanges();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('USP Content has been saved successfully')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Now you can edit USP only.')", true);
            }
        }
        else
        {
            Int16 id = Convert.ToInt16(hddId.Value);

            //var chkusp = db.USPDetails.Where(r => r.UspId > 0).ToList();

            var cat = db.USPDetails.Where(r => r.UspId == id).FirstOrDefault();

            if (cat != null)
            {
                cat.UspTitle = txttitle.Text.Trim();
                cat.UspSubTitle = txtsubtitle.Text.Trim();
                cat.Url = txturl.Text.Trim() == "" ? "#" : txturl.Text.Trim();
                cat.IconImageName = hddImg.Value;
                //cat.CollectionName = txtCollection.Text;
                cat.DisplayOrder = Convert.ToInt16(txtDisplayOrder.Text);

                db.SaveChanges();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('USP Content has been updated successfully')", true);
            }
        }
        bindGrid();
        clear();
    }

    void clear()
    {
        txtSearch.Text  = hddId.Value = hddImg.Value = txttitle.Text = txtsubtitle.Text = "";
        txturl.Text = "#";
        txtDisplayOrder.Text = "1";
        //txtCollection.Text 
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
            //var cat = db.USPDetails.Where(r => r.CollectionId == id).FirstOrDefault();
            //db.CollectionMasters.Remove(cat);
            //db.SaveChanges();
            //bindGrid();
        }
    }

    private void getDetail(Int32 id)
    {
        var cat = db.USPDetails.Where(r => r.UspId == id).FirstOrDefault();
        if (cat != null)
        {
            txttitle.Text = cat.UspTitle;
            txtsubtitle.Text = cat.UspSubTitle;
            txturl.Text = cat.Url;
            txtDisplayOrder.Text = cat.DisplayOrder.ToString();
            hddId.Value = cat.UspId.ToString();
            hddImg.Value = cat.IconImageName;
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