﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PreOrderPlatform.Entity.Models;

#nullable disable

namespace PreOrderPlatform.Entity.Migrations
{
    [DbContext(typeof(PreOrderSystemContext))]
    partial class PreOrderSystemContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PreOrderPlatform.Entity.Models.Business", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<string>("Email")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("email");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<string>("LogoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("owner_id");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("phone");

                    b.Property<bool>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("status");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Business", (string)null);
                });

            modelBuilder.Entity("PreOrderPlatform.Entity.Models.BusinessPaymentCredential", b =>
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

                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("business_id");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime")
                        .HasColumnName("create_at");

                    b.Property<bool>("IsMain")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMomoActive")
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

                    b.Property<bool>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId", "IsMain")
                        .IsUnique()
                        .HasFilter("[IsMain] = 1");

                    b.ToTable("BusinessPaymentCredential", (string)null);
                });

            modelBuilder.Entity("PreOrderPlatform.Entity.Models.Campaign", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("business_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("created_at");

                    b.Property<int>("DepositPercent")
                        .HasColumnType("int")
                        .HasColumnName("deposit_percent");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<DateTime>("EndAt")
                        .HasColumnType("datetime")
                        .HasColumnName("end_at");

                    b.Property<DateTime?>("ExpectedShippingDate")
                        .HasColumnType("date")
                        .HasColumnName("expected_shipping_date");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("is_deleted");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("owner_id");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("product_id");

                    b.Property<DateTime>("StartAt")
                        .HasColumnType("datetime")
                        .HasColumnName("start_at");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("updated_at");

                    b.Property<string>("Visibility")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("ProductId");

                    b.ToTable("Campaign", (string)null);
                });

            modelBuilder.Entity("PreOrderPlatform.Entity.Models.CampaignDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<int>("AllowedQuantity")
                        .HasColumnType("int")
                        .HasColumnName("allowed_quantity");

                    b.Property<Guid>("CampaignId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("campaign_id");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("is_deleted");

                    b.Property<int>("Phase")
                        .HasColumnType("int")
                        .HasColumnName("phase");

                    b.Property<string>("PhaseStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric(18,0)")
                        .HasColumnName("price");

                    b.Property<int>("TotalOrdered")
                        .HasColumnType("int")
                        .HasColumnName("total_ordered");

                    b.HasKey("Id");

                    b.HasIndex("CampaignId");

                    b.ToTable("CampaignDetail", (string)null);
                });

            modelBuilder.Entity("PreOrderPlatform.Entity.Models.CampaignImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CampaignId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsThumbnail")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CampaignId", "IsThumbnail")
                        .IsUnique()
                        .HasFilter("[IsThumbnail] = 1");

                    b.ToTable("CampaignImage");
                });

            modelBuilder.Entity("PreOrderPlatform.Entity.Models.Category", b =>
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

            modelBuilder.Entity("PreOrderPlatform.Entity.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime?>("CancelledAt")
                        .HasColumnType("datetime")
                        .HasColumnName("cancelled_at");

                    b.Property<string>("CancelledBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("cancelled_by");

                    b.Property<string>("CancelledReason")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("cancelled_reason");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("EstimatedDeliveryDate")
                        .HasColumnType("datetime")
                        .HasColumnName("estimated_delivery_date");

                    b.Property<bool>("IsDeposited")
                        .HasColumnType("bit")
                        .HasColumnName("is_deposited");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("note");

                    b.Property<decimal>("RequiredDepositAmount")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("required_deposit_amount");

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
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("status");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("numeric(18,0)")
                        .HasColumnName("total_price");

                    b.Property<int>("TotalQuantity")
                        .HasColumnType("int")
                        .HasColumnName("total_quantity");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("updated_at");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("updated_by");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("PreOrderPlatform.Entity.Models.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("CampaignDetailId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("campaign_detail_id");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("order_id");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("numeric(18,0)")
                        .HasColumnName("unit_price");

                    b.HasKey("Id");

                    b.HasIndex("CampaignDetailId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItem", (string)null);
                });

            modelBuilder.Entity("PreOrderPlatform.Entity.Models.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Method")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("method");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("order_id");

                    b.Property<DateTime?>("PayedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("payed_at");

                    b.Property<decimal?>("PaymentAmount")
                        .HasColumnType("numeric(18,0)")
                        .HasColumnName("payment_amount");

                    b.Property<int?>("PaymentCount")
                        .HasColumnType("int")
                        .HasColumnName("payment_count");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("status");

                    b.Property<string>("TransactionId")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("transaction_id");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("updated_at");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("Payment", (string)null);
                });

            modelBuilder.Entity("PreOrderPlatform.Entity.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("business_id");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("category_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<string>("Image")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("image");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<bool>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("status");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("PreOrderPlatform.Entity.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("PreOrderPlatform.Entity.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("ActionToken")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("action_token");

                    b.Property<DateTime?>("ActionTokenExpiration")
                        .HasColumnType("datetime")
                        .HasColumnName("action_token_expiration");

                    b.Property<string>("ActionTokenType")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("action_token_type");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("address");

                    b.Property<Guid?>("BusinessId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("business_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("created_at");

                    b.Property<string>("District")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("district");

                    b.Property<string>("Email")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("password");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("phone");

                    b.Property<string>("Province")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("province");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("role_id");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("status");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("updated_at");

                    b.Property<string>("Ward")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ward");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("RoleId");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("PreOrderPlatform.Entity.Models.Business", b =>
                {
                    b.HasOne("PreOrderPlatform.Entity.Models.User", "Owner")
                        .WithMany("Businesses")
                        .HasForeignKey("OwnerId")
                        .HasConstraintName("FK__Business__owner___29572725");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("PreOrderPlatform.Entity.Models.BusinessPaymentCredential", b =>
                {
                    b.HasOne("PreOrderPlatform.Entity.Models.Business", "Business")
                        .WithMany("BusinessPaymentCredentials")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__BusinessP__busin__2D27B809");

                    b.Navigation("Business");
                });

            modelBuilder.Entity("PreOrderPlatform.Entity.Models.Campaign", b =>
                {
                    b.HasOne("PreOrderPlatform.Entity.Models.Business", "Business")
                        .WithMany("Campaigns")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK__Campaign__busine__37A5467C");

                    b.HasOne("PreOrderPlatform.Entity.Models.User", "Owner")
                        .WithMany("Campaigns")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK__Campaign__owner___36B12243");

                    b.HasOne("PreOrderPlatform.Entity.Models.Product", "Product")
                        .WithMany("Campaigns")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK__Campaign__produc__35BCFE0A");

                    b.Navigation("Business");

                    b.Navigation("Owner");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("PreOrderPlatform.Entity.Models.CampaignDetail", b =>
                {
                    b.HasOne("PreOrderPlatform.Entity.Models.Campaign", "Campaign")
                        .WithMany("CampaignDetails")
                        .HasForeignKey("CampaignId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__CampaignD__campa__3A81B327");

                    b.Navigation("Campaign");
                });

            modelBuilder.Entity("PreOrderPlatform.Entity.Models.CampaignImage", b =>
                {
                    b.HasOne("PreOrderPlatform.Entity.Models.Campaign", "Campaign")
                        .WithMany("Images")
                        .HasForeignKey("CampaignId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Campaign");
                });

            modelBuilder.Entity("PreOrderPlatform.Entity.Models.Order", b =>
                {
                    b.HasOne("PreOrderPlatform.Entity.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__Order__user_id__3D5E1FD2");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PreOrderPlatform.Entity.Models.OrderItem", b =>
                {
                    b.HasOne("PreOrderPlatform.Entity.Models.CampaignDetail", "CampaignDetail")
                        .WithMany("OrderItems")
                        .HasForeignKey("CampaignDetailId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK__OrderItem__campa__412EB0B6");

                    b.HasOne("PreOrderPlatform.Entity.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__OrderItem__order__403A8C7D");

                    b.Navigation("CampaignDetail");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("PreOrderPlatform.Entity.Models.Payment", b =>
                {
                    b.HasOne("PreOrderPlatform.Entity.Models.Order", "Order")
                        .WithMany("Payments")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK__Payment__order_i__44FF419A");

                    b.HasOne("PreOrderPlatform.Entity.Models.User", "User")
                        .WithMany("Payments")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Payment__user_id__440B1D61");

                    b.Navigation("Order");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PreOrderPlatform.Entity.Models.Product", b =>
                {
                    b.HasOne("PreOrderPlatform.Entity.Models.Business", "Business")
                        .WithMany("Products")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__Product__busines__32E0915F");

                    b.HasOne("PreOrderPlatform.Entity.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__Product__categor__31EC6D26");

                    b.Navigation("Business");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("PreOrderPlatform.Entity.Models.User", b =>
                {
                    b.HasOne("PreOrderPlatform.Entity.Models.Business", "Business")
                        .WithMany("Users")
                        .HasForeignKey("BusinessId")
                        .HasConstraintName("FK__User__business_i__2A4B4B5E");

                    b.HasOne("PreOrderPlatform.Entity.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__User__role_id__267ABA7A");

                    b.Navigation("Business");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("PreOrderPlatform.Entity.Models.Business", b =>
                {
                    b.Navigation("BusinessPaymentCredentials");

                    b.Navigation("Campaigns");

                    b.Navigation("Products");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("PreOrderPlatform.Entity.Models.Campaign", b =>
                {
                    b.Navigation("CampaignDetails");

                    b.Navigation("Images");
                });

            modelBuilder.Entity("PreOrderPlatform.Entity.Models.CampaignDetail", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("PreOrderPlatform.Entity.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("PreOrderPlatform.Entity.Models.Order", b =>
                {
                    b.Navigation("OrderItems");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("PreOrderPlatform.Entity.Models.Product", b =>
                {
                    b.Navigation("Campaigns");
                });

            modelBuilder.Entity("PreOrderPlatform.Entity.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("PreOrderPlatform.Entity.Models.User", b =>
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
