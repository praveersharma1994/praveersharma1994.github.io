using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class index : System.Web.UI.Page
{
    FabAccessoriesEntities db = new FabAccessoriesEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindofferbanner();
            BindUSP();
            bindhomegallery();
        }
    }

    public void bindofferbanner()
    {
        var sb = db.StripBanners.Where(r => r.BannerId > 0).OrderBy(r => r.DisplayOrder).ToList();

        if (sb.Count != 0)
        {
            rptoffer.DataSource = sb;
            rptoffer.DataBind();
        }
        else
        {
            rptoffer.DataSource = null;
            rptoffer.DataBind();
        }
    }

    public void BindUSP()
    {
        var usp = db.USPDetails.Where(r => r.UspId > 0).OrderBy(r => r.DisplayOrder).ToList();

        if (usp.Count != 0)
        {
            rptusp.DataSource = usp;
            rptusp.DataBind();
        }
        else
        {
            rptusp.DataSource = null;
            rptusp.DataBind();
        }
    }


    public void bindhomegallery()
    {
        DataTable dt = DataAccess.GetDataTable("SELECT top 120  Productmaster.Id, Productmaster.SkuName, CollectionMaster.CollectionName, categorymaster.categoryname, Productmaster.CollectionId, Productmaster.CategoryId, Productmaster.Title, Productmaster.Description, Productmaster.MaterialId, Productmaster.MRP, Productmaster.SRP as Offerprice, Productmaster.Offerprice as SRP, Productmaster.StockPcs,  Productmaster.ShowHide, Productmaster.MDate, Productmaster.Image,MaterialMaster.Material as MaterialName,CategoryMaster.CategoryName as ItemName, isnull(ColorMaster.ColorId,0) as ColorId, ISNULL(SizeMaster.SizeId,0) as SizeId, isnull(ColorMaster.ColorName,'') as ColorName, ISNULL(SizeMaster.SizeName,'') as SizeName, ProductMaster.GenderType FROM Productmaster left join  MaterialMaster on Productmaster.Materialid= MaterialMaster.Materialid left join CategoryMaster on Productmaster.categoryid= CategoryMaster.categoryid left join ColorMaster on Productmaster.ColorId= ColorMaster.ColorId left join SizeMaster on Productmaster.SizeId= SizeMaster.SizeId left join CollectionMaster on CollectionMaster.CollectionId=ProductMaster.CollectionId where stockpcs>=1 and showhide=1 and SRP>0 order by MDate desc", CommandType.Text);
        DataTable dthb = DataAccess.GetDataTable("select * from homegallerybanner order by bannerid desc", CommandType.Text);

        string str = "";

        int tothb = dthb.Rows.Count;

        int remnhb = tothb;

        int hbrow = 0;

        int bannerreptcount = 15;
        int bannerloopcount = 0;


        if (dt.Rows.Count > 0)
        {
            for (int k = 0; k < dt.Rows.Count; k++)
            {

                if (k == 6 || (bannerloopcount == k && bannerloopcount > 6))
                {
                    if (dthb.Rows.Count > 0 && remnhb > 0)
                    {

                        str += "<div class='item product-item banner-img'>";
                        str += "<div class='img-box'><a href='" + dthb.Rows[hbrow]["bannerurl"].ToString() + "'><img class='img-responsive' src='upload/banner/" + dthb.Rows[hbrow]["bannerimg"].ToString() + "' /></a></div>";
                        str += "</div>";

                        remnhb = remnhb - 1;
                        hbrow = hbrow + 1;

                        bannerloopcount = bannerreptcount + k;

                    }
                }

                //string gotopage = Page.ResolveClientUrl(CommanClass.url(dt.Rows[k]["collectionname"].ToString()) + "/" + CommanClass.url(dt.Rows[k]["categoryname"].ToString()) + "/" + CommanClass.url(dt.Rows[k]["title"].ToString()) + "-" + dt.Rows[k]["id"].ToString() + ".html");

                str += "<div class='item product-item'>";

                //str += "<div class='add-to-wishlist'><i class='fa fa-heart-o'></i></div>";

                str += "<div class='add-to-wishlist' onclick=addtowish('" + dt.Rows[k]["id"].ToString() + "','1','0','0') ><i id='iwish" + dt.Rows[k]["id"].ToString() + "' title='Add to Wishlist' class='fa fa-heart-o'></i></div>";

                // str += "<a class='add-to-cart pro" + dt.Rows[k]["id"].ToString() + "' href='javascript:void(0);'  onclick=AddToCart('" + dt.Rows[k]["id"].ToString() + "','1','0','0') >Add to Cart</a><a href='javascript:void(0);' onclick=BuyNow('" + dt.Rows[k]["id"].ToString() + "','1','0','0') class='add-to-cart buy-now'>Buy Now </a>";


                str += "<div class='item-img'><a href='" + Common.url(dt.Rows[k]["collectionname"].ToString()) + "/" + Common.url(dt.Rows[k]["categoryname"].ToString()) + "/" + Common.url(dt.Rows[k]["title"].ToString()) + "-" + dt.Rows[k]["id"].ToString() + ".html" + "'><img class='img-responsive' src='upload/products/large/" + dt.Rows[k]["Image"].ToString() + "' /></a></div>";

                str += "<div class='text'>";

                str += "<span class='title'><a href='" + Common.url(dt.Rows[k]["collectionname"].ToString()) + "/" + Common.url(dt.Rows[k]["categoryname"].ToString()) + "/" + Common.url(dt.Rows[k]["title"].ToString()) + "-" + dt.Rows[k]["id"].ToString() + ".html" + "'>" + dt.Rows[k]["title"].ToString() + "</a></span>";

                string srp = Convert.ToInt16(dt.Rows[k]["Offerprice"]).ToString();
                string mrp = Convert.ToInt16(dt.Rows[k]["MRP"]).ToString();

                decimal bal = Convert.ToDecimal(mrp) - Convert.ToDecimal(srp);

                decimal percnt = Math.Round((bal / Convert.ToDecimal(mrp)) * 100);

                str += "<div class='price-box'>";
                str += "<p class='price-detail'>";
                str += "<span class='special-price'>₹" + srp + "</span>";
                str += "<span class='old-price'>₹" + mrp + "</span>";
                str += "<span class='off-price'>off " + percnt + "%</span>";
                str += "</p>";
                str += "</div>";
                str += "</div>";


                // string btntext = "onclick=AddToCart('" + dt.Rows[k]["id"].ToString() + "','1','0','0')";

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

                        if (proid == Convert.ToDecimal(dt.Rows[k]["id"].ToString()))
                        {
                            matchpro = 1;
                            break;
                        }

                    }

                    str += "<div class='buttonBox'>";

                    if (matchpro == 1)
                    {
                        str += "<a class='add-to-cart' disabled style='background-color:#272625;' >Added to Cart</a><a href='javascript:void(0);' onclick=BuyNow('" + dt.Rows[k]["id"].ToString() + "','1','0','0') class='add-to-cart buy-now'>Buy Now </a>";
                    }
                    else
                    {
                        str += "<a class='add-to-cart pro" + dt.Rows[k]["id"].ToString() + "' href='javascript:void(0);'  onclick=AddToCart('" + dt.Rows[k]["id"].ToString() + "','1','0','0') >Add to Cart</a><a href='javascript:void(0);' onclick=BuyNow('" + dt.Rows[k]["id"].ToString() + "','1','0','0') class='add-to-cart buy-now'>Buy Now </a>";
                    }


                    str += "</div>";

                }
                else
                {
                    str += "<div class='buttonBox'>";
                    str += "<a class='add-to-cart pro" + dt.Rows[k]["id"].ToString() + "' href='javascript:void(0);'  onclick=AddToCart('" + dt.Rows[k]["id"].ToString() + "','1','0','0') >Add to Cart</a><a href='javascript:void(0);' onclick=BuyNow('" + dt.Rows[k]["id"].ToString() + "','1','0','0') class='add-to-cart buy-now'>Buy Now </a>";
                    str += "</div>";
                }


                str += "</div>";



            }
        }

        ltrprogallery.Text = str;
    }

}