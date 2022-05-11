using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Authentication;
using System.IO;
using System.Text;
public partial class Admin_Appusers : System.Web.UI.Page
{
    FabAccessoriesEntities db = new FabAccessoriesEntities();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillGrid();
        }
    }

    public void FillGrid()
    {
        var cat = db.UserInfoes.Where(r => (r.DeviceId != null && r.DeviceId != "")).OrderByDescending(r => r.Id).ToList();
        if (DropDownList1.SelectedValue == "isactive")
        {
            cat = cat.Where(r => r.IsActive == true).OrderByDescending(r => r.Id).ToList();
        }
        else if (DropDownList1.SelectedValue == "notactive")
        {
            cat = cat.Where(r => r.IsActive == false).OrderByDescending(r => r.Id).ToList();
        }

        if (txtsearch.Text.Trim() != "")
        {
            cat = cat.Where(r => r.FirstName.ToLower().Contains(txtsearch.Text.ToLower().Trim()) || r.ContactNo.ToLower().Contains(txtsearch.Text.ToLower().Trim()) || r.Email.ToLower().Contains(txtsearch.Text.ToLower().Trim())).OrderByDescending(r => r.Id).ToList();
        }
        if (cat != null && cat.Count > 0)
        {
            int pagesize = Convert.ToInt16(drpPagging.SelectedValue);
            gvProduct.PageSize = pagesize;
            gvProduct.DataSource = cat;
            gvProduct.DataBind();
        }
        else
        {
            gvProduct.DataSource = null;
            gvProduct.DataBind();
        }
    }

    protected void gvProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProduct.PageIndex = e.NewPageIndex;
        FillGrid();
    }

    protected void drpPagging_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        FillGrid();
    }

    protected void btnsendnotification_Click(object sender, EventArgs e)
    {
        string msg = txtmessage.Text.Trim();
        string userids = ""; int i = 0;
        foreach (GridViewRow grdRow in gvProduct.Rows)
        {
            CheckBox chkNewArrival = (CheckBox)grdRow.FindControl("chkNewArrival");
            HiddenField hddInnerId = (HiddenField)grdRow.FindControl("hddId");
            if (hddInnerId != null && chkNewArrival.Checked == true)
            {
                if (i == 0)
                {
                    userids = hddInnerId.Value;
                    i = i + 1;
                }
                else
                {
                    userids = userids + "," + hddInnerId.Value;
                }
            }
        }
        if (userids != "")
        {
            int q = 0;
            //----------- FOR Notification -----------
            string playerids = "declare @ss nvarchar(max)='' select @ss= @ss + coalesce(deviceid+', ','') from userinfo where id in (" + userids + ") select distinct column1 as deviceid from fnSeprateValues(@ss)  where column1!=''";
            DataTable pdt = DataAccess.GetDataTable(playerids, CommandType.Text);
            List<string> playerid = new List<string>();
            if (pdt != null && pdt.Rows.Count > 0)
            {
                foreach (DataRow did in pdt.Rows)
                {
                    string Deveceid = did["deviceid"].ToString();
                    if (Deveceid != "")
                    {
                        playerid.Add(Deveceid.ToString());
                    }
                }
                //PushNotifier.SendPromotionsNotification(msg, playerid, "Fab Fashion", "http://fabfashionaccessories.com/admincss/images/avatar.png", "Event", txttitle.Text);
                q = 1;
            }

            txtmessage.Text = txttitle.Text = "";
            if (q == 1)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Notification send successfully');hihepopup();", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('No User is selected');hihepopup();", true);
        }

    }
    protected void btndeleteuser_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow grdRow in gvProduct.Rows)
        {
            CheckBox chkNewArrival = (CheckBox)grdRow.FindControl("chkNewArrival");
            HiddenField hddInnerId = (HiddenField)grdRow.FindControl("hddId");
            if (hddInnerId != null && chkNewArrival.Checked == true)
            {

                int deluser = DataAccess.ExecuteQuery("delete from UserInfo where ID=" + hddInnerId.Value + "; delete from dbo.OrderTbl where UserId=" + hddInnerId.Value + "; delete from dbo.OrderDetail where OrderNo in (select OrderNo from dbo.OrderTbl where UserId=" + hddInnerId.Value + "); delete from dbo.OrderTracking where OrderNo in (select OrderNo from dbo.OrderTbl where UserId=" + hddInnerId.Value + "); delete from dbo.OrderShippingDetail where OrderNo in (select OrderNo from dbo.OrderTbl where UserId=" + hddInnerId.Value + "); delete from dbo.OrderPaymentResponse where OrderId in (select OrderNo from dbo.OrderTbl where UserId=" + hddInnerId.Value + "); delete from dbo.OrderBillingInfo where OrderNo in (select OrderNo from dbo.OrderTbl where UserId=" + hddInnerId.Value + ");", CommandType.Text);


            }
        }

        FillGrid();
        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Selected users has been deleted.');", true);

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // base.VerifyRenderingInServerForm(control);
    }


   
    protected void btndownloadexcel_Click(object sender, EventArgs e)
    {
        try
        {


            string search = " where DeviceId is not null and DeviceId !='' and 1=1 ";


            if (DropDownList1.SelectedValue == "isactive")
            {
                search += " and IsActive == true ";
            }
            else if (DropDownList1.SelectedValue == "notactive")
            {
                search += " and IsActive == false ";
            }

            if (txtsearch.Text.Trim() != "")
            {
                search += " and (FirstName like '%" + txtsearch.Text.Trim() + "%' or contactno like '%" + txtsearch.Text.Trim() + "%' or email like '%" + txtsearch.Text.Trim() + "%' )";
            }




            DataTable dt = DataAccess.GetDataTable("select FirstName as Name, Address, City, State, Country, Zip, ContactNo, Gender, Email, Username, Password, convert(nvarchar,CreateDate,103) as CreateDate, Registerfrom, DeviceId, PlayerId from userinfo " + search + " order by id desc", CommandType.Text);


            //ExporttoExcel(dt);

            //if (dt.Rows.Count > 0)
            //{
            Response.Clear();
            Response.ClearContent();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=FabRegUserList.xls");

            // Create a dynamic control, populate and render it
            GridView excel = new GridView();
            excel.DataSource = dt;
            excel.DataBind();
            excel.RenderControl(new HtmlTextWriter(Response.Output));

            Response.Flush();
            Response.SuppressContent = true;
            Response.End();
            // }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            //HttpContext.Current.Response.End();
            //throw new ExceptionTranslater(ex);
        }
    }
}