using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class admin_DiscountCoupon : System.Web.UI.Page
{
    FabAccessoriesEntities db = new FabAccessoriesEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            getList();

            txtCode.Text = gen_discountcode().ToUpper();
        }
        // HtmlGenericControl li = (HtmlGenericControl)this.Master.FindControl("liMaster");
        //li.Attributes.Add("class", "dropdown active");

    }
    private string gen_discountcode()
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[10];
        var random = new Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }


        //string GenCode = Guid.NewGuid().ToString().Substring(0,9);
        var finalString = new String(stringChars);
        return finalString;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        //  var displayOrd = Convert.ToInt32(txtDisplayOrder.Text);

        System.Globalization.DateTimeFormatInfo dtinfo = new System.Globalization.DateTimeFormatInfo();
        dtinfo.ShortDatePattern = "dd/MM/yyyy";


        if (btnSave.Text == "Submit")
        {
            try
            {
                var dis = db.DiscountCoupons.Where(r => r.DiscountCode.ToLower() == txtCode.Text.ToLower()).FirstOrDefault();
                if (dis != null)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "script", "jError('Discount Code allready exist !! ')", true);
                    return;
                }
                dis = new DiscountCoupon();
                dis.DiscountPerc = Convert.ToDecimal(txtName.Text.Trim().ToString());
                dis.DiscountCode = txtCode.Text.Trim().ToUpper();
                dis.Addon = System.DateTime.Now;
                dis.EditedOn = Convert.ToDateTime(txtexpirydate.Text.Trim(), dtinfo);
                if (chk_active.Checked == true)
                {
                    dis.Status = "1";
                }
                else
                {
                    dis.Status = "0";
                }

                //db.AddObject("DiscountCoupon", dis);
                db.DiscountCoupons.Add(dis);
                db.SaveChanges();
                string DiscountId = dis.Id.ToString();
                if (DiscountId != "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "script", "jSuccess(' Discount created successfully !! ')", true);

                    clear();
                    getList();
                }
                else
                {
                    hddType.Value = txtCode.Text.ToString();
                    ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "script", "jError('" + hddType.Value + " allready exist !! ')", true);
                    txtCode.Focus();
                }
            }
            catch (Exception ex)
            {
                hddType.Value = txtCode.Text.ToString();
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "script", "jError('" + hddType.Value + " allready exist !! ')", true);
                txtCode.Focus();
            }
        }
        else
        {
            if (btnSave.Text == "Update")
            {
                try
                {
                    Int32 ids = Convert.ToInt16(hddId.Value);
                    var dis = db.DiscountCoupons.Where(r => r.DiscountCode.ToLower() == txtCode.Text.ToLower() && r.Id != ids).FirstOrDefault();
                    if (dis != null)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "script", "jError('Discount Code allready exist !! ')", true);
                        return;
                    }
                    dis = db.DiscountCoupons.Where(r => r.Id == ids).FirstOrDefault();
                    if (dis != null)
                    {
                        dis.DiscountPerc = Convert.ToDecimal(txtName.Text.Trim().ToString());
                        dis.DiscountCode = txtCode.Text.Trim().ToUpper();
                        dis.EditedOn = Convert.ToDateTime(txtexpirydate.Text.Trim(), dtinfo);
                        if (chk_active.Checked == true)
                        {
                            dis.Status = "1";
                        }
                        else
                        {
                            dis.Status = "0";
                        }
                        db.SaveChanges();
                    }

                    int ProdId = dis.Id;
                    if (ProdId > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "script", "jSuccess('Discount  Updated successfully !! ')", true);

                        clear();
                        getList();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "script", "jError('Discount  allready exist !! ')", true);
                        txtName.Focus();
                    }
                }
                catch (Exception)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "script", "jError('Discount allready exist !! ')", true);
                    txtName.Focus();
                }
            }
        }

    }

    protected void lnk_newguidclick(object sender, EventArgs e)
    {
        txtCode.Text = gen_discountcode().ToUpper();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        clear();
    }
    void clear()
    {
        txtName.Text = txtCode.Text = "";
        txtCode.Text = "";
        txtexpirydate.Text = "";
        //hddType.Value = txtPassword.Text = txtName.Text = txtLoginId.Text = txtEmail.Text = txtContact.Text = txtConPassword.Text = "";
        //drpUserType.SelectedIndex = 0;
        btnSave.Text = "Submit";
        hddId.Value = "";
        // txtDisplayOrder.Text = "";
        txtName.Focus();
        chk_active.Checked = false;
    }


    #region List
    private void getList()
    {

        var itm = db.DiscountCoupons.ToList();
        if (txtSearchName.Text.Trim().Length > 0)
        {
            itm = db.DiscountCoupons.Where(r => r.DiscountCode.ToLower().Contains(txtSearchName.Text.ToLower().Trim())).ToList();
        }
        grdList.PageSize = Convert.ToInt16(drpPageSize.SelectedValue);
        grdList.DataSource = itm;
        grdList.DataBind();


    }




    protected void grdList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        getList();
    }
    protected void drpUserType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grdList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Int64 id = Convert.ToInt64(e.CommandArgument.ToString());

        var dis = db.DiscountCoupons.Where(r => r.Id == id).FirstOrDefault();
        if (e.CommandName == "edt")
        {


            if (dis != null)
            {
                txtName.Text = dis.DiscountPerc.ToString();
                txtCode.Text = dis.DiscountCode.ToUpper();
                hddId.Value = dis.Id.ToString();
                txtexpirydate.Text = dis.EditedOn == null ? "" : Convert.ToDateTime(dis.EditedOn).ToString("dd/MM/yyyy");
                if (dis.Status == "1")
                {
                    chk_active.Checked = true;
                }
                else
                {
                    chk_active.Checked = false;
                }

                btnSave.Text = "Update";
            }
            txtName.Focus();
        }
        if (e.CommandName == "del")
        {
            db.DiscountCoupons.Remove(dis);
            db.SaveChanges();
            getList();
            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "script", "jError('Record Deleted Successfully !! ')", true);
            //message("Success", "User created successfully", "success.jpg");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        getList();
    }
    protected void grdList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdList.PageIndex = e.NewPageIndex;
        getList();
    }
    #endregion
    protected void grdList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            CheckBox chk = (CheckBox)e.Row.FindControl("chk_status");
            HiddenField HD = (HiddenField)e.Row.FindControl("hd_status");
            string str = HD.Value;
            if (HD.Value.ToString() == "1")
            {
                chk.Checked = true;
            }
            else
            {
                chk.Checked = false;
            }
        }


    }
}