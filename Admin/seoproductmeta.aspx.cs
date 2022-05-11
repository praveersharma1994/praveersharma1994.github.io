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

public partial class seoproductmeta : System.Web.UI.Page
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

    private void createXLSConnection()
    {
        lblMessage.Text = "";
        if ((fuExcel.PostedFile == null) && !(Path.GetExtension(fuExcel.PostedFile.FileName).ToLower().Contains(".xls")) || (fuExcel.PostedFile.ContentLength <= 0))
        {
            lblMessage.Text = "upload file shoud be excel format";
        }
        else
        {
            string fileName = fuExcel.FileName.ToString();
            ViewState["fileName"] = fileName;
            string strpath = Server.MapPath("~/seo/excel/" + "/");
            strpath += fileName.Trim();
            fuExcel.PostedFile.SaveAs(strpath);
        }

        try
        {
            if (Path.GetExtension(fuExcel.PostedFile.FileName).ToLower() == ".xls")
            {
                xlsConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Server.MapPath("~/seo/excel/" + "/" + fuExcel.FileName.ToString()) + ";" + "Extended Properties='Excel 8.0;IMEX=1;'";
            }
            if (Path.GetExtension(fuExcel.PostedFile.FileName).ToLower() == ".xlsx")
            {
                xlsConnStr = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Server.MapPath("~/seo/excel/" + "/" + fuExcel.FileName.ToString()) + ";" + "Extended Properties='Excel 12.0 Xml; HDR=YES;'";
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
        }
        catch (Exception e1)
        {
            lblMessage.Text = e1.ToString();
        }

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        createXLSConnection();
        FillGrid();
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
    protected void gv1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gv1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteMeta")
        {
            DataSet dsExport = (DataSet)ViewState["ds"];
            if (dsExport != null)
            {
                DataRow[] dr = dsExport.Tables[0].Select("Sku='" + e.CommandArgument.ToString() + "'");
                if (dr.Length > 0)
                {
                    dsExport.Tables[0].Rows.Remove(dr[0]);
                    ViewState["ds"] = dsExport;
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "alert", "alert('Information deleted successfully.');", true);
                    FillGrid();
                }
            }
        }
    }


    protected void btnEmport_Click(object sender, EventArgs e)
    {
        if (ViewState["ds"] != null)
        {

            ds = (DataSet)ViewState["ds"];


            DataTable table = ds.Tables[0];



            for (int i = 0; i < table.Rows.Count; i++)
            {
                string stritem = table.Rows[i]["Sku"].ToString().Trim();
                var chk_itemcode = db.ProductMasters.Where(r => r.SkuName.ToLower() == stritem.ToLower()).FirstOrDefault();

                if (chk_itemcode != null)
                {
                    var producturl = "";
                    var oldproducturl = "";
                    var data = from pro in db.ProductMasters
                               where pro.SkuName.ToLower() == chk_itemcode.SkuName.ToLower()
                               join itm in db.CategoryMasters on pro.CategoryId equals itm.CategoryId
                               join cat in db.CollectionMasters on pro.CollectionId equals cat.CollectionId
                               select new { Item = cat.CollectionName, SubCategory = itm.CategoryName, Itemcode = pro.SkuName, Title = pro.Title, productId = pro.Id, ProductDesc = pro.Title };

                    foreach (var metalist in data.ToList())
                    {

                        //if (table.Rows[i]["Alt"].ToString() != "")
                        //{
                        producturl = url + "jewellery/" + Common.url(metalist.SubCategory) + "/" + Common.url((metalist.Title).ToString()) + "-" + metalist.productId + ".html";
                        //}
                        //else
                        //{
                        //    oldproducturl = url + "jewellery/" + CommanClass.url(metalist.SubCategory) + "/" + CommanClass.url((metalist.ProductDesc).ToString(), 60) + "-" + metalist.Barcode + ".html";
                        //}

                        oldproducturl = producturl; // url + "jewellery/" + CommanClass.url(metalist.SubCategory) + "/" + CommanClass.url((metalist.ProductDesc).ToString(), 100) + "-" + metalist.productId + ".html";

                        var result = DB.Business.SPs.SpCheckSeo(producturl, table.Rows[i]["Title"].ToString(), metalist.Itemcode, table.Rows[i]["Keywords"].ToString(), table.Rows[i]["title"].ToString(), "", oldproducturl, "insertseodata").GetDataSet();

                    }

                }



            }



            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "alert", "alert('Data Import successfully');", true);

            //using (var con = new SqlConnection(WebConfigurationManager.ConnectionStrings["JewelsConStr"].ConnectionString))
            //{

            //    con.Open();
            //    // Create a table with some rows. 
            //    DataTable table = ds.Tables[0];

            //    // Get a reference to a single row in the table. 
            //    DataRow[] rowArray = table.Select();

            //    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
            //    {
            //        bulkCopy.DestinationTableName = "dbo.seo";
            //        bulkCopy.ColumnMappings.Add("Url", "Url");
            //        bulkCopy.ColumnMappings.Add("Title", "Title");
            //        bulkCopy.ColumnMappings.Add("keywords", "keyword");
            //        bulkCopy.ColumnMappings.Add("Description", "Description");
            //        try
            //        {
            //            // Write the array of rows to the destination.
            //            bulkCopy.WriteToServer(rowArray);
            //            DB.Business.SPs.SpCheckSeo("");
            //            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "alert", "alert('Data Import successfully');", true);
            //            ViewState["ds"] = null;
            //            FillGrid();
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine(ex.Message);
            //        }
            //    }
            //}
        }
    }
    protected void btn_ecelformat(object sender, EventArgs e)
    {
        string fname = Server.MapPath("~/seo/excel/productmetafile.xlsx");
        FileInfo file = new FileInfo(fname);
        Response.Clear();
        Response.AddHeader("Content-Disposition", "attachment;filename=" + file.Name);
        Response.AddHeader("Content-Length", file.Length.ToString());
        Response.ContentType = "Seo/Excel/xls";
        Response.TransmitFile(fname);
        Response.End();
    }

}