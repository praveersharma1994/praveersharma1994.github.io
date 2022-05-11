using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Transactions;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
public partial class Admin_StockEntry : System.Web.UI.Page
{
    FabAccessoriesEntities db = new FabAccessoriesEntities();
    public string tempPath = "upload/Products/";

    DataSet ds = new DataSet(); DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            binddropdowns(); bindproducts();

            if (Request.QueryString["Proid"] != null)
            {
                if (Request.QueryString["Proid"].ToString() != "")
                {
                    EditMode(Request.QueryString["Proid"].ToString());
                }
            }

            bindfeatures();

        }
    }

    void bindfeatures()
    {
        var feaM = db.FeatureMasters.ToList();

        ViewState["feaM"] = feaM;
        grdFeatures.DataSource = feaM.ToList();
        grdFeatures.DataBind();
    }

    private void binddropdowns()
    {
        //-------------- CollectionMasters-----------
        var col = db.CollectionMasters.ToList();
        drpCollection.DataSource = col;
        drpCollection.DataTextField = "CollectionName";
        drpCollection.DataValueField = "CollectionId";
        drpCollection.DataBind();
        //-------------- CategoryMasters-----------
        var cat = db.CategoryMasters.ToList();
        drpCategory.DataSource = cat;
        drpCategory.DataTextField = "CategoryName";
        drpCategory.DataValueField = "CategoryId";
        drpCategory.DataBind();
        //-------------- MaterialMasters-----------
        var mat = db.MaterialMasters.ToList();
        drpMaterial.DataSource = mat;
        drpMaterial.DataTextField = "Material";
        drpMaterial.DataValueField = "MaterialId";
        drpMaterial.DataBind();

        var sz = db.SizeMasters.OrderBy(r => r.SizeName).ToList();
        drpsize.DataSource = sz;
        drpsize.DataTextField = "SizeName";
        drpsize.DataValueField = "SizeId";
        drpsize.DataBind();

        drpsize.Items.Insert(0, new ListItem("--Select--", "0"));

        var colr = db.ColorMasters.OrderBy(r => r.ColorName).ToList();
        drpcolor.DataSource = colr;
        drpcolor.DataTextField = "ColorName";
        drpcolor.DataValueField = "ColorId";
        drpcolor.DataBind();

        drpcolor.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void btnSeach_Click(object sender, EventArgs e)
    {

    }

    protected void drpPagging_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }

    void uploadotherImg(int id)
    {
        string fn = "";
        if (removedfile.Value != "")
        {
            string[] rem = removedfile.Value.Split(',');
            for (int r = 0; r < rem.Length; r++)
            {
                fn = rem[r].ToString();
                var a = db.OtherImages.Where(rs => rs.ProductId == id && rs.ImageName == fn).FirstOrDefault();
                if (a != null)
                {
                    db.OtherImages.Remove(a);
                    db.SaveChanges();
                }
                if (File.Exists(Server.MapPath("~/upload/Products/OtherSmall/") + rem[r]))
                {
                    File.Delete(Server.MapPath("~/upload/Products/OtherSmall/") + rem[r]);
                }
                if (File.Exists(Server.MapPath("~/upload/Products/OtherLarge/") + rem[r]))
                {
                    File.Delete(Server.MapPath("~/upload/Products/OtherLarge/") + rem[r]);
                }
            }
        }

        HttpFileCollection hff = Request.Files;
        int i = 0;
        if (fluMain.HasFile)
        {
            i = 1;
        }

        for (; i < hff.Count; i++)
        {
            HttpPostedFile hfFile = hff[i];
            if (hfFile.ContentLength > 0)
            {
                string filename = id + "@" + (Common.GetRandomAlphaNumeric()) + Path.GetExtension(hfFile.FileName);

                //string path = Path.Combine("~/" + tempPath + "/OtherSmall/", filename);
                string path = Server.MapPath("~/" + tempPath + "/OtherSmall/" + filename);
               // string small = Server.MapPath("~/" + tempPath + "/temp/othersmall/" + filename);
                var targetFilethumb = path; // HttpContext.Current.Server.MapPath(path);
                Stream strm1 = hfFile.InputStream;
                ImgResize.img(0.4, strm1, targetFilethumb, 200, 200);

                //string pathl = Path.Combine("~/" + tempPath + "/OtherLarge/", filename);
                string pathl = Server.MapPath("~/" + tempPath + "/OtherLarge/" + filename);
               // string large = Server.MapPath("~/" + tempPath + "/temp/otherlarge/" + filename);
                var targetFile = pathl; // HttpContext.Current.Server.MapPath(pathl);



                Stream strm = hfFile.InputStream;
                ImgResize.img(0.4, strm, targetFile, 600, 600);


                //string watermarkImg = Server.MapPath("~/images/fab-wtrMark1.png");
                //string watermarkImgSmall = Server.MapPath("~/images/fab-wtrMark2.png");

                //CreateWaterMark1(large, watermarkImg, pathl);
                ////File.Delete(targetPath);
                //// GenerateMedium(0.4, strm, large);
                //CreateWaterMark1(small, watermarkImgSmall, path);



                OtherImage othrimg = new OtherImage();
                othrimg.ImageName = filename;
                othrimg.ProductId = id;
                othrimg.Displayorder = i;
                db.OtherImages.Add(othrimg);
                db.SaveChanges();
            }
        }
    }

    void CreateWaterMark1(string mainImg, string watermark, string savePath)
    {
        System.Drawing.Image image = System.Drawing.Image.FromFile(@mainImg);//This is the background image 
        System.Drawing.Image logo = System.Drawing.Image.FromFile(@watermark); //This is your watermark 
        Graphics g = System.Drawing.Graphics.FromImage(image); //Create graphics object of the background image //So that you can draw your logo on it
        Bitmap TransparentLogo = new Bitmap(logo.Width, logo.Height); //Create a blank bitmap object //to which we //draw our transparent logo
        Graphics TGraphics = Graphics.FromImage(TransparentLogo);//Create a graphics object so that //we can draw //on the blank bitmap image object
        ColorMatrix ColorMatrix = new ColorMatrix(); //An image is represenred as a 5X4 matrix(i.e 4 //columns and 5 //rows) 
        ColorMatrix.Matrix33 = 1F;//the 3rd element of the 4th row represents the transparency 
        ImageAttributes ImgAttributes = new ImageAttributes();//an ImageAttributes object is used to set all //the alpha //values.This is done by initializing a color matrix and setting the alpha scaling value in the matrix.The address of //the color matrix is passed to the SetColorMatrix method of the //ImageAttributes object, and the //ImageAttributes object is passed to the DrawImage method of the Graphics object.
        ImgAttributes.SetColorMatrix(ColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap); TGraphics.DrawImage(logo, new Rectangle(0, 0, TransparentLogo.Width, TransparentLogo.Height), 0, 0, TransparentLogo.Width, TransparentLogo.Height, GraphicsUnit.Pixel, ImgAttributes);
        TGraphics.Dispose();
        g.DrawImage(TransparentLogo, 0, 1);

        // File.Delete(mainImg);
        image.Save(savePath, ImageFormat.Jpeg);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtQty.Text.Trim() == "")
        {
            txtQty.Text = "1";
        }

        if (txtsrp.Text.Trim() == "")
        {
            txtsrp.Text = "0.00";
        }

        if (txtmrp.Text.Trim() == "")
        {
            txtmrp.Text = "0.00";
        }

        if (fluMain.HasFile)
        {
            hddImg.Value = Common.url(txtSKU.Text) + Path.GetExtension(fluMain.FileName);
            string fileName = hddImg.Value;

            //string pathm = Path.Combine("~/" + tempPath + "/small/", fileName);
            string pathm = Server.MapPath("~/" + tempPath + "/small/" + fileName);
           // string small = Server.MapPath("~/" + tempPath + "/temp/small/" + fileName);

            var targetFileM = pathm; // HttpContext.Current.Server.MapPath(pathm);
            Stream strm1 = fluMain.PostedFile.InputStream;
            ImgResize.img(0.4, strm1, targetFileM, 200, 200);

            //string pathl = Path.Combine("~/" + tempPath + "/large/", fileName);
            string pathl = Server.MapPath("~/" + tempPath + "/large/" + fileName);
            //string large = Server.MapPath("~/" + tempPath + "/temp/large/" + fileName);

            var targetFileL = pathl;// HttpContext.Current.Server.MapPath(large);
            Stream strm2 = fluMain.PostedFile.InputStream;
            ImgResize.img(0.4, strm2, targetFileL, 600, 600);

            //string watermarkImg = Server.MapPath("~/images/fab-wtrMark1.png");
            //string watermarkImgSmall = Server.MapPath("~/images/fab-wtrMark2.png");

            //CreateWaterMark1(large, watermarkImg, pathl);
            ////File.Delete(targetPath);
            //// GenerateMedium(0.4, strm, large);
            //CreateWaterMark1(small, watermarkImgSmall, pathm);

            



        }
        if (btnSave.Text == "Submit")
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                var pm1 = db.ProductMasters.Where(r => r.SkuName == txtSKU.Text.Trim()).FirstOrDefault();
                if (pm1 == null)
                {
                    ProductMaster pm = new ProductMaster();
                    pm.CollectionId = Convert.ToInt32(drpCollection.SelectedValue);
                    pm.CategoryId = Convert.ToInt32(drpCategory.SelectedValue);
                    pm.Description = txtDescription.Text.Trim();
                    pm.Grosswt = Convert.ToDecimal("0.000");
                    pm.IsCommon = 0;
                    pm.MaterialId = Convert.ToInt32(drpMaterial.SelectedValue);
                    pm.MDate = DateTime.Now;
                    pm.MRP = Convert.ToDecimal(txtmrp.Text.Trim());
                    pm.NetWt = Convert.ToDecimal("0.000");
                    pm.ShowHide = 1;
                    pm.Offerprice = 0;
                    pm.SkuName = txtSKU.Text.Trim();
                    pm.SRP = Convert.ToDecimal(txtsrp.Text.Trim());
                    pm.StockPcs = Convert.ToInt32(txtQty.Text.Trim());
                    pm.Title = txtTitle.Text.Trim();
                    pm.Image = hddImg.Value;
                    pm.OtherImage = "";
                    pm.SizeId = Convert.ToInt64(drpsize.SelectedValue);
                    pm.ColorId = Convert.ToInt64(drpcolor.SelectedValue);

                    pm.Variations = txtvariations.Text.Trim();

                    //pm.AvlColors = txtavlcolors.Text.Trim();
                    //pm.AvlSizes = txtavlsizes.Text.Trim();

                    pm.GenderType = drpgender.SelectedValue;
                    db.ProductMasters.Add(pm);
                    db.SaveChanges();
                    uploadotherImg(Convert.ToInt32(pm.Id));
                    ts.Complete();
                    ts.Dispose();


                    var pid = pm.Id;
                    foreach (GridViewRow row in grdFeatures.Rows)
                    {
                        HiddenField hddFeatureId = (HiddenField)row.FindControl("hddFeatureId");
                        TextBox txtValue = (TextBox)row.FindControl("txtValue");
                        ProductFeature fea = new ProductFeature();
                        if (hddFeatureId != null)
                        {
                            fea.FeatureId = Convert.ToInt32(hddFeatureId.Value);
                            fea.FeatureValue = txtValue.Text;
                            fea.ProductId = Convert.ToInt64(pid);
                            db.ProductFeatures.Add(fea);
                        }
                        db.SaveChanges();

                    }



                    clear();
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Product has been saved successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('SKU Already Inserted');", true);
                }
            }
        }

        else if (btnSave.Text == "Update")
        {
            Int64 pid = Convert.ToInt64(hddId.Value);
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                var pm = db.ProductMasters.Where(r => r.Id == pid).FirstOrDefault();
                pm.CollectionId = Convert.ToInt32(drpCollection.SelectedValue);
                pm.CategoryId = Convert.ToInt32(drpCategory.SelectedValue);
                pm.Description = txtDescription.Text.Trim();
                pm.Grosswt = Convert.ToDecimal("0.000");
                pm.IsCommon = 0;
                pm.MaterialId = Convert.ToInt32(drpMaterial.SelectedValue);
                pm.MDate = DateTime.Now;
                pm.MRP = Convert.ToDecimal(txtmrp.Text.Trim());
                pm.NetWt = Convert.ToDecimal("0.000");
                pm.ShowHide = 1;
                pm.Offerprice = 0;
                pm.SkuName = txtSKU.Text.Trim();
                pm.SRP = Convert.ToDecimal(txtsrp.Text.Trim());
                pm.StockPcs = Convert.ToInt32(txtQty.Text.Trim());
                pm.Image = hddImg.Value;
                pm.OtherImage = "";
                pm.Title = txtTitle.Text.Trim();
                pm.SizeId = Convert.ToInt64(drpsize.SelectedValue);
                pm.ColorId = Convert.ToInt64(drpcolor.SelectedValue);

                pm.Variations = txtvariations.Text.Trim();

                // pm.AvlColors = txtavlcolors.Text.Trim();
                // pm.AvlSizes = txtavlsizes.Text.Trim();

                pm.GenderType = drpgender.SelectedValue;
                db.SaveChanges();
                uploadotherImg(Convert.ToInt32(pm.Id));
                hddId.Value = pid.ToString();
                ts.Complete();
                ts.Dispose();
                //clear();



                foreach (GridViewRow row in grdFeatures.Rows)
                {
                    HiddenField hddFeatureId = (HiddenField)row.FindControl("hddFeatureId");
                    TextBox txtValue = (TextBox)row.FindControl("txtValue");

                    if (hddFeatureId != null)
                    {
                        Int32 feeId = Convert.ToInt16(hddFeatureId.Value);
                        var fe = db.ProductFeatures.Where(r => r.ProductId == pid && r.FeatureId == feeId).FirstOrDefault();
                        if (fe != null)
                        {
                            fe.FeatureValue = txtValue.Text;
                        }
                        else
                        {
                            ProductFeature fea = new ProductFeature();
                            fea.FeatureId = Convert.ToInt32(hddFeatureId.Value);
                            fea.FeatureValue = txtValue.Text;
                            fea.ProductId = Convert.ToInt64(pid);
                            db.ProductFeatures.Add(fea);
                        }
                    }
                    db.SaveChanges();
                }


                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('Product has been updated successfully');", true);
            }
        }
    }

    void clear()
    {
        txtDescription.Text = txtmrp.Text = txtQty.Text = txtSKU.Text = txtsrp.Text = txtTitle.Text = hddId.Value = hddImg.Value = removedfile.Value = "";
        txtSKU.Enabled = true;
        drpsize.SelectedIndex = drpcolor.SelectedIndex = 0;
        drpgender.SelectedValue = "N";
        repimages.DataSource = null;
        repimages.DataBind();
        imgPriview.ImageUrl = "../images/noimage.jpg";
        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "changeurl();", true);
    }

    [WebMethod]
    public static string checkExist(string sku, string pid)
    {
        FabAccessoriesEntities db = new FabAccessoriesEntities();
        int prodid = Convert.ToInt32(pid);
        string response = "0";
        try
        {
            if (pid != "" && pid != "0")
            {
                var chk = db.ProductMasters.Where(r => r.SkuName == sku && r.Id != prodid).FirstOrDefault();
                if (chk != null)
                {
                    response = "1";
                }
            }
            else
            {
                var chk = db.ProductMasters.Where(r => r.SkuName == sku).FirstOrDefault();
                if (chk != null)
                {
                    response = "1";
                }
            }
        }
        catch (Exception ex)
        {
            Common.LogError(ex);
            response = "2";
        }
        return response;
    }

    private void bindproducts()
    {
        try
        {
            List<SqlParameter> param = new List<SqlParameter>();
            var query = "select pm.*,cm.categoryname,isnull((select STUFF((SELECT distinct ',' + t1.imagename  from otherimage t1 where pm.id = t1.productid FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,1,'')),'') as otherimage1 from productmaster pm left join categorymaster cm on cm.categoryid = pm.categoryid where pm.id >0 order by pm.id desc";
            var filter = "";
            DataTable dtrecords = DataAccess.GetDataTable(query + filter, CommandType.Text, param.ToArray());
            int pagesize = Convert.ToInt16(drpPagging.SelectedValue);
            GridView2.PageSize = pagesize;
            GridView2.DataSource = dtrecords;
            GridView2.DataBind();
        }
        catch (Exception ex)
        {
            Common.LogError(ex);
        }
    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        bindproducts();
    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Int32 id = Convert.ToInt32(e.CommandArgument.ToString());
        if (e.CommandName.ToLower() == "edititem")
        {
            EditMode(id.ToString());
        }

        if (e.CommandName.ToLower() == "deleteitem")
        {
            var delproid = db.OrderDetails.Where(r => r.ProductId == id).FirstOrDefault();
            if (delproid == null)
            {
                var prodid = db.ProductMasters.Where(r => r.Id == id).FirstOrDefault();
                db.ProductMasters.Remove(prodid);
                db.SaveChanges();
                bindproducts();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "opendiv();alert('Delete successfully')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "aa", "alert('This product is in orderlist')", true);
            }
        }
    }

    void EditMode(string id)
    {
        txtSKU.Enabled = false;
        hddId.Value = id;
        decimal ids = Convert.ToDecimal(id);
        var prod = db.ProductMasters.FirstOrDefault(r => r.Id == ids);
        if (prod != null)
        {
            txtSKU.Text = prod.SkuName;
            txtDescription.Text = prod.Description;
            txtmrp.Text = Convert.ToDecimal(prod.MRP).ToString("0.00");
            txtQty.Text = prod.StockPcs.ToString();
            txtsrp.Text = Convert.ToDecimal(prod.SRP).ToString("0.00");
            txtTitle.Text = prod.Title;

            drpCategory.SelectedValue = prod.CategoryId.ToString();
            drpMaterial.SelectedValue = prod.MaterialId.ToString();

            drpsize.SelectedValue = prod.SizeId.ToString();
            drpcolor.SelectedValue = prod.ColorId.ToString();
            drpgender.SelectedValue = prod.GenderType.ToString();

            txtvariations.Text = prod.Variations;

            //txtavlsizes.Text = prod.AvlSizes;
            //txtavlcolors.Text = prod.AvlColors;

            hddImg.Value = prod.Image;

            var otherimg = db.OtherImages.Where(r => r.ProductId == ids).Select(r => r.ImageName).ToList();
            if (otherimg != null && otherimg.Count > 0)
            {
                repimages.DataSource = otherimg;
                repimages.DataBind();
            }
            else
            {
                repimages.DataSource = null;
                repimages.DataBind();
            }

            if (prod.Image == "")
            {
                imgPriview.ImageUrl = "../images/noimage.jpg";
            }
            else
            {
                imgPriview.ImageUrl = "../" + tempPath + "small/" + prod.Image;
            }

            btnSave.Text = "Update";
        }
    }

    protected void grdFeatures_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        HiddenField hddFeatureId = (HiddenField)e.Row.FindControl("hddFeatureId");
        if (hddFeatureId != null)
        {
            if (hddId.Value.Trim().Length > 0)
            {
                Int64 fId = Convert.ToInt64(hddFeatureId.Value);
                Int64 pId = Convert.ToInt64(hddId.Value);
                var pf = db.ProductFeatures.Where(r => r.FeatureId == fId && r.ProductId == pId).FirstOrDefault();
                if (pf != null)
                {
                    TextBox txtValue = (TextBox)e.Row.FindControl("txtValue");
                    txtValue.Text = pf.FeatureValue;
                }
            }
        }
    }
}