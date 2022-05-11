using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

/// <summary>
/// Summary description for Google Analytics
/// Version:    2.0
/// Author:     Sarin Na Wangkanai  
/// Website:    www.sarin.mobi  
/// License:    GPL 
/// </summary>
namespace Google
{
    public class Analytics
    {
        public string UA { get; set; }

        #region BrowserSettings
        public bool ClientInfo { get; set; }
        public string DomainName { get; set; }
        public bool AllowLinker { get; set; }
        public bool AllowHash { get; set; }
        public bool DetectFlash { get; set; }
        public bool DetectTitle { get; set; }
        #endregion
        #region Campaign
        public int CampaignCookieTimeout { get; set; }
        public bool CampaignTrack { get; set; }
        public string CampaignName { get; set; }
        public string CampaignMedium { get; set; }
        public string CampaignSource { get; set; }
        public string CampaignTerm { get; set; }
        public string CampaignContent { get; set; }
        public string CampaignNoOverride { get; set; }
        #endregion

        /// <summary>
        /// Google Analytics allows you to define custom segments and analyze the behavior of each segment.
        /// </summary>
        /// <remarks>http://www.google.com/support/googleanalytics/bin/answer.py?answer=57045</remarks>
        public string VisitorSegment { get; set; }

        private string GoogleJavascriptLibrary
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("(function() {");
                builder.AppendLine("\tvar ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;");
                builder.AppendLine("\tga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';");
                builder.AppendLine("\tvar s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);");
                builder.AppendLine("})();");
                return builder.ToString();
            }
        }
        private Transaction trans { get; set; }

        /// <summary>
        /// Create the google analytics tracking code instance
        /// </summary>
        /// <param name="ua">(UA-XXXXX-YY) Your web property ID, informally referred to as UA number, can be found by clicking the "check status" link or by searching for "UA-" in the source code of your web page.</param>
        public Analytics(string ua)
        {
            this.UA = ua;
            this.ClientInfo = true;
            this.AllowHash = true;
            this.DetectFlash = true;
            this.DetectTitle = true;
            this.CampaignTrack = true;
        }

        /// <summary>
        /// Add transaction tracking to the google analytics tracker code
        /// </summary>
        /// <param name="transaction">transaction object</param>
        public void AddTrans(Transaction transaction)
        {
            this.trans = transaction;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("<script type=\"text/javascript\">");
            builder.AppendLine("\tvar _gaq = _gaq || [];");
            builder.AppendLine(string.Format("\t_gaq.push(['_setAccount', '{0}']);", UA));
            if (!ClientInfo) builder.AppendLine("\t_gaq.push(['_setClientInfo', false]);");
            if (DomainName != null) builder.AppendLine(string.Format("\t_gaq.push(['_setDomainName', '{0}']);", DomainName));
            if (AllowLinker) builder.AppendLine("\t_gaq.push(['_setAllowLinker', true]);");
            if (!AllowHash) builder.AppendLine("\t_gaq.push(['_setAllowHash', false]);");
            if (!DetectFlash) builder.AppendLine("\t_gaq.push(['_setDetectFlash', false]);");
            if (!DetectTitle) builder.AppendLine("\t_gaq.push(['_setDetectTitle', false]);");

            if (!CampaignTrack) builder.AppendLine("\t_gaq.push(['_setCampaignTrack', false]);");
            if (CampaignCookieTimeout > 0) builder.AppendLine(string.Format("\t_gaq.push(['_setCampaignCookieTimeout', {0}]);", CampaignCookieTimeout));
            if (CampaignName != null) builder.AppendLine(string.Format("\t_gaq.push(['_setCampNameKey', '{0}']);", CampaignName));
            if (CampaignMedium != null) builder.AppendLine(string.Format("\t_gaq.push(['_setCampMediumKey', '{0}']);",CampaignMedium));
            if (CampaignSource != null) builder.AppendLine(string.Format("\t_gaq.push(['_setCampSourceKey', '{0}']);", CampaignSource));
            if (CampaignTerm != null) builder.AppendLine(string.Format("\t_gaq.push(['_setCampTermKey', '{0}']);", CampaignTerm));
            if (CampaignContent != null) builder.AppendLine(string.Format("\t_gaq.push(['_setCampContentKey', '{0}']);", CampaignContent));
            if (CampaignNoOverride != null) builder.AppendLine(string.Format("\t_gaq.push(['_setCampNOKey', '{0}']);", CampaignNoOverride));

            if (VisitorSegment != null) builder.AppendLine(string.Format("\t_gaq.push(['_setVar','{0}']);", VisitorSegment));

            builder.AppendLine("\t_gaq.push(['_trackPageview']);");
            if (trans != null)
            {
                builder.AppendLine("\t" + trans.AddTrans());
                foreach (Item item in trans.Items)
                    builder.AppendLine("\t" + item.ToScript(trans.OrderID));
                builder.AppendLine("\t" + trans.Submit());
            }
            builder.AppendLine();
            builder.AppendLine(GoogleJavascriptLibrary);
            builder.AppendLine("</script>");
            return builder.ToString();
        }
    }
    public class Event
    {
        public string Category { get; set; }
        public string Action { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }

        public Event(string category, string action)
        {
            this.Category = category;
            this.Action = action;
        }

        public Event(string category, string action, string label, string value):this(category, action)
        {
            this.Label = label;
            this.Value = value;
        }

        public override string ToString()
        {
            return string.Format("_gaq.push(['_trackEvent', '{0}', '{1}', '{2}', '{3}]);",
                Category, Action, Label, Value);
        }
    }
    public class Transaction
    {
        public string OrderID { get; set; }     // order ID - required
        public string StoreName { get; set; }   // affiliation or store name
        public decimal Total { get; set; }      // total - required
        public decimal Tax { get; set; }        // tax
        public decimal Shipping { get; set; }   // shipping cost
        public string City { get; set; }        // city
        public string State { get; set; }       // state or province
        public string Country { get; set; }     // country


        public List<Item> Items { get; set; }   // List of itmes in this invoice transaction

        /// <summary>
        /// The transaction object stores all the related information about a single transaction, such as the order ID, shipping charges, and billing address.
        /// </summary>
        /// <param name="orderID">transaction order id (required)</param>
        /// <param name="storename">affiliation or store name</param>
        /// <param name="tax">tax</param>
        /// <param name="shipping">shipping cost</param>
        /// <param name="city">city</param>
        /// <param name="state">state or province</param>
        /// <param name="country">country</param>
        public Transaction(string orderID, string storename, decimal tax, decimal shipping, string city, string state, string country)
        {
            this.OrderID = orderID;
            this.StoreName = storename;
            this.Tax = tax;
            this.Shipping = shipping;
            this.City = city;
            this.State = state;
            this.Country = country;
            this.Items = new List<Item>();
        }

        /// <summary>
        /// add item might be called for every item in the shopping cart
        /// where your ecommerce engine loops through each item in the cart and
        /// prints out _addItem for each
        /// </summary>
        /// <param name="item">item object</param>
        public void Add(Item item)
        {
            this.Total += (item.Price * item.Quantity);
            this.Items.Add(item);
        }

        #region googlejavascript
        /// <summary>
        /// create a transaction javascript method call
        /// </summary>
        /// <returns>google javascript codes</returns>
        internal string AddTrans()
        {            
            return string.Format("_gaq.push(['_addTrans', '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}']);",
                OrderID, StoreName, Total, Tax, Shipping, City, State, Country);
        }
        /// <summary>
        /// submits transaction to the Analytics servers
        /// </summary>
        /// <returns>google javascript codes</returns>
        internal string Submit()
        {
            return "_gaq.push(['_trackTrans']);";
        }
        #endregion
    }
    public class Item
    {
        public string SKU { get; set; }         // SKU/code - required
        public string Name { get; set; }        // product name
        public string Category { get; set; }    // category or variation
        public decimal Price { get; set; }      // unit price - required
        public int Quantity { get; set; }       // quantity - required

        /// <summary>
        /// tracks information about each individual item in the user's shopping cart 
        /// and associates the item with each transaction.
        /// </summary>
        /// <param name="sku">SKU/Code (required)</param>
        /// <param name="name">Product name (optional)</param>
        /// <param name="category">Category (optional)</param>
        /// <param name="price">unit price (required)</param>
        /// <param name="quantity">quantity (required)</param>
        public Item(string sku, string name, string category, decimal price, int quantity)
        {
            this.SKU = sku;
            this.Name = name;
            this.Category = category;
            this.Price = price;
            this.Quantity = quantity;
        }

        /// <summary>
        /// Create a item javascript method call
        /// </summary>
        /// <param name="orderid">order ID for the transcation</param>
        /// <returns>google javascript codes</returns>
        internal string ToScript(string orderid)
        {
            return string.Format("_gaq.push(['_addItem', '{0}', '{1}', '{2}', '{3}', '{4}', '{5}']);",
                orderid, SKU, Name, Category, Price, Quantity);
        }
    }
}