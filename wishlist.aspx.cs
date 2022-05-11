using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class wishlist : System.Web.UI.Page
{

    FabAccessoriesEntities db = new FabAccessoriesEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getcart();
        }
    }


    private void getcart()
    {
        var currenyVal = 1.00m;
        decimal tot = 0.00m;
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
        //Literal lblCart = (Literal)this.Master.FindControl("ltritemcount");
        DataTable table = HttpContext.Current.Cache[cartp] as DataTable;
        if (table != null)
        {
            List<cart> crt = new List<cart>();
            foreach (DataRow cartRow in table.Rows)
            {
                cart c = new cart();
                c.Date = Convert.ToDateTime(cartRow["AddDate"].ToString());
                c.ext1 = cartRow["ext1"].ToString();
                c.ext2 = cartRow["ext2"].ToString();
                c.ext3 = cartRow["ext3"].ToString();
                c.remarks = cartRow["remarks"] == null ? "" : cartRow["remarks"].ToString() == "0" ? "" : cartRow["remarks"].ToString();
                c.ProductId = Convert.ToInt64(cartRow["ProductId"].ToString());
                var cartP = (from p in db.ProductMasters
                             join cat in db.CategoryMasters on p.CategoryId equals cat.CategoryId
                             join col in db.CollectionMasters on p.CollectionId equals col.CollectionId
                             join clr in db.ColorMasters on p.ColorId equals clr.ColorId into cm
                             from clr in cm.DefaultIfEmpty()
                             join sz in db.SizeMasters on p.SizeId equals sz.SizeId into sizem
                             from sz in sizem.DefaultIfEmpty()

                             where p.Id == c.ProductId
                             select new
                             {
                                 p.Id,
                                 p.SkuName,
                                 p.MRP,
                                 p.SRP,
                                 p.Description,
                                 p.Title,
                                 CollectionName = col.CollectionName,
                                 CategoryName = cat.CategoryName,
                                 price = p.MRP,
                                 saleprice = p.SRP,
                                 SizeName = sz.SizeName,
                                 ColorName = clr.ColorName,
                                 p.Image,
                                 p.Variations
                             }).ToList();
                var pCart = (from p in cartP

                             select new
                             {
                                 p.Id,
                                 p.SkuName,
                                 p.MRP,
                                 p.SRP,
                                 p.Description,
                                 p.Title,
                                 p.CollectionName,
                                 p.CategoryName,
                                 ProductPrice = Convert.ToDecimal(p.price) / currenyVal,
                                 discountprice = Convert.ToDecimal(Convert.ToDecimal(p.saleprice) / currenyVal).ToString("0.00"),
                                 p.SizeName,
                                 p.Image,
                                 p.ColorName,
                                 p.Variations
                                 //discountprice_old = Convert.ToDecimal((Convert.ToDecimal((Convert.ToDecimal(p.price) / currenyVal)) - Convert.ToDecimal((Convert.ToDecimal((Convert.ToDecimal(p.price) / currenyVal)) / 100) * Convert.ToDecimal(p.Discount)))).ToString("0.00")

                             }).FirstOrDefault();
                if (pCart != null)
                {
                    c.shortDesc = pCart.Title == null ? "" : pCart.Title;
                    c.unitPrice = Convert.ToDecimal(Convert.ToDecimal(pCart.ProductPrice).ToString("0.00"));
                    c.img = pCart.Image;
                    c.modelNo = pCart.SkuName;
                    c.ext1 = pCart.CollectionName;
                    c.ext2 = pCart.CategoryName;
                    c.discountprice = pCart.discountprice;
                    c.Color = pCart.ColorName;
                    c.Variation = pCart.Variations;
                    c.Size = pCart.SizeName;

                }
                else
                {
                    c.shortDesc = "";
                    c.unitPrice = 0.00m;
                    c.oldPrice = 0.00m;
                    c.img = "noimg.jpg";
                    c.modelNo = "";
                    c.discountprice = "0.00";
                    c.modelNo = "";
                    c.ext1 = "";
                    c.ext2 = "";
                    c.Color = "";
                    c.Variation = "";
                    c.Size = "";
                }

                c.Qty = Convert.ToInt16(cartRow["Qty"].ToString());

                //if (cartRow["remarks"] == null || cartRow["remarks"] == "0")
                //{
                //    c.remarks = "";
                //}
                //else
                //{
                //    c.remarks = cartRow["remarks"].ToString();
                //}
                if (cartRow["size"].ToString() != "")
                {
                    c.Size = cartRow["size"].ToString();
                }
                else
                {
                    c.Size = c.Size;
                }

                

                
               
               
                if (Convert.ToDecimal(c.unitPrice) > Convert.ToDecimal(c.discountprice))
                {
                    tot += Convert.ToDecimal(c.Qty) * Convert.ToDecimal(c.discountprice);
                }
                else
                {
                    tot += Convert.ToDecimal(c.Qty) * Convert.ToDecimal(c.unitPrice);
                }
                //DataSet ds = DB.Business.SPs.ApProductHomeProcedure("", c.ProductId.ToString(), "", SessionState.currency, "checkqty").GetDataSet();
                //DataRow[] dr = table.Select("productId='" + c.ProductId.ToString() + "'");
                //int rowIndex = table.Rows.IndexOf(dr[0]);
                //if (Convert.ToInt32(ds.Tables[0].Rows[0]["balance"]) >= Convert.ToInt32(c.Qty))
                //{
                //    c.qtystatus = " In Stock:" + ds.Tables[0].Rows[0]["balance"].ToString();

                //}
                //else
                //{
                //    if (Convert.ToInt32(ds.Tables[0].Rows[0]["remade"]) == 1)
                //    {
                //        string qty = (Convert.ToInt32(c.Qty) - Convert.ToInt32(ds.Tables[0].Rows[0]["balance"])).ToString();
                //        if (ds.Tables[0].Rows[0]["balance"].ToString() == "0")
                //        {
                //            c.qtystatus = " On order:" + qty;
                //        }
                //        else
                //        {
                //            c.qtystatus = "In Stock:" + ds.Tables[0].Rows[0]["balance"] + " | On order:" + qty;
                //        }
                //    }
                //    else
                //    {
                //        c.qtystatus = " In Stock:" + ds.Tables[0].Rows[0]["balance"].ToString();
                //    }
                //}
                //table.Rows[rowIndex]["qtystatus"] = c.qtystatus;
                HttpContext.Current.Cache.Insert(cartp, table, null, DateTime.Now.AddDays(5), System.Web.Caching.Cache.NoSlidingExpiration);
                crt.Add(c);
               
                // pids.Add(c.ProductId);
            }

            //ltrlSubTotal.Text = ltrlNetTotal.Text = Math.Round(Convert.ToDecimal((Convert.ToDecimal(tot)).ToString())).ToString("0");
            repCart.DataSource = crt.ToList();
            repCart.DataBind();
            // join cp in cart on p.ProductId equals cartP.ToList()
            //var cartProduct = db.ProductMasters.Where(r=>crt.Contains(r.ProductId))
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "", "funOpen()", true);
            // lblCart.Text = table.Rows.Count.ToString(); ;
        }
        else
        {
            divCart.Style.Add("display", "none");
            divEmptyCart.Style.Add("display", "block");
            //lblCart.Text = "0";
            //ltrlSubTotal.Text = ltrlNetTotal.Text = Math.Round(Convert.ToDecimal("0.00")).ToString("0");
        }
    }

    public class variationlist
    {
        public string variationname { get; set; }
        public string grpname { get; set; }
    }

    public class cart
    {
        public Int64 ProductId { get; set; }
        public DateTime Date { get; set; }
        public int Qty { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string ext1 { get; set; }
        public string ext2 { get; set; }
        public string ext3 { get; set; }
        public string shortDesc { get; set; }
        public string modelNo { get; set; }
        public string img { get; set; }
        public decimal unitPrice { get; set; }
        public decimal oldPrice { get; set; }
        public string discountprice { get; set; }
        public string qtystatus { get; set; }
        public string remarks { get; set; }
        public string Variation { get; set; }
    }

    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        TextBox txtQty = (TextBox)sender;
        RepeaterItem gr = (RepeaterItem)txtQty.NamingContainer;
        HiddenField hddId = (HiddenField)gr.FindControl("hddId");
        Literal alerttext = (Literal)gr.FindControl("alerttext");
        Label instockerror = (Label)gr.FindControl("instockerror");
        // Literal alerttext = (Literal)gr.FindControl("alerttext");
        string cartp = "";
        string addqtystatus = "1";
        DataSet ds = new DataSet();
        //ds = DB.Business.SPs.ApProductHomeProcedure("", hddId.Value, "", SessionState.currency, "checkqty").GetDataSet();
        //if (Convert.ToInt32(ds.Tables[0].Rows[0]["balance"]) >= Convert.ToInt32(txtQty.Text))
        //{
        //    addqtystatus = "1";
        //}
        //else
        //{
        //    if (Convert.ToInt32(ds.Tables[0].Rows[0]["remade"]) == 1)
        //    {
        //        addqtystatus = "1";
        //        // string qty = (Convert.ToInt32(txtQty.Text) - Convert.ToInt32(ds.Tables[0].Rows[0]["balance"])).ToString();
        //        // alerttext.Text = "In Stock:" + ds.Tables[0].Rows[0]["balance"] + " | On order:" + qty;
        //    }
        //    else
        //    {
        //        addqtystatus = "0";
        //        txtQty.Text = ds.Tables[0].Rows[0]["balance"].ToString();
        //        alerttext.Text = "In Stock:" + ds.Tables[0].Rows[0]["balance"];
        //        instockerror.Text = "You can not add more then " + ds.Tables[0].Rows[0]["balance"] + " item";

        //    }
        //}
        if (addqtystatus == "1")
        {
            if (Convert.ToInt32(txtQty.Text) > 0)
            {

            }
            else
            {
                txtQty.Text = "1";
            }
            if (HttpContext.Current.Request.Cookies["fabwish"] != null)
            {
                cartp = HttpContext.Current.Request.Cookies["fabwish"].Value;
            }

            DataTable table = HttpContext.Current.Cache[cartp] as DataTable;

            if (table != null)
            {
                DataRow[] dr = table.Select("productId='" + hddId.Value + "'");
                if (dr.Length > 0)
                {
                    int rowIndex = table.Rows.IndexOf(dr[0]);

                    if (txtQty.Text.Trim() != "")
                    {
                        table.Rows[rowIndex]["Qty"] = txtQty.Text;
                        //table.Rows[rowIndex]["qtystatus"] = alerttext.Text;
                    }
                    else
                    {
                        table.Rows[rowIndex]["Qty"] = 1;
                        //table.Rows[rowIndex]["qtystatus"] = alerttext.Text;
                    }
                }
            }
            HttpContext.Current.Cache.Insert(cartp, table, null, DateTime.Now.AddDays(5), System.Web.Caching.Cache.NoSlidingExpiration);
            getcart();
        }

        else
        {
            //   BindCart();
        }
    }

    protected void repCart_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string cartp = "";
        if (HttpContext.Current.Request.Cookies["fabwish"] != null)
        {
            cartp = HttpContext.Current.Request.Cookies["fabwish"].Value;
        }
        DataTable table = HttpContext.Current.Cache[cartp] as DataTable;
        if (table != null)
        {
            DataRow[] dr = table.Select("productId='" + e.CommandArgument.ToString() + "'");
            table.Rows.Remove(dr[0]);
            if (table.Rows.Count > 0)
            {
                HttpContext.Current.Cache.Insert(cartp, table, null, DateTime.Now.AddDays(5), System.Web.Caching.Cache.NoSlidingExpiration);
            }
            else
            {
                var cookie = new HttpCookie("fabwish", "")
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                HttpContext.Current.Response.Cookies.Add(cookie);
                HttpContext.Current.Cache.Remove(cartp);
            }
            getcart();

            Label lbl = (Label)this.Master.FindControl("lblwishlistitems");

            lbl.Text = table.Rows.Count.ToString();
        }
    }


    //protected void txtremarks_TextChanged(object sender, EventArgs e)
    //{
    //    TextBox txtremrks = (TextBox)sender;
    //    RepeaterItem gr = (RepeaterItem)txtremrks.NamingContainer;
    //    HiddenField hddId = (HiddenField)gr.FindControl("hddId");
    //    string cartp = "";

    //    if (HttpContext.Current.Request.Cookies["fabcart"] != null)
    //    {
    //        cartp = HttpContext.Current.Request.Cookies["fabcart"].Value;
    //    }

    //    DataTable table = HttpContext.Current.Cache[cartp] as DataTable;

    //    if (table != null)
    //    {
    //        DataRow[] dr = table.Select("productId='" + hddId.Value + "'");
    //        if (dr.Length > 0)
    //        {
    //            int rowIndex = table.Rows.IndexOf(dr[0]);

    //            table.Rows[rowIndex]["remarks"] = txtremrks.Text.Trim();
    //        }
    //    }
    //    HttpContext.Current.Cache.Insert(cartp, table, null, DateTime.Now.AddDays(5), System.Web.Caching.Cache.NoSlidingExpiration);

    //}

    protected void repCart_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DropDownList drpvariatioin = (DropDownList)e.Item.FindControl("drpvariatioin");
        HiddenField hfvariation = (HiddenField)e.Item.FindControl("hfvariation");

        HiddenField hfsize = (HiddenField)e.Item.FindControl("hfsize");
        HiddenField hfcolor = (HiddenField)e.Item.FindControl("hfcolor");
        HiddenField hfremarks = (HiddenField)e.Item.FindControl("hfremarks");

        HtmlGenericControl dvvariation = (HtmlGenericControl)e.Item.FindControl("dvvariation");

        if (drpvariatioin != null)
        {
            string[] vr = hfvariation.Value.ToString().Split(',');

            if (vr.Length > 0 && vr[0] != "")
            {

                List<variationlist> lstvar = new List<variationlist>();
                if (hfsize.Value != "" || hfcolor.Value != "")
                {
                    variationlist vl1 = new variationlist();
                    vl1.variationname = hfsize.Value + "-" + hfcolor.Value;
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

                drpvariatioin.DataSource = lstvar;
                drpvariatioin.DataTextField = "variationname";
                drpvariatioin.DataValueField = "variationname";
                drpvariatioin.DataBind();



                for (int k = 0; k < lstvar.Count; k++)
                {
                    if (lstvar[k].variationname == hfremarks.Value)
                    {
                        drpvariatioin.SelectedValue = lstvar[k].variationname;
                        break;
                    }
                }


            }
            else
            {

                if (hfsize.Value == "" && hfcolor.Value == "")
                {
                    dvvariation.Visible = false;
                }
                else
                {
                    List<variationlist> lstvar = new List<variationlist>();
                    variationlist vl1 = new variationlist();
                    vl1.variationname = hfsize.Value + "-" + hfcolor.Value;
                    vl1.grpname = "vari";

                    lstvar.Add(vl1);

                    drpvariatioin.DataSource = lstvar;
                    drpvariatioin.DataTextField = "variationname";
                    drpvariatioin.DataValueField = "variationname";
                    drpvariatioin.DataBind();
                }
            }
        }
        else
        {
            dvvariation.Visible = false;
        }
    }
    protected void drpvariatioin_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList drpvariation = (DropDownList)sender;
        RepeaterItem gr = (RepeaterItem)drpvariation.NamingContainer;
        HiddenField hddId = (HiddenField)gr.FindControl("hddId");
        string cartp = "";

        if (HttpContext.Current.Request.Cookies["fabwish"] != null)
        {
            cartp = HttpContext.Current.Request.Cookies["fabwish"].Value;
        }

        DataTable table = HttpContext.Current.Cache[cartp] as DataTable;

        if (table != null)
        {
            DataRow[] dr = table.Select("productId='" + hddId.Value + "'");
            if (dr.Length > 0)
            {
                int rowIndex = table.Rows.IndexOf(dr[0]);

                table.Rows[rowIndex]["remarks"] = drpvariation.SelectedItem.Text;
            }
        }
        HttpContext.Current.Cache.Insert(cartp, table, null, DateTime.Now.AddDays(5), System.Web.Caching.Cache.NoSlidingExpiration);

    }


    protected void lnkaddtocart_Click(object sender, EventArgs e)
    {
        try
        {


            LinkButton lnkcart = (LinkButton)sender;
            RepeaterItem rpt = (RepeaterItem)lnkcart.NamingContainer;

            HiddenField hddId = (HiddenField)rpt.FindControl("hddId");
            HiddenField hfsize = (HiddenField)rpt.FindControl("hfsize");
            DropDownList drpvariatioin = (DropDownList)rpt.FindControl("drpvariatioin");

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
            DataRow[] drExists = table.Select("productId='" + hddId.Value + "'");
            if (drExists.Count() <= 0)
            {

                string remark = "";

                for (int k = 0; k < drpvariatioin.Items.Count; k++)
                {
                    if (drpvariatioin.Items[k].Selected == true)
                    {
                        remark = drpvariatioin.Items[k].ToString();
                        break;
                    }
                }



                DataRow dr = table.NewRow();
                dr["ProductId"] = hddId.Value;
                dr["Qty"] = "1";
                dr["size"] = hfsize.Value.Trim().ToString();
                dr["remarks"] = remark;
                dr["AddDate"] = DateTime.Now;
                dr["qtystatus"] = "";
                table.Rows.Add(dr);
            }
            HttpContext.Current.Cache.Insert(cartp, table, null, DateTime.Now.AddDays(5), System.Web.Caching.Cache.NoSlidingExpiration);

           

            Label lbl = (Label)this.Master.FindControl("lblcartitems");

            lbl.Text = table.Rows.Count.ToString();



            string cartpwish = "";
            if (HttpContext.Current.Request.Cookies["fabwish"] != null)
            {
                cartpwish = HttpContext.Current.Request.Cookies["fabwish"].Value;
            }
            DataTable tablewish = HttpContext.Current.Cache[cartpwish] as DataTable;
            if (tablewish != null)
            {
                DataRow[] dr = tablewish.Select("productId='" + hddId.Value + "'");
                tablewish.Rows.Remove(dr[0]);
                if (tablewish.Rows.Count > 0)
                {
                    HttpContext.Current.Cache.Insert(cartpwish, tablewish, null, DateTime.Now.AddDays(5), System.Web.Caching.Cache.NoSlidingExpiration);
                }
                else
                {
                    var cookie = new HttpCookie("fabwish", "")
                    {
                        Expires = DateTime.Now.AddDays(-1)
                    };
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    HttpContext.Current.Cache.Remove(cartpwish);
                }
                getcart();

                Label lbl1 = (Label)this.Master.FindControl("lblwishlistitems");

                lbl1.Text = tablewish.Rows.Count.ToString();
            }




        }
        catch
        {

        }
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


}