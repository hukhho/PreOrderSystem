﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PreorderPlatform.Entity.Models;

#nullable disable

namespace PreorderPlatform.Entity.Migrations
{
    [DbContext(typeof(PreOrderSystemContext))]
    [Migration("20230715170439_DbInit")]
    partial class DbInit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PreorderPlatform.Entity.Models.Business", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<string>("Email")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("owner_id");

                    b.Property<string>("Phone")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("phone");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Business", (string)null);
                });

            modelBuilder.Entity("PreorderPlatform.Entity.Models.BusinessPaymentCredential", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("BankAccountNumber")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("bank_account_number");

                    b.Property<string>("BankName")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("bank_name");

                    b.Property<string>("BankRecipientName")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("bank_recipient_name");

                    b.Property<Guid?>("BusinessId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("business_id");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime")
                        .HasColumnName("create_at");

                    b.Property<bool?>("IsMain")
                        .HasColumnType("bit")
                        .HasColumnName("is_main");

                    b.Property<bool?>("IsMomoActive")
                        .HasColumnType("bit")
                        .HasColumnName("is_momo_active");

                    b.Property<string>("MomoAccessToken")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("momo_access_token");

                    b.Property<string>("MomoApiKey")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("momo_api_key");

                    b.Property<string>("MomoPartnerCode")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("momo_partner_code");

                    b.Property<string>("MomoSecretToken")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("momo_secret_token");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("BusinessPaymentCredential", (string)null);
                });

            modelBuilder.Entity("PreorderPlatform.Entity.Models.Campaign", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid?>("BusinessId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("business_id");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime")
                        .HasColumnName("create_at");

                    b.Property<int?>("DepositPercent")
                        .HasColumnType("int")
                        .HasColumnName("deposit_percent");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<DateTime?>("EndAt")
                        .HasColumnType("datetime")
                        .HasColumnName("end_at");

                    b.Property<DateTime?>("ExpectedShippingDate")
                        .HasColumnType("date")
                        .HasColumnName("expected_shipping_date");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("modified_at");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("owner_id");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("product_id");

                    b.Property<DateTime?>("StartAt")
                        .HasColumnType("datetime")
                        .HasColumnName("start_at");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("ProductId");

                    b.ToTable("Campaign", (string)null);
                });

            modelBuilder.Entity("PreorderPlatform.Entity.Models.CampaignDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<int?>("AllowedQuantity")
                        .HasColumnType("int")
                        .HasColumnName("allowed_quantity");

                    b.Property<Guid?>("CampaignId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("campaign_id");

                    b.Property<int?>("Phase")
                        .HasColumnType("int")
                        .HasColumnName("phase");

                    b.Property<decimal?>("Price")
                        .HasColumnType("numeric(18,0)")
                        .HasColumnName("price");

                    b.Property<int?>("TotalOrdered")
                        .HasColumnType("int")
                        .HasColumnName("total_ordered");

                    b.HasKey("Id");

                    b.HasIndex("CampaignId");

                    b.ToTable("CampaignDetail", (string)null);
                });

            modelBuilder.Entity("PreorderPlatform.Entity.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("PreorderPlatform.Entity.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("created_at");

                    b.Property<decimal?>("IsDeposited")
                        .HasColumnType("numeric(18,0)")
                        .HasColumnName("is_deposited");

                    b.Property<string>("RevicerName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("revicer_name");

                    b.Property<string>("RevicerPhone")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("revicer_phone");

                    b.Property<string>("ShippingAddress")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("shipping_address");

                    b.Property<string>("ShippingCode")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("shipping_code");

                    b.Property<string>("ShippingDistrict")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("shipping_district");

                    b.Property<decimal?>("ShippingPrice")
                        .HasColumnType("numeric(18,0)")
                        .HasColumnName("shipping_price");

                    b.Property<string>("ShippingProvince")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("shipping_province");

                    b.Property<string>("ShippingStatus")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("shipping_status");

                    b.Property<string>("ShippingWard")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("shipping_ward");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("status");

                    b.Property<decimal?>("TotalPrice")
                        .HasColumnType("numeric(18,0)")
                        .HasColumnName("total_price");

                    b.Property<int?>("TotalQuantity")
                        .HasColumnType("int")
                        .HasColumnName("total_quantity");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("PreorderPlatform.Entity.Models.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid?>("CampaignDetailId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("campaign_detail_id");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("order_id");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.Property<decimal?>("UnitPrice")
                        .HasColumnType("numeric(18,0)")
                        .HasColumnName("unit_price");

                    b.HasKey("Id");

                    b.HasIndex("CampaignDetailId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItem", (string)null);
                });

            modelBuilder.Entity("PreorderPlatform.Entity.Models.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Method")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("method");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("order_id");

                    b.Property<DateTime?>("PayedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("payed_at");

                    b.Property<int?>("PaymentCount")
                        .HasColumnType("int")
                        .HasColumnName("payment_count");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("status");

                    b.Property<decimal?>("Total")
                        .HasColumnType("numeric(18,0)")
                        .HasColumnName("total");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("Payment", (string)null);
                });

            modelBuilder.Entity("PreorderPlatform.Entity.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid?>("BusinessId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("business_id");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("category_id");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<string>("Image")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("image");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<decimal?>("Price")
                        .HasColumnType("numeric(18,0)")
                        .HasColumnName("price");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("PreorderPlatform.Entity.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("PreorderPlatform.Entity.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("address");

                    b.Property<Guid?>("BusinessId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("business_id");

                    b.Property<string>("District")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("district");

                    b.Property<string>("Email")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("password");

                    b.Property<string>("Phone")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("phone");

                    b.Property<string>("Province")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("province");

                    b.Property<Guid?>("RoleId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("role_id");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("status");

                    b.Property<string>("Ward")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ward");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("RoleId");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("PreorderPlatform.Entity.Models.Business", b =>
                {
                    b.HasOne("PreorderPlatform.Entity.Models.User", "Owner")
                        .WithMany("Businesses")
                        .HasForeignKey("OwnerId")
                        .HasConstraintName("FK__Business__owner___29572725");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("PreorderPlatform.Entity.Models.BusinessPaymentCredential", b =>
                {
                    b.HasOne("PreorderPlatform.Entity.Models.Business", "Business")
                        .WithMany("BusinessPaymentCredentials")
                        .HasForeignKey("BusinessId")
                        .HasConstraintName("FK__BusinessP__busin__2D27B809");

                    b.Navigation("Business");
                });

            modelBuilder.Entity("PreorderPlatform.Entity.Models.Campaign", b =>
                {
                    b.HasOne("PreorderPlatform.Entity.Models.Business", "Business")
                        .WithMany("Campaigns")
                        .HasForeignKey("BusinessId")
                        .HasConstraintName("FK__Campaign__busine__37A5467C");

                    b.HasOne("PreorderPlatform.Entity.Models.User", "Owner")
                        .WithMany("Campaigns")
                        .HasForeignKey("OwnerId")
                        .HasConstraintName("FK__Campaign__owner___36B12243");

                    b.HasOne("PreorderPlatform.Entity.Models.Product", "Product")
                        .WithMany("Campaigns")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK__Campaign__produc__35BCFE0A");

                    b.Navigation("Business");

                    b.Navigation("Owner");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("PreorderPlatform.Entity.Models.CampaignDetail", b =>
                {
                    b.HasOne("PreorderPlatform.Entity.Models.Campaign", "Campaign")
                        .WithMany("CampaignDetails")
                        .HasForeignKey("CampaignId")
                        .HasConstraintName("FK__CampaignD__campa__3A81B327");

                    b.Navigation("Campaign");
                });

            modelBuilder.Entity("PreorderPlatform.Entity.Models.Order", b =>
                {
                    b.HasOne("PreorderPlatform.Entity.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Order__user_id__3D5E1FD2");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PreorderPlatform.Entity.Models.OrderItem", b =>
                {
                    b.HasOne("PreorderPlatform.Entity.Models.CampaignDetail", "CampaignDetail")
                        .WithMany("OrderItems")
                        .HasForeignKey("CampaignDetailId")
                        .HasConstraintName("FK__OrderItem__campa__412EB0B6");

                    b.HasOne("PreorderPlatform.Entity.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK__OrderItem__order__403A8C7D");

                    b.Navigation("CampaignDetail");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("PreorderPlatform.Entity.Models.Payment", b =>
                {
                    b.HasOne("PreorderPlatform.Entity.Models.Order", "Order")
                        .WithMany("Payments")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK__Payment__order_i__44FF419A");

                    b.HasOne("PreorderPlatform.Entity.Models.User", "User")
                        .WithMany("Payments")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Payment__user_id__440B1D61");

                    b.Navigation("Order");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PreorderPlatform.Entity.Models.Product", b =>
                {
                    b.HasOne("PreorderPlatform.Entity.Models.Business", "Business")
                        .WithMany("Products")
                        .HasForeignKey("BusinessId")
                        .HasConstraintName("FK__Product__busines__32E0915F");

                    b.HasOne("PreorderPlatform.Entity.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK__Product__categor__31EC6D26");

                    b.Navigation("Business");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("PreorderPlatform.Entity.Models.User", b =>
                {
                    b.HasOne("PreorderPlatform.Entity.Models.Business", "Business")
                        .WithMany("Users")
                        .HasForeignKey("BusinessId")
                        .HasConstraintName("FK__User__business_i__2A4B4B5E");

                    b.HasOne("PreorderPlatform.Entity.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK__User__role_id__267ABA7A");

                    b.Navigation("Business");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("PreorderPlatform.Entity.Models.Business", b =>
                {
                    b.Navigation("BusinessPaymentCredentials");

                    b.Navigation("Campaigns");

                    b.Navigation("Products");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("PreorderPlatform.Entity.Models.Campaign", b =>
                {
                    b.Navigation("CampaignDetails");
                });

            modelBuilder.Entity("PreorderPlatform.Entity.Models.CampaignDetail", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("PreorderPlatform.Entity.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("PreorderPlatform.Entity.Models.Order", b =>
                {
                    b.Navigation("OrderItems");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("PreorderPlatform.Entity.Models.Product", b =>
                {
                    b.Navigation("Campaigns");
                });

            modelBuilder.Entity("PreorderPlatform.Entity.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("PreorderPlatform.Entity.Models.User", b =>
                {
                    b.Navigation("Businesses");

                    b.Navigation("Campaigns");

                    b.Navigation("Orders");

                    b.Navigation("Payments");
                });
#pragma warning restore 612, 618
        }
    }
}
