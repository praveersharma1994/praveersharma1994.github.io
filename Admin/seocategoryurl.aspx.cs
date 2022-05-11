using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class seocategoryurl : System.Web.UI.Page
{
    FabAccessoriesEntities db = new FabAccessoriesEntities();

    string url = "https://www.fabfashionaccessories.com/";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fill_catgoryurl("c");
        }
    }

    private void fill_catgoryurl(string type = "")
    {
        DataTable dtnew = sfill_table();
        if (type == "c")
        {

            var group = (from grp in db.CollectionMasters
                         join pro in db.ProductMasters on grp.CollectionId equals pro.CollectionId
                         where (grp.CollectionName != "" && pro.ShowHide == 1)
                         select new { grp.CollectionName, grp.CollectionId }).Distinct().OrderBy(r => r.CollectionName);

            foreach (var newgroup in group)
            {
                DataRow dr = dtnew.NewRow();
                dr["CategoryUrl"] = url + "collection/" + Common.url(newgroup.CollectionName) + "-" + newgroup.CollectionId + ".html";
                dtnew.Rows.Add(dr);
            }

            //Jewellery Shopping END

            //online Jewellery Start 1st level

            //var group1 = (from grp in db.Itembanners
            //              join pro in db.Products on grp.ID equals pro.ItemID
            //              join subGrp in db.SubGroups on pro.SubGroupId equals subGrp.ID
            //              where (
            //             grp.Name != "" && grp.Name != "-" && pro.ProductStatus != "Cart Product"

            //              )
            //              select new { grp.Name, grp.ID }).Distinct().OrderBy(r => r.Name);
            //foreach (var shopby in group1)
            //{
            //    DataRow dr1 = dtnew.NewRow();
            //    dr1["CategoryUrl"] = url + "online-jewellery/" + CommanClass.url(shopby.Name) + "-" + shopby.ID + ".html";
            //    dtnew.Rows.Add(dr1);

            //    //online Jewellery End 1st level

            //    //online-jewellery 2nd level Start
            //    Int32 shopbyid = shopby.ID;

            //    var groupunder = (from grp in db.Items
            //                      join pro in db.Products on grp.ID equals pro.ItemID
            //                      join subGrp in db.SubGroups on pro.SubGroupId equals subGrp.ID
            //                      where (
            //                      subGrp.Name != "" && subGrp.Name != "-" && grp.ID == shopbyid
            //                          //grp.ID == ((from prd in db.SubGroups where (prd.Name == "-") select prd.ID).FirstOrDefault())
            //                          //grp.ID == ((from prd in db.Products where (prd.SubGroupId == subGrp.ID) select prd.ItemID).FirstOrDefault())
            //                      )
            //                      select new { subGrp.Name, subGrp.ID, itemId = grp.ID, groupName = grp.Name }).Distinct().OrderBy(r => r.Name);

            //    foreach (var grpunder in groupunder)
            //    {
            //        DataRow dr2 = dtnew.NewRow();
            //        dr2["CategoryUrl"] = url + "online-jewellery/" + CommanClass.url(grpunder.groupName) + "/" + CommanClass.url(grpunder.Name) + "-" + grpunder.ID + ".html";
            //        dtnew.Rows.Add(dr2);
            //    }



            //}

            ////online-jewellery 2nd level end

            ////collection Url Start 1st level


            //var collect = (from coll in db.Categories
            //               join pro in db.Products on coll.ID equals pro.CategoryID
            //               where pro.ProductStatus == "New Entry" && coll.Name.Trim() != "" && coll.Name.Trim() != "<Primary>"
            //               select new { coll.ID, coll.Name, coll.Descr, coll.Img }).Distinct().OrderBy(r => r.Name);


            //foreach (var collection in collect)
            //{


            //    DataRow dr3 = dtnew.NewRow();
            //    dr3["CategoryUrl"] = url + "online-collection/" + CommanClass.url(collection.Name) + "-" + collection.ID + ".html";
            //    dtnew.Rows.Add(dr3);


            //}

            ////var stone = (from ston in db.Stones
            ////             join pro in db.Product_Stones on ston.ID equals pro.StoneID
            ////             join prots in db.Products on pro.productId equals prots.productId
            ////             where (ston.Name.Length <= 23 && prots.ProductStatus == "New Entry")
            ////             select new { ston.Name, ston.ID }).Distinct().OrderBy(r => r.Name);


            //var stone = (from ston in db.Stones
            //             join pro in db.Product_Stones on ston.ID equals pro.StoneID
            //             select new { ston.Name, ston.ID }).Distinct().OrderBy(r => r.Name);

            //foreach (var stonecollection in stone)
            //{
            //    DataRow dr4 = dtnew.NewRow();
            //    dr4["CategoryUrl"] = url + "gemstone-jewellery/" + CommanClass.url(stonecollection.Name) + "-" + stonecollection.ID + ".html";
            //    dtnew.Rows.Add(dr4);
            //}

            //collection Url End 1st level

        }
        else
        {

            var SQLQuery = @"select url as CategoryUrl,seo.*  from seo where barcode='website'";


            using (var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["JewelsConStr"].ConnectionString))
            {
                try
                {

                    using (var command = new SqlCommand())
                    {
                        command.CommandText = SQLQuery.Trim();
                        command.CommandType = CommandType.Text;
                        command.Connection = dbConnection;
                        dbConnection.Open();
                        dtnew.Load(command.ExecuteReader(CommandBehavior.CloseConnection));

                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

        }

        gv1.DataSource = dtnew;
        gv1.DataBind();

    }

    private DataTable sfill_table()
    {
        DataTable dt = new DataTable();

        DataColumn dc1 = new DataColumn("CategoryUrl", typeof(string));
        dt.Columns.Add(dc1);
        DataColumn dc2 = new DataColumn("MetaTitle", typeof(string));
        dt.Columns.Add(dc2);
        DataColumn dc3 = new DataColumn("MetaDesc", typeof(string));
        dt.Columns.Add(dc3);
        DataColumn dc4 = new DataColumn("MetaKeyword", typeof(string));
        dt.Columns.Add(dc4);
        DataColumn dc5 = new DataColumn("altdata", typeof(string));
        dt.Columns.Add(dc5);



        return dt;


    }

    protected void rdo_category_CheckedChanged(object sender, EventArgs e)
    {
        fill_catgoryurl("c");
    }
    protected void rdo_website_CheckedChanged(object sender, EventArgs e)
    {
        fill_catgoryurl("s");
    }

    private void Excel_Export(Panel panelReport1, string fileName)
    {
        Response.Clear();
        Response.Buffer = true;
        string file = "seo-category-" + DateTime.Now.ToShortDateString().Replace("/", "-");
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


    protected void btnsave_Click(object sender, EventArgs e)
    {
        Excel_Export(pnlGroupExport, "");
    }

    protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl_meta = (Label)e.Row.FindControl("lblurl");
            var meta = db.Seos.Where(r => r.Url.ToLower() == lbl_meta.Text.ToLower()).FirstOrDefault();


            if (meta != null)
            {
                ((Label)e.Row.FindControl("lblmetatitle")).Text = meta.Title.ToString();
                ((Label)e.Row.FindControl("lblmetadescription")).Text = meta.Description.ToString();
                ((Label)e.Row.FindControl("lblmetakeyword")).Text = meta.keyword.ToString();
                ((Label)e.Row.FindControl("lblalt")).Text = Convert.ToString(meta.MetaUrl);


                if (meta.Title == null || meta.Title.Trim() == "")
                {
                    lbl_meta.Style.Add("color", "red");
                }


            }
            else
            {

                lbl_meta.Style.Add("color", "red");

            }
        }

    }
}