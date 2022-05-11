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

public partial class admin_Stock : System.Web.UI.Page
{
    FabAccessoriesEntities db = new FabAccessoriesEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = DataAccess.GetDataTable("Select Distinct(categoryname) as category,cm.categoryid from categorymaster cm join ProductMaster pm on cm.categoryid = pm.categoryid  where  stockpcs>0 order by categoryname", CommandType.Text);

            if (dt.Rows.Count > 0)
            {
                drpcategoryid.DataSource = dt;
                drpcategoryid.DataTextField = "category";
                drpcategoryid.DataValueField = "categoryid";
                drpcategoryid.DataBind();
                drpcategoryid.Items.Insert(0, new ListItem("Select", "0"));
            }
            bindproducts();
        }
    }

    private void bindproducts()
    {
        try
        {
            List<SqlParameter> param = new List<SqlParameter>();

            var query = "select pm.*,cm.categoryname,isnull((select STUFF((SELECT distinct ',' + t1.imagename  from otherimage t1 where pm.id = t1.productid FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,1,'')),'') as otherimage1 from productmaster pm left join categorymaster cm on cm.categoryid = pm.categoryid where pm.id >0 ";
            var filter = "";

            if (txtskumatching.Text.Trim() != "")
            {
                filter = filter + " and LOWER(pm.SKUName) like '%" + txtskumatching.Text.Trim().ToString().ToLower() + "%'";
                param.Add(new SqlParameter("@SKUno", txtskumatching.Text.Trim().ToString()));
            }

            if (drpcategoryid.SelectedIndex > 0)
            {
                filter = filter + " and  pm.categoryid=" + drpcategoryid.SelectedValue.ToString() + "";
                param.Add(new SqlParameter("@categoryid", drpcategoryid.SelectedValue.Trim().ToString()));
            }

            if (txtfromdate.Text.Trim()!="")
            {
                filter = filter + " and  pm.mdate>= convert(datetime,'" + txtfromdate.Text.Trim() + "',103) ";
                //param.Add(new SqlParameter("@categoryid", drpcategoryid.SelectedValue.Trim().ToString()));
            }

            if (txttodate.Text.Trim() != "")
            {
                filter = filter + " and  pm.mdate<= convert(datetime,'" + txttodate.Text.Trim() + "',103) ";
                //param.Add(new SqlParameter("@categoryid", drpcategoryid.SelectedValue.Trim().ToString()));
            }

            if (DropDownList1.SelectedValue == "show")
            {
                filter = filter + " and showhide=1";
            }
            if (DropDownList1.SelectedValue == "hide")
            {
                filter = filter + " and  showhide=0";
            }

            DataTable dtrecords = DataAccess.GetDataTable(query + filter + " order by pm.id desc", CommandType.Text, param.ToArray());
            int pagesize = Convert.ToInt16(drpPagging.SelectedValue);
            GridView2.PageSize = pagesize;
            GridView2.DataSource = dtrecords;
            GridView2.DataBind();
            ltrtproducts.Text = dtrecords.Rows.Count.ToString();
        }
        catch (Exception ex)
        {
            Common.LogError(ex);
        }
    }

    protected void btnsearchmatchitems_Click(object sender, EventArgs e)
    {
        bindproducts();
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string selectedids = "0";
        string nonselectedids = "0";

        for (int k = 0; k < GridView2.Rows.Count; k++)
        {
            CheckBox chk = (CheckBox)GridView2.Rows[k].FindControl("chkshowhide");
            HiddenField hddId = (HiddenField)GridView2.Rows[k].FindControl("hddId");

            if (chk.Checked == true)
            {
                selectedids += "," + hddId.Value;
            }
            else
            {
                nonselectedids += "," + hddId.Value;
            }
        }

        int x = DataAccess.ExecuteQuery("update productmaster set showhide=1 where id in (" + selectedids + ")", CommandType.Text);

        int y = DataAccess.ExecuteQuery("update productmaster set showhide=0 where id in (" + nonselectedids + ")", CommandType.Text);

        if (x > 0 || y > 0)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "alert", "alert('Show/Hide status has been updated');", true);
        }
    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        bindproducts();
    }

    protected void btnexportexcel_Click(object sender, EventArgs e)
    {
        List<SqlParameter> param1 = new List<SqlParameter>();

        var query = "select pm.*,cm.categoryname,isnull((select STUFF((SELECT distinct ',' + t1.imagename  from otherimage t1 where pm.id = t1.productid FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,1,'')),'') as otherimage1 from productmaster pm left join categorymaster cm on cm.categoryid = pm.categoryid where pm.id >0 ";

        var filter = "";

        if (txtskumatching.Text.Trim() != "")
        {
            filter = filter + " and LOWER(pm.SKUName) like '%" + txtskumatching.Text.Trim().ToString().ToLower() + "%'";
            param1.Add(new SqlParameter("@SKUno", txtskumatching.Text.Trim().ToString()));
        }
        if (drpcategoryid.SelectedIndex > 0)
        {
            filter = filter + " and  pm.categoryid='" + drpcategoryid.SelectedValue.Trim().ToString() + "'";
            param1.Add(new SqlParameter("@categoryid", drpcategoryid.SelectedValue.Trim().ToString()));
        }

        DataTable mydt = DataAccess.GetDataTable(query + filter + " order by pm.id desc", CommandType.Text, param1.ToArray());
        Response.Clear();
        Response.ClearContent();
        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition", "attachment; filename=StockExcelFile.xls");

        // Create a dynamic control, populate and render it
        GridView excel = new GridView();
        excel.DataSource = mydt;
        excel.DataBind();
        excel.RenderControl(new HtmlTextWriter(Response.Output));
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    protected void drpPagging_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindproducts();
    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Int32 id = Convert.ToInt32(e.CommandArgument.ToString());
        if (e.CommandName.ToLower() == "edtqty")
        {
            GridViewRow row1 = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            TextBox txtQty = (TextBox)row1.FindControl("txtqty");
            int qty = Convert.ToInt32(txtQty.Text);
            var cat = db.ProductMasters.Where(r => r.Id == id).FirstOrDefault();
            cat.StockPcs = qty;
            db.SaveChanges();
            bindproducts();
        }
        if (e.CommandName.ToLower() == "offerprice")
        {
            GridViewRow row1 = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            TextBox txtofferprice = (TextBox)row1.FindControl("txtofferprice");
            int OfferPrice = Convert.ToInt32(txtofferprice.Text);
            var cat = db.ProductMasters.Where(r => r.Id == id).FirstOrDefault();
            cat.SRP = OfferPrice;
            db.SaveChanges();
            bindproducts();
        }
    }
    protected void btnupdateSRP_Click(object sender, EventArgs e)
    {
        

        for (int k = 0; k < GridView2.Rows.Count; k++)
        {
            CheckBox chk = (CheckBox)GridView2.Rows[k].FindControl("chkshowhide");
            HiddenField hddId = (HiddenField)GridView2.Rows[k].FindControl("hddId");
            TextBox txtofferprice = (TextBox)GridView2.Rows[k].FindControl("txtofferprice");


            if (chk.Checked == true)
            {

                decimal OfferPrice = Convert.ToDecimal(txtofferprice.Text);

                decimal id = Convert.ToDecimal(hddId.Value);

                var cat = db.ProductMasters.Where(r => r.Id == id).FirstOrDefault();
                cat.SRP = OfferPrice;
                db.SaveChanges();
            }

        }


        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "alert", "alert('SRP Price has been updated');", true);

    }
    protected void btnupdateQTY_Click(object sender, EventArgs e)
    {
        for (int k = 0; k < GridView2.Rows.Count; k++)
        {
            CheckBox chk = (CheckBox)GridView2.Rows[k].FindControl("chkshowhide");
            HiddenField hddId = (HiddenField)GridView2.Rows[k].FindControl("hddId");
            TextBox txtqty = (TextBox)GridView2.Rows[k].FindControl("txtqty");


            if (chk.Checked == true)
            {
                decimal id = Convert.ToDecimal(hddId.Value);
                int qty = Convert.ToInt32(txtqty.Text);
                var cat = db.ProductMasters.Where(r => r.Id == id).FirstOrDefault();
                cat.StockPcs = qty;
                db.SaveChanges();
            }

        }


        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "alert", "alert('Qty has been updated');", true);
    }
}