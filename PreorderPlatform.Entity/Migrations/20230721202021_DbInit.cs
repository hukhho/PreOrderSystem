using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PreOrderPlatform.Entity.Migrations
{
    public partial class DbInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Business",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    email = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    owner_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Business", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessPaymentCredential",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    business_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    bank_account_number = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    bank_name = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    bank_recipient_name = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    momo_api_key = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    momo_partner_code = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    momo_access_token = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    momo_secret_token = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    is_momo_active = table.Column<bool>(type: "bit", nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    create_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessPaymentCredential", x => x.id);
                    table.ForeignKey(
                        name: "FK__BusinessP__busin__2D27B809",
                        column: x => x.business_id,
                        principalTable: "Business",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    image = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    business_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.id);
                    table.ForeignKey(
                        name: "FK__Product__busines__32E0915F",
                        column: x => x.business_id,
                        principalTable: "Business",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Product__categor__31EC6D26",
                        column: x => x.category_id,
                        principalTable: "Category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    email = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    password = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ward = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    district = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    business_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    action_token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    action_token_expiration = table.Column<DateTime>(type: "datetime", nullable: true),
                    action_token_type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                    table.ForeignKey(
                        name: "FK__User__business_i__2A4B4B5E",
                        column: x => x.business_id,
                        principalTable: "Business",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__User__role_id__267ABA7A",
                        column: x => x.role_id,
                        principalTable: "Role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Campaign",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    start_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    end_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    deposit_percent = table.Column<int>(type: "int", nullable: false),
                    expected_shipping_date = table.Column<DateTime>(type: "date", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Visibility = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    owner_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    business_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaign", x => x.id);
                    table.ForeignKey(
                        name: "FK__Campaign__busine__37A5467C",
                        column: x => x.business_id,
                        principalTable: "Business",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Campaign__owner___36B12243",
                        column: x => x.owner_id,
                        principalTable: "User",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Campaign__produc__35BCFE0A",
                        column: x => x.product_id,
                        principalTable: "Product",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    total_quantity = table.Column<int>(type: "int", nullable: false),
                    total_price = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    is_deposited = table.Column<bool>(type: "bit", nullable: false),
                    required_deposit_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    revicer_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    revicer_phone = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    shipping_address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    shipping_province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    shipping_ward = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    shipping_district = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    shipping_code = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    shipping_price = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    shipping_status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    estimated_delivery_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    cancelled_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    cancelled_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cancelled_reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.id);
                    table.ForeignKey(
                        name: "FK__Order__user_id__3D5E1FD2",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CampaignDetail",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    phase = table.Column<int>(type: "int", nullable: false),
                    allowed_quantity = table.Column<int>(type: "int", nullable: false),
                    PhaseStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    total_ordered = table.Column<int>(type: "int", nullable: false),
                    campaign_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    price = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignDetail", x => x.id);
                    table.ForeignKey(
                        name: "FK__CampaignD__campa__3A81B327",
                        column: x => x.campaign_id,
                        principalTable: "Campaign",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CampaignImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsThumbnail = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    CampaignId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CampaignImage_Campaign_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaign",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    payment_count = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    payment_amount = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    transaction_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    method = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    payed_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.id);
                    table.ForeignKey(
                        name: "FK__Payment__order_i__44FF419A",
                        column: x => x.order_id,
                        principalTable: "Order",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Payment__user_id__440B1D61",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    campaign_detail_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    unit_price = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.id);
                    table.ForeignKey(
                        name: "FK__OrderItem__campa__412EB0B6",
                        column: x => x.campaign_detail_id,
                        principalTable: "CampaignDetail",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__OrderItem__order__403A8C7D",
                        column: x => x.order_id,
                        principalTable: "Order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Business_owner_id",
                table: "Business",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessPaymentCredential_business_id_IsMain",
                table: "BusinessPaymentCredential",
                columns: new[] { "business_id", "IsMain" },
                unique: true,
                filter: "[IsMain] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_Campaign_business_id",
                table: "Campaign",
                column: "business_id");

            migrationBuilder.CreateIndex(
                name: "IX_Campaign_owner_id",
                table: "Campaign",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "IX_Campaign_product_id",
                table: "Campaign",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignDetail_campaign_id",
                table: "CampaignDetail",
                column: "campaign_id");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignImage_CampaignId_IsThumbnail",
                table: "CampaignImage",
                columns: new[] { "CampaignId", "IsThumbnail" },
                unique: true,
                filter: "[IsThumbnail] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_Order_user_id",
                table: "Order",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_campaign_detail_id",
                table: "OrderItem",
                column: "campaign_detail_id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_order_id",
                table: "OrderItem",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_order_id",
                table: "Payment",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_user_id",
                table: "Payment",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_business_id",
                table: "Product",
                column: "business_id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_category_id",
                table: "Product",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_business_id",
                table: "User",
                column: "business_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_role_id",
                table: "User",
                column: "role_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Business__owner___29572725",
                table: "Business",
                column: "owner_id",
                principalTable: "User",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Business__owner___29572725",
                table: "Business");

            migrationBuilder.DropTable(
                name: "BusinessPaymentCredential");

            migrationBuilder.DropTable(
                name: "CampaignImage");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "CampaignDetail");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Campaign");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Business");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
