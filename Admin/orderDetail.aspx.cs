using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_orderdetail : System.Web.UI.Page
{
    FabAccessoriesEntities db = new FabAccessoriesEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["order"] != null)
            {
                bindgrid();
            }
        }
    }

    public void bindgrid()
    {
        string Oredreno = Request["order"].ToString();
        enquiryno.InnerText = "Order No:" + Oredreno;
        var getdetail = (from odt in db.OrderDetails
                         from pdm in db.ProductMasters.Where(pdm => pdm.Id == odt.ProductId)
                         where odt.OrderNo == Oredreno
                         select new
                         {
                             odt.ProductId,
                             odt.ModelNo,
                             odt.Item,
                             odt.Qty,
                             odt.Price,
                             odt.Total,
                             odt.Img,
                             ShortDesc = odt.ShortDesc == null ? "" : odt.ShortDesc,
                             unitprice = pdm.MRP,
                             odt.Size,
                             odt.Comment
                         }).ToList();
        //var getdetail = 0;

        if (getdetail.Count > 0)
        {
            //ltrlTotalAmt.Text = getdetail.Sum(x => Math.Round(Convert.ToDecimal((x.unitprice) * (x.Qty)))).ToString();

            repCart.DataSource = getdetail;
            repCart.DataBind();

            var amt = db.OrderTbls.Where(r => r.OrderNo == Oredreno).FirstOrDefault();
            string dis = amt.CouponAmt == null ? "0.00" : amt.CouponAmt.ToString();
            ltrlTotalAmt.Text = amt.OrderTotal.ToString().Replace(".00", "");
            if (dis != "0.00")
            {
                ltrsubtot.Text = amt.OrderTotal.ToString().Replace(".00", "");
                ltrdiscount.Text = amt.CouponAmt.ToString().Replace(".00", "");
                ltrlTotalAmt.Text = (amt.OrderTotal - amt.CouponAmt).ToString().Replace(".00","");
                discount.Visible = true;
            }

            if (amt.status == "1")
            {
                lblpaymentstatus.Text = "Paid";
                lblpaymentstatus.Style.Add("color", "Green");

                var payid = db.OrderPaymentResponses.Where(r => r.OrderId == Oredreno).FirstOrDefault();

                if (payid != null)
                {
                    lblpaymentid.Text = " | Payment ID: " + payid.PaymentId;
                }
                else
                {
                    lblpaymentid.Text = "";
                }

            }
            else
            {
                lblpaymentstatus.Text = "Unpaid";
                lblpaymentstatus.Style.Add("color", "Red");
            }


            var billinfo = db.OrderBillingInfoes.Where(r => r.OrderNo == Oredreno).FirstOrDefault();

            if (billinfo != null)
            {
                lblbillname.Text = billinfo.Name;
                lblbilladdress1.Text = billinfo.Address;

                if (Convert.ToString(billinfo.Address2) != "")
                {
                    lblbilladdress2.Text = billinfo.Address2 +"<br />";
                }
                else
                {
                    lblbilladdress2.Style.Add("display","none");
                }


                
                lblbillcity.Text = billinfo.City;
                lblbillstate.Text = billinfo.State;
                lblbillcountry.Text = billinfo.Country;
                lblbillzip.Text = billinfo.Zip;
                lblbillcontactno.Text = billinfo.ContactNo;
                lblbillemail.Text = billinfo.Email;
            }



            var shipinfo = db.OrderShippingDetails.Where(r => r.OrderNo == Oredreno).FirstOrDefault();

            if (shipinfo != null)
            {
                lblshipname.Text = shipinfo.Name;
                lblshipaddress1.Text = shipinfo.Address;

                if (Convert.ToString(shipinfo.Address2) != "")
                {
                    lblshipaddress2.Text = shipinfo.Address2 + "<br />";
                }
                else
                {
                    lblshipaddress2.Style.Add("display", "none");
                }

                lblshipcity.Text = shipinfo.City;
                lblshipstate.Text = shipinfo.State;
                lblshipcountry.Text = shipinfo.Country;
                lblshipzip.Text = shipinfo.Zip;
                lblshipcontactno.Text = shipinfo.ContactNo;
                lblshipemail.Text = shipinfo.Email;
            }



        }
    }
}