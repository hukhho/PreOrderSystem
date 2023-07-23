using PreOrderPlatform.Entity.Enum.Campaign;

namespace PreOrderPlatform.Entity.Models
{
    public partial class CampaignDetail
    {
        public CampaignDetail()
        {
            OrderItems = new HashSet<OrderItem>();
        }
        public Guid Id { get; set; }
        public int Phase { get; set; }
        public int AllowedQuantity { get; set; }
        public PhaseStatus PhaseStatus { get; set; }  // new field
        public int TotalOrdered { get; set; }
        public Guid CampaignId { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Campaign Campaign { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
