using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class seocategorymeta : System.Web.UI.Page
{
    FabAccessoriesEntities db = new FabAccessoriesEntities();
    DataSet ds = new DataSet();
    public string url = "https://www.fabfashionaccessories.com/";
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["JewelsConStr"].ToString());

    static int upid;
    static string xlsConnStr = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void btnSeach_Click(object sender, EventArgs e)
    {
        createXLSConnection();
        FillGrid();
    }

    private void createXLSConnection()
    {
        lblMessage.Text = "";
        if (((FileUpload1.PostedFile == null) && !(Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower().Contains(".xls")) && !(Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower().Contains(".xls"))) || (FileUpload1.PostedFile.ContentLength <= 0))
        {
            lblMessage.Text = "upload file shoud be excel format";
        }
        else
        {
            string fileName = FileUpload1.FileName.ToString();
            ViewState["fileName"] = fileName;
            string strpath = Server.MapPath("~/seo/excel/" + "/");
            strpath += fileName.Trim();
            FileUpload1.PostedFile.SaveAs(strpath);
        }
        //try
        //{
        if (Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower() == ".xls")
        {
            xlsConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Server.MapPath("~/seo/excel/" + "/" + FileUpload1.FileName.ToString()) + ";" + "Extended Properties='Excel 8.0;IMEX=1;'";
        }
        if (Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower() == ".xlsx")
        {
            xlsConnStr = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Server.MapPath("~/seo/excel/" + "/" + FileUpload1.FileName.ToString()) + ";" + "Extended Properties='Excel 12.0 Xml; HDR=YES;'";
        }
        using (OleDbConnection connection = new OleDbConnection(xlsConnStr))
        {
            connection.Open();
            OleDbDataAdapter adp = new OleDbDataAdapter("Select * FROM [Sheet1$]", connection);
            adp.Fill(ds, "[Sheet1$]");
            connection.Close();
            connection.Dispose();
            //CommenClass.removeBlankRow(ds.Tables[0] as DataTable, 1);
            ViewState["ds"] = ds;
        }
        //}
        //catch (Exception e1)
        //{
        //    lblMessage.Text = e1.ToString();
        //}

    }

    public void FillGrid()
    {
        try
        {

            ds = (DataSet)ViewState["ds"];
            gv1.DataSource = ds;
            gv1.DataBind();
            gv1.Visible = true;


            // var meta =from s in db.Seos.Where(r=>r.Url.Contains(
        }
        catch (Exception e1)
        {
            lblMessage.Text = e1.ToString();
        }
    }
    protected void gv1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv1.PageIndex = e.NewPageIndex;
        FillGrid();
    }


    protected void btnEdit_Click(object sender, EventArgs e)
    {

        ds = (DataSet)ViewState["ds"];

        DataTable table = ds.Tables[0];
        string str = "";
        for (int i = 0; i < table.Rows.Count; i++)
        {
            if (table.Rows[i]["CategoryUrl"].ToString() == "" || table.Rows[i]["CategoryUrl"].ToString() == null)
            {
                table.Rows[i].Delete();
            }
            else
            {
                str = rdo_category.Checked == true ? "category" : "website";

                var result = DB.Business.SPs.SpCheckSeo(table.Rows[i]["CategoryUrl"].ToString(), table.Rows[i]["Title"].ToString(), str, table.Rows[i]["Keywords"].ToString(), table.Rows[i]["Description"].ToString(), table.Rows[i]["Alt"].ToString(), "", "CategoryMeta").GetDataSet();
            }

        }
        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "alert", "alert('Data Import successfully');", true);
    }



    protected void btn_ecelformat(object sender, EventArgs e)
    {
        string fname = Server.MapPath("~/seo/excel/categorymetafile.xlsx");
        FileInfo file = new FileInfo(fname);
        Response.Clear();
        Response.AddHeader("Content-Disposition", "attachment;filename=" + file.Name);
        Response.AddHeader("Content-Length", file.Length.ToString());
        Response.ContentType = "Seo/Excel/xls";
        Response.TransmitFile(fname);
        Response.End();

    }

}