using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class myaccount : System.Web.UI.Page
{
    FabAccessoriesEntities db = new FabAccessoriesEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Session["UserId"] == null || Session["LoginId"] == null)
            {
                Response.Redirect(Page.ResolveUrl("index.html"));
            }
            else
            {
                if (Session["FName"] != null)
                {
                    ltrusername.Text = "Hello, " + Session["FName"].ToString();

                    GetUserDetail();

                    GetPaidOrderList();
                }
            }
        }
    }
    protected void lnkmyaccountlogout_Click(object sender, EventArgs e)
    {
        Session["UserId"] = null;
        Session["LoginId"] = null;
        Session.Abandon();
        Session.Clear();
        Response.Redirect(Page.ResolveUrl("index.html"));
    }
    protected void btnupdateinfo_Click(object sender, EventArgs e)
    {
        if (Session["UserId"] != null && Session["LoginId"] != null)
        {

            decimal uid = Convert.ToDecimal(Session["UserId"].ToString());

            var uinfo = db.UserInfoes.Where(r => r.Id == uid).FirstOrDefault();

            if (uinfo != null)
            {
                uinfo.FirstName = txtfname.Text.Trim();
                uinfo.LastName = txtlname.Text.Trim();
                uinfo.Email = txtemail.Text.Trim();
                uinfo.ContactNo = txtmobileno.Text.Trim();
                uinfo.Address = txtaddress.Text.Trim();
                uinfo.City = txtcity.Text.Trim();
                uinfo.State = txtstate.Text.Trim();
                uinfo.Zip = txtpincode.Text.Trim();

                db.SaveChanges();

                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Profile has been updated.');", true);

            }
        }
        else
        {
            Response.Redirect(Page.ResolveUrl("index.html"));
        }
    }

    public void GetUserDetail()
    {
        if (Session["UserId"] != null && Session["LoginId"] != null)
        {

            decimal uid = Convert.ToDecimal(Session["UserId"].ToString());

            var uinfo = db.UserInfoes.Where(r => r.Id == uid).FirstOrDefault();

            if (uinfo != null)
            {


                txtfname.Text = uinfo.FirstName;
                txtlname.Text = uinfo.LastName;
                txtemail.Text = uinfo.Email;
                txtmobileno.Text = uinfo.ContactNo;
                txtaddress.Text = uinfo.Address;


                txtcity.Text = uinfo.City;
                txtstate.Text = uinfo.State;
                drpcountry.SelectedValue = uinfo.Country;
                txtpincode.Text = uinfo.Zip;

            }


        }
        else
        {
            Response.Redirect(Page.ResolveUrl("index.html"));
        }
    }
    protected void btnupdatepassword_Click(object sender, EventArgs e)
    {
        if (Session["UserId"] != null && Session["LoginId"] != null)
        {

            if (txtnewpass.Text.Trim() == txtconfirmpass.Text.Trim())
            {

                decimal uid = Convert.ToDecimal(Session["UserId"].ToString());

                string oldpas = txtcurrentpass.Text.Trim();

                var uinfo = db.UserInfoes.Where(r => r.Id == uid && r.Password == oldpas).FirstOrDefault();

                if (uinfo != null)
                {

                    uinfo.Password = txtconfirmpass.Text.Trim();

                    db.SaveChanges();

                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Password has been changed.');", true);


                }
                else
                {
                    txtcurrentpass.Focus();
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Please enter valid current password');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('New Password & Confirm Password should be same.');", true);
            }


        }
        else
        {
            Response.Redirect(Page.ResolveUrl("index.html"));
        }
    }



    protected void rptorder_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem item = e.Item;

        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            Repeater rptinner = (Repeater)item.FindControl("rptinner");

            var enqno = DataBinder.Eval(e.Item.DataItem, "Orderno");

            DataTable ddt = DataAccess.GetDataTable("select productmaster.id,productmaster.Title, ModelNo as SkuName, Item as ItemName, convert(decimal(18,3),grosswt) as grosswt, Size,  qty, OrderDetail.price,isnull(OrderDetail.discountedprice,0) as discountedprice, total, shortdesc as remark, OrderTbl.OrderNo, ordercurrency, convert(nvarchar,OrderTbl.orderdate,103) as OrderDate,OrderTbl.OrderTotal from OrderDetail left join OrderTbl on OrderTbl.orderno=OrderDetail.orderno left join productmaster on productmaster.skuname=OrderDetail.modelno where orderdetail.orderno='" + enqno + "' ", CommandType.Text);

            if (ddt.Rows.Count > 0)
            {
                rptinner.DataSource = ddt;
                rptinner.DataBind();

                //Literal ltrenqdate = (Literal)rptinner.Controls[0].FindControl("ltrenqdate");
                //Literal ltrenqno = (Literal)rptinner.Controls[0].FindControl("ltrenqno");
                //Literal ltrtot = (Literal)rptinner.Controls[2].FindControl("ltrtot");

                //ltrenqdate.Text = ddt.Rows[0]["EnqDate"].ToString();
                //ltrenqno.Text = ddt.Rows[0]["OrderNo"].ToString();
                //ltrtot.Text = "$ " + ddt.Rows[0]["OrderTotal"].ToString();
            }
            else
            {
                rptinner.DataSource = null;
                rptinner.DataBind();
            }
        }
    }

    public void GetPaidOrderList()
    {

        if (Session["UserId"] != null && Session["LoginId"] != null)
        {

            decimal uid = Convert.ToDecimal(Session["UserId"].ToString());

            DataTable dt = DataAccess.GetDataTable("select OrderNo, convert(nvarchar,orderdate,103) as OrderDate, OrderTotal, ordercurrency, ShippingAmt, couponamt, OrderBy, PaymentMode from OrderTbl left join OrderPaymentResponse on OrderTbl.OrderNo=OrderPaymentResponse.OrderId where userid=" + uid + " order by orderdate desc", CommandType.Text);

            if (dt.Rows.Count > 0)
            {
                rptorder.DataSource = dt;
                rptorder.DataBind();

                //Literal ltrenqdate = (Literal)rptouter.Controls[0].FindControl("ltrenqdate");
                //Literal ltrenqno = (Literal)rptouter.Controls[0].FindControl("ltrenqno");
                //Literal ltrtot = (Literal)rptouter.Controls[0].FindControl("ltrtot");
                //ltrenqdate.Text= dt.Rows
                spnordlist.Visible = false;
            }
            else
            {
                rptorder.DataSource = null;
                rptorder.DataBind();
                spnordlist.Visible = true;
            }
        }
    }
}