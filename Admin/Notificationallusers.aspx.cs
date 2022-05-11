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
public partial class Admin_Notificationallusers : System.Web.UI.Page
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
        //(from ta in context.TestAddresses
        // select ta.Name).Distinct();

        //var cat = (from lh in db.LoginHistories
        //           where lh.PlayerId != null && lh.PlayerId != ""
        //           select new { lh.PlayerId }).Distinct().ToList();


        DataTable dt = DataAccess.GetDataTable("select distinct (playerid) as playerid, isnull((select top 1 Email from UserInfo where Email = LoginHistory.Email ),0) as isUser, email from LoginHistory where playerid!='' and playerid is not null ", CommandType.Text);

        if (dt.Rows.Count > 0)
        {
            int pagesize = Convert.ToInt16(drpPagging.SelectedValue);
            gvProduct.PageSize = pagesize;
            gvProduct.DataSource = dt;
            gvProduct.DataBind();
        }
        else
        {
            gvProduct.DataSource = null;
            gvProduct.DataBind();
        }
       
        //if (cat != null && cat.Count > 0)
        //{
        //    int pagesize = Convert.ToInt16(drpPagging.SelectedValue);
        //    gvProduct.PageSize = pagesize;
        //    gvProduct.DataSource = cat;
        //    gvProduct.DataBind();
        //}
        //else
        //{
        //    gvProduct.DataSource = null;
        //    gvProduct.DataBind();
        //}
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
        string deviceids = ""; int i = 0;
        foreach (GridViewRow grdRow in gvProduct.Rows)
        {
            CheckBox chkNewArrival = (CheckBox)grdRow.FindControl("chkNewArrival");
            Literal ltrdeviceid = (Literal)grdRow.FindControl("ltrdeviceid");
            if (ltrdeviceid != null && chkNewArrival.Checked == true)
            {
                if (i == 0)
                {
                    deviceids = ltrdeviceid.Text;
                    i = i + 1;
                }
                else
                {
                    deviceids = deviceids + "," + ltrdeviceid.Text;
                }
            }
        }
        if (deviceids != "")
        {
            int q = 0;
            //----------- FOR Notification -----------
            // string playerids = "declare @ss nvarchar(max)='' select @ss= @ss + coalesce(deviceid+', ','') from userinfo where id in (" + userids + ") select distinct column1 as deviceid from fnSeprateValues(@ss)  where column1!=''";
            // DataTable pdt = DataAccess.GetDataTable(playerids, CommandType.Text);

            string[] pdt = deviceids.Split(',');

            List<string> playerid = new List<string>();
            if (pdt != null && pdt[0] != "")
            {

                for (int k = 0; k < pdt.Count(); k++)
                {
                    string Deveceid = pdt[k].ToString();
                    if (Deveceid != "")
                    {
                        playerid.Add(Deveceid.ToString());
                    }
                }


                //PushNotifier.SendPromotionsNotification(msg, playerid, "Fab Fashion", "http://www.fabfashionaccessories.com/admincss/images/avatar.png", "Event", txttitle.Text);
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
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('No Device is selected');hihepopup();", true);
        }

    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        // base.VerifyRenderingInServerForm(control);
    }



    protected void btndownloadexcel_Click(object sender, EventArgs e)
    {
        try
        {


            //string search = " where DeviceId is not null and DeviceId !='' and 1=1 ";


            //if (DropDownList1.SelectedValue == "isactive")
            //{
            //    search += " and IsActive == true ";
            //}
            //else if (DropDownList1.SelectedValue == "notactive")
            //{
            //    search += " and IsActive == false ";
            //}

            //if (txtsearch.Text.Trim() != "")
            //{
            //    search += " and (FirstName like '%" + txtsearch.Text.Trim() + "%' or contactno like '%" + txtsearch.Text.Trim() + "%' or email like '%" + txtsearch.Text.Trim() + "%' )";
            //}




            DataTable dt = DataAccess.GetDataTable("select distinct (playerid) as playerid, isnull((select top 1 Email from UserInfo where Email = LoginHistory.Email ),0) as isUser, email from LoginHistory where playerid!='' and playerid is not null", CommandType.Text);


            //ExporttoExcel(dt);

            //if (dt.Rows.Count > 0)
            //{
            Response.Clear();
            Response.ClearContent();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=FabAllUserList.xls");

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
            //HttpContext.Current.Response.End();
            //throw new ExceptionTranslater(ex);
        }
    }

}