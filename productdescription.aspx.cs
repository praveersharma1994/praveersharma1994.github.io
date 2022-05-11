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

public partial class productdescription : System.Web.UI.Page
{
    public int startRowIndex = 0;

    int count;

    DataTable dt = new DataTable();

    public string img1 = ""; public string imgthmb = "";

    string imgurl = "";

    FabAccessoriesEntities db = new FabAccessoriesEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.DataBind();

        Session["Url"] = Request.Url.ToString();
        Session["UrlP"] = Request.Url.ToString();
        if (RouteData.Values["col"] != null && RouteData.Values["cat"] != null && RouteData.Values["id"] != null)
        {
            string id = RouteData.Values["id"].ToString();
            if (id.Contains("."))
            {
                id = id.Substring(id.LastIndexOf("-") + 1);
                id = id.Substring(0, id.LastIndexOf("."));
                ViewState["ID"] = id;
                hfproid.Value = id;
            }
        }

        if (!Page.IsPostBack)
        {
            if (ViewState["ID"] != null)
            {
                DataTable dtpro = DataAccess.GetDataTable("SELECT  Productmaster.Id, productmaster.avlsizes, productmaster.variations, productmaster.avlcolors, Productmaster.SkuName, CollectionMaster.collectionid, CollectionMaster.CollectionName, categorymaster.categoryname, Productmaster.CollectionId, Productmaster.CategoryId, Productmaster.Title, Productmaster.Description, Productmaster.MaterialId, Productmaster.MRP, Productmaster.SRP as Offerprice, Productmaster.Offerprice as SRP, Productmaster.StockPcs,  Productmaster.ShowHide, Productmaster.MDate, Productmaster.Image,MaterialMaster.Material as MaterialName,CategoryMaster.CategoryName as ItemName, isnull(ColorMaster.ColorId,0) as ColorId, ISNULL(SizeMaster.SizeId,0) as SizeId, isnull(ColorMaster.ColorName,'') as ColorName, ISNULL(SizeMaster.SizeName,'') as SizeName, ProductMaster.GenderType FROM Productmaster left join  MaterialMaster on Productmaster.Materialid= MaterialMaster.Materialid left join CategoryMaster on Productmaster.categoryid= CategoryMaster.categoryid left join ColorMaster on Productmaster.ColorId= ColorMaster.ColorId left join SizeMaster on Productmaster.SizeId= SizeMaster.SizeId left join CollectionMaster on CollectionMaster.CollectionId=ProductMaster.CollectionId where stockpcs>=1 and showhide=1 and SRP>0 and productmaster.id=" + hfproid.Value + "", CommandType.Text);

                if (dtpro.Rows.Count > 0)
                {
                    ltrtitle.Text = dtpro.Rows[0]["title"].ToString();
                    ltrsku.Text = dtpro.Rows[0]["skuname"].ToString();

                    ltrmrp.Text = dtpro.Rows[0]["MRP"].ToString();
                    ltrofferprice.Text = dtpro.Rows[0]["Offerprice"].ToString();

                    ltrcollectionname.Text = aheadcollection.InnerText = dtpro.Rows[0]["collectionname"].ToString();
                    aheadcollection.HRef = Page.ResolveUrl("collection/" + Common.url(dtpro.Rows[0]["collectionname"].ToString()) + "-" + dtpro.Rows[0]["collectionid"].ToString() + ".html");
                    ltrcategory.Text = ltrheadcategory.Text = dtpro.Rows[0]["categoryname"].ToString();
                    ltrmaterial.Text = dtpro.Rows[0]["materialname"].ToString();

                    img1 = dtpro.Rows[0]["image"].ToString();

                    ltrimg.Text = "<a href='" + Page.ResolveUrl("upload/products/large/" + img1) + "' rev='" + Page.ResolveUrl("upload/products/large/" + img1) + "' rel='zoom-id:mainhref' class='Selector' >";
                    ltrimg.Text += "<img src='" + Page.ResolveUrl("upload/products/small/" + img1) + "' class='img-responsive smallimg' onerror='this.src='" + Page.ResolveUrl("images/noimage.jpg") + "'' /></a>";

                    //<a href=' <%=Page.ResolveUrl("upload/products/large/"+img1) %>' rev='<%=Page.ResolveUrl("upload/products/large/"+img1) %>' rel="zoom-id:mainhref" class="Selector">
                    //                    <img src='<%=(Page.ResolveUrl("upload/products/small/"+img1))%>' class="img-responsive smallimg" onerror="this.src='<%=Page.ResolveUrl("images/noimage.jpg")%>'" />
                    //                </a>


                    lt_img.Text += "<img  id='img' name='image' src='" + Page.ResolveUrl("upload/products/large/" + dtpro.Rows[0]["image"].ToString()) + "' itemprop='image' style='width:100px;' onerror=this.src='" + Page.ResolveUrl("images/noimage.png") + "' />";

                    imgurl = "https://www.SStyleFactory.com/upload/products/large/" + dtpro.Rows[0]["image"].ToString();

                    if (dtpro.Rows[0]["sizename"].ToString() == "")
                    {
                        trsize.Visible = false;
                        ltrsize.Text = "";
                    }
                    else
                    {
                        ltrsize.Text = dtpro.Rows[0]["sizename"].ToString();
                    }

                    if (dtpro.Rows[0]["colorname"].ToString() == "")
                    {
                        trcolor.Visible = false;
                        ltrcolor.Text = "";
                    }
                    else
                    {
                        ltrcolor.Text = dtpro.Rows[0]["colorname"].ToString();
                    }


                    if (dtpro.Rows[0]["variations"].ToString() == "")
                    {
                        dvvariation.Visible = false;
                    }
                    else
                    {
                        string[] vr = dtpro.Rows[0]["variations"].ToString().Split(',');

                        if (vr.Length > 0 && vr[0] != "")
                        {

                            List<variationlist> lstvar = new List<variationlist>();

                            if (ltrsize.Text.Trim() != "" || ltrcolor.Text.Trim() != "")
                            {
                                variationlist vl1 = new variationlist();
                                vl1.variationname = ltrsize.Text.Trim() + "-" + ltrcolor.Text.Trim();
                                vl1.grpname = "vari";

                                lstvar.Add(vl1);
                            }


                            for (int p = 0; p < vr.Length; p++)
                            {

                                variationlist vl = new variationlist();

                                vl.variationname = vr[p].ToString().Trim();
                                vl.grpname = "vari";

                                lstvar.Add(vl);

                            }

                            //rptvariation1.DataSource = lstvar;
                            //rptvariation1.DataBind();


                            rptvariation.DataSource = lstvar;
                            rptvariation.DataTextField = "variationname";
                            rptvariation.DataValueField = "variationname";
                            rptvariation.DataBind();

                            // rptvariation.SelectedIndex = 0;



                        }
                        else
                        {
                            dvvariation.Visible = false;
                        }

                    }


                    //if (dtpro.Rows[0]["avlsizes"].ToString() == "")
                    //{
                    //    //travlsize.Visible = false;
                    //    //ltravlsize.Text = "";
                    //}
                    //else
                    //{

                    //    string[] sz = dtpro.Rows[0]["avlsizes"].ToString().Split(',');

                    //    if (sz.Length > 0 && sz[0] != "")
                    //    {
                    //        for (int p = 0; p < sz.Length; p++)
                    //        {
                    //            //ltravlsize.Text += "<span>" + sz[p].ToString() + "</span>";
                    //        }
                    //    }
                    //    else
                    //    {
                    //        //travlsize.Visible = false;
                    //        //ltravlsize.Text = "";
                    //    }


                    //    //ltravlsize.Text = dtpro.Rows[0]["avlsizes"].ToString();
                    //}

                    //if (dtpro.Rows[0]["avlcolors"].ToString() == "")
                    //{
                    //    //travlcolor.Visible = false;
                    //    //ltravlcolors.Text = "";
                    //}
                    //else
                    //{

                    //    string[] cl = dtpro.Rows[0]["avlcolors"].ToString().Split(',');

                    //    if (cl.Length > 0 && cl[0] != "")
                    //    {
                    //        for (int p = 0; p < cl.Length; p++)
                    //        {
                    //            //ltravlcolors.Text += "<span>" + cl[p].ToString() + "</span>";
                    //        }
                    //    }
                    //    else
                    //    {
                    //        //travlcolor.Visible = false;
                    //        //ltravlcolors.Text = "";
                    //    }


                    //    //ltravlcolors.Text = dtpro.Rows[0]["avlcolors"].ToString();
                    //}


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
                    DataTable table = HttpContext.Current.Cache[cartp] as DataTable;
                    if (table != null)
                    {
                        int matchpro = 0;

                        for (int p = 0; p < table.Rows.Count; p++)
                        {
                            decimal proid = Convert.ToDecimal(table.Rows[p]["productid"].ToString());

                            if (proid == Convert.ToDecimal(hfproid.Value))
                            {
                                matchpro = 1;
                                break;
                            }

                        }


                        if (matchpro == 1)
                        {
                            lnkaddtocart.Text = " <i class='fa fa-shopping-bag'></i>Added to Cart";
                            lnkaddtocart.Enabled = false;
                            //lnkaddtocart.Style.Add("background-color", "#da712d");
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
                    var cartpwish = HttpContext.Current.Request.Cookies["fabwish"].Value;
                    DataTable tablewish = HttpContext.Current.Cache[cartpwish] as DataTable;
                    if (tablewish != null)
                    {
                        int matchpro = 0;

                        for (int p = 0; p < tablewish.Rows.Count; p++)
                        {
                            decimal proid = Convert.ToDecimal(tablewish.Rows[p]["productid"].ToString());

                            if (proid == Convert.ToDecimal(hfproid.Value))
                            {
                                matchpro = 1;
                                break;
                            }

                        }


                        if (matchpro == 1)
                        {
                            lnkaddtowish.Text = "<i class='fa fa-heart'></i>";
                            lnkaddtowish.ToolTip = "Added to Wishlist";
                            lnkaddtowish.Enabled = false;
                            //lnkaddtocart.Style.Add("background-color", "#da712d");
                        }

                    }



                    ltrdescription.Text = dtpro.Rows[0]["description"].ToString();

                    decimal bal = (Convert.ToDecimal(dtpro.Rows[0]["MRP"].ToString()) - Convert.ToDecimal(dtpro.Rows[0]["Offerprice"].ToString()));

                    decimal percnt = Math.Round((bal / Convert.ToDecimal(dtpro.Rows[0]["MRP"].ToString())) * 100);


                    ltroffamt.Text = percnt + "%";

                    //currentImg.Src = Page.ResolveUrl("upload/products/large/") + dtpro.Rows[0]["image"].ToString();

                    //currentImg1.Src = Page.ResolveUrl("upload/products/small/") + dtpro.Rows[0]["image"].ToString();



                    DataTable relpro = DataAccess.GetDataTable("SELECT  Productmaster.Id, Productmaster.SkuName, CollectionMaster.CollectionName, categorymaster.categoryname, Productmaster.CollectionId, Productmaster.CategoryId, Productmaster.Title, Productmaster.Description, Productmaster.MaterialId, Productmaster.MRP, Productmaster.SRP as Offerprice, Productmaster.Offerprice as SRP, Productmaster.StockPcs,  Productmaster.ShowHide, Productmaster.MDate, Productmaster.Image,MaterialMaster.Material as MaterialName,CategoryMaster.CategoryName as ItemName, isnull(ColorMaster.ColorId,0) as ColorId, ISNULL(SizeMaster.SizeId,0) as SizeId, isnull(ColorMaster.ColorName,'') as ColorName, ISNULL(SizeMaster.SizeName,'') as SizeName, ProductMaster.GenderType FROM Productmaster left join  MaterialMaster on Productmaster.Materialid= MaterialMaster.Materialid left join CategoryMaster on Productmaster.categoryid= CategoryMaster.categoryid left join ColorMaster on Productmaster.ColorId= ColorMaster.ColorId left join SizeMaster on Productmaster.SizeId= SizeMaster.SizeId left join CollectionMaster on CollectionMaster.CollectionId=ProductMaster.CollectionId where stockpcs>=1 and showhide=1 and SRP>0 and productmaster.collectionid=" + dtpro.Rows[0]["collectionid"].ToString() + " and productmaster.categoryid=" + dtpro.Rows[0]["categoryid"].ToString() + " and productmaster.id!=" + hfproid.Value + " ", CommandType.Text);

                    if (relpro.Rows.Count > 0)
                    {
                        rptrelatedproducts.DataSource = relpro;
                        rptrelatedproducts.DataBind();

                        relproduct.Visible = true;
                    }
                    else
                    {
                        relproduct.Visible = false;
                    }


                    DataTable dtthumb = DataAccess.GetDataTable("SELECT  * FROM OtherImage where productid=" + hfproid.Value + " ", CommandType.Text);

                    if (dtthumb.Rows.Count > 0)
                    {
                        rptthumbimg.DataSource = dtthumb;
                        rptthumbimg.DataBind();
                    }


                    DataTable dtfeatures = DataAccess.GetDataTable("select f.featurename, fv.Featurevalue from featuremaster f inner join productfeatures fv on f.featureid=fv.featureid where fv.productid=" + hfproid.Value + " and fv.featurevalue!=''", CommandType.Text);


                    rep.DataSource = dtfeatures;
                    rep.DataBind();

                    decimal pid = Convert.ToDecimal(hfproid.Value);
                    var prorv = db.ProductReviews.Where(r => r.ProductId == pid && r.Status == 1).ToList();

                    if (prorv.Count != 0)
                    {
                        rptreviews.DataSource = prorv;
                        rptreviews.DataBind();
                    }

                    var usp = db.USPDetails.Where(r => r.UspId > 0).OrderBy(r => r.DisplayOrder).ToList();

                    if (usp.Count != 0)
                    {
                        rptusp.DataSource = usp;
                        rptusp.DataBind();
                    }

                    
                    fbsharelink.HRef = "https://www.facebook.com/sharer/sharer.php?u=" + Request.Url.ToString().Replace(".aspx", ".html") + "&t=" + ltrtitle.Text;

                    twitersharelink.HRef = "https://twitter.com/home?status=" + ltrtitle.Text + " " + Request.Url.ToString().Replace(".aspx", ".html");

                    pinterestsharelink.HRef = "http://pinterest.com/pin/create/button/?url= " + Request.Url.ToString().Replace(".aspx", ".html") + "&media=" + imgurl + "&description=" + ltrtitle.Text.Trim();



                }

                SeoData();

            }
        }

    }

    void SeoData()
    {
        string pageurl = Request.Url.ToString();


        HtmlMeta meta1 = new HtmlMeta();
        HtmlMeta meta2 = new HtmlMeta();
        HtmlMeta meta3 = new HtmlMeta();
        HtmlMeta meta4 = new HtmlMeta();

        HtmlMeta meta5 = new HtmlMeta();
        HtmlMeta meta6 = new HtmlMeta();
        HtmlMeta meta7 = new HtmlMeta();

        HtmlHead hd = (HtmlHead)this.Master.FindControl("Head1");




        meta1.Attributes.Add("property", "og:title");
        meta1.Attributes.Add("content", ltrtitle.Text.Trim());
        hd.Controls.Add(meta1);

        meta3.Attributes.Add("property", "og:url");
        meta3.Attributes.Add("content", pageurl);
        hd.Controls.Add(meta3);

        meta4.Attributes.Add("property", "og:description");
        meta4.Attributes.Add("content", ltrdescription.Text);
        hd.Controls.Add(meta4);


        //metalink.Href = pageurl.Replace(".aspx", ".html");

        meta2.Attributes.Add("property", "og:image");
        meta2.Attributes.Add("content", imgurl);
        hd.Controls.Add(meta2);



        meta5.Attributes.Add("name", "twitter:title");
        meta5.Attributes.Add("content", ltrtitle.Text.Trim());
        hd.Controls.Add(meta5);

        meta6.Attributes.Add("name", "twitter:url");
        meta6.Attributes.Add("content", pageurl);
        hd.Controls.Add(meta6);

        meta7.Attributes.Add("name", "twitter:image");
        meta7.Attributes.Add("content", imgurl);
        hd.Controls.Add(meta7);

    }

    protected void lnkaddtocart_Click(object sender, EventArgs e)
    {
        try
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
            DataTable table = HttpContext.Current.Cache[cartp] as DataTable;
            if (table == null)
            {
                table = CreateDataTable();
            }
            DataRow[] drExists = table.Select("productId='" + hfproid.Value + "'");
            if (drExists.Count() <= 0)
            {

                string remark = "";

                for (int k = 0; k < rptvariation.Items.Count; k++)
                {
                    if (rptvariation.Items[k].Selected == true)
                    {
                        remark = rptvariation.Items[k].ToString();
                        break;
                    }
                }



                DataRow dr = table.NewRow();
                dr["ProductId"] = hfproid.Value;
                dr["Qty"] = "1";
                dr["size"] = ltrsize.Text.Trim();
                dr["remarks"] = remark;
                dr["AddDate"] = DateTime.Now;
                dr["qtystatus"] = "";
                table.Rows.Add(dr);
            }
            HttpContext.Current.Cache.Insert(cartp, table, null, DateTime.Now.AddDays(5), System.Web.Caching.Cache.NoSlidingExpiration);

            lnkaddtocart.Text = " <i class='fa fa-shopping-bag'></i>Added to Cart";
            lnkaddtocart.Enabled = false;
            //lnkaddtocart.Style.Add("background-color","#da712d");

            Label lbl = (Label)this.Master.FindControl("lblcartitems");

            lbl.Text = table.Rows.Count.ToString();

        }
        catch
        {

        }
    }

    public class variationlist
    {
        public string variationname { get; set; }
        public string grpname { get; set; }
    }


    public DataTable CreateDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ProductId");
        dt.Columns.Add("Qty");
        dt.Columns.Add("size");
        dt.Columns.Add("ext1");
        dt.Columns.Add("ext2");
        dt.Columns.Add("ext3");
        dt.Columns.Add("AddDate");
        dt.Columns.Add("qtystatus");
        dt.Columns.Add("remarks");
        return dt;
    }
    protected void lnkbuynow_Click(object sender, EventArgs e)
    {
        try
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
            DataTable table = HttpContext.Current.Cache[cartp] as DataTable;
            if (table == null)
            {
                table = CreateDataTable();
            }
            DataRow[] drExists = table.Select("productId='" + hfproid.Value + "'");
            if (drExists.Count() <= 0)
            {

                string remark = "";

                for (int k = 0; k < rptvariation.Items.Count; k++)
                {
                    if (rptvariation.Items[k].Selected == true)
                    {
                        remark = rptvariation.Items[k].ToString();
                        break;
                    }
                }

                DataRow dr = table.NewRow();
                dr["ProductId"] = hfproid.Value;
                dr["Qty"] = "1";
                dr["size"] = ltrsize.Text.Trim();
                dr["remarks"] = remark;
                dr["AddDate"] = DateTime.Now;
                dr["qtystatus"] = "";
                table.Rows.Add(dr);
            }
            HttpContext.Current.Cache.Insert(cartp, table, null, DateTime.Now.AddDays(5), System.Web.Caching.Cache.NoSlidingExpiration);


            Response.Redirect(Page.ResolveUrl("mycart.html"));

        }
        catch
        {

        }
    }
    protected void btnreviewsubmit_Click(object sender, EventArgs e)
    {
        if (txtname.Text.Trim() != "" && txtreview.Text.Trim() != "")
        {

            if (Rating1.CurrentRating == 0)
            {
                Rating1.CurrentRating = 2;
            }


            ProductReview pr = new ProductReview();

            pr.ProductId = Convert.ToDecimal(hfproid.Value);
            pr.ItemCode = ltrsku.Text;
            pr.Name = txtname.Text.Trim();
            pr.Rating = Rating1.CurrentRating;
            pr.Review = txtreview.Text.Trim();
            pr.Status = 0;
            pr.AdDate = System.DateTime.Now;


            db.ProductReviews.Add(pr);
            db.SaveChanges();

            txtname.Text = txtreview.Text = "";
            Rating1.CurrentRating = 2;

            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Your valuable review has been submitted. Thank You.')", true);

        }
    }
    protected void lnkaddtowish_Click(object sender, EventArgs e)
    {
        try
        {
            if (HttpContext.Current.Request.Cookies["fabwish"] == null)
            {
                Guid gui = Guid.NewGuid();
                var cookie = new HttpCookie("fabwish", gui.ToString())
                {
                    Expires = DateTime.Now.AddDays(5)
                };
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            var cartp = HttpContext.Current.Request.Cookies["fabwish"].Value;
            DataTable table = HttpContext.Current.Cache[cartp] as DataTable;
            if (table == null)
            {
                table = CreateDataTable();
            }
            DataRow[] drExists = table.Select("productId='" + hfproid.Value + "'");
            if (drExists.Count() <= 0)
            {

                string remark = "";

                for (int k = 0; k < rptvariation.Items.Count; k++)
                {
                    if (rptvariation.Items[k].Selected == true)
                    {
                        remark = rptvariation.Items[k].ToString();
                        break;
                    }
                }



                DataRow dr = table.NewRow();
                dr["ProductId"] = hfproid.Value;
                dr["Qty"] = "1";
                dr["size"] = ltrsize.Text.Trim();
                dr["remarks"] = remark;
                dr["AddDate"] = DateTime.Now;
                dr["qtystatus"] = "";
                table.Rows.Add(dr);
            }
            HttpContext.Current.Cache.Insert(cartp, table, null, DateTime.Now.AddDays(5), System.Web.Caching.Cache.NoSlidingExpiration);

            lnkaddtowish.Text = "<i class='fa fa-heart'></i>";
            lnkaddtowish.Enabled = false;
            //lnkaddtocart.Style.Add("background-color","#da712d");

            Label lbl = (Label)this.Master.FindControl("lblwishlistitems");

            lbl.Text = table.Rows.Count.ToString();

        }
        catch
        {

        }
    }
    protected void rptrelatedproducts_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
}