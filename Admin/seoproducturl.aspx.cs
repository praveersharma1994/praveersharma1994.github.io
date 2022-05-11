using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class seoproducturl : System.Web.UI.Page
{
    FabAccessoriesEntities db = new FabAccessoriesEntities();

    public string url = "https://www.fabfashionaccessories.com/";
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["JewelsConStr"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillmaster();
        }
    }

    private void fillmaster()
    {
        var item = (from it in db.CategoryMasters join pro in db.ProductMasters on it.CategoryId equals pro.CategoryId select new { it.CategoryName, it.CategoryId }).Distinct().OrderBy(x => x.CategoryName).ToList();
        drpDisplay.DataTextField = "CategoryName";
        drpDisplay.DataValueField = "CategoryID";
        drpDisplay.DataSource = item;
        drpDisplay.DataBind();
        drpDisplay.Items.Insert(0, new ListItem("All", "0"));


        var category = (from cate in db.CollectionMasters join pro in db.ProductMasters on cate.CollectionId equals pro.CollectionId select new { cate.CollectionName, cate.CollectionId }).Distinct().OrderBy(x => x.CollectionName).ToList();
        drpMeta.DataTextField = "CollectionName";
        drpMeta.DataValueField = "CollectionID";
        drpMeta.DataSource = category;
        drpMeta.DataBind();
        drpMeta.Items.Insert(0, new ListItem("All", "0"));



    }

    public class newlist
    {
        public string SubCategory { get; set; }
        public int Cateid { get; set; }
        public string barcode { get; set; }
        public int ItemId { get; set; }
        public string Item { get; set; }
        public int OcassionId { get; set; }
        public string ItemCode { get; set; }
        public string Title { get; set; }
        public string productDes { get; set; }
        public string productId { get; set; }
        public string ProductDesc { get; set; }

        public string MetaUrl { get; set; }
        public string seotitle { get; set; }
        public string Description { get; set; }

        public string seobarcode { get; set; }
        public string keyword { get; set; }



    }


    private void getProductList()
    {
        List<newlist> li = new List<newlist>();
        string query = "";

        int itemid = Convert.ToInt32(drpDisplay.SelectedValue);
        int cateid = Convert.ToInt32(drpMeta.SelectedValue);
        char s = '1';
        if (drpDisplay.SelectedIndex != 0)
        {
            query = "where itm.categoryid=" + drpDisplay.SelectedValue.ToString() + "";
            s = '2';
        }

        if (drpMeta.SelectedIndex != 0)
        {
            if (s == '2')
            {
                query += " and cate.collecrtionID=" + drpMeta.SelectedValue.ToString() + "";
            }
            else
            {
                query = "where cate.collecrtionID=" + drpMeta.SelectedValue.ToString() + "";

            }

        }



        //var data = conn.Query<newlist>("select cate.Name as SubCategory, pro.CategoryID as Cateid, pro.Barcode as barcode, itm.ID as ItemId, itm.Name as Item, pro.OcassionId, pro.ItemCode, pro.productDes as Title, pro.Barcode as productId, pro.productDes as ProductDesc, se.MetaUrl,se.Title as seotitle, se.[Description], se.barcode as seobarcode, se.keyword from products pro join item itm on pro.itemid=itm.id join category cate on pro.categoryid=cate.id left join seo se on replace(pro.itemcode,' ','')=replace(se.barcode,' ','') " + query + "").OrderByDescending(r => r.productId).ToList();
        DataTable data = DataAccess.GetDataTable("select cate.CollectionName as colname, pro.CategoryID as Cateid,  itm.CategoryImg as ItemId, itm.CategoryName as Item, pro.SkuName as Itemcode, pro.Title as Title, pro.Description as ProductDesc, pro.Id as Productid,se.MetaUrl,se.Title as seotitle, se.[Description], se.barcode as seobarcode, se.keyword from ProductMaster pro join CategoryMaster itm on pro.CategoryId=itm.CategoryId left join CollectionMaster cate on pro.CollectionId=cate.CollectionId left join seo se on pro.Id=se.proid " + query + " order by productid desc", CommandType.Text);
        if (data.Rows.Count > 0)
        {
            gv1.DataSource = data;
            gv1.Width = 2500;
        }
        else
        {
            gv1.DataSource = data;
            gv1.Width = 1100;
        }



        gv1.DataBind();
    }
    string removeHtml(string u)
    {
        u = Regex.Replace(u, @"[^0-9a-zA-Z-]+", "");
        return u.Trim().Replace(" ", "-");
    }

    private void Excel_Export(Panel panelReport1, string fileName)
    {
        Response.Clear();
        Response.Buffer = true;
        string file = "seo-product-" + DateTime.Now.ToShortDateString().Replace("/", "-");
        if (fileName != "")
        {
            file += "-" + fileName;
        }
        Response.AddHeader("content-disposition", "attachment;filename=" + file + ".xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        HtmlForm frm = new HtmlForm();
        panelReport1.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(panelReport1);
        frm.RenderControl(hw);
        //style to format numbers to string
        //string style = @"<style> .textmode { mso-number-format:\@; } </style>";
        //  Response.Write(style);
        Response.Output.Write(sw.ToString());

        Response.Flush();
        Response.End();
    }

    protected void bntExport_Click(object sender, EventArgs e)
    {


        Excel_Export(pnlGroupExport, "");

    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        getProductList();
    }
}