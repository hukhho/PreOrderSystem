namespace PreOrderPlatform.Entity.Models
{
        public partial class Business
        {
            public Business()
            {
                Id = Guid.NewGuid();
                DateTime now = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(7)).DateTime;
                CreatedAt = now;
                UpdatedAt = now;
                BusinessPaymentCredentials = new HashSet<BusinessPaymentCredential>();
                Campaigns = new HashSet<Campaign>();
                Products = new HashSet<Product>();
                Users = new HashSet<User>();
            }

            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string? Address { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
            public string? LogoUrl { get; set; }
            public Guid? OwnerId { get; set; }
            public bool IsVerified { get; set; }
            public bool Status { get; set; }

            public virtual User? Owner { get; set; }
            public virtual ICollection<BusinessPaymentCredential> BusinessPaymentCredentials { get; set; }
            public virtual ICollection<Campaign> Campaigns { get; set; }
            public virtual ICollection<Product> Products { get; set; }
            public virtual ICollection<User> Users { get; set; }
        }
}
