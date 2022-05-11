using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Newtonsoft.Json;
using System.IO;
using System.Globalization;
using System.Web.Services;
using System.Text;
using System.Security.Cryptography;
using paytm;
using PaytmContant;
using System.Configuration;
using System.Net.Mail;
/// <summary>
/// Summary description for FabFashionAccessories
/// </summary>
[WebService(Namespace = "https://fabfashionaccessories.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class FabFashionAccessories : System.Web.Services.WebService
{
    FabAccessoriesEntities db = new FabAccessoriesEntities();
    string OrderId = "";
    string TxnId = "";
    string status = "0";
    string discountamt = "0";
    public FabFashionAccessories()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string bindmenu(string loginid)
    {
        DataTable dt = new DataTable();
        List<Homepage> hmp = new List<Homepage>();
        List<bannerimage> mbban = new List<bannerimage>();
        List<itembannerimage> itmban = new List<itembannerimage>();
        List<bannerimage> colban = new List<bannerimage>();
        List<exibition> exbt = new List<exibition>();
        List<collection> collist = new List<collection>();
        List<menubar> menuist = new List<menubar>();
        List<menubar> catlist = new List<menubar>();
        List<newarrival> newarivallist = new List<newarrival>();
        List<newarrival1> newarivallist1 = new List<newarrival1>();
        string sliderurl = "";
        //------------------------------ Main banner --------------------------//
        sliderurl = "https://www.sstylefactory.com/upload/banner/";
        dt = DataAccess.GetDataTable("select * from Banner where bannerof='app'", CommandType.Text);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bannerimage bn = new bannerimage();
                string image = sliderurl + dt.Rows[i]["bannerimg"].ToString();
                bn.imagename = image;
                bn.id = dt.Rows[i]["BannerUrl"].ToString();
                bn.title = "";
                mbban.Add(bn);
            }
        }

        //------------------------------ Item Banner --------------------------//
        sliderurl = "https://www.sstylefactory.com/upload/Itembanner/";
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
        sliderurl = "https://www.sstylefactory.com/upload/mobile/SpecialBanner/";
        dt = DataAccess.GetDataTable("select * from CollectionBanner ", CommandType.Text);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bannerimage colbn = new bannerimage();
                string image = sliderurl + dt.Rows[i]["Imgname"].ToString();
                colbn.imagename = image;
                colbn.id = dt.Rows[i]["Url"].ToString();
                colbn.title = dt.Rows[i]["title"].ToString();
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
        dt = DataAccess.GetDataTable("select distinct(cm.categoryname) as itemname,pm.categoryid from productmaster pm left join categorymaster cm on cm.categoryid = pm.categoryid where cm.categoryname !='-' and showhide=1 and offerprice=0 order by categoryname asc", CommandType.Text);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                collection col = new collection();
                col.collectionName = dt.Rows[i]["itemname"].ToString();
                col.categoryId = dt.Rows[i]["categoryid"].ToString();
                collist.Add(col);
            }
        }

        //------------------------------ menubar --------------------------//
        //first query=and Offerprice=0;
        //thirsquery=and Offerprice>0
        DataSet ds = DataAccess.GetDataSet("select count(*) as collection from productmaster where showhide=1 ;select count (*) as catalog from catalogs;select count(*) as promotion from productmaster where showhide=1 and SRP>0 ;select count(*) as exibition from exibition where CONVERT(date,todate,106)>=CONVERT(date,DATEADD(minute,750,getdate()),106)  ", CommandType.Text);

        DataTable dtcol = DataAccess.GetDataTable("select distinct colm.collectionName, colm.collectionid, colm.CollectionImg, colm.DisplayOrder from productmaster pm inner join collectionmaster colm on colm.collectionid = pm.collectionid order by colm.DisplayOrder", CommandType.Text);

        List<category> clist = new List<category>();
        //menubar menu0 = new menubar();
        ////----------- Home
        //menu0.menu = "Home";
        //menu0.id = "0";
        //menu0.show = "true";
        //menuist.Add(menu0);

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
        menu2.catlist = clist;
        menuist.Add(menu2);



        if (dtcol.Rows.Count > 0)
        {
            for (int p = 0; p < dtcol.Rows.Count; p++)
            {
                menubar menu = new menubar();
                menu.menu = dtcol.Rows[p]["collectionname"].ToString();
                menu.id = dtcol.Rows[p]["collectionid"].ToString();
                menu.show = "true";

                //menu.imgpath = "http://fabfashionaccessories.com/appcollectionimg/" + menu.id + ".jpg";
                menu.imgpath = "https://sstylefactory.com/appcollectionimg/" + dtcol.Rows[p]["CollectionImg"].ToString();

                DataTable dtcat = DataAccess.GetDataTable("select distinct cat.CategoryName, cat.categoryid, cat.DisplayOrder from productmaster pm inner join categorymaster cat on cat.categoryid = pm.CategoryId where pm.CollectionId=" + dtcol.Rows[p]["collectionid"].ToString() + " order by cat.DisplayOrder", CommandType.Text);

                List<category> ctlist = new List<category>();

                if (dtcat.Rows.Count > 0)
                {



                    for (int k = 0; k < dtcat.Rows.Count; k++)
                    {
                        category cat = new category();

                        cat.categoryid = dtcat.Rows[k]["categoryid"].ToString();
                        cat.categoryname = dtcat.Rows[k]["categoryname"].ToString();

                        ctlist.Add(cat);
                    }


                }

                menu.catlist = ctlist;

                menuist.Add(menu);
                catlist.Add(menu);
            }
        }


        //----------- collection
        //if (ds.Tables[0].Rows[0]["collection"] != null)
        //{
        //    menubar menu = new menubar();
        //    menu.menu = "Collections";
        //    menu.id = "3";
        //    if (Convert.ToDecimal(ds.Tables[0].Rows[0]["collection"]) > 0)
        //    {
        //        menu.show = "true";
        //    }
        //    else
        //    {
        //        menu.show = "false";
        //    }
        //    menuist.Add(menu);
        //}



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
            menu.catlist = clist;
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
                menu.show = "false";
            }
            else
            {
                menu.show = "false";
            }
            menu.catlist = clist;
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
            menu.catlist = clist;
            menuist.Add(menu);
        }

        //----------- About
        menubar menu1 = new menubar();
        menu1.menu = "IT STORE";
        menu1.id = "1";
        menu1.show = "true";
        menu1.catlist = clist;
        menuist.Add(menu1);


        //----------- Contact
        menubar menu3 = new menubar();
        menu3.menu = "Contact";
        menu3.id = "7";
        menu3.show = "true";
        menu3.catlist = clist;
        menuist.Add(menu3);

        menubar menu8 = new menubar();
        menu8.menu = "Policies";
        menu8.id = "8";
        menu8.show = "true";
        menu8.catlist = clist;
        menuist.Add(menu8);


        //----------- About
        menu1 = new menubar();
        menu1.menu = "About";
        menu1.id = "1";
        menu1.show = "true";
        menu1.catlist = clist;
        menuist.Add(menu1);




        DataTable getproduct = new DataTable();

        getproduct = DataAccess.GetDataTable("SELECT top 6  Productmaster.Id, Productmaster.SkuName, Productmaster.CollectionId, Productmaster.CategoryId, Productmaster.Title, Productmaster.Description, Productmaster.MaterialId, Productmaster.MRP, Productmaster.SRP as Offerprice, Productmaster.Offerprice as SRP,Productmaster.StockPcs, Productmaster.Grosswt, Productmaster.NetWt, Productmaster.ShowHide, Productmaster.MDate, Productmaster.IsCommon, Productmaster.Image, Productmaster.OtherImage,MaterialMaster.Material as MaterialName,CategoryMaster.CategoryName as ItemName, isnull(ColorMaster.ColorId,0) as ColorId, ISNULL(SizeMaster.SizeId,0) as SizeId, isnull(ColorMaster.ColorName,'') as ColorName, ISNULL(SizeMaster.SizeName,'') as SizeName, ProductMaster.GenderType FROM Productmaster left join  MaterialMaster on Productmaster.Materialid= MaterialMaster.Materialid left join CategoryMaster on Productmaster.categoryid= CategoryMaster.categoryid left join ColorMaster on Productmaster.ColorId= ColorMaster.ColorId left join SizeMaster on Productmaster.SizeId= SizeMaster.SizeId where stockpcs>=1 and showhide=1  ORDER BY mdate desc", CommandType.Text);

        if (getproduct.Rows.Count > 0)
        {
            for (int p = 0; p < getproduct.Rows.Count; p++)
            {
                newarrival na = new newarrival();
                newarrival1 na1 = new newarrival1();


                na.ProductId = getproduct.Rows[p]["id"].ToString();
                na.Title = getproduct.Rows[p]["Title"].ToString();
                na.Description = getproduct.Rows[p]["Description"].ToString();
                na.Image = getproduct.Rows[p]["Image"].ToString();
                na.MRP = getproduct.Rows[p]["MRP"].ToString();
                na.OfferPrice = getproduct.Rows[p]["Offerprice"].ToString();

                na1.RowNum = p + 1;
                na1.ProductId = getproduct.Rows[p]["id"].ToString();
                na1.Title = getproduct.Rows[p]["Title"].ToString();
                na1.Description = getproduct.Rows[p]["Description"].ToString();
                na1.Image = getproduct.Rows[p]["Image"].ToString();
                na1.MRP = Convert.ToDecimal(getproduct.Rows[p]["MRP"].ToString());
                na1.Offerprice = Convert.ToDecimal(getproduct.Rows[p]["Offerprice"].ToString());
                na1.Id = Convert.ToDecimal(getproduct.Rows[p]["id"].ToString());
                na1.SkuName = getproduct.Rows[p]["skuname"].ToString();
                na1.CollectionId = Convert.ToDecimal(getproduct.Rows[p]["collectionid"].ToString());
                na1.CategoryId = Convert.ToDecimal(getproduct.Rows[p]["categoryid"].ToString());
                na1.MaterialId = Convert.ToDecimal(getproduct.Rows[p]["Materialid"].ToString());
                na1.SRP = Convert.ToDecimal(getproduct.Rows[p]["SRP"].ToString());
                na1.StockPcs = Convert.ToDecimal(getproduct.Rows[p]["StockPcs"].ToString());
                na1.Grosswt = Convert.ToDecimal(getproduct.Rows[p]["Grosswt"].ToString());
                na1.NetWt = Convert.ToDecimal(getproduct.Rows[p]["NetWt"].ToString());
                na1.ShowHide = Convert.ToInt16(getproduct.Rows[p]["ShowHide"].ToString());
                na1.MDate = getproduct.Rows[p]["MDate"].ToString();
                na1.IsCommon = Convert.ToInt16(getproduct.Rows[p]["IsCommon"].ToString());
                na1.OtherImage = getproduct.Rows[p]["OtherImage"].ToString();
                na1.MaterialName = getproduct.Rows[p]["MaterialName"].ToString();
                na1.ItemName = getproduct.Rows[p]["ItemName"].ToString();
                na1.ColorId = Convert.ToDecimal(getproduct.Rows[p]["ColorId"].ToString());
                na1.SizeId = Convert.ToDecimal(getproduct.Rows[p]["SizeId"].ToString());
                na1.ColorName = getproduct.Rows[p]["ColorName"].ToString();
                na1.SizeName = getproduct.Rows[p]["SizeName"].ToString();
                na1.GenderType = getproduct.Rows[p]["GenderType"].ToString();

                newarivallist.Add(na);
                newarivallist1.Add(na1);
            }
        }



        Homepage hm = new Homepage();
        hm.BannerList = mbban;
        hm.ItemBannerList = itmban;
        hm.CollectionBannerList = colban;
        hm.exibition = exbt;
        //hm.collection = collist;
        hm.menubarlist = menuist;
        hm.BrowseCategory = catlist;
        hm.wishcount = "0";
        hm.cartcount = "0";
        hm.NewArrivalList = newarivallist;
        hm.NewArrivalList1 = newarivallist1;
        hmp.Add(hm);

        return (JsonConvert.SerializeObject(hmp));
    }

    [WebMethod]
    public string registeruser(string compname, string contactperson, string mobile, string loginid, string address, string city, string state, string country, string username, string pwd, string device, string deviceid, string action)
    {
        UserRegisterResponse response = new UserRegisterResponse();
        if (action != null)
        {
            if (action == "registeruser")
            {
                try
                {
                    if (compname != null && contactperson != null && mobile != null && loginid != null && address != null && city != null && country != null && username != null && pwd != null)
                    {
                        if (string.IsNullOrEmpty(loginid) || string.IsNullOrEmpty(pwd) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(city) || string.IsNullOrEmpty(mobile))
                        {
                            response.Response = "0";
                            response.Message = "fields cannot be blank!";
                        }
                        else
                        {
                            var user = db.UserInfoes.FirstOrDefault(x => x.Username == username || x.Email == loginid);
                            if (user != null)
                            {
                                response.Response = "0";
                                response.Message = "UserName/Email is alreday exists!";
                            }
                            else
                            {
                                UserInfo newuser = new UserInfo();
                                newuser.Address = address;
                                newuser.City = city;
                                newuser.Company = compname.Trim();
                                newuser.ContactNo = mobile;
                                newuser.ContactPerson = contactperson;
                                newuser.Country = country;
                                newuser.CreateDate = DateTime.Now;
                                newuser.Designation = "";
                                newuser.Email = loginid;
                                newuser.FirstName = "";
                                newuser.Gender = "";
                                newuser.IsActive = true;
                                newuser.LastName = "";

                                newuser.Password = pwd;
                                newuser.DeviceId = deviceid.Trim();
                                newuser.PlayerId = deviceid.Trim();
                                newuser.Registerfrom = device;
                                newuser.State = state;
                                newuser.Username = username;
                                newuser.Zip = "";
                                db.UserInfoes.Add(newuser);
                                db.SaveChanges();

                                if (!db.NewsLetters.Any(r => r.EmailId == newuser.Email))
                                {
                                    var NewsChk = new NewsLetter();
                                    NewsChk.EmailId = loginid;
                                    NewsChk.CreateDate = DateTime.Now;
                                    NewsChk.Status = 1;
                                    db.NewsLetters.Add(NewsChk);
                                    db.SaveChanges();
                                }

                                sendmail sm = new sendmail();

                                string body = sm.MailFormat(newuser.Username, newuser.Username, newuser.Password, "", "Registration");
                                string adminbody = sm.MailFormat(newuser.Username, newuser.Email, newuser.Password, Encrypt(newuser.Id.ToString().Trim()), "adminregistraion");
                                string adminrs = sm.SendMail(newuser.Username, newuser.Email, newuser.ContactNo, "New Registration on Fab Fashion Accessories", adminbody, "Admin");
                                string rs = sm.SendMail(newuser.Username, newuser.Email, newuser.ContactNo, "Thank you For Registration on Fab Fashion Accessories", body, "Client");

                                if (rs == "Sucess" && adminrs == "Sucess")
                                {
                                    response.Response = "1";
                                    response.UserId = newuser.Id.ToString();
                                    response.UserName = newuser.Username.ToString();
                                    response.Message = "success";
                                    newuser.Mailstatus = 1;
                                }
                                else
                                {
                                    response.Response = "0";
                                    response.UserId = newuser.Id.ToString();
                                    response.UserName = newuser.Username.ToString();
                                    response.Message = "Mail is not send";
                                }
                            }
                        }
                    }
                    else
                    {
                        response.Response = "0";
                        response.Message = "Parameter not supplied!";
                    }
                }
                catch (Exception ex)
                {
                    response.Response = "0";
                    response.Message = "Internal error!";
                }
            }

         //=================================  UPDATE PROFILE =========================================
            else if (action == "updateuser")
            {

                if (string.IsNullOrEmpty(username))
                {
                    response.Response = "0";
                    response.Message = "fields cannot be blank!";
                }
                else
                {
                    var newuser = db.UserInfoes.FirstOrDefault(x => x.Username == username && x.Email == loginid);
                    if (newuser != null)
                    {
                        newuser.Address = address;
                        newuser.City = city;
                        newuser.State = state;
                        newuser.Company = compname;
                        newuser.ContactNo = mobile;
                        newuser.ContactPerson = contactperson;
                        newuser.Country = country;
                        newuser.Password = pwd;
                        db.SaveChanges();

                        response.Response = "1";
                        response.UserId = newuser.Id.ToString();
                        response.UserName = newuser.Username.Trim();
                        response.Message = "success";
                    }
                    else
                    {
                        response.Response = "0";
                        response.Message = "user is not exist!";
                    }
                }
            }
        }

        else
        {
            response.Response = "0";
            response.Message = "Action is Empty !";
        }
        return JsonConvert.SerializeObject(response);
    }

    [WebMethod]
    public string loginuser(string loginid, string pwd, string deviceid)
    {
        LoginResponse response = new LoginResponse();
        string userdet = "";
        try
        {
            if (loginid != null && pwd != null)
            {
                loginid = loginid.ToString();
                pwd = pwd.ToString();
                if (deviceid != null) { deviceid = deviceid.ToString().Trim(); }

                if (string.IsNullOrEmpty(loginid) || string.IsNullOrEmpty(pwd))
                {
                    response.Response = "0";
                    response.Message = "loginid or password cannot be blank!";
                }
                else
                {
                    var user = db.UserInfoes.FirstOrDefault(x => x.Username == loginid);
                    if (user != null)
                    {
                        if (user.IsActive == true)
                        {
                            if (user.Password.Equals(pwd))
                            {
                                response.Response = "1";
                                response.UserId = user.Id.ToString();
                                response.UserName = user.Username.ToString();
                                response.ContactNo = user.ContactNo.ToString();
                                response.Email = user.Email.ToString();
                                response.City = user.City.ToString();
                                response.country = user.Country.ToString();
                                response.DeviceId = user.DeviceId == null ? "" : user.DeviceId.ToString();
                                response.Message = "success";
                                userdet = JsonConvert.SerializeObject(user);
                                if (user.DeviceId != null && user.DeviceId != "")
                                {
                                    if (!user.DeviceId.Contains(deviceid))
                                    {
                                        user.DeviceId = user.DeviceId + "," + deviceid;
                                        user.PlayerId = deviceid;
                                    }
                                }
                                else
                                {
                                    user.DeviceId = deviceid;
                                    user.PlayerId = deviceid;
                                }

                                db.SaveChanges();
                            }
                            else
                            {
                                response.Response = "0";
                                response.Message = "Invalid password!";
                            }
                        }
                        else
                        {
                            response.Response = "0";
                            response.Message = "user not active!";
                        }
                    }
                    else
                    {
                        response.Response = "0";
                        response.Message = "user does not exist!";
                    }
                }
            }
            else
            {
                response.Response = "0";
                response.Message = "Parameter not supplied!";
            }

        }
        catch (Exception ex)
        {
            response.Response = "0";
            response.Message = "Internal error!";
        }

        var msg = JsonConvert.SerializeObject(response);

        return ("[{\"Response\":[" + msg + "]" + ",\"userlist\":[" + userdet + "]}]");
    }



    [WebMethod]
    public string SocialLogin(string email, string name)
    {
        LoginResponse response = new LoginResponse();
        string userdet = "";
        try
        {
            if (email != null && email != "")
            {

                if (string.IsNullOrEmpty(email))
                {
                    response.Response = "0";
                    response.Message = "loginid cannot be blank!";
                }
                else
                {
                    var user = db.UserInfoes.FirstOrDefault(x => x.Username == email);
                    if (user != null)
                    {
                        if (user.IsActive == true)
                        {
                            response.Response = "1";
                            response.UserId = user.Id.ToString();
                            response.Password = user.Password.ToString();
                            response.UserName = user.Username.ToString();
                            response.ContactNo = user.ContactNo.ToString();
                            response.Email = user.Email.ToString();
                            response.City = user.City.ToString();
                            response.country = user.Country.ToString();
                            response.DeviceId = user.DeviceId == null ? "" : user.DeviceId.ToString();
                            response.Message = "success";
                            userdet = JsonConvert.SerializeObject(user);

                        }
                        else
                        {
                            response.Response = "0";
                            response.Message = "user not active!";
                        }
                    }
                    else
                    {
                        // register user

                        UserInfo newuser = new UserInfo();
                        newuser.Address = "";
                        newuser.City = "";
                        newuser.Company = "";
                        newuser.ContactNo = "";
                        newuser.ContactPerson = name;
                        newuser.Country = "";
                        newuser.CreateDate = DateTime.Now;
                        newuser.Designation = "";
                        newuser.Email = email;
                        newuser.FirstName = "";
                        newuser.Gender = "";
                        newuser.IsActive = true;
                        newuser.LastName = "";

                        newuser.Password = System.DateTime.Now.Ticks.ToString().Substring(6, 5);
                        newuser.DeviceId = "";
                        newuser.PlayerId = "";
                        newuser.Registerfrom = "Android";
                        newuser.State = "";
                        newuser.Username = email;
                        newuser.Zip = "";
                        db.UserInfoes.Add(newuser);
                        db.SaveChanges();

                        if (!db.NewsLetters.Any(r => r.EmailId == newuser.Email))
                        {
                            var NewsChk = new NewsLetter();
                            NewsChk.EmailId = email;
                            NewsChk.CreateDate = DateTime.Now;
                            NewsChk.Status = 1;
                            db.NewsLetters.Add(NewsChk);
                            db.SaveChanges();
                        }

                        sendmail sm = new sendmail();

                        string body = sm.MailFormat(newuser.Username, newuser.Username, newuser.Password, "", "Registration");
                        string adminbody = sm.MailFormat(newuser.Username, newuser.Email, newuser.Password, Encrypt(newuser.Id.ToString().Trim()), "adminregistraion");
                        string adminrs = sm.SendMail(newuser.Username, newuser.Email, newuser.ContactNo, "New Registration on Fab Fashion Accessories", adminbody, "Admin");
                        string rs = sm.SendMail(newuser.Username, newuser.Email, newuser.ContactNo, "Thank you For Registration on Fab Fashion Accessories", body, "Client");

                        if (rs == "Sucess" && adminrs == "Sucess")
                        {

                            response.Response = "1";
                            response.UserId = newuser.Id.ToString();
                            response.Password = newuser.Password.ToString();
                            response.UserName = newuser.Username.ToString();
                            response.ContactNo = newuser.ContactNo.ToString();
                            response.Email = newuser.Email.ToString();
                            response.City = newuser.City.ToString();
                            response.country = newuser.Country.ToString();
                            response.DeviceId = newuser.DeviceId == null ? "" : user.DeviceId.ToString();
                            response.Message = "success";
                            userdet = JsonConvert.SerializeObject(newuser);

                        }
                        else
                        {
                            response.Response = "0";
                            response.UserId = newuser.Id.ToString();
                            response.UserName = newuser.Username.ToString();
                            response.Message = "Mail is not send";
                        }
                    }
                }
            }
            else
            {
                response.Response = "0";
                response.Message = "Parameter not supplied!";
            }

        }
        catch (Exception ex)
        {
            response.Response = "0";
            response.Message = "Internal error!";
        }

        var msg = JsonConvert.SerializeObject(response);

        return ("[{\"Response\":[" + msg + "]" + ",\"userlist\":[" + userdet + "]}]");
    }




    [WebMethod]
    public string discountcouponlist()
    {


        DateTime dt = System.DateTime.Now;

        var user = db.DiscountCoupons.Where(r => r.Status == "1" && r.EditedOn > dt).OrderByDescending(r => r.Id).ToList(); ;
        return JsonConvert.SerializeObject(user);

    }


    [WebMethod]
    public string forgorpassword(string emailid)
    {
        string response = "";
        var userdetail = db.UserInfoes.FirstOrDefault();
        if (emailid.Contains("@"))
        {
            userdetail = db.UserInfoes.Where(r => r.Email == emailid).FirstOrDefault();
        }
        else
        {
            userdetail = db.UserInfoes.Where(r => r.Username == emailid).FirstOrDefault();
        }
        if (userdetail != null)
        {
            if (userdetail.IsActive)
            {
                sendmail sm = new sendmail();
                string body = sm.MailFormat(userdetail.Username, userdetail.Username, userdetail.Password, "", "Forgotpassword");
                string responseq = sm.SendMail(userdetail.Username, userdetail.Email, userdetail.ContactNo, "Forgot Password", body, "Client");
                if (responseq.ToLower() == "sucess")
                {
                    response += "{\"response\":\"1\"}";
                }
                else
                {
                    response += "{\"response\":\"0\"}";
                }
            }
            else
            {
                response += "{\"response\":\"2\"}";
            }
        }
        else
        {
            response += "{\"response\":\"0\"}";
        }

        return (response);
    }

    [WebMethod]
    public string userhistory(int userid, string logindate, string logoutdate, string playerid, string emailid, string contactno, string deviceid, string devicename, string location)
    {
        try
        {
            if (logindate != null && logindate != "" && playerid != "")
            {
                var historyinfo = db.LoginHistories.Where(r => r.PlayerId == playerid).FirstOrDefault();

                if (historyinfo == null)
                {

                    LoginHistory lh = new LoginHistory();
                    lh.UserName = "";
                    lh.LoginDate = System.DateTime.Now;
                    lh.from = "App";
                    lh.Email = emailid;
                    lh.ContactNo = contactno;
                    lh.PlayerId = playerid;
                    lh.DeviceId = deviceid;
                    lh.DeviceName = devicename;
                    lh.Location = location;
                    db.LoginHistories.Add(lh);
                    db.SaveChanges();
                }

            }
            else if (logoutdate != null && logoutdate != "" && playerid != "")
            {
                //var userinfo = db.UserInfoes.Where(r => r.Id == userid).FirstOrDefault();
                var upd = db.LoginHistories.Where(r => r.PlayerId == playerid && r.from == "App").OrderByDescending(r => r.Id).FirstOrDefault();

                if (upd != null)
                {
                    upd.LogoutDate = System.DateTime.Now;

                    upd.Email = emailid;
                    upd.ContactNo = contactno;
                    upd.DeviceId = deviceid;
                    upd.DeviceName = devicename;
                    upd.Location = location;

                    db.SaveChanges();
                }
            }

            return "{\"response\":\"1\"}";
        }
        catch (Exception e)
        {

            return "{\"response\":\"0\"}";
            //Common.LogError(e);
        }
    }

    [WebMethod]
    public string getlist(string menu, string collectionid, string categoryid, string materialid, string pmin, string pmax, string sort, string pricefrom, string to, string colorid, string sizeid, string gendertype)
    {
        FabAccessoriesEntities db = new FabAccessoriesEntities();

        string response = string.Empty;
        string filter = string.Empty;
        string subquery = string.Empty;
        string addedquery = string.Empty;
        string filter2 = string.Empty;

        filter = "mdate desc";

        if (sort.ToLower() == "l")
        {
            filter = " mrp Asc";
        }
        else if (sort.ToLower() == "h")
        {
            filter = " mrp desc";
        }
        else if (sort.ToLower() == "i")
        {
            addedquery += "and StockPcs>0";

        }
        else if (sort.ToLower() == "o")
        {
            addedquery += " and StockPcs=0";
        }

        if (menu.Contains("newarrivals"))
        {
            menu = menu + ",";
        }
        menu = "'" + menu.Replace(",", "','") + "'";
        if (collectionid != "")
        {
            subquery += " and Productmaster.collectionid in(" + collectionid + ")";
        }
        if (categoryid != "")
        {
            subquery += " and Productmaster.categoryid in(" + categoryid + ")";
        }
        if (materialid != "")
        {
            subquery += " and Productmaster.materialid in(" + materialid + ")";
        }

        if (colorid != "")
        {
            subquery += " and Productmaster.colorid in(" + colorid + ")";
        }
        if (sizeid != "")
        {
            subquery += " and Productmaster.sizeid in(" + sizeid + ")";
        }
        if (materialid != "")
        {
            subquery += " and Productmaster.gendertype in(" + gendertype + ")";
        }


        if (pricefrom != "" && to != "" && pricefrom != "''" && to != "''")
        {
            subquery += " and (mrp between " + pricefrom + " and " + to + " ) ";
        }


        string search = "0";

        DataTable getproduct = new DataTable();
        if (menu.Contains("newarrivals"))
        {
            menu = menu.Replace("'newarrivals',", "");
            menu = menu.TrimStart(',');

            if (menu != "" && menu != "''")
            {
                //  subquery += " and itemname in(" + menu + ")";
            }
            //and Offerprice=0
            getproduct = DataAccess.GetDataTable("SELECT  ROW_NUMBER() OVER ( ORDER BY " + filter + " ) AS RowNum, Productmaster.Id, Productmaster.SkuName, Productmaster.CollectionId, Productmaster.CategoryId, Productmaster.Title, Productmaster.Description, Productmaster.MaterialId, Productmaster.MRP, Productmaster.SRP as Offerprice, Productmaster.Offerprice as SRP, Productmaster.StockPcs, Productmaster.Grosswt, Productmaster.NetWt, Productmaster.ShowHide, Productmaster.MDate, Productmaster.IsCommon, Productmaster.Image, Productmaster.OtherImage,MaterialMaster.Material as MaterialName,CategoryMaster.CategoryName as ItemName, isnull(ColorMaster.ColorId,0) as ColorId, ISNULL(SizeMaster.SizeId,0) as SizeId, isnull(ColorMaster.ColorName,'') as ColorName, ISNULL(SizeMaster.SizeName,'') as SizeName, ProductMaster.GenderType FROM Productmaster left join  MaterialMaster on Productmaster.Materialid= MaterialMaster.Materialid left join CategoryMaster on Productmaster.categoryid= CategoryMaster.categoryid left join ColorMaster on Productmaster.ColorId= ColorMaster.ColorId left join SizeMaster on Productmaster.SizeId= SizeMaster.SizeId where stockpcs>=1 and showhide=1 " + subquery + " " + addedquery + "  ORDER BY " + filter, CommandType.Text);
        }
        else if (menu.ToLower().Contains("promotions"))
        {
            menu = menu.Replace("'promotions'", "");
            menu = menu.TrimStart(',');
            if (menu != "")
            {
                subquery += " and CategoryName in(" + menu + ")";
            }
            getproduct = DataAccess.GetDataTable("SELECT  ROW_NUMBER() OVER ( ORDER BY " + filter + " ) AS RowNum, Productmaster.Id, Productmaster.SkuName, Productmaster.CollectionId, Productmaster.CategoryId, Productmaster.Title, Productmaster.Description, Productmaster.MaterialId, Productmaster.MRP, Productmaster.SRP as Offerprice, Productmaster.Offerprice as SRP, Productmaster.StockPcs, Productmaster.Grosswt, Productmaster.NetWt, Productmaster.ShowHide, Productmaster.MDate, Productmaster.IsCommon, Productmaster.Image, Productmaster.OtherImage,MaterialMaster.Material as MaterialName,CategoryMaster.CategoryName as ItemName, isnull(ColorMaster.ColorId,0) as ColorId, ISNULL(SizeMaster.SizeId,0) as SizeId, isnull(ColorMaster.ColorName,'') as ColorName, ISNULL(SizeMaster.SizeName,'') as SizeName, ProductMaster.GenderType FROM Productmaster left join  MaterialMaster on Productmaster.Materialid= MaterialMaster.Materialid left join CategoryMaster on Productmaster.categoryid= CategoryMaster.categoryid left join ColorMaster on Productmaster.ColorId= ColorMaster.ColorId left join SizeMaster on Productmaster.SizeId= SizeMaster.SizeId where stockpcs>=1 and showhide=1 and SRP>0" + subquery + " " + addedquery + "  ORDER BY " + filter + "", CommandType.Text);
        }
        else if (menu.Contains("searchfor"))
        {

            search = "1";
            menu = menu.Replace("searchfor-", "");
            menu = menu.TrimStart(',');
            menu = menu.TrimEnd(',');
            if (menu != "")
            {
                if (subquery != "")
                {
                    subquery += " and ";
                }
                else
                {
                    subquery += "and";
                }

                if (menu.ToLower() == "'ring'" || menu.ToLower() == "'rings'")
                {
                    subquery += " CategoryName = '" + menu.Replace("'", "").Replace("@", "&").Replace("%20", " ").TrimEnd(',') + "'";
                }
                else
                {
                    subquery += " skuname like '%" + (menu.Replace("'", "").Replace("@", "&").Replace("%20", " ").Replace("-", "/")).TrimEnd(',') + "%'";
                    subquery += " or Material like '%" + menu.Replace("'", "").Replace("@", "&").Replace("%20", " ").TrimEnd(',') + "%'";
                    subquery += " or CategoryName like '%" + menu.Replace("'", "").Replace("@", "&").Replace("%20", " ").TrimEnd(',') + "%'";
                    subquery += " or title like '%" + menu.Replace("'", "").Replace("@", "&").Replace("%20", " ").TrimEnd(',') + "%'";
                }
            }
            //and Offerprice=0
            string query = "SELECT ROW_NUMBER() OVER ( ORDER BY " + filter + " ) AS RowNum,Productmaster.Id, Productmaster.SkuName, Productmaster.CollectionId, Productmaster.CategoryId, Productmaster.Title, Productmaster.Description, Productmaster.MaterialId, Productmaster.MRP, Productmaster.SRP as Offerprice, Productmaster.Offerprice as SRP, Productmaster.StockPcs, Productmaster.Grosswt, Productmaster.NetWt, Productmaster.ShowHide, Productmaster.MDate, Productmaster.IsCommon, Productmaster.Image, Productmaster.OtherImage,MaterialMaster.Material as MaterialName,CategoryMaster.CategoryName as ItemName, isnull(ColorMaster.ColorId,0) as ColorId, ISNULL(SizeMaster.SizeId,0) as SizeId, isnull(ColorMaster.ColorName,'') as ColorName, ISNULL(SizeMaster.SizeName,'') as SizeName, ProductMaster.GenderType FROM Productmaster left join  MaterialMaster on Productmaster.Materialid= MaterialMaster.Materialid left join CategoryMaster on Productmaster.categoryid= CategoryMaster.categoryid  left join ColorMaster on Productmaster.ColorId= ColorMaster.ColorId left join SizeMaster on Productmaster.SizeId= SizeMaster.SizeId  where stockpcs>=1 and showhide=1  " + subquery + " " + addedquery + "  ORDER BY " + filter + "";
            getproduct = DataAccess.GetDataTable(query, CommandType.Text);
        }
        else
        {
            string query = string.Empty;
            if (menu == "''")
            {
                //and Offerprice=0
                query = "SELECT  ROW_NUMBER() OVER ( ORDER BY " + filter + " ) AS RowNum, Productmaster.Id, Productmaster.SkuName, Productmaster.CollectionId, Productmaster.CategoryId, Productmaster.Title, Productmaster.Description, Productmaster.MaterialId, Productmaster.MRP, Productmaster.SRP as Offerprice, Productmaster.Offerprice as SRP, Productmaster.StockPcs, Productmaster.Grosswt, Productmaster.NetWt, Productmaster.ShowHide, Productmaster.MDate, Productmaster.IsCommon, Productmaster.Image, Productmaster.OtherImage,MaterialMaster.Material as MaterialName,CategoryMaster.CategoryName as ItemName, isnull(ColorMaster.ColorId,0) as ColorId, ISNULL(SizeMaster.SizeId,0) as SizeId, isnull(ColorMaster.ColorName,'') as ColorName, ISNULL(SizeMaster.SizeName,'') as SizeName, ProductMaster.GenderType FROM Productmaster left join  MaterialMaster on Productmaster.Materialid= MaterialMaster.Materialid left join CategoryMaster on Productmaster.categoryid= CategoryMaster.categoryid  left join ColorMaster on Productmaster.ColorId= ColorMaster.ColorId left join SizeMaster on Productmaster.SizeId= SizeMaster.SizeId where stockpcs>=1 and showhide=1   " + subquery + " " + addedquery + "  ORDER BY " + filter + "";
            }
            else
            {
                //and Offerprice=0
                query = "SELECT  ROW_NUMBER() OVER ( ORDER BY " + filter + " ) AS RowNum, Productmaster.Id, Productmaster.SkuName, Productmaster.CollectionId, Productmaster.CategoryId, Productmaster.Title, Productmaster.Description, Productmaster.MaterialId, Productmaster.MRP, Productmaster.SRP as Offerprice, Productmaster.Offerprice as SRP, Productmaster.StockPcs, Productmaster.Grosswt, Productmaster.NetWt, Productmaster.ShowHide, Productmaster.MDate, Productmaster.IsCommon, Productmaster.Image, Productmaster.OtherImage,MaterialMaster.Material as MaterialName,CategoryMaster.CategoryName as ItemName, isnull(ColorMaster.ColorId,0) as ColorId, ISNULL(SizeMaster.SizeId,0) as SizeId, isnull(ColorMaster.ColorName,'') as ColorName, ISNULL(SizeMaster.SizeName,'') as SizeName, ProductMaster.GenderType FROM Productmaster left join  MaterialMaster on Productmaster.Materialid= MaterialMaster.Materialid left join CategoryMaster on Productmaster.categoryid= CategoryMaster.categoryid  left join ColorMaster on Productmaster.ColorId= ColorMaster.ColorId left join SizeMaster on Productmaster.SizeId= SizeMaster.SizeId where stockpcs>=1 and showhide=1  and CategoryName in(" + menu + ") " + subquery + " " + addedquery + "  ORDER BY " + filter + "";
            }
            getproduct = DataAccess.GetDataTable(query, CommandType.Text);
        }
        DataSet ds = new DataSet();
        if (getproduct.Rows.Count > 0)
        {
            DataTable ddt = new DataTable();

            if (menu.Contains("promotions"))
            {
                if (collectionid != "")
                {
                    ds = DataAccess.GetDataSet("select distinct collectionname as StyleName,Collectionid as Id from CollectionMaster where CollectionId in(select CollectionId from productmaster);select distinct CategoryName as CollectionName,CategoryId as Id from CategoryMaster where CategoryId in(select CategoryId from productmaster where collectionid=" + collectionid + ");select distinct Material, MaterialId as Id from MaterialMaster where MaterialId in(select MaterialId from productmaster);select " + getproduct.Rows.Count + " as rc; select min(MRP) as minprice, max(MRP) as maxprice from productmaster where stockpcs>=1 and showhide!=0 and SRP!=0;", CommandType.Text);
                }
                else
                {
                    ds = DataAccess.GetDataSet("select distinct collectionname as StyleName,Collectionid as Id from CollectionMaster where CollectionId in(select CollectionId from productmaster);select distinct CategoryName as CollectionName,CategoryId as Id from CategoryMaster where CategoryId in(select CategoryId from productmaster);select distinct Material, MaterialId as Id from MaterialMaster where MaterialId in(select MaterialId from productmaster);select " + getproduct.Rows.Count + " as rc; select min(MRP) as minprice, max(MRP) as maxprice from productmaster where stockpcs>=1 and showhide!=0 and SRP!=0;", CommandType.Text);
                }
            }
            else
            {
                //and offerprice=0
                if (collectionid != "")
                {
                    ds = DataAccess.GetDataSet("select distinct collectionname as StyleName,Collectionid as Id from CollectionMaster where CollectionId in(select CollectionId from productmaster);select distinct CategoryName as CollectionName,CategoryId as Id, displayorder from CategoryMaster where CategoryId in(select CategoryId from productmaster where collectionid=" + collectionid + ") order by displayorder;select distinct Material, MaterialId as Id from MaterialMaster where MaterialId in(select MaterialId from productmaster);select " + getproduct.Rows.Count + " as rc; select min(MRP) as minprice, max(MRP) as maxprice from productmaster where stockpcs>=1 and showhide!=0 ;", CommandType.Text);
                }
                else
                {
                    if (search == "1")
                    {
                        ds = DataAccess.GetDataSet("select distinct collectionname as StyleName,Collectionid as Id from CollectionMaster where CollectionId in(select CollectionId from productmaster) and collectionName like '%" + (menu.Replace("'", "").Replace("@", "&").Replace("%20", " ").Replace("-", "/")).TrimEnd(',') + "%' ;select distinct CategoryName as CollectionName,CategoryId as Id, displayorder from CategoryMaster where CategoryId in(select CategoryId from productmaster) and categoryname like '%" + (menu.Replace("'", "").Replace("@", "&").Replace("%20", " ").Replace("-", "/")).TrimEnd(',') + "%' order by displayorder;select distinct Material, MaterialId as Id from MaterialMaster where MaterialId in(select MaterialId from productmaster) and material like '%" + (menu.Replace("'", "").Replace("@", "&").Replace("%20", " ").Replace("-", "/")).TrimEnd(',') + "%' ;select " + getproduct.Rows.Count + " as rc; select min(MRP) as minprice, max(MRP) as maxprice from productmaster where stockpcs>=1 and showhide!=0 ;", CommandType.Text);
                    }
                    else
                    {
                        ds = DataAccess.GetDataSet("select distinct collectionname as StyleName,Collectionid as Id from CollectionMaster where CollectionId in(select CollectionId from productmaster);select distinct CategoryName as CollectionName,CategoryId as Id, displayorder from CategoryMaster where CategoryId in(select CategoryId from productmaster) order by displayorder;select distinct Material, MaterialId as Id from MaterialMaster where MaterialId in(select MaterialId from productmaster);select " + getproduct.Rows.Count + " as rc; select min(MRP) as minprice, max(MRP) as maxprice from productmaster where stockpcs>=1 and showhide!=0 ;", CommandType.Text);
                    }
                }
            }


            //DataTable dtcolor = DataAccess.GetDataTable("select distinct colorname,colorid as Id from colormaster where colorid in(select colorid from productmaster);", CommandType.Text);
            //DataTable dtsize = DataAccess.GetDataTable("select distinct sizename,sizeid as Id from sizemaster where sizeid in(select sizeid from productmaster);", CommandType.Text);

            DataTable dtcolor = DataAccess.GetDataTable("select distinct colorname, colorid as Id from colormaster;", CommandType.Text);
            DataTable dtsize = DataAccess.GetDataTable("select distinct sizename,sizeid as Id from sizemaster;", CommandType.Text);


            if (pmin != "" && pmax != "")
            {
                getproduct = getproduct.AsEnumerable().Skip(Convert.ToInt32(pmin)).Take(Convert.ToInt32(pmax)).CopyToDataTable();




                ds.Tables.Add(getproduct);
                ds.Tables.Add(dtcolor);
                ds.Tables.Add(dtsize);
                response = JsonConvert.SerializeObject(ds);
            }
            else
            {
                ds.Tables.Add(getproduct);
                ds.Tables.Add(dtcolor);
                ds.Tables.Add(dtsize);
                response = JsonConvert.SerializeObject(ds);
            }
            // response = JsonConvert.SerializeObject(getproduct);
        }
        else
        {
            response = JsonConvert.SerializeObject("No Record(s) Found");
        }
        return (response);
    }

    [WebMethod]
    public string productdetails(string productid)
    {
        DataTable getproduct = new DataTable();
        if (productid != "")
        {
            Int64 prid = Convert.ToInt64(productid);

            List<productdetail> details = new List<productdetail>();
            List<otherimage> oth = new List<otherimage>();
            DataTable prolist = DataAccess.GetDataTable("select  pm.Id, pm.SkuName, pm.CollectionId, pm.CategoryId, pm.Title, pm.Description, pm.MaterialId, pm.MRP, pm.SRP as Offerprice, pm.Offerprice as SRP, pm.StockPcs, pm.Grosswt, pm.NetWt, pm.ShowHide, pm.MDate, pm.IsCommon, pm.Image, pm.OtherImage,colm.CollectionName,catm.CategoryName,mm.Material, isnull(sz.SizeName,'') as SizeName, isnull(col.ColorName,'') as ColorName, pm.GenderType from productmaster pm left join dbo.CollectionMaster colm on pm.collectionid=colm.collectionid left join dbo.CategoryMaster catm on pm.categoryid=catm.categoryid left join dbo.MaterialMaster mm on pm.MaterialId=mm.MaterialId left join SizeMaster sz on sz.SizeId=pm.SizeId left join ColorMaster col on col.ColorId = pm.ColorId where id=" + prid + " and ShowHide = 1 ", CommandType.Text);
            if (prolist.Rows.Count > 0)
            {
                productdetail productd = new productdetail();
                productd.Productid = prid.ToString();
                productd.Sku = prolist.Rows[0]["SkuName"].ToString();
                productd.CollectionId = Convert.ToInt32(prolist.Rows[0]["collectionid"].ToString());
                productd.CategoryId = Convert.ToInt32(prolist.Rows[0]["CategoryId"].ToString());
                productd.MaterialId = Convert.ToInt32(prolist.Rows[0]["MaterialId"].ToString());
                productd.Itemname = prolist.Rows[0]["CategoryName"].ToString();
                TextInfo cultInfo = new CultureInfo("en-US", false).TextInfo;
                productd.Material = cultInfo.ToTitleCase(prolist.Rows[0]["Material"].ToString());
                productd.Collection = (prolist.Rows[0]["CollectionName"].ToString().ToLower() == "" ? "" : prolist.Rows[0]["CollectionName"].ToString().ToLower());

                productd.offerprice = Convert.ToDecimal(prolist.Rows[0]["Offerprice"].ToString());
                productd.Producttitle = prolist.Rows[0]["Title"] == null ? "" : prolist.Rows[0]["Title"].ToString();
                productd.ProductDescription = prolist.Rows[0]["Description"] == null ? "" : prolist.Rows[0]["Description"].ToString();

                productd.MRP = Math.Round(Convert.ToDecimal(prolist.Rows[0]["Offerprice"].ToString()) > 0 ? Convert.ToDecimal(prolist.Rows[0]["Offerprice"].ToString()) : Convert.ToDecimal(prolist.Rows[0]["MRP"].ToString()));
                //productd.MRP = Math.Round(Convert.ToDecimal(prolist.Rows[0]["MRP"].ToString()));
                productd.SRP = Math.Round(Convert.ToDecimal(prolist.Rows[0]["Offerprice"].ToString()));


                productd.Netwt = prolist.Rows[0]["NetWt"].ToString();
                productd.Grosswt = prolist.Rows[0]["Grosswt"].ToString();
                productd.StockPcs = Convert.ToInt32(prolist.Rows[0]["StockPcs"].ToString());
                productd.Stock = Convert.ToInt32(prolist.Rows[0]["StockPcs"].ToString()) > 0 ? "In Stock" : "On Order";

                productd.SizeName = prolist.Rows[0]["SizeName"].ToString();
                productd.ColorName = prolist.Rows[0]["ColorName"].ToString();
                productd.GenderType = prolist.Rows[0]["GenderType"].ToString();

                productd.ProductURL = "https://www.sstylefactory.com/" + Common.url(productd.Collection.ToString()) + "/" + Common.url(productd.Itemname.ToString()) + "/" + Common.url(productd.Producttitle.ToString()) + "-" + productd.Productid + ".html";



                productd.imagename = prolist.Rows[0]["Image"].ToString() == "" ? "" : ("https://www.sstylefactory.com/upload/Products/large/" + prolist.Rows[0]["Image"].ToString());

                var otherimg = db.OtherImages.Where(r => r.ProductId == prid).ToList();

                foreach (var item in otherimg)
                {
                    otherimage othernew = new otherimage();
                    othernew.smallurl = "https://www.sstylefactory.com/upload/Products/OtherSmall/" + item.ImageName;
                    othernew.bigurl = "https://www.sstylefactory.com/upload/Products/OtherLarge/" + item.ImageName;
                    oth.Add(othernew);
                }

                details.Add(productd);

                //and Offerprice=0
                getproduct = DataAccess.GetDataTable("SELECT top(10) ROW_NUMBER() OVER ( ORDER BY id desc ) AS RowNum, Productmaster.Id, Productmaster.SkuName, Productmaster.CollectionId, Productmaster.CategoryId, Productmaster.Title, Productmaster.Description, Productmaster.MaterialId, Productmaster.MRP, Productmaster.SRP as Offerprice, Productmaster.Offerprice as SRP, Productmaster.StockPcs, Productmaster.Grosswt, Productmaster.NetWt, Productmaster.ShowHide, Productmaster.MDate, Productmaster.IsCommon, Productmaster.Image, Productmaster.OtherImage,MaterialMaster.Material as MaterialName,CategoryMaster.CategoryName as ItemName, isnull(sz.SizeName,'') as SizeName, isnull(col.ColorName,'') as ColorName, Productmaster.GenderType FROM Productmaster left join  MaterialMaster on Productmaster.Materialid= MaterialMaster.Materialid left join CategoryMaster on Productmaster.categoryid= CategoryMaster.categoryid left join SizeMaster sz on sz.SizeId=Productmaster.SizeId left join ColorMaster col on col.ColorId = Productmaster.ColorId where stockpcs>=1 and showhide=1  and Productmaster.categoryid=" + productd.CategoryId + " ORDER BY id desc", CommandType.Text);
            }
            else
            {

            }
            string res = JsonConvert.SerializeObject(details);
            dynamic RelatedProducts;
            string[] arr = new string[] { };
            if ((getproduct.Rows.Count > 0))
            {
                RelatedProducts = getproduct;
            }
            else
            {
                RelatedProducts = arr;
            }
            // RelatedProducts = arr;
            return ("[{\"Productdetail\":" + res + "" + ",\"OtherImages\":" + JsonConvert.SerializeObject(oth) + ",\"RelatedProducts\":" + JsonConvert.SerializeObject(RelatedProducts) + "}]");
        }
        else
        {
            return ("{\"response\":\"Product id is not pass\"}");
        }
    }

    [WebMethod]
    public string contactus(string name, string email, string contactno, string message, string subject)
    {
        LoginResponse response = new LoginResponse();

        FabAccessoriesEntities db = new FabAccessoriesEntities();
        ContactU cou = new ContactU();
        cou.Name = name;
        cou.Email = email;
        cou.Message = message;
        cou.Subject = subject;
        cou.ContactNo = contactno;
        cou.AdDate = DateTime.Now;
        db.ContactUs.Add(cou);
        db.SaveChanges();
        try
        {
            sendmail sm = new sendmail();
            string mailbody = sm.MailFormat(name, email, contactno, message, "Admin");
            string sendmail = sm.SendMail(name, email, contactno, subject, mailbody, "Admin");
            //string sendmail = "Sucess";
            if (sendmail == "Sucess")
            {
                response.Response = "1";
                response.Message = "Success";
            }
            else
            {
                response.Response = "0";
                response.Message = "Failure";
            }
        }
        catch
        {
            response.Response = "0";
            response.Message = "Failure";
        }

        return JsonConvert.SerializeObject(response);

    }

    [WebMethod]
    public string search(string keyword)
    {
        List<items> itm = new List<items>();
        DataTable dt = DB.Business.SPs.SpProducts(0, keyword, "searchproduct").GetDataSet().Tables[0];
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow item in dt.Rows.Cast<System.Data.DataRow>().Take(15))
            {
                items it = new items();

                if (keyword.ToLower().Trim() == "rings" || keyword.ToLower().Trim() == "ring")
                {
                    if (!item["itemname"].ToString().Trim().ToLower().Contains("earring"))
                    {
                        it.searchname = item["itemname"].ToString().Trim().ToLower();
                        itm.Add(it);
                    }
                }
                else
                {
                    it.searchname = item["itemname"].ToString().Trim().ToLower();
                    itm.Add(it);
                }
            }
        }
        return JsonConvert.SerializeObject(itm);
    }

    [WebMethod]
    public string applycouponcode(string Couponcode)
    {
        FabAccessoriesEntities db = new FabAccessoriesEntities();
        string res = "";
        var dis = db.DiscountCoupons.Where(r => r.DiscountCode.ToLower() == Couponcode.ToLower()).FirstOrDefault();
        if (dis != null)
        {
            if (dis.Status == "1")
            {
                res = "{\"Response\" :" + dis.DiscountPerc.ToString() + ",\"res\":1,\"Message\":\"Coupon Code Applied\"}";
            }
            else
            {
                res = "{\"Response\" :\"Coupon Code has been Expired\",\"res\":0,\"Message\":\"Coupon Code has been Expired\"}";
            }
        }
        else
        {
            res = "{\"Response\" :\"Coupon Code has been Expired\",\"res\":0,\"Message\":\"Invalid Coupon Code\"}";
        }
        return res;
    }

    [WebMethod]
    public string Appcheckout(string data)
    {
        string userid = ""; checkoutresponse response = new checkoutresponse();
        string paytmcall = "";
        string shippingamt = "0.00";
        try
        {
            string umail = "";
            string contno = "";
            string uid = "";
            dynamic checkoutdetail = JsonConvert.DeserializeObject(data);
            var cartproducts = checkoutdetail.CartData.ToString();
            var UserDetail = checkoutdetail.UserDetail;
            var UserBilling = checkoutdetail.BillingDetail;
            var ShippingDetail = checkoutdetail.ShippingDetail;
            var CartAmount = checkoutdetail.CartAmount;

            DataTable table = (DataTable)JsonConvert.DeserializeObject(cartproducts, (typeof(DataTable)));
            string useremail = UserDetail.useremail;
            var allNew = db.NewsLetters.Where(r => r.EmailId.ToLower() == useremail).FirstOrDefault();
            if (allNew == null)
            {
                if (UserDetail.newsletters != null && UserDetail.newsletters == "1")
                {
                    var news = new NewsLetter();
                    news.EmailId = useremail.ToLower();
                    news.CreateDate = DateTime.Now;
                    db.NewsLetters.Add(news);
                    db.SaveChanges();
                    // sendNewsletterEmail(txtEmailBilling.Text);
                }
            }

            string OrderNo = "";
            Int64 ordId = 1;
            var ord = db.OrderTbls.OrderByDescending(r => r.Id).FirstOrDefault();
            if (ord != null)
            {
                ordId = ord.Id + 1;
            }

            OrderNo = "000" + ordId;
            if (ordId.ToString().Length < 5)
            {
                OrderNo = OrderNo.Substring(OrderNo.Length - 4);
            }

            var user = db.UserInfoes.Where(r => r.Email == useremail).FirstOrDefault();

            if (user == null)
            {
                user = new UserInfo();
                user.Email = useremail;
                user.FirstName = UserBilling.billingname;
                user.ContactPerson = UserBilling.billingname;
                //user.LastName = UserBilling.lastname;
                user.Password = DateTime.Now.Ticks.ToString().Substring(11, 6);
                user.Address = UserBilling.billingaddress1 + " " + UserBilling.billingaddress2;
                user.City = UserBilling.billingcity;
                contno = user.ContactNo = UserBilling.billingcontactno;
                user.Country = UserBilling.billingcountry;
                user.CreateDate = DateTime.Now;
                user.State = UserBilling.billingstate;
                user.Zip = UserBilling.billingzip;
                user.IsActive = true;
                if (UserDetail.device != null)
                {
                    user.Registerfrom = UserDetail.device;
                }
                else
                {
                    user.Registerfrom = "Android";
                }
                user.Gender = "Private";

                //string ss = UserBilling.billingname.ToString();

                //string uname = "";

                //if (ss.Length > 4)
                //{
                //    uname = UserBilling.billingname.ToString().Substring(0, 4).Replace(" ", "_") + Common.GetRandomAlphaNumeric();
                //}
                //else
                //{
                //    uname = UserBilling.billingname.ToString().Replace(" ", "_") + Common.GetRandomAlphaNumeric();
                //}

                //var chkusername = db.UserInfoes.Where(r => r.Username == uname).FirstOrDefault();
                //if (chkusername == null)
                //{
                //    user.Username = uname;
                //}
                //else
                //{
                //    int i = 0;
                //    do
                //    {
                //        if (ss.Length > 4)
                //        {
                //            uname = UserBilling.billingname.ToString().Substring(0, 4).Replace(" ", "_") + Common.GetRandomAlphaNumeric();
                //        }
                //        else
                //        {
                //            uname = UserBilling.billingname.ToString().Replace(" ", "_") + Common.GetRandomAlphaNumeric();
                //        }

                //        var chkuname = db.UserInfoes.Where(r => r.Username == uname).FirstOrDefault();
                //        if (chkuname == null)
                //        {
                //            user.Username = uname;
                //            i = 1;
                //        }

                //    } while (i == 0);

                //    user.Username = uname;
                //}

                user.Username = useremail;

                user.DeviceId = UserDetail.deviceid;
                db.UserInfoes.Add(user);
                db.SaveChanges(); userid = user.Id.ToString();
                Int64 id = user.Id;
                uid = id.ToString();

                sendmail sm = new sendmail();

                string body = sm.MailFormat(user.Username, user.Username, user.Password, "", "Registration");
                string adminbody = sm.MailFormat(user.Username, user.Email, user.Password, Encrypt(user.Id.ToString().Trim()), "adminregistraion");
                string adminrs = sm.SendMail(user.Username, user.Email, user.ContactNo, "New Registration on Fab Fashion Accessories", adminbody, "Admin");
                string rs = sm.SendMail(user.Username, user.Email, user.ContactNo, "Thank you For Registration on Fab Fashion Accessories", body, "Client");

            }

            else
            {
                userid = user.Id.ToString();
            }

            if (userid.ToString() != "")
            {
                OrderNo = "FFA" + OrderNo; string Checksum = "";
                if (userid.Trim().Length > 0)
                {
                    string Discountamount = "0.00";
                    var ship = Convert.ToDecimal(CartAmount.shippingcharges);
                    OrderTbl ordTbl = new OrderTbl();
                    ordTbl.OrderNo = OrderNo;
                    ordTbl.UserId = Convert.ToInt64(userid);
                    ordTbl.OrderTotal = Convert.ToDecimal(CartAmount.subtotal);
                    ordTbl.OrderCurrency = "INR";
                    ordTbl.ShippingAmt = ship;
                    ordTbl.CouponCode = CartAmount.couponcode;
                    ordTbl.CouponType = "";
                    ordTbl.CouponAmt = Convert.ToDecimal(CartAmount.discountamount);
                    ordTbl.Comment = "";
                    ordTbl.status = "0";
                    ordTbl.OrderBy = UserDetail.device;
                    ordTbl.OrderDate = DateTime.Now;
                    Discountamount = discountamt = CartAmount.discountamount;
                    db.OrderTbls.Add(ordTbl);

                    db.SaveChanges();

                    OrderTracking ordTrack = new OrderTracking();
                    ordTrack.OrderNo = OrderNo;
                    ordTrack.OrderStatus = "Processed";
                    ordTrack.StatusDate = ordTrack.CreateDate = DateTime.Now;
                    ordTrack.DeliveredDate = Convert.ToDateTime("01/01/1900");
                    ordTrack.ExpectedDeliverDate = Convert.ToDateTime("01/01/1900");
                    ordTrack.ShippedDate = Convert.ToDateTime("01/01/1900");
                    db.OrderTrackings.Add(ordTrack);
                    db.SaveChanges();

                    decimal tot = 0.00m;
                    string cartPro = "";
                    if (OrderNo.Trim().Length > 0)
                    {
                        if (table != null)
                        {
                            foreach (DataRow cartRow in table.Rows)
                            {
                                cart c = new cart();
                                c.Qty = Convert.ToInt16(cartRow["productqty"].ToString());
                                c.ProductId = Convert.ToDecimal(cartRow["productid"].ToString());
                                decimal d = Convert.ToDecimal(c.ProductId);
                                string avlqty = "0";

                                // var checkproduct = db.ProductMasters.Where(r => r.Id == d && r.ShowHide == 1).FirstOrDefault();

                                var checkproduct = (from p in db.ProductMasters
                                                    join cate in db.CategoryMasters on p.CategoryId equals cate.CategoryId
                                                    where p.Id == c.ProductId && p.ShowHide == 1
                                                    select new { p.Id, p.Image, p.Title, p.SkuName, ItemName = cate.CategoryName, cate.CategoryId }).FirstOrDefault();

                                if (checkproduct != null)
                                {
                                    OrderDetail orddetail = new OrderDetail();
                                    orddetail.OrderNo = OrderNo;
                                    orddetail.Comment = cartRow["comment"].ToString();
                                    orddetail.ProductId = Convert.ToInt64(checkproduct.Id);
                                    orddetail.Img = checkproduct.Image;
                                    orddetail.ModelNo = checkproduct.SkuName;
                                    orddetail.Item = checkproduct.ItemName;
                                    orddetail.Price = Math.Round(Convert.ToDecimal(cartRow["mrp"]));
                                    orddetail.Total = Math.Round(Convert.ToDecimal(Convert.ToDecimal(cartRow["mrp"].ToString())) * Convert.ToInt16(c.Qty));
                                    orddetail.ShortDesc = checkproduct.Title.ToString();
                                    orddetail.Qty = Convert.ToInt32(c.Qty);
                                    db.OrderDetails.Add(orddetail);
                                    tot += Math.Round(Convert.ToDecimal(orddetail.Total));
                                    db.SaveChanges();

                                    //cartPro += " <tr><td><p><img style='float:left;'  src='https://www.fabfashionaccessories.com/upload/product/small/'" + c.img + "/>" + c.shortDesc + "</p> </td><td>" + c.modelNo + "</td><td> Rs. " + c.unitPrice + "</td><td>" + c.Qty + "</td><td> Rs. " + pTot + "</td></tr>";
                                    //cartPro += "<tr><td colspan='5'><hr style='margin:3px' /></td></tr>";

                                }
                            }
                        }
                        string cartProduct = "<table width='100%' style='font-family:Verdana;font-size:11px; border:solid 1px #ccc;'>";
                        cartProduct += "<tr><td style='width:40%'>  </td><td style='width:15%'>Model No</td><td style='width:15%'>Unit Price</td><td style='width:15%'>Quantity</td><td style='width:15%'>Total</td> </tr>";
                        cartProduct += "<tr><td colspan='5'><hr/></td></tr>";
                        cartProduct += cartPro;

                        cartProduct += "<tr><td></td><td></td><td></td><td>Sub-Total:</td><td> Rs. " + CartAmount.subtotal + "</td></tr>";
                        cartProduct += "<tr><td></td><td></td><td></td><td>Shipping Charges:</td><td> Rs. " + ship + "</td></tr>";

                        if (Convert.ToDecimal(Discountamount) > 0)
                        {
                            cartProduct += "<tr><td></td><td></td><td></td><td>Discount:</td><td> Rs. " + Discountamount + "</td></tr>";
                        }
                        else
                        {
                            cartProduct += "<tr><td></td><td></td><td></td><td>Discount:</td><td> Rs. " + 0 + "</td></tr>";
                        }
                        cartProduct += "<tr><td></td><td></td><td></td><td colspan='2'> <hr style='margin:3px' /></td></tr>";
                        cartProduct += "<tr><td></td><td></td><td></td><td>Total Amt:</td><td> Rs. " + CartAmount.grandtotal + "</td></tr>";
                        cartProduct += " </table>";

                        OrderBillingInfo ordBilling = new OrderBillingInfo();
                        ordBilling.Name = UserBilling.billingname;
                        ordBilling.Email = UserBilling.billingemail;
                        ordBilling.ContactNo = UserBilling.billingcontactno;
                        ordBilling.Address = UserBilling.billingaddress1;
                        ordBilling.Address2 = UserBilling.billingaddress2;
                        ordBilling.City = UserBilling.billingcity;
                        ordBilling.State = UserBilling.billingstate;
                        ordBilling.Country = UserBilling.billingcountry;
                        ordBilling.Zip = UserBilling.billingzip;
                        ordBilling.CreateDate = DateTime.Now;
                        ordBilling.OrderNo = OrderNo;
                        db.OrderBillingInfoes.Add(ordBilling);
                        db.SaveChanges();

                        OrderShippingDetail ordShipping = new OrderShippingDetail();
                        ordShipping.Name = ShippingDetail.shippingname;
                        ordShipping.Email = ShippingDetail.shippingemail;
                        ordShipping.ContactNo = ShippingDetail.shippingcontactno;
                        ordShipping.Address = ShippingDetail.shippingaddress1;
                        ordShipping.Address2 = ShippingDetail.shippingaddress2;
                        ordShipping.City = ShippingDetail.shippingcity;
                        ordShipping.State = ShippingDetail.shippingstate;
                        ordShipping.Country = ShippingDetail.shippingcountry;
                        ordShipping.Zip = ShippingDetail.shippingzip;
                        ordShipping.CreateDate = DateTime.Now;
                        ordShipping.OrderNo = OrderNo;
                        db.OrderShippingDetails.Add(ordShipping);
                        db.SaveChanges();

                        OrderPaymentResponse payres = new OrderPaymentResponse();
                        payres.OrderId = OrderNo;
                        payres.PaymentId = "";
                        payres.PaymentMode = CartAmount.PayMode;
                        payres.Status = "Unpaid";
                        payres.Message = "";
                        payres.Mode = CartAmount.PayMode;
                        payres.CreateDate = DateTime.Now;
                        db.OrderPaymentResponses.Add(payres);
                        db.SaveChanges();

                        db.SaveChanges();


                        // sendEmail(cartProduct, OrderNo, CartAmount.subtotal, ship, "INR", Discountamount.ToString());


                    }




                    string customerPhone = ShippingDetail.shippingcontactno == null ? "" : ShippingDetail.shippingcontactno;
                    string grandtotal = CartAmount.grandtotal;
                    CashFreeToken cft = new CashFreeToken();
                    Checksum = cft.Main(OrderNo, grandtotal, useremail, customerPhone);
                }

                response.Response = "1";
                response.OrderId = OrderNo;
                response.Amount = CartAmount.grandtotal;
                response.Message = "Success";
                response.Checksum = Checksum;

                paytmcall = "https://www.sstylefactory.com/ipaytm.aspx?amount=" + CartAmount.subtotal + "&discount=" + discountamt + "&shippingAmt=" + shippingamt + "&OrderNo=" + OrderNo + "&usermail=" + useremail + "&contno=" + contno + "&uid=" + userid + "";
                //paytmcheckout(ltrlSubTotal.Text.Trim(), Discountamount, ship.ToString(), OrderNo);
                response.ipaytmwebcall = paytmcall;
            }
        }
        catch (Exception ex)
        {
            Common.LogError(ex);
            response.Response = "0";
            response.Message = "Internal Error";
            response.OrderId = "";
            response.Amount = "0.0";
            response.Checksum = "";
            //response.ipaytmwebcall=
        }
        return JsonConvert.SerializeObject(response);
    }

    [WebMethod]
    public string MyAccount(string Userid)
    {

        List<myorders> hmp = new List<myorders>();

        List<Itemdetails> ban = new List<Itemdetails>();
        List<orderdetail> odd = new List<orderdetail>();

        DataTable result = DataAccess.GetDataTable("select img, ModelNo as SkuName, Item as ItemName, qty, orderdetail.price, total,shortdesc as remark, Ordertbl.OrderNo, convert(nvarchar,Ordertbl.orderdate,103) as EnqDate,Ordertbl.OrderTotal,isnull(orderdetail.discountedprice,0) as discountedprice, isnull(ordertbl.couponamt,'0.00') as CouponAmt, grosswt,netwt,mm.Material as materialName from orderdetail left join ordertbl on ordertbl.orderno=orderdetail.orderno left join productmaster pm on pm.skuname=orderdetail.ModelNo left join MaterialMaster mm on mm.materialid=pm.materialid where ordertbl.userid=" + Userid + " order by orderdate desc", CommandType.Text);

        if (result != null)
        {
            string test = string.Empty;
            foreach (DataRow item in result.Rows)
            {
                Itemdetails bn = new Itemdetails();
                if (test == "")
                {
                    test = item["OrderNo"].ToString();
                    bn.anything = "1";
                }
                else if (test != "" && item["OrderNo"].ToString() != test)
                {
                    test = item["OrderNo"].ToString();
                    bn.anything = "1";
                }
                else
                {
                    bn.anything = "";
                }


                bn.Image = item["img"].ToString();
                bn.Item = item["ItemName"].ToString();
                bn.Qty = Convert.ToInt32(item["qty"].ToString());
                bn.SKU = item["SkuName"].ToString();
                bn.Price = (Convert.ToDecimal(item["discountedprice"].ToString()) > 0 ? Convert.ToDecimal(item["discountedprice"].ToString()) : Convert.ToDecimal(item["price"].ToString()));
                bn.totalamount = (Convert.ToDecimal(item["discountedprice"].ToString()) > 0 ? Convert.ToDecimal(item["discountedprice"].ToString()) * Convert.ToDecimal(item["qty"].ToString()) : Convert.ToDecimal(item["price"].ToString()) * Convert.ToDecimal(item["qty"].ToString()));
                bn.OrderNo = item["OrderNo"].ToString();
                bn.orderdate = item["EnqDate"].ToString();
                bn.OrderTotal = Convert.ToDecimal(item["OrderTotal"].ToString());
                bn.CouponAmt = Convert.ToDecimal(item["CouponAmt"].ToString());
                bn.grosswt = item["grosswt"].ToString();
                bn.materialname = item["materialName"].ToString();
                bn.netwt = item["netwt"].ToString();
                ban.Add(bn);
            }
        }

        DataTable singresult = DataAccess.GetDataTable("select ordertbl.OrderNo, isnull(ot.OrderStatus,'') as OrderStatus, convert(nvarchar,orderdate,103) as EnqDate,OrderTotal, ordertbl.CouponAmt, isnull((select top 1 Trackurl from ShipMaster where shipname=ot.shippedby),'') as TrackUrl from ordertbl left join OrderTracking ot on ot.orderno=ordertbl.orderno where userid=" + Userid + " order by orderdate desc", CommandType.Text);

        if (singresult != null)
        {
            foreach (DataRow odddetails in singresult.Rows)
            {
                orderdetail od = new orderdetail();
                od.NetTotal = Convert.ToDecimal(odddetails["OrderTotal"].ToString());
                od.Enqdate = odddetails["EnqDate"].ToString();
                od.OrderNo = odddetails["OrderNo"].ToString();
                od.TrackUrl = odddetails["TrackUrl"].ToString();
                od.OrderStatus = odddetails["OrderStatus"].ToString();
                od.CouponAmt = odddetails["CouponAmt"].ToString();
                odd.Add(od);
            }
        }
        myorders hm = new myorders();
        hm.Orderitemdetails = ban;
        hm.Orderdetails = odd;
        hmp.Add(hm);

        if (result.Rows.Count == 0)
        {
            count c = new count();
            c.response = "0";
            return JsonConvert.SerializeObject(c);
        }
        else
        {
            return JsonConvert.SerializeObject(hmp);
        }
    }

    [WebMethod]
    public string paymentResponse1(string Orderno, string TransactionId, string Paymentstatus)
    {
        response response = new response();
        OrderId = Orderno;
        TxnId = TransactionId;
        status = Paymentstatus;


        if (OrderId != null && OrderId != "")
        {
            var pay = db.OrderPaymentResponses.Where(r => r.OrderId == OrderId).FirstOrDefault();
            if (pay != null)
            {
                pay.OrderId = OrderId.ToString();
                pay.CreateDate = System.DateTime.Now;
                pay.PaymentId = TxnId;
                if (status.ToLower() == "success")
                {
                    pay.Status = "success";
                    var ordersucc = db.OrderTbls.Where(r => r.OrderNo == OrderId).FirstOrDefault();
                    if (ordersucc != null)
                    {
                        ordersucc.status = "1";
                        db.SaveChanges();
                    }
                }
                else
                {
                    pay.Status = "Unpaid";
                    string ss = JsonConvert.SerializeObject("0");
                    //  Response.Write(ss.Replace("\\", ""));
                }
                db.SaveChanges();
                if (status.ToLower() == "success")
                {
                    getOrderDetail(OrderId);
                }
                response.Response = "1";
                response.Message = "Your Order has been Placed Successfully";
            }
            else
            {
                response.Response = "0";
                response.Message = "No Order";
            }


        }
        else
        {
            response.Response = "0";
            response.Message = "Order no is not Pass";
        }
        return JsonConvert.SerializeObject(response);



        //if (OrderId != null && OrderId != "")
        //{
        //    //  paymentdetail();
        //    response.Response = "1";
        //    response.Message = "Your Order has been Placed Successfully";
        //}
        //else
        //{
        //    response.Response = "0";
        //    response.Message = "Order no is not Pass";
        //}
        //return JsonConvert.SerializeObject(response);
    }

    [WebMethod]
    public string paymentResponse(string data)
    {
        dynamic Responsedetail = JsonConvert.DeserializeObject(data);
        var responsed = Responsedetail.CashFreeResponse;
        response response = new response();
        OrderId = responsed.orderId;
        TxnId = responsed.referenceId;
        status = responsed.txStatus;

        if (OrderId != null && OrderId != "")
        {
            var pay = db.OrderPaymentResponses.Where(r => r.OrderId == OrderId).FirstOrDefault();
            if (pay != null)
            {
                pay.OrderId = OrderId.ToString();
                pay.CreateDate = System.DateTime.Now;
                pay.PaymentId = TxnId;
                if (status.ToLower() == "success")
                {
                    pay.Status = "success";
                    var ordersucc = db.OrderTbls.Where(r => r.OrderNo == OrderId).FirstOrDefault();
                    if (ordersucc != null)
                    {
                        ordersucc.status = "1";
                        db.SaveChanges();
                    }
                }
                else
                {
                    pay.Status = "Unpaid";
                    string ss = JsonConvert.SerializeObject("0");
                    //  Response.Write(ss.Replace("\\", ""));
                }
                db.SaveChanges();
                if (status.ToLower() == "success")
                {
                    getOrderDetail(OrderId);
                }
                response.Response = "1";
                response.Message = "Your Order has been Placed Successfully";
            }
            else
            {
                response.Response = "0";
                response.Message = "No Order";
            }


        }
        else
        {
            response.Response = "0";
            response.Message = "Order no is not Pass";
        }
        return JsonConvert.SerializeObject(response);
    }

    void getOrderDetail(string orid)
    {
        string OrderId = orid;
        var orderM = from ordDetail in db.OrderDetails
                     join ord in db.OrderTbls on ordDetail.OrderNo equals ord.OrderNo
                     where (ordDetail.OrderNo == OrderId)
                     select new
                     {
                         ordDetail.Img,
                         ordDetail.ModelNo,
                         ordDetail.Item,
                         ord.ShippingAmt,
                         ordDetail.Price,
                         ordDetail.Total,
                         ordDetail.Qty,
                         ordDetail.Size,
                         ord.OrderCurrency,
                         ord.CouponAmt,
                         ord.OrderTotal,
                         ProductTot = ordDetail.Total
                     };

        var orderDtl = orderM.ToList();

        var orderPay = (from p in db.OrderPaymentResponses
                        where p.OrderId == orid
                        select p).FirstOrDefault();
        OrderPaymentResponse response = new OrderPaymentResponse();
        var dds = (from p in db.OrderPaymentResponses
                   join q in db.OrderTbls on p.OrderId equals q.OrderNo
                   where p.OrderId == orid
                   orderby p.OrderId
                   select new
                   {
                       q.OrderTotal,
                       p.PaymentId,
                       p.PaymentMode,
                       p.Status,
                       p.Mode,
                       p.Message
                   });
        discountamt = orderDtl.FirstOrDefault().CouponAmt.ToString();
        string shipamt = orderDtl.FirstOrDefault().ShippingAmt.ToString();
        string currency = orderDtl.FirstOrDefault().OrderCurrency.ToString();

        db.SaveChanges();


        var orderstatus = db.OrderPaymentResponses.Where(r => r.OrderId == OrderId && r.Status.ToLower() == "success").FirstOrDefault();
        if (orderstatus != null)
        {
            foreach (var item in orderDtl)
            {
                var aa = db.ProductMasters.Where(r => r.SkuName == item.ModelNo).FirstOrDefault();

                if (aa != null)
                {
                    aa.StockPcs = (aa.StockPcs - item.Qty);
                    db.SaveChanges();
                }
            }
            AddGoogleEcommerceTraking(orderDtl.FirstOrDefault().OrderCurrency, OrderId);

            decimal amount = 0;
            string cartpro = "";
            var orderdetail = db.OrderDetails.Where(r => r.OrderNo == OrderId).ToList();
            if (orderdetail != null)
            {
                string url = "https://www.sstylefactory.com/upload/products/small/";
                foreach (var itemdetail in orderdetail)
                {
                    amount += Convert.ToDecimal(itemdetail.Qty) * Convert.ToDecimal(itemdetail.Price);
                    cartpro += "<tr><td style='padding: 5px 10px;border-right: 1px solid #ccc; vertical-align: top;'><img style='float:left;' width='54px' height='54px'  src='" + url + itemdetail.Img.ToString() + "'/><p style='vertical-align: top;font-size: 13px;width: 65%;float: right;margin: 5px 0;'>" + itemdetail.ShortDesc + "</p> </td><td style='padding: 5px 10px;border-right: 1px solid #ccc; vertical-align: top;'><p style='vertical-align: top;font-size: 13px;'>" + itemdetail.ModelNo + "</p></td><td style='padding: 5px 10px;border-right: 1px solid #ccc; vertical-align: top;'><p style='vertical-align: top;font-size: 13px;'> Rs. " + itemdetail.Price + "</p></td><td style='padding: 5px 10px;border-right: 1px solid #ccc; vertical-align: top;'><p style='vertical-align: top;font-size: 13px;'>" + itemdetail.Qty + "</p></td><td style='padding: 5px 10px; vertical-align: top;'><p style='vertical-align: top;font-size: 14px;'> Rs. " + Convert.ToDecimal(itemdetail.Qty) * Convert.ToDecimal(itemdetail.Price) + "</p></td></tr>";

                }
            }
            decimal tamount = Convert.ToDecimal(amount) + Convert.ToDecimal(shipamt) - Convert.ToDecimal(discountamt.ToString());
            sendEmail(cartpro, orid, amount, shipamt, currency, discountamt.ToString());
        }
        else
        {

        }
    }

    void AddGoogleEcommerceTraking(string currency, string Orderno)
    {
        string orderId = Orderno;

        decimal gaShip = 0;
        decimal currPrice = 1;

        var uinfo = (from user in db.UserInfoes
                     join ord in db.OrderTbls on user.Id equals ord.UserId
                     where ord.OrderNo == orderId
                     select new { user.City, user.State, user.Country, ord.OrderTotal, ord.ShippingAmt }).FirstOrDefault();

        gaShip = Convert.ToDecimal(uinfo.ShippingAmt.ToString());
        Google.Transaction transaction = new Google.Transaction(orderId, "sstylefactory.com", 0, Convert.ToDecimal(gaShip.ToString("0.00")), uinfo.City, uinfo.State, uinfo.Country);
        var itemDetail = (from ordDe in db.OrderDetails
                          join pro in db.ProductMasters on ordDe.ModelNo equals pro.SkuName
                          join itm in db.CategoryMasters on pro.CategoryId equals itm.CategoryId
                          select new { ordDe.OrderNo, ProductCode = pro.SkuName, ordDe.Price, ordDe.Qty, itemName = itm.CategoryName })
                             .Where(r => r.OrderNo == orderId);
        int id = 1;
        foreach (var item in itemDetail.ToList())
        {
            decimal gaPrice = 0;
            gaPrice = Convert.ToDecimal(item.Price / currPrice);
            transaction.Add(new Google.Item(item.ProductCode, item.itemName + "-" + item.ProductCode, item.itemName, Convert.ToDecimal(gaPrice.ToString("0.00")), Convert.ToInt32(item.Qty)));
        }

    }

    void sendEmail(string cart, string orderno, decimal amt, string shippingamt, string cur, string discamt)
    {
        var billingdetail = (from b in db.OrderBillingInfoes
                             join s in db.OrderShippingDetails on b.OrderNo equals s.OrderNo
                             where b.OrderNo == OrderId
                             select new
                             {
                                 b.OrderNo,
                                 b.Name,
                                 b.State,
                                 b.Zip,
                                 b.Id,
                                 b.Email,
                                 b.Country,
                                 b.ContactNo,
                                 b.City,
                                 b.Address2,
                                 b.Address,
                                 sname = s.Name,
                                 sstate = s.State,
                                 scountry = s.Country,
                                 szip = s.Zip,
                                 semail = s.Email,
                                 saddress = s.Address,
                                 saddress2 = s.Address2,
                                 scontact = s.ContactNo,
                                 scity = s.City
                             }).FirstOrDefault();
        if (billingdetail != null)
        {
            string msg = "";
            msg = msg + "";
            msg = "<table cellpadding='5' cellspacing='0' width='100%'  style='border:1px solid #efefef;'><thead style='background-color:#efefef'><tr> <td style='width: 40%'></td><td style='width: 15%'>Model No</td><td style='width: 15%'>Unit Price</td><td style='width: 15%'>Quantity</td><td style='width: 15%'>Total</td> </tr></thead> <tbody>";

            msg = msg + cart;

            decimal Total = Convert.ToDecimal(amt) + Convert.ToDecimal(shippingamt) - Convert.ToDecimal(discamt);

            msg = msg + "<tr><td colspan='2' style='border-top: 1px solid #ccc;text-align:left;'></td><td colspan='2' style='border-top: 1px solid #ccc;font-size: 13px;text-align:right;padding:5px;'>Sub Total (" + cur + "):</td><td style='border-top: 1px solid #ccc;font-size: 13px;padding:5px;' align='center'>" + amt + "</td></tr>";

            decimal Grandtotal = 0;

            if (Convert.ToDecimal(discamt) > 0)
            {
                Grandtotal = Convert.ToDecimal(amt) + Convert.ToDecimal(shippingamt) - Convert.ToDecimal(discamt);

                msg = msg + "<tr><td colspan='2' style='border-top: 1px solid #ccc;text-align:left;'></td><td colspan='2' style='border-top: 1px solid #ccc;font-size: 13px;text-align:right;padding:5px;'>Discount(" + cur + "):</td><td style='border-top: 1px solid #ccc;font-size: 13px;padding: 5px;' align='center'>" + discamt + "</td></tr>";
                msg = msg + "<tr><td colspan='2' style='border-top: 1px solid #ccc;text-align:left;'></td><td colspan='2' style='border-top: 1px solid #ccc;font-size: 13px;text-align:right;padding:5px;'>Grand Total(" + cur + "):</td><td style='border-top: 1px solid #ccc;font-size: 13px;padding: 5px;' align='center'>" + Grandtotal + "</td></tr>";
            }
            else
            {
                Grandtotal = Convert.ToDecimal(amt) + Convert.ToDecimal(shippingamt);
                msg = msg + "<tr><td colspan='2' style='border-top: 1px solid #ccc;text-align:left;'></td><td colspan='2' style='border-top: 1px solid #ccc;font-size: 13px;text-align:right;padding:5px;'>Grand Total(" + cur + "):</td><td style='border-top: 1px solid #ccc;font-size: 13px;padding: 5px;' align='center'>" + Grandtotal + "</td></tr>";
            }

            msg += "</tbody></table>";

            msg = msg + "<div><table border='0' cellspacing='0' cellpadding='0' width='620' style='background:#f9f9f9;'><tbody><tr><td style='padding: 10px 12px;'>";

            msg = msg + "<p style='margin:0; padding:0px 0; font-weight:600;font-size:14px; padding:10px 0'>Delivery Address:</p>";

            msg = msg + "<p style='margin:0; padding:0px 0; font-weight:500;font-size:12px;'>" + billingdetail.sname + ", " + billingdetail.saddress + ", " + billingdetail.saddress2 + "<br />";

            msg = msg + billingdetail.scity + ", " + billingdetail.sstate + "<br />";

            msg = msg + billingdetail.scountry + ", " + billingdetail.szip + "<br />";

            msg = msg + "Contact No.: " + billingdetail.scontact + "<br />";

            msg = msg + "Email: " + billingdetail.semail + "<br />";

            msg = msg + "</p></td></tr>";
            msg += "</tbody></table></div>";

            sendmail sm = new sendmail();
            string body = "";
            String FILENAME = HttpContext.Current.Server.MapPath(@"~\emailer\order.html");
            StreamReader objStreamReader = File.OpenText(FILENAME);
            body = objStreamReader.ReadToEnd();
            body = body.Replace("##name##", (billingdetail.Name).First().ToString().ToUpper() + (billingdetail.Name).Substring(1));
            body = body.Replace("##orderno##", orderno);
            body = body.Replace("##itemdetail##", msg);

            try
            {
                string responseq = sm.SendMail(billingdetail.Name, billingdetail.Email, billingdetail.scontact, "Your Order at Fab Fashion Accessories has been placed successfully.", body, "Client");
                string sendmailtoadmin = sm.SendMail(billingdetail.Name, billingdetail.Email, billingdetail.scontact, "Order on Fab Fashion Accessories (Success)", body, "Admin");
            }
            catch (Exception ex)
            {
                Common.LogError(ex);
            }
        }
    }

     [WebMethod]
    public string orderstatus(string Orderno)
    {
        response response = new response();
        OrderId = Orderno;
       // TxnId = TransactionId;
       // status = Paymentstatus;


        if (OrderId != null && OrderId != "")
        {
            var pay = db.OrderPaymentResponses.Where(r => r.OrderId == OrderId).FirstOrDefault();
            if (pay != null)
            {
                
                if  (pay.Status.ToLower() == "success")
                {                    
                        response.Response = "1";
                        response.Tid = pay.PaymentId;
                        response.Message = "success";
                }
                else
                {
                     response.Response = "0";
                     response.Tid = "";
                     response.Message = "unpaid";
                   // string ss = JsonConvert.SerializeObject("0");
                  
                }             
               // response.Message = "Your Order has been Placed Successfully";
            }
            else
            {
                response.Response = "0";
                response.Message = "No Order";
            }


        }
        else
        {
            response.Response = "0";
            response.Message = "Order no is not Pass";
        }
        return JsonConvert.SerializeObject(response);
    }


    private string Encrypt(string clearText)
    {
        string EncryptionKey = "fabfashion";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }

    public class UserRegisterResponse
    {
        public string Response { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
    }

    class bannerimage
    {
        public string imagename { get; set; }
        public string title { get; set; }
        public string id { get; set; }
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
        public string categoryId { get; set; }
    }

    class menubar
    {
        public string id { get; set; }
        public string menu { get; set; }
        public string show { get; set; }
        public string imgpath { get; set; }
        public List<category> catlist { get; set; }

    }

    class category
    {
        public string categoryname { get; set; }
        public string categoryid { get; set; }
    }



    class Homepage
    {
        public List<bannerimage> BannerList { get; set; }
        public List<itembannerimage> ItemBannerList { get; set; }
        public List<bannerimage> CollectionBannerList { get; set; }
        public List<exibition> exibition { get; set; }
        public List<collection> collection { get; set; }
        public List<menubar> menubarlist { get; set; }
        public List<menubar> BrowseCategory { get; set; }
        public List<newarrival> NewArrivalList { get; set; }
        public List<newarrival1> NewArrivalList1 { get; set; }
        public string wishcount { get; set; }
        public string cartcount { get; set; }
    }

    public class newarrival
    {
        public string ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string MRP { get; set; }
        public string OfferPrice { get; set; }
        public string Image { get; set; }
    }

    public class newarrival1
    {

        public int RowNum { get; set; }
        public string ProductId { get; set; }
        public decimal Id { get; set; }
        public string SkuName { get; set; }
        public decimal CollectionId { get; set; }
        public decimal CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal MaterialId { get; set; }
        public decimal MRP { get; set; }
        public decimal Offerprice { get; set; }
        public decimal SRP { get; set; }
        public decimal StockPcs { get; set; }
        public decimal Grosswt { get; set; }
        public decimal NetWt { get; set; }
        public int ShowHide { get; set; }
        public string MDate { get; set; }
        public int IsCommon { get; set; }
        public string Image { get; set; }
        public string OtherImage { get; set; }
        public string MaterialName { get; set; }
        public string ItemName { get; set; }
        public decimal ColorId { get; set; }
        public decimal SizeId { get; set; }
        public string ColorName { get; set; }
        public string SizeName { get; set; }
        public string GenderType { get; set; }



    }

    public class LoginResponse
    {
        public string Response { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string ContactNo { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string country { get; set; }
        public string Message { get; set; }
        public string DeviceId { get; set; }
        public string Password { get; set; }
    }

    public class productdetail
    {
        public string Productid { get; set; }
        public string Sku { get; set; }
        public int CollectionId { get; set; }
        public int CategoryId { get; set; }
        public int MaterialId { get; set; }
        public string Itemname { get; set; }
        public string Material { get; set; }
        public string Collection { get; set; }
        public string Producttitle { get; set; }
        public string ProductDescription { get; set; }
        public decimal MRP { get; set; }
        public decimal SRP { get; set; }
        public decimal offerprice { get; set; }
        public int StockPcs { get; set; }
        public string Stock { get; set; }
        public string Grosswt { get; set; }
        public string Netwt { get; set; }
        public string imagename { get; set; }
        public string SizeName { get; set; }
        public string ColorName { get; set; }
        public string GenderType { get; set; }
        public string ProductURL { get; set; }

    }

    public class otherimage
    {
        public string bigurl { get; set; }
        public string smallurl { get; set; }
    }

    public class items
    {
        public string searchname { get; set; }
    }

    public class cart
    {
        public decimal ProductId { get; set; }
        public int Qty { get; set; }
        public string Size { get; set; }
        public string shortDesc { get; set; }
        public string modelNo { get; set; }
        public string img { get; set; }
        public decimal Price { get; set; }
    }

    public class myorders
    {
        public List<Itemdetails> Orderitemdetails { get; set; }
        public List<orderdetail> Orderdetails { get; set; }
    }

    public class Itemdetails
    {
        public Decimal totalamount { get; set; }
        public Int32 Qty { get; set; }
        public string SKU { get; set; }
        public string Image { get; set; }
        public string Item { get; set; }
        public decimal Price { get; set; }
        public string OrderNo { get; set; }
        public string orderdate { get; set; }
        public decimal OrderTotal { get; set; }
        public decimal CouponAmt { get; set; }
        public string grosswt { get; set; }
        public string netwt { get; set; }
        public string materialname { get; set; }
        public string anything { get; set; }
    }

    public class orderdetail
    {
        public decimal NetTotal { get; set; }
        public string Enqdate { get; set; }
        public string OrderNo { get; set; }
        public string TrackUrl { get; set; }
        public string OrderStatus { get; set; }
        public string CouponAmt { get; set; }
    }

    public class count
    {
        public string response { get; set; }
    }

    public class checkoutresponse
    {
        public string Response { get; set; }
        public string OrderId { get; set; }
        public string Checksum { get; set; }
        public string Amount { get; set; }
        public string Message { get; set; }
        public string ipaytmwebcall { get; set; }

    }

    public class response
    {
        public string Response { get; set; }
        public string Message { get; set; }
        public string ipaytmwebcall { get; set; }
        public string Tid { get; set; }
    }




    public class CashFreeToken
    {
        private string CreateToken(string message, string secret)
        {
            secret = secret ?? "";
            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }

        public string Main(string orderId, string orderAmount, string customerEmail, string customerPhone)
        {
            String appId = "3226f3726891cb1b66f568746223";
            String secret = "396eac0e84e0397432c1885afa61ea3f083ae9f1";

            //---------  Testing --------------
            //String appId = "1322123034d868333dfcf7012231";
            //String secret = "2da31727ad90f5754a47e6c04a94638b125a7d89";

            String data = "appId=" + appId + "&orderId=" + orderId + "&orderAmount=" + orderAmount + "&customerEmail=" + customerEmail + "&customerPhone=" + customerPhone + "&orderCurrency=" + "INR";

            CashFreeToken n = new CashFreeToken();
            String signature = n.CreateToken(data, secret);
            return (signature);
        }
    }
}
