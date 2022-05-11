using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class admin_ChangePassword : System.Web.UI.Page
{
    FabAccessoriesEntities db = new FabAccessoriesEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["adminid"] == null)
            {             
                Response.Redirect("../adminlogin.aspx");
            }
        }
        
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            var chkoldp = db.AdminLogins.Where(r => r.Password == txt_old.Text.Trim() && r.LoginId == "Admin").FirstOrDefault();
            if (chkoldp != null)
            {
                chkoldp.Password = txt_new.Text.Trim();
                db.SaveChanges();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "alert", "alert('Password changed successfully..........');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "alert", "alert('Incorrect Old Password........');", true);
            }

            
        }
        catch
        {
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txt_old.Text = txt_new.Text = txtUrl.Text = "";
    }
}