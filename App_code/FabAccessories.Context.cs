//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

public partial class FabAccessoriesEntities : DbContext
{
    public FabAccessoriesEntities()
        : base("name=FabAccessoriesEntities")
    {
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }

    public DbSet<AdminLogin> AdminLogins { get; set; }
    public DbSet<Banner> Banners { get; set; }
    public DbSet<Catalog> Catalogs { get; set; }
    public DbSet<CategoryMaster> CategoryMasters { get; set; }
    public DbSet<CollectionBanner> CollectionBanners { get; set; }
    public DbSet<CollectionMaster> CollectionMasters { get; set; }
    public DbSet<ContactU> ContactUs { get; set; }
    public DbSet<Exibition> Exibitions { get; set; }
    public DbSet<Itembanner> Itembanners { get; set; }
    public DbSet<MaterialMaster> MaterialMasters { get; set; }
    public DbSet<NewsLetter> NewsLetters { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<OrderTbl> OrderTbls { get; set; }
    public DbSet<Producthistory> Producthistories { get; set; }
    public DbSet<productstone> productstones { get; set; }
    public DbSet<UserInfo> UserInfoes { get; set; }
    public DbSet<WebCollInnerBanner> WebCollInnerBanners { get; set; }
    public DbSet<ProductMaster_1> ProductMaster_1 { get; set; }
    public DbSet<OtherImage> OtherImages { get; set; }
    public DbSet<OrderBillingInfo> OrderBillingInfoes { get; set; }
    public DbSet<OrderShippingDetail> OrderShippingDetails { get; set; }
    public DbSet<OrderTracking> OrderTrackings { get; set; }
    public DbSet<DiscountCoupon> DiscountCoupons { get; set; }
    public DbSet<OrderPaymentResponse> OrderPaymentResponses { get; set; }
    public DbSet<ShipMaster> ShipMasters { get; set; }
    public DbSet<LoginHistory> LoginHistories { get; set; }
    public DbSet<SizeMaster> SizeMasters { get; set; }
    public DbSet<HomeGalleryBanner> HomeGalleryBanners { get; set; }
    public DbSet<FeatureMaster> FeatureMasters { get; set; }
    public DbSet<ProductFeature> ProductFeatures { get; set; }
    public DbSet<ProductMaster> ProductMasters { get; set; }
    public DbSet<Seo> Seos { get; set; }
    public DbSet<ProductReview> ProductReviews { get; set; }
    public DbSet<USPDetail> USPDetails { get; set; }
    public DbSet<StripBanner> StripBanners { get; set; }
    public DbSet<ColorMaster> ColorMasters { get; set; }
}
