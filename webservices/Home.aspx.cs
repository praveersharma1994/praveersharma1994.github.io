using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;


public partial class webservices_Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bindslider();
    }

    private void bindslider()
    {
        DataTable dt = new DataTable();
        List<Homepage> hmp = new List<Homepage>();
        List<bannerimage> mbban = new List<bannerimage>();
        List<itembannerimage> itmban = new List<itembannerimage>();
        List<bannerimage> colban = new List<bannerimage>();
        List<exibition> exbt = new List<exibition>();
        List<collection> collist = new List<collection>();
        List<menubar> menuist = new List<menubar>();
        string sliderurl = "";
        //------------------------------ Main banner --------------------------//
        sliderurl = "http://www.fabfashionaccessories.com/upload/banner/";
        dt = DataAccess.GetDataTable("select * from Banner where bannerof='app'", CommandType.Text);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bannerimage bn = new bannerimage();
                string image = sliderurl + dt.Rows[i]["bannerimg"].ToString();
                bn.imagename = image;
                bn.url = dt.Rows[i]["BannerUrl"].ToString();
                mbban.Add(bn);
            }
        }

        //------------------------------ Item Banner --------------------------//
        sliderurl = "http://www.fabfashionaccessories.com/upload/Itembanner/";
        dt = DataAccess.GetDataTable("select * from itembanner", CommandType.Text);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                itembannerimage itmbn = new itembannerimage();
                string image = sliderurl + dt.Rows[i]["imagename"].ToString();
                itmbn.imagename = image;
                itmbn.url = dt.Rows[i]["ImageUrl"].ToString();
                itmbn.name = dt.Rows[i]["item"].ToString();
                itmban.Add(itmbn);
            }
        }

        //------------------------------ CollectionBanner Banner --------------------------//
        sliderurl = "http://www.fabfashionaccessories.com/upload/mobile/SpecialBanner/";
        dt = DataAccess.GetDataTable("select * from CollectionBanner ", CommandType.Text);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bannerimage colbn = new bannerimage();
                string image = sliderurl + dt.Rows[i]["Imgname"].ToString();
                colbn.imagename = image;
                colbn.url = dt.Rows[i]["Url"].ToString();
                colban.Add(colbn);
            }
        }

        //------------------------------ Exibition --------------------------//
        dt = DataAccess.GetDataTable("select * from exibition where  CONVERT(date,todate,106)>=CONVERT(date,DATEADD(minute,750,getdate()),106)", CommandType.Text);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                exibition exb = new exibition();

                exb.ExibitionId = dt.Rows[i]["ExibitionId"].ToString();
                exb.ExibitionName = dt.Rows[i]["ExibitionName"].ToString();
                exb.Exibitiondate = dt.Rows[i]["Exibitiondate"].ToString();
                exb.ExibitionPlace = dt.Rows[i]["ExibitionPlace"].ToString();
                exb.HallNo = dt.Rows[i]["HallNo"].ToString();
                exb.BoothNo = dt.Rows[i]["BoothNo"].ToString();
                exbt.Add(exb);
            }
        }


        //------------------------------ collection --------------------------//
        dt = DataAccess.GetDataTable("select distinct(cm.categoryname) as itemname from productmaster pm left join categorymaster cm on cm.categoryid = pm.categoryid where cm.categoryname !='-' and showhide=1 and offerprice=0 order by categoryname asc", CommandType.Text);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                collection col = new collection();
                col.collectionName = dt.Rows[i]["itemname"].ToString();
                collist.Add(col);
            }
        }

        //------------------------------ menubar --------------------------//
        DataSet ds = DataAccess.GetDataSet("select count(*) as collection from productmaster where showhide=1 and Offerprice=0;select count (*) as catalog from catalogs;select count(*) as promotion from productmaster where showhide=1 and Offerprice>0;select count(*) as exibition from exibition where CONVERT(date,todate,106)>=CONVERT(date,DATEADD(minute,750,getdate()),106)  ", CommandType.Text);
        menubar menu0 = new menubar();
        //----------- Home
        menu0.menu = "Home";
        menu0.id = "0";
        menu0.show = "true";
        menuist.Add(menu0);
        //----------- About
        menubar menu1 = new menubar();
        menu1.menu = "About";
        menu1.id = "1";
        menu1.show = "true";
        menuist.Add(menu1);
        //----------- New Arrivals
        menubar menu2 = new menubar();
        menu2.menu = "New Arrivals";
        menu2.id = "2";
        if (ds.Tables[0].Rows[0]["collection"] != null)
        {
            menu2.show = "true";
        }
        else
        {
            menu2.show = "fasle";
        }
        menuist.Add(menu2);
        //----------- collection
        if (ds.Tables[0].Rows[0]["collection"] != null)
        {
            menubar menu = new menubar();
            menu.menu = "Collections";
            menu.id = "3";
            if (Convert.ToDecimal(ds.Tables[0].Rows[0]["collection"]) > 0)
            {
                menu.show = "true";
            }
            else
            {
                menu.show = "false";
            }
            menuist.Add(menu);
        }
        //----------- catalog
        if (ds.Tables[1].Rows[0]["catalog"] != null)
        {
            menubar menu = new menubar();
            menu.menu = "Catalogs";
            menu.id = "4";
            if (Convert.ToDecimal(ds.Tables[1].Rows[0]["catalog"]) > 0)
            {
                menu.show = "true";
            }
            else
            {
                menu.show = "false";
            }
            menuist.Add(menu);
        }
        //----------- promotion
        if (ds.Tables[2].Rows[0]["promotion"] != null)
        {
            menubar menu = new menubar();
            menu.menu = "Promotions";
            menu.id = "5";
            if (Convert.ToDecimal(ds.Tables[2].Rows[0]["promotion"]) > 0)
            {
                menu.show = "true";
            }
            else
            {
                menu.show = "false";
            }
            menuist.Add(menu);
        }
        //----------- exibition
        if (ds.Tables[3].Rows[0]["exibition"] != null)
        {
            menubar menu = new menubar();
            menu.menu = "Exhibitions";
            menu.id = "6";
            if (Convert.ToDecimal(ds.Tables[3].Rows[0]["exibition"]) > 0)
            {
                menu.show = "true";
            }
            else
            {
                menu.show = "false";
            }
            menuist.Add(menu);
        }
        //----------- Contact
        menubar menu3 = new menubar();
        menu3.menu = "Contact";
        menu3.id = "7";
        menu3.show = "true";
        menuist.Add(menu3);

        Homepage hm = new Homepage();
        hm.BannerList = mbban;
        hm.ItemBannerList = itmban;
        hm.CollectionBannerList = colban;
        hm.exibition = exbt;
        hm.collection = collist;
        hm.menubarlist = menuist;
        hmp.Add(hm);

        Response.Write(JsonConvert.SerializeObject(hmp));
    }

    class bannerimage
    {
        public string imagename { get; set; }
        public string url { get; set; }
    }

    class itembannerimage
    {
        public string imagename { get; set; }
        public string url { get; set; }
        public string name { get; set; }
    }

    class exibition
    {
        public string ExibitionId { get; set; }
        public string ExibitionName { get; set; }
        public string Exibitiondate { get; set; }
        public string ExibitionPlace { get; set; }
        public string HallNo { get; set; }
        public string BoothNo { get; set; }
    }

    class collection
    {
        public string collectionName { get; set; }
    }

    class menubar
    {
        public string id { get; set; }
        public string menu { get; set; }
        public string show { get; set; }       
    }

    class Homepage
    {
        public List<bannerimage> BannerList { get; set; }
        public List<itembannerimage> ItemBannerList { get; set; }
        public List<bannerimage> CollectionBannerList { get; set; }
        public List<exibition> exibition { get; set; }
        public List<collection> collection { get; set; }
        public List<menubar> menubarlist { get; set; }
    }

}