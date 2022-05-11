<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Routing" %>
<script RunAt="server">

    protected void Application_BeginRequest(object sender, EventArgs e)
    {
      //  HttpContext.Current.Response.AddHeader("x-frame-options", "SAMEORIGIN");
       // HttpContext.Current.Response.AddHeader("x-frame-options", "DENY");

       
        
        //if (HttpContext.Current.Request.Url.ToString().ToLower() == "https://www.dwsjewellery.com/jewellery-shopping")
        //{
        //    HttpContext.Current.Response.Status = "301 Moved Permanently";
        //    HttpContext.Current.Response.AddHeader("Location", Request.Url.ToString().ToLower().Replace("https://www.dwsjewellery.com/jewellery-shopping", "https://www.dwsjewellery.com/jewellery-shopping.html"));
        //}
        //else if (HttpContext.Current.Request.Url.ToString().ToLower() == "https://www.dwsjewellery.com/online-jewellery")
        //{
        //    HttpContext.Current.Response.Status = "301 Moved Permanently";
        //    HttpContext.Current.Response.AddHeader("Location", Request.Url.ToString().ToLower().Replace("https://www.dwsjewellery.com/online-jewellery", "https://www.dwsjewellery.com/online-jewellery.html"));
        //}
        //else if (HttpContext.Current.Request.Url.ToString().ToLower() == "https://www.dwsjewellery.com/online-collection")
        //{
        //    HttpContext.Current.Response.Status = "301 Moved Permanently";
        //    HttpContext.Current.Response.AddHeader("Location", Request.Url.ToString().ToLower().Replace("https://www.dwsjewellery.com/online-collection", "https://www.dwsjewellery.com/online-collection.html"));
        //}
        //else if (HttpContext.Current.Request.Url.ToString().ToLower()=="https://www.dwsjewellery.com/gemstone-jewellery")
        //{
        //    HttpContext.Current.Response.Status = "301 Moved Permanently";
        //    HttpContext.Current.Response.AddHeader("Location", Request.Url.ToString().ToLower().Replace("https://www.dwsjewellery.com/gemstone-jewellery", "https://www.dwsjewellery.com/gemstone-jewellery.html"));
        //}
        //else if (HttpContext.Current.Request.Url.ToString().ToLower() == "https://www.dwsjewellery.com/jewellery-shopping/")
        //{
        //    HttpContext.Current.Response.Status = "301 Moved Permanently";
        //    HttpContext.Current.Response.AddHeader("Location", Request.Url.ToString().ToLower().Replace("https://www.dwsjewellery.com/jewellery-shopping/", "https://www.dwsjewellery.com/jewellery-shopping.html"));
        //}
        //else if (HttpContext.Current.Request.Url.ToString().ToLower() == "https://www.dwsjewellery.com/online-jewellery/")
        //{
        //    HttpContext.Current.Response.Status = "301 Moved Permanently";
        //    HttpContext.Current.Response.AddHeader("Location", Request.Url.ToString().ToLower().Replace("https://www.dwsjewellery.com/online-jewellery/", "https://www.dwsjewellery.com/online-jewellery.html"));
        //}
        //else if (HttpContext.Current.Request.Url.ToString().ToLower() == "https://www.dwsjewellery.com/online-collection/")
        //{
        //    HttpContext.Current.Response.Status = "301 Moved Permanently";
        //    HttpContext.Current.Response.AddHeader("Location", Request.Url.ToString().ToLower().Replace("https://www.dwsjewellery.com/online-collection/", "https://www.dwsjewellery.com/online-collection.html"));
        //}
        //else if (HttpContext.Current.Request.Url.ToString().ToLower() == "https://www.dwsjewellery.com/gemstone-jewellery/")
        //{
        //    HttpContext.Current.Response.Status = "301 Moved Permanently";
        //    HttpContext.Current.Response.AddHeader("Location", Request.Url.ToString().ToLower().Replace("https://www.dwsjewellery.com/gemstone-jewellery/", "https://www.dwsjewellery.com/gemstone-jewellery.html"));
        //}

        //else if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("https://www.dwsjewellery.com/shopbygroup.html"))
        //{
        //    HttpContext.Current.Response.Status = "301 Moved Permanently";
        //    HttpContext.Current.Response.AddHeader("Location", Request.Url.ToString().ToLower().Replace("https://www.dwsjewellery.com/shopbygroup.html", "https://www.dwsjewellery.com/jewellery-shopping.html"));
        //}

        //else if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("https://www.dwsjewellery.com/shopbyjewellery.html"))
        //{
        //    HttpContext.Current.Response.Status = "301 Moved Permanently";
        //    HttpContext.Current.Response.AddHeader("Location", Request.Url.ToString().ToLower().Replace("https://www.dwsjewellery.com/shopbyjewellery.html", "https://www.dwsjewellery.com/online-jewellery.html"));
        //}

        //else if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("https://www.dwsjewellery.com/shopbycollection.html"))
        //{
        //    HttpContext.Current.Response.Status = "301 Moved Permanently";
        //    HttpContext.Current.Response.AddHeader("Location", Request.Url.ToString().ToLower().Replace("https://www.dwsjewellery.com/shopbycollection.html", "https://www.dwsjewellery.com/online-collection.html"));
        //}

        //else if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("https://www.dwsjewellery.com/shopbygemstone.html"))
        //{
        //    HttpContext.Current.Response.Status = "301 Moved Permanently";
        //    HttpContext.Current.Response.AddHeader("Location", Request.Url.ToString().ToLower().Replace("https://www.dwsjewellery.com/shopbygemstone.html", "https://www.dwsjewellery.com/gemstone-jewellery.html"));
        //}
        
        
        
    }
    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        registerRoutes(RouteTable.Routes);
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        //Exception ex = Server.GetLastError();

        ////Response.Redirect("http://localhost:1494/DWS_wholesale(17_10_2016)_sujeetnew(server)/404notfound.html", false);
        ////Response.Redirect("https://www.dwsjewellery.com/404notfound.html", false);
        ////Server.Transfer("https://www.dwsjewellery.com/404notfound.html");
        //Server.Transfer("http://localhost:1494/DWS_wholesale(17_10_2016)_sujeetnew(server)/404notfound.html", false);
        //Server.ClearError();




       // Server.Transfer("~/404notfound.aspx", false);

        //Exception exception = Server.GetLastError();
        //if (exception is HttpUnhandledException)
        //{
        //    if (exception.InnerException == null)
        //    {
        //        Server.Transfer("404notfound.aspx", false);
        //        return;
        //    }
        //    exception = exception.InnerException;
        //}

        //if (exception is HttpException)
        //{
        //    if (((HttpException)exception).GetHttpCode() == 404)
        //    {

        //        // Log if wished.
        //        Server.ClearError();
        //        Server.Transfer("~/404notfound.aspx", false);
        //        return;
        //    }
        //}

        //if (Context != null && Context.IsCustomErrorEnabled)
        //    Server.Transfer("~/404notfound.aspx", false);


        return;
    }


    //private bool AreCustomErrorsEnabledForCurrentRequest(System.Web.Configuration.CustomErrorsSection section)
    //{
    //    return section.Mode == System.Web.Configuration.CustomErrorsMode.On ||
    //           (section.Mode == System.Web.Configuration.CustomErrorsMode.RemoteOnly && !Context.Request.IsLocal);
    //}

    //private HttpResponse Response
    //{
    //    get { return Context.Response; }
    //}

    //private HttpServerUtility Server
    //{
    //    get { return Context.Server; }
    //}

    //private HttpContext Context
    //{
    //    get { return HttpContext.Current; }
    //}

    void registerRoutes(RouteCollection routes)
    {
        routes.Ignore("{resource}.axd/{*pathInfo}");
        routes.Ignore("{resource}.jpg/{*pathInfo}");
        routes.Ignore("{resource}.png/{*pathInfo}");
        routes.Ignore("{resource}.gif/{*pathInfo}");
        routes.Ignore("{resource}.JPG/{*pathInfo}");
        routes.Ignore("{resource}.PNG/{*pathInfo}");
        routes.Ignore("{resource}.GIF/{*pathInfo}");
        routes.Ignore("{sub}/{inner}/{resource}.axd/{*pathInfo}");
        routes.MapPageRoute("pagenotfound", "404notfound.html", "~/404notfound.aspx");
        routes.MapPageRoute("demo", "demo.html", "~/demo.aspx");
        routes.MapPageRoute("index", "index.html", "~/index.aspx");
        routes.MapPageRoute("login", "login.html", "~/login.aspx");
        routes.MapPageRoute("aboutus", "aboutus.html", "~/aboutus.aspx");
        routes.MapPageRoute("helpcenter", "helpcenter.html", "~/helpcenter.aspx");
        routes.MapPageRoute("downloadapp", "downloadapp.html", "~/downloadapp.aspx");
        routes.MapPageRoute("userarea", "userarea.html", "~/userarea.aspx");
        routes.MapPageRoute("sitemap", "sitemap.html", "~/sitemap.aspx");
        routes.MapPageRoute("checkout", "checkout.html", "~/checkout.aspx");
        routes.MapPageRoute("quick", "quickpay.html", "~/quickpay.aspx");
        routes.MapPageRoute("mycart", "mycart.html", "~/mycart.aspx");
        routes.MapPageRoute("customjewelry", "customjewelry.html", "~/customjewelry.aspx");
        
        routes.MapPageRoute("upcomingshows", "upcomingshows.html", "~/upcomingshows.aspx");
        routes.MapPageRoute("makeownbrand", "makeownbrand.html", "~/makeownbrand.aspx");
        routes.MapPageRoute("ourpolicies", "ourpolicies.html", "~/ourpolicies.aspx");
        routes.MapPageRoute("terms", "terms.html", "~/terms.aspx");
        routes.MapPageRoute("privacypolicy", "privacypolicy.html", "~/privacypolicy.aspx");
        routes.MapPageRoute("factoryimage", "factoryimage.html", "~/factoryimage.aspx");
        routes.MapPageRoute("contactus", "contactus.html", "~/contactus.aspx");
        routes.MapPageRoute("howtoorder", "howtoorder.html", "~/howtoorder.aspx");

        routes.MapPageRoute("myaccount", "myaccount.html", "~/myaccount.aspx");


   
        
        routes.MapPageRoute("faq", "faq.html", "~/faq.aspx");
        routes.MapPageRoute("shippingreturns", "shippingreturns.html", "~/shippingreturns.aspx");
        routes.MapPageRoute("returnsandrefund", "returnsandrefund.html", "~/returnsandrefund.aspx");
        
        routes.MapPageRoute("creditdebit", "creditdebit.html", "~/creditdebit.aspx");
        routes.MapPageRoute("testimonials", "testimonials.html", "~/testimonials.aspx");
       // routes.MapPageRoute("search", "search/{sid}", "~/groupproducts.aspx");

        routes.MapPageRoute("search", "search/{sid}", "~/productgallery.aspx");
        
        routes.MapPageRoute("collection", "collection/{name}", "~/productgallery.aspx");
        routes.MapPageRoute("jewellery", "online-jewellery/{name}", "~/groupproducts.aspx");
        routes.MapPageRoute("jewellerySub", "online-jewellery/{name}/{id}", "~/groupproducts.aspx");
        routes.MapPageRoute("collectionDws", "online-collection/{name}", "~/groupproducts.aspx");
        routes.MapPageRoute("ProductDetail", "{col}/{cat}/{id}", "~/productdescription.aspx");
        routes.MapPageRoute("gemstone", "gemstone-jewellery/{name}", "~/groupproducts.aspx");
        routes.MapPageRoute("wisthlist", "wishlist.html", "~/wishlist.aspx");




        routes.MapPageRoute("errorpage", "ErrorPage.html", "~/ErrorPage.aspx");
    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
        Session["newsletter"] = "1";
    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
