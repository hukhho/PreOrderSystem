using Microsoft.EntityFrameworkCore;

namespace PreOrderPlatform.Entity.Models
{
    public partial class PreOrderSystemContext : DbContext
    {
        public PreOrderSystemContext() { }

        public PreOrderSystemContext(DbContextOptions<PreOrderSystemContext> options)
            : base(options) { }

        public virtual DbSet<Business> Businesses { get; set; } = null!;
        public virtual DbSet<BusinessPaymentCredential> BusinessPaymentCredentials { get; set; } =
            null!;
        public virtual DbSet<Campaign> Campaigns { get; set; } = null!;
        public virtual DbSet<CampaignDetail> CampaignDetails { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderItem> OrderItems { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer(
//                    "Server=(local);Uid=sa;Pwd=123;Database=PreOrderSystem"
//                );
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set global DeleteBehavior to NoAction
            //foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            //{
            //    relationship.DeleteBehavior = DeleteBehavior.NoAction;
            //}

            modelBuilder.Entity<Business>(entity =>
            {
                entity.ToTable("Business");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Email).IsUnicode(false).HasColumnName("email");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.OwnerId).HasColumnName("owner_id");

                entity.Property(e => e.Phone).IsUnicode(false).HasColumnName("phone");

                entity.Property(e => e.Status).HasColumnName("status");

                entity
                    .HasOne(d => d.Owner)
                    .WithMany(p => p.Businesses)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("FK__Business__owner___29572725");
            });

            modelBuilder.Entity<BusinessPaymentCredential>(entity =>
            {
                entity.ToTable("BusinessPaymentCredential");

                entity.Property(e => e.Id).HasColumnName("id");

                entity
                    .Property(e => e.BankAccountNumber)
                    .IsUnicode(false)
                    .HasColumnName("bank_account_number");

                entity.Property(e => e.BankName).IsUnicode(false).HasColumnName("bank_name");

                entity
                    .Property(e => e.BankRecipientName)
                    .IsUnicode(false)
                    .HasColumnName("bank_recipient_name");

                entity.Property(e => e.BusinessId).HasColumnName("business_id");

                entity
                    .Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("create_at");


                entity.HasIndex(e => new { e.BusinessId, e.IsMain })
                        .IsUnique()
                        .HasFilter("[IsMain] = 1");

                entity.Property(e => e.IsMomoActive).HasColumnName("is_momo_active");

                entity
                    .Property(e => e.MomoAccessToken)
                    .IsUnicode(false)
                    .HasColumnName("momo_access_token");

                entity.Property(e => e.MomoApiKey).IsUnicode(false).HasColumnName("momo_api_key");

                entity
                    .Property(e => e.MomoPartnerCode)
                    .IsUnicode(false)
                    .HasColumnName("momo_partner_code");

                entity
                    .Property(e => e.MomoSecretToken)
                    .IsUnicode(false)
                    .HasColumnName("momo_secret_token");

                entity.Property(e => e.Status).HasColumnName("status");

                entity
                    .HasOne(d => d.Business)
                    .WithMany(p => p.BusinessPaymentCredentials)
                    .HasForeignKey(d => d.BusinessId)
                    .HasConstraintName("FK__BusinessP__busin__2D27B809");
            });

            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.ToTable("Campaign");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BusinessId).HasColumnName("business_id");

                entity
                    .Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity
                   .Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .HasColumnName("updated_at");

                entity.Property(e => e.DepositPercent).HasColumnName("deposit_percent");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.EndAt).HasColumnType("datetime").HasColumnName("end_at");

                entity
                    .Property(e => e.ExpectedShippingDate)
                    .HasColumnType("date")
                    .HasColumnName("expected_shipping_date");

           

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.OwnerId).HasColumnName("owner_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.StartAt).HasColumnType("datetime").HasColumnName("start_at");

                entity.Property(e => e.Status)
                      .IsRequired()
                 .HasConversion<string>();

                entity.Property(e => e.Type)
                    .IsRequired()
                     .HasConversion<string>();

                entity.Property(e => e.Visibility)
                    .IsRequired()
                    .HasConversion<string>();

                entity.Property(e => e.Location)
                    .IsRequired()
                     .HasConversion<string>();

                entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");

                entity
                    .HasOne(d => d.Business)
                    .WithMany(p => p.Campaigns)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK__Campaign__busine__37A5467C");

                entity
                    .HasOne(d => d.Owner)
                    .WithMany(p => p.Campaigns)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK__Campaign__owner___36B12243");

                entity
                    .HasOne(d => d.Product)
                    .WithMany(p => p.Campaigns)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.NoAction) // prevent cascade delete
                    .HasConstraintName("FK__Campaign__produc__35BCFE0A");
            });

            modelBuilder.Entity<CampaignImage>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Url)
                    .IsRequired();

                entity.Property(e => e.Description);

                entity.Property(e => e.IsThumbnail)
                    .HasDefaultValue(false);

                entity.HasIndex(e => new { e.CampaignId, e.IsThumbnail })
                       .IsUnique()
                       .HasFilter("[IsThumbnail] = 1");

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.CampaignId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CampaignDetail>(entity =>
            {
                entity.ToTable("CampaignDetail");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AllowedQuantity).HasColumnName("allowed_quantity");

                entity.Property(e => e.CampaignId).HasColumnName("campaign_id");

                entity.Property(e => e.Phase).HasColumnName("phase");

                entity
                    .Property(e => e.Price)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("price");

                entity.Property(e => e.TotalOrdered).HasColumnName("total_ordered");

                //        public PhaseStatus PhaseStatus { get; set; }  // new field

                entity.Property(e => e.PhaseStatus)
                    .IsRequired()
                    .HasConversion<string>();

                // Add the IsDeleted property in your database and set it to false by default
                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValue(false)
                    .HasColumnName("is_deleted");

                entity
                    .HasOne(d => d.Campaign)
                    .WithMany(p => p.CampaignDetails)
                    .HasForeignKey(d => d.CampaignId)
                    .HasConstraintName("FK__CampaignD__campa__3A81B327");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Id).HasColumnName("id");

                entity
                    .Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.IsDeposited).IsRequired().HasColumnName("is_deposited");

                entity
                    .Property(e => e.RequiredDepositAmount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("required_deposit_amount");

                entity.Property(e => e.RevicerName).HasColumnName("revicer_name");

                entity
                    .Property(e => e.RevicerPhone)
                    .IsUnicode(false)
                    .HasColumnName("revicer_phone");

                entity.Property(e => e.ShippingAddress).HasColumnName("shipping_address");

                entity
                    .Property(e => e.ShippingCode)
                    .IsUnicode(false)
                    .HasColumnName("shipping_code");

                entity.Property(e => e.ShippingDistrict).HasColumnName("shipping_district");

                entity
                    .Property(e => e.ShippingPrice)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("shipping_price");

                entity.Property(e => e.ShippingProvince).HasColumnName("shipping_province");

                entity.Property(e => e.ShippingWard).HasColumnName("shipping_ward");

                
                entity
                    .Property(e => e.TotalPrice)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("total_price");

                entity.Property(e => e.TotalQuantity).HasColumnName("total_quantity");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
                //Hey copilot make me public DateTime? EstimatedDeliveryDate { get; set; } public DateTime? CancelledAt { get; set; } public string? CancelledBy { get; set; } public string? UpdatedBy { get; set; } public string? Note { get; set; }
                entity.Property(e => e.EstimatedDeliveryDate)
                    .HasColumnType("datetime")
                    .HasColumnName("estimated_delivery_date");
                entity.Property(e => e.CancelledAt)
                    .HasColumnType("datetime")
                    .HasColumnName("cancelled_at");
                entity.Property(e => e.CancelledBy)
                .HasColumnName("cancelled_by");
                entity.Property(e => e.UpdatedBy)
                .HasColumnName("updated_by");
                entity.Property(e => e.CancelledReason)
               .HasColumnName("cancelled_reason");  
                entity.Property(e => e.Note)
                .HasColumnName("note");
                entity
                 .Property(e => e.ShippingStatus)
                 .HasConversion<string>()
                 .HasColumnName("shipping_status");

                entity
                    .Property(e => e.Status)
                    .HasConversion<string>()
                    .HasColumnName("status");

                entity
                    .HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Order__user_id__3D5E1FD2");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("OrderItem");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CampaignDetailId).HasColumnName("campaign_detail_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity
                    .Property(e => e.UnitPrice)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("unit_price");

                entity
                    .HasOne(d => d.CampaignDetail)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.CampaignDetailId)
                    .OnDelete(DeleteBehavior.NoAction) // prevent cascade delete
                    .HasConstraintName("FK__OrderItem__campa__412EB0B6");

                entity
                    .HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrderItem__order__403A8C7D");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Method).HasColumnName("method");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.PayedAt).HasColumnType("datetime").HasColumnName("payed_at");

                entity.Property(e => e.PaymentCount).HasColumnName("payment_count");

                entity.Property(e => e.Status).HasColumnName("status");
                //public DateTime? UpdatedAt { get; set; } public decimal? PaymentAmount { get; set; }
                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
                entity.Property(e => e.PaymentAmount)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("payment_amount");

                //public PaymentMethod Method { get; set; } public PaymentStatus Status { get; set; }         public string? CancelledReason { get; set; }
          
                entity.Property(e => e.Method)
                    .HasConversion<string>()
                    .HasColumnName("method");   
                entity.Property(e => e.Status)
                .HasConversion<string>()
                    .HasColumnName("status");

                //        public string? TransactionId { get; set; }
                entity.Property(e => e.TransactionId)
                .HasColumnName("transaction_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity
                    .HasOne(d => d.Order)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__Payment__order_i__44FF419A");

                entity
                    .HasOne(d => d.User)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Payment__user_id__440B1D61");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BusinessId).HasColumnName("business_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Image).IsUnicode(false).HasColumnName("image");

                entity.Property(e => e.Name).HasColumnName("name");

                entity
                  .Property(e => e.CreatedAt)
                  .HasColumnType("datetime")
                  .HasColumnName("created_at");

                entity
                   .Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .HasColumnName("updated_at");


                entity.Property(e => e.Status).HasColumnName("status");

                entity
                    .HasOne(d => d.Business)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BusinessId)
                    .HasConstraintName("FK__Product__busines__32E0915F");

                entity
                    .HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Product__categor__31EC6D26");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.BusinessId).HasColumnName("business_id");

                entity.Property(e => e.District).HasColumnName("district");

                entity.Property(e => e.Email).IsUnicode(false).HasColumnName("email");

                entity.Property(e => e.FirstName).HasColumnName("first_name");

                entity.Property(e => e.LastName).HasColumnName("last_name");

                entity.Property(e => e.Password).IsUnicode(false).HasColumnName("password");

                entity.Property(e => e.Phone).IsUnicode(false).HasColumnName("phone");

                entity.Property(e => e.Province).HasColumnName("province");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                //public string ActionToken { get; set; } public DateTime? ActionTokenExpiration { get; set; } public ActionType? ActionTokenType { get; set; }
                
                entity.Property(e => e.ActionToken)
                    .HasColumnName("action_token");
                entity.Property(e => e.ActionTokenExpiration)
                    .HasColumnType("datetime")
                    .HasColumnName("action_token_expiration");
                entity.Property(e => e.ActionTokenType) 
                    .HasConversion<string>()
                    .HasColumnName("action_token_type");
                //            public UserStatus Status { get; set; }
                entity.Property(e => e.Status)
                    .HasConversion<string>()
                    .HasColumnName("status");   
                entity
                    .Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity
                   .Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .HasColumnName("updated_at");


                entity.Property(e => e.Ward).HasColumnName("ward");

                entity
                    .HasOne(d => d.Business)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.BusinessId)
                    .HasConstraintName("FK__User__business_i__2A4B4B5E");

                entity
                    .HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__User__role_id__267ABA7A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
