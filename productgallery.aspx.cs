using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;

public partial class productgallery : System.Web.UI.Page
{
    public int startRowIndex = 0;

    int count;

    int pagesize = 20;

    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.DataBind();

        if (Page.RouteData.Values["name"] != null)
        {
            string colid = RouteData.Values["name"].ToString();
            string col = RouteData.Values["name"].ToString();

            col = col.Substring(0, col.LastIndexOf('-'));

            if (colid.Contains("."))
            {
                colid = colid.Substring(colid.LastIndexOf("-") + 1);
                colid = colid.Substring(0, colid.LastIndexOf("."));
                ViewState["colid"] = colid;

                DataTable dtitem = DataAccess.GetDataTable("select * from collectionmaster where collectionid='" + colid + "'", CommandType.Text);

                if (dtitem.Rows.Count > 0)
                {
                    //spnarrow2.InnerText = "»";
                    asubcategory.Text = dtitem.Rows[0]["collectionname"].ToString(); ;
                    //asubcategory.HRef = Page.ResolveUrl((itm) + "-" + itemid) + ".html";
                }

            }

        }
        else if (Page.RouteData.Values["sid"] != null)
        {
            string id = Page.RouteData.Values["sid"].ToString();
            if (id.Contains("."))
            {
                id = id.Substring(0, id.LastIndexOf("."));
                ViewState["search"] = id.Replace("-", " ");

                asubcategory.Text = ViewState["search"].ToString();
            }
        }

        if (!IsPostBack)
        {
            bindfilter();
            //getprice();
           
            getproducts();
        }

    }

    private void bindfilter()
    {
        DataSet filterds = new DataSet();
        if (Page.RouteData.Values["name"] != null)
        {
            filterds = DataAccess.GetDataSet("select distinct isnull(cm.categoryname,'') as categoryname, cm.categoryid, 0 as Isselected from ProductMaster pm left join CategoryMaster cm on cm.CategoryId= pm.CategoryId  where showhide=1 and CollectionId=" + ViewState["colid"].ToString() + " order by CategoryName; " +
        " select distinct isnull(mm.Material,'') as Material, mm.MaterialId, 0 as Isselected from ProductMaster pm left join MaterialMaster mm on mm.MaterialId= pm.MaterialId  where showhide=1 and CollectionId=" + ViewState["colid"].ToString() + " and Material!='' order by Material; " +
" select distinct isnull(sz.sizename,'') as sizename, sz.sizeid, 0 as Isselected from ProductMaster pm left join SizeMaster sz on sz.SizeId= pm.SizeId  where showhide=1 and CollectionId=" + ViewState["colid"].ToString() + " and SizeName!='' order by SizeName; " +
" select distinct isnull(clr.colorname,'') as colorname, clr.colorid, 0 as Isselected from ProductMaster pm left join ColorMaster clr on clr.ColorId= pm.ColorId  where showhide=1 and CollectionId=" + ViewState["colid"].ToString() + " and ColorName!='' order by ColorName;", CommandType.Text);
            //" select distinct isnull(GenderType,'') as gendertype, 0 as Isselected from ProductMaster pm where showhide=1 and GenderType!='' and GenderType!='0' and CollectionId=" + ViewState["colid"].ToString() + " ", CommandType.Text);
        }
        else if (Page.RouteData.Values["sid"] != null)
        {
            filterds = DataAccess.GetDataSet("select distinct isnull(cm.categoryname,'') as categoryname, cm.categoryid, 0 as Isselected from ProductMaster pm left join CategoryMaster cm on cm.CategoryId= pm.CategoryId  where showhide=1 and isnull(cm.categoryname,'') like '%" + ViewState["search"].ToString() + "%' order by CategoryName; " +
        " select distinct isnull(mm.Material,'') as Material, mm.MaterialId, 0 as Isselected from ProductMaster pm left join MaterialMaster mm on mm.MaterialId= pm.MaterialId  where showhide=1 and Material!='' and isnull(mm.Material,'') like '%" + ViewState["search"].ToString() + "%'  order by Material; " +
" select distinct isnull(sz.sizename,'') as sizename, sz.sizeid, 0 as Isselected from ProductMaster pm left join SizeMaster sz on sz.SizeId= pm.SizeId and isnull(sz.sizename,'') like '%" + ViewState["search"].ToString() + "%'   where showhide=1 and SizeName!='' order by SizeName; " +
" select distinct isnull(clr.colorname,'') as colorname, clr.colorid, 0 as Isselected from ProductMaster pm left join ColorMaster clr on clr.ColorId= pm.ColorId  where showhide=1 and ColorName!='' and isnull(clr.colorname,'') like '%" + ViewState["search"].ToString() + "%' order by ColorName;", CommandType.Text);

            //" select distinct isnull(GenderType,'') as gendertype, 0 as Isselected from ProductMaster pm where showhide=1 and GenderType!='' and GenderType!='0' ", CommandType.Text);
        }
        if (filterds.Tables.Count > 0)
        {
            if (filterds.Tables[0].Rows.Count > 0)
            {
                repfamily.DataSource = filterds.Tables[0];
                repfamily.DataTextField = "categoryname";
                repfamily.DataValueField = "categoryid";
                repfamily.DataBind();
            }
            else
            {
                //subgroup.Style.Add("display", "none");
            }



            if (filterds.Tables[1].Rows.Count > 0)
            {
                rptmaterial.DataSource = filterds.Tables[1];
                rptmaterial.DataTextField = "Material";
                rptmaterial.DataValueField = "MaterialId";
                rptmaterial.DataBind();
            }
            else
            {
                //subgroup.Style.Add("display", "none");
            }


            if (filterds.Tables[2].Rows.Count > 0)
            {
                rptsize.DataSource = filterds.Tables[2];
                rptsize.DataTextField = "SizeName";
                rptsize.DataValueField = "sizeid";
                rptsize.DataBind();
            }
            else
            {
                //subgroup.Style.Add("display", "none");
            }

            if (filterds.Tables[3].Rows.Count > 0)
            {
                rptcolor.DataSource = filterds.Tables[3];
                rptcolor.DataTextField = "colorname";
                rptcolor.DataValueField = "colorid";
                rptcolor.DataBind();
            }
            else
            {
                //subgroup.Style.Add("display", "none");
            }


            //if (filterds.Tables[4].Rows.Count > 0)
            //{
            //    chkgender.DataSource = filterds.Tables[4];
            //    chkgender.DataTextField = "gendertype";
            //    chkgender.DataValueField = "gendertype";
            //    chkgender.DataBind();
            //}
            //else
            //{
            //    //subgroup.Style.Add("display", "none");
            //}



            string val1 = "500";
            string val2 = "1000";
            string val3 = "2000";
            string val4 = "3000";
            string val5 = "5000";

            string p0 = "Below " + val1;
            string p1 = val1 + " to " + val2;
            string p2 = val2 + " to " + val3;
            string p3 = val3 + "  to " + val4;
            string p4 = val4 + "  to " + val5;
            string p5 = "Above " + val5;
            DataTable dt = new DataTable();
            dt.Columns.Add("value");
            DataRow dr = dt.NewRow();
            dr["value"] = p0;
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["value"] = p1;
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["value"] = p2;
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["value"] = p3;
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["value"] = p4;
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["value"] = p5;
            dt.Rows.Add(dr);
            chkPrice.DataSource = dt;
            chkPrice.DataTextField = "value";
            chkPrice.DataValueField = "value";
            chkPrice.DataBind();


            //if (filterds.Tables[0].Rows.Count > 0)
            //{
            //    repColl.DataSource = filterds.Tables[0];
            //    repColl.DataBind();

            //}
            //if (filterds.Tables[2].Rows.Count > 0)
            //{
            //    repfamily.DataSource = filterds.Tables[2];
            //    repfamily.DataBind();
            //}
            //if (filterds.Tables[3].Rows.Count > 0)
            //{
            //    txtmaxdiawt.Text = Convert.ToDecimal(filterds.Tables[3].Rows[0]["maxdiawt"]).ToString("0.000");
            //    txtmindiawt.Text = Convert.ToDecimal(filterds.Tables[3].Rows[0]["mindiawt"]).ToString("0.000");
            //}
        }

    }

    private void getprice()
    {
        DataTable getminmax = new DataTable();
        if (Page.RouteData.Values["name"] != null)
        {
            getminmax = DataAccess.GetDataTable("select mymin=min(SRP),mymax=MAX(SRP) from ProductMaster where showhide=1", CommandType.Text);
        }
        else if (Page.RouteData.Values["searchitem"] != null)
        {
            string query = "select mymin=min(SRP),mymax=MAX(SRP) from ProductMaster where showhide=1 and Offerprice=0 and  skuname like '%" + Page.RouteData.Values["searchitem"].ToString().Substring(0, Page.RouteData.Values["searchitem"].ToString().LastIndexOf('.')).Replace("-", "/") + "%' or itemname like '%" + Page.RouteData.Values["searchitem"].ToString().Substring(0, Page.RouteData.Values["searchitem"].ToString().LastIndexOf('.')).Replace("-", "/") + "%' or StyleName like '%" + Page.RouteData.Values["searchitem"].ToString().Substring(0, Page.RouteData.Values["searchitem"].ToString().LastIndexOf('.')).Replace("-", "/") + "%' or SgroupName like '%" + Page.RouteData.Values["searchitem"].ToString().Substring(0, Page.RouteData.Values["searchitem"].ToString().LastIndexOf('.')).Replace("-", "/") + "%";
            getminmax = DataAccess.GetDataTable(query, CommandType.Text);
        }

        hddmaxvalue.Value = getminmax.Rows[0]["mymax"].ToString();
        hddminvalue.Value = getminmax.Rows[0]["mymin"].ToString();
    }


    protected void getproducts()
    {
        if (ViewState["colid"] != null || ViewState["search"] != null)
        {
            // string search = "";
            string search = filter();

            if (ViewState["search"] != null)
            {


                string ss = " and (";

                if (ViewState["search"].ToString().ToLower() == "ring" || ViewState["search"].ToString().ToLower() == "rings")
                {
                    ss += " categoryname='ring' or categoryname='rings' ";
                }
                else
                {
                    ss += " categoryname like '%" + ViewState["search"].ToString() + "%' or skuname like '%" + ViewState["search"].ToString() + "%' or collectionname like '%" + ViewState["search"].ToString() + "%' or title like '%" + ViewState["search"].ToString() + "%' or description like '%" + ViewState["search"].ToString() + "%' or MaterialMaster.Material like '%" + ViewState["search"].ToString() + "%' or colorname like '%" + ViewState["search"].ToString() + "%' or sizename like '%" + ViewState["search"].ToString() + "%' ";
                }


                ss += " )";


                dt = DataAccess.GetDataTable("SELECT  Productmaster.Id, CollectionMaster.CollectionName, Productmaster.SkuName, categorymaster.categoryname, Productmaster.CollectionId, Productmaster.CategoryId, Productmaster.Title, Productmaster.Description, Productmaster.MaterialId, Productmaster.MRP, Productmaster.SRP as Offerprice, Productmaster.Offerprice as SRP, Productmaster.StockPcs,  Productmaster.ShowHide, Productmaster.MDate, Productmaster.Image,MaterialMaster.Material as MaterialName,CategoryMaster.CategoryName as ItemName, isnull(ColorMaster.ColorId,0) as ColorId, ISNULL(SizeMaster.SizeId,0) as SizeId, isnull(ColorMaster.ColorName,'') as ColorName, ISNULL(SizeMaster.SizeName,'') as SizeName, ProductMaster.GenderType FROM Productmaster left join  MaterialMaster on Productmaster.Materialid= MaterialMaster.Materialid left join CategoryMaster on Productmaster.categoryid= CategoryMaster.categoryid left join ColorMaster on Productmaster.ColorId= ColorMaster.ColorId left join SizeMaster on Productmaster.SizeId= SizeMaster.SizeId left join CollectionMaster on CollectionMaster.CollectionId=ProductMaster.CollectionId where stockpcs>=1 and showhide=1 and SRP>0 " + ss, CommandType.Text);

                //// textdiv.Visible = false;
                //if (ViewState["search"].ToString().ToLower() == "ring" || ViewState["search"].ToString().ToLower() == "rings")
                //{
                //    pList = pList.ToList().Where(r => r.ModelNo.ToLower().Contains(ViewState["search"].ToString()) || r.CategoryName.ToLower().Trim().Contains(ViewState["search"].ToString().ToLower().Trim()) || r.SubCategoryName.ToLower().Trim().Contains(ViewState["search"].ToString().ToLower().Trim()) || r.Tags.ToLower().Trim() == "rings").ToList();
                //}
                //else
                //{
                //    pList = pList.ToList().Where(r => r.ModelNo.ToLower().Contains(ViewState["search"].ToString()) || r.CategoryName.ToLower().Trim().Contains(ViewState["search"].ToString().ToLower().Trim()) || r.SubCategoryName.ToLower().Trim().Contains(ViewState["search"].ToString().ToLower().Trim()) || r.ShortDesc.ToLower().Trim().Contains(ViewState["search"].ToString().ToLower().Trim()) || r.Description.ToLower().Trim().Contains(ViewState["search"].ToString().ToLower().Trim())).ToList();
                //}
                ////ltrheadingtitle.Text = 
                //ltrlCategory.Text = ViewState["search"].ToString();

                //if (pList.Count == 1)
                //{
                //    ltrtagline.Text = pList.Count.ToString() + " result " + ltrtagline.Text.Trim();
                //}
                //else if (pList.Count > 1)
                //{
                //    ltrtagline.Text = pList.Count.ToString() + " results " + ltrtagline.Text.Trim();
                //}
                //else
                //{
                //    ltrtagline.Text = "No result found !";
                //}
            }


            else
            {

                dt = DataAccess.GetDataTable("SELECT  Productmaster.Id, CollectionMaster.CollectionName, Productmaster.SkuName, categorymaster.categoryname, Productmaster.CollectionId, Productmaster.CategoryId, Productmaster.Title, Productmaster.Description, Productmaster.MaterialId, Productmaster.MRP, Productmaster.SRP as Offerprice, Productmaster.Offerprice as SRP, Productmaster.StockPcs,  Productmaster.ShowHide, Productmaster.MDate, Productmaster.Image,MaterialMaster.Material as MaterialName,CategoryMaster.CategoryName as ItemName, isnull(ColorMaster.ColorId,0) as ColorId, ISNULL(SizeMaster.SizeId,0) as SizeId, isnull(ColorMaster.ColorName,'') as ColorName, ISNULL(SizeMaster.SizeName,'') as SizeName, ProductMaster.GenderType FROM Productmaster left join  MaterialMaster on Productmaster.Materialid= MaterialMaster.Materialid left join CategoryMaster on Productmaster.categoryid= CategoryMaster.categoryid left join ColorMaster on Productmaster.ColorId= ColorMaster.ColorId left join SizeMaster on Productmaster.SizeId= SizeMaster.SizeId left join CollectionMaster on CollectionMaster.CollectionId=ProductMaster.CollectionId where stockpcs>=1 and showhide=1 and SRP>0 and productmaster.collectionid=" + ViewState["colid"].ToString() + " order by mdate desc ", CommandType.Text);
            }

            if (dt.Rows.Count > 0)
            {





                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    dt.Rows[i]["matching"] = CommanClass.url(dt.Rows[i]["matching"].ToString());
                //}

                DataRow[] dr;



                // if (search.Trim().Length > 0)
                // {
                dr = dt.Select(search);
                if (dr.Length > 0) { dt = dr.CopyToDataTable(); }
                else { dt = dt.Clone(); }
                // }

                if (dr.Length > 0)
                {

                    dt = dr.CopyToDataTable();


                    DataView dv = new DataView(dt);

                    if (drpsorting.SelectedValue.ToLower() == "lth")
                    {
                        dv.Sort = "offerprice asc";
                    }
                    else if (drpsorting.SelectedValue.ToLower() == "htl")
                    {
                        dv.Sort = "offerprice desc";
                    }
                    else
                    {
                        dv.Sort = "mdate desc";
                    }


                    dt = dv.ToTable();


                    PagedDataSource pgitems = new PagedDataSource();
                    pgitems.DataSource = dt.DefaultView;
                    pgitems.AllowPaging = true;

                    //Control page size from here 
                    pgitems.PageSize = Convert.ToInt16(drppage.SelectedValue); // pagesize;
                    pgitems.CurrentPageIndex = PageNumber;



                    lnkpre.Enabled = !pgitems.IsFirstPage;
                    lnknext.Enabled = !pgitems.IsLastPage;

                    ltrcurpage.Text = (PageNumber + 1).ToString();
                    ltrtotpage.Text = pgitems.PageCount.ToString();

                    rptgallery.DataSource = pgitems;
                    rptgallery.DataBind();


                    dvnorecord.Visible = false;
                    dvpaging.Visible = true;


                    //dt.DefaultView.Sort = hffilervalue.Value;

                    //ViewState["dt"] = dt;

                    //ListView1.DataSource = dt;
                    //ListView1.DataBind();

                    //DataPager1.PageSize = 30;// Convert.ToInt32(hfpagevalue.Value);

                    //DataPager1.SetPageProperties(DataPager1.StartRowIndex, DataPager1.MaximumRows, true);
                }
                else
                {
                    rptgallery.DataSource = null;
                    rptgallery.DataBind();

                    dvnorecord.Visible = true;
                    dvpaging.Visible = false;
                }


            }

            else
            {
                rptgallery.DataSource = null;
                rptgallery.DataBind();

                dvnorecord.Visible = true;
                dvpaging.Visible = false;
            }
        }
    }


    protected void repfamily_SelectedIndexChanged(object sender, EventArgs e)
    {
        getproducts();
    }

    string filter()
    {
        string search = "";
        string selCategory = string.Join("','", (repfamily.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value).ToList()).ToArray());
        string selPrice = string.Join(",", (chkPrice.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Text).ToList()).ToArray());
        string selMaterial = string.Join("','", (rptmaterial.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value).ToList()).ToArray());
        string selSize = string.Join("','", (rptsize.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value).ToList()).ToArray());
        string selColor = string.Join("','", (rptcolor.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value).ToList()).ToArray());

        //string selgender = string.Join("','", (chkgender.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value).ToList()).ToArray());


        //selStyle = string.Join("','", (rptstyles.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value).ToList()).ToArray());
        //selStone = string.Join("','", (rptstones.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value).ToList()).ToArray());
        //selSubStyle = string.Join("','", (rptsubstyles.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value).ToList()).ToArray());

        //if (selSubCategory.Trim().Length > 0)
        //{
        //    selSubCategory = "'" + selSubCategory + "'";
        //    search += "category  IN(" + selSubCategory + ") AND ";
        //}

        //if (strCollection.Trim().Length > 0)
        //{
        //    strCollection = "'" + strCollection + "'";
        //    search += "collection  IN(" + strCollection + ") AND ";
        //}

        if (selCategory.Trim().Length > 0)
        {
            selCategory = "'" + selCategory + "'";
            search += " Categoryid  IN(" + selCategory + ") AND ";
        }

        if (selMaterial.Trim().Length > 0)
        {
            selMaterial = "'" + selMaterial + "'";
            search += "materialid  IN(" + selMaterial + ") AND ";
        }

        if (selSize.Trim().Length > 0)
        {
            selSize = "'" + selSize + "'";
            search += " sizeid  IN(" + selSize + ") AND ";
        }

        if (selColor.Trim().Length > 0)
        {
            selColor = "'" + selColor + "'";
            search += "colorid  IN(" + selColor + ") AND ";
        }

        //if (selgender.Trim().Length > 0)
        //{
        //    selgender = "'" + selgender + "'";
        //    search += "GenderType  IN(" + selgender + ") AND ";
        //}

        //if (selStyle.Trim().Length > 0)
        //{
        //    selStyle = "'" + selStyle + "'";
        //    search += "styleid  IN(" + selStyle + ") AND ";
        //}

        //if (selStone.Trim().Length > 0)
        //{
        //selStone = "'" + selStone + "'";
        //search += "stone.id  IN(" + selStone + ") AND ";
        //}

        if (selPrice.Trim().Length > 0)
        {
            string searchPrice = "";
            string[] price = selPrice.Split(',');
            searchPrice += " ( ";
            for (int i = 0; i < price.Length; i++)
            {
                string[] currentPrice = Regex.Split(price[i], "to");
                if (!price[i].ToString().ToLower().Contains("above") && !price[i].ToString().ToLower().Contains("below"))
                {
                    searchPrice += " offerprice >= " + currentPrice[0] + "  AND   offerprice <= " + currentPrice[1] + "  OR ";
                }
                else if (price[i].ToString().ToLower().Contains("above"))
                {
                    currentPrice = Regex.Split(currentPrice[0], "Above");
                    searchPrice += "     offerprice >= " + currentPrice[1] + " OR ";
                }
                else if (price[i].ToString().ToLower().Contains("below"))
                {
                    currentPrice = Regex.Split(currentPrice[0], "Below");
                    searchPrice += "     offerprice <= " + currentPrice[1] + " OR ";
                }
            }
            if (searchPrice.Length > 0)
            {
                searchPrice = searchPrice.Substring(0, searchPrice.LastIndexOf("OR"));
                searchPrice += " ) ";
                search += searchPrice + " AND ";
            }
        }

        //if (lblpricehead.Text.Trim().ToLower() != "all price")
        //{
        //    string pri = lblpricehead.Text.Replace("to", "-");
        //    if (lblpricehead.Text.Trim().ToLower().Contains("above"))
        //    {
        //        pri = lblpricehead.Text.ToLower().Replace("above", " ");
        //        search += "productPrice >= '" + pri.Trim() + "' AND ";
        //    }
        //    else
        //    {
        //        pri = lblpricehead.Text.Replace("to", "-");
        //        string[] price = pri.Split('-');
        //        search += "productPrice >= '" + price[0].ToString().Trim() + "' AND ";
        //        search += "productPrice <= '" + price[1].ToString().Trim() + "' AND ";
        //    }
        //}




        if (search.Trim().Length > 0)
        {
            search = search.Substring(0, search.LastIndexOf("AND") - 1);
        }

        return search;
    }






    //protected void chkgender_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    getproducts();
    //}
    //protected void chkPrice_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    getproducts();
    //}
    //protected void rptmaterial_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    getproducts();
    //}
    //protected void rptsize_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    getproducts();
    //}
    //protected void rptcolor_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    getproducts();
    //}

    public int PageNumber
    {
        get
        {
            if (ViewState["PageNumber"] != null)
            {
                return Convert.ToInt32(ViewState["PageNumber"]);
            }
            else
            {
                return 0;
            }
        }
        set { ViewState["PageNumber"] = value; }
    }

    protected void lnkpre_Click(object sender, EventArgs e)
    {
        PageNumber -= 1;
        getproducts();
    }
    protected void lnknext_Click(object sender, EventArgs e)
    {

        string ss = lnknext.Text;

        if (ltrcurpage.Text.Trim() != "" && hf1.Value.ToLower() == "go" && Convert.ToInt32(ltrcurpage.Text.Trim()) != 0)
        {

            if (Convert.ToInt32(ltrtotpage.Text) < Convert.ToInt32(ltrcurpage.Text.Trim()))
            {
                ltrcurpage.Text = ltrtotpage.Text;

            }



            PageNumber = Convert.ToInt32(ltrcurpage.Text.Trim()) - 1;
            hf1.Value = "";
        }
        else
        {
            PageNumber += 1;
        }
        getproducts();
    }
    protected void rptgallery_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HiddenField hflatestproid = (HiddenField)e.Item.FindControl("hflatestproid");

        HtmlAnchor btncart = (HtmlAnchor)e.Item.FindControl("btnaddtocart");

        HtmlGenericControl btnaddtowish = (HtmlGenericControl)e.Item.FindControl("btnaddtowish");



        if (btncart != null)
        {



            if (HttpContext.Current.Request.Cookies["fabcart"] == null)
            {
                Guid gui = Guid.NewGuid();
                var cookie = new HttpCookie("fabcart", gui.ToString())
                {
                    Expires = DateTime.Now.AddDays(5)
                };
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            var cartp = HttpContext.Current.Request.Cookies["fabcart"].Value;
            //Literal lblCart = (Literal)this.Master.FindControl("ltritemcount");
            DataTable table = HttpContext.Current.Cache[cartp] as DataTable;
            if (table != null)
            {

                int matchpro = 0;

                for (int p = 0; p < table.Rows.Count; p++)
                {
                    decimal proid = Convert.ToDecimal(table.Rows[p]["productid"].ToString());

                    if (proid == Convert.ToDecimal(hflatestproid.Value))
                    {
                        matchpro = 1;
                        break;
                    }

                }


                if (matchpro == 1)
                {
                    // str += "<a class='add-to-cart' disabled style='background-color:#da712d;' ><i class='fa fa-shopping-bag'></i> Added to Cart</a>";
                    btncart.Disabled = true;
                    //btncart.Style.Add("background-color", "#da712d");
                    btncart.Style.Add("background-color", "#272625");
                    btncart.InnerHtml = "Added to Cart";
                }
            }

            if (HttpContext.Current.Request.Cookies["fabwish"] == null)
            {
                Guid gui = Guid.NewGuid();
                var cookie = new HttpCookie("fabwish", gui.ToString())
                {
                    Expires = DateTime.Now.AddDays(5)
                };
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            var wishp = HttpContext.Current.Request.Cookies["fabwish"].Value;
            //Literal lblCart = (Literal)this.Master.FindControl("ltritemcount");
            DataTable tablewish = HttpContext.Current.Cache[wishp] as DataTable;
            if (tablewish != null)
            {

                int matchpro = 0;

                for (int p = 0; p < tablewish.Rows.Count; p++)
                {
                    decimal proid = Convert.ToDecimal(tablewish.Rows[p]["productid"].ToString());

                    if (proid == Convert.ToDecimal(hflatestproid.Value))
                    {
                        matchpro = 1;
                        break;
                    }

                }


                if (matchpro == 1)
                {
                    // str += "<a class='add-to-cart' disabled style='background-color:#da712d;' ><i class='fa fa-shopping-bag'></i> Added to Cart</a>";
                    btnaddtowish.Disabled = true;
                    //btncart.Style.Add("background-color", "#da712d");
                    //btncart.Style.Add("background-color", "#272625");
                    btnaddtowish.InnerHtml = "<i id='iwish'+" + hflatestproid.Value + " title='Added to Wishlist' class='fa fa-heart'></i>";
                }



            }


        }
    }
    protected void lnkgo_Click(object sender, EventArgs e)
    {


        if (ltrcurpage.Text.Trim() != "" && hf1.Value.ToLower() == "go" && Convert.ToInt32(ltrcurpage.Text.Trim()) != 0)
        {

            if (Convert.ToInt32(ltrtotpage.Text) < Convert.ToInt32(ltrcurpage.Text.Trim()))
            {
                ltrcurpage.Text = ltrtotpage.Text;

            }

            PageNumber = Convert.ToInt32(ltrcurpage.Text.Trim()) - 1;
            hf1.Value = "";

            lnkgo.Style.Add("display", "none");
            lnknext.Style.Add("display", "inline-block");

        }

        getproducts();
    }
    protected void drpsorting_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["PageNumber"] = null;
        getproducts();
    }
    protected void drppage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["PageNumber"] = null;
        getproducts();
    }
    protected void lnkapplyfilter_Click(object sender, EventArgs e)
    {
        getproducts();
    }
}