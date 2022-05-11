using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class newsletterlist : System.Web.UI.Page
{
    FabAccessoriesEntities db = new FabAccessoriesEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindGrid();
        }
    }

    private void bindGrid()
    {
        var cat = db.NewsLetters.Where(r => r.Id > 0).OrderByDescending(r => r.Id).ToList();

        if (cat.Count != 0)
        {
            grdList.DataSource = cat;
            grdList.DataBind();
        }
    }

    protected void drpPagging_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void grdList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdList.PageIndex = e.NewPageIndex;
        bindGrid();
    }

    protected void grdList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Int32 id = Convert.ToInt32(e.CommandArgument.ToString());
        if (e.CommandName.ToLower() == "del")
        {
            var cat = db.NewsLetters.Where(r => r.Id == id).FirstOrDefault();
            if (cat != null)
            {
                db.NewsLetters.Remove(cat);
                db.SaveChanges();
                bindGrid();
            }
        }
        
    }

    private void getDetail(Int32 id)
    {
        //var cat = db.Testimonials.Where(r => r.id == id).FirstOrDefault();
        //if (cat != null)
        //{
        //    txtname.Text = cat.Name;
        //    txtsku.Text = cat.SkuNo;
        //    txttitle.Text = cat.Title;
        //    txtdescription.Text = cat.Description;
        //    hddId.Value = cat.id.ToString();
        //    btnSave.Text = "Update";
        //}
    }

    protected void btnSeach_Click(object sender, EventArgs e)
    {
        bindGrid();
    }
}