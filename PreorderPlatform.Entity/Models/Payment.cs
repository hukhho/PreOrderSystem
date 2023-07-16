using System;
using System.Collections.Generic;

namespace PreorderPlatform.Entity.Models
{
    public partial class Payment
    {
        public Payment()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string? Method { get; set; }
        public decimal? Total { get; set; }
        public int? PaymentCount { get; set; }
        public DateTime? PayedAt { get; set; }
        public string? Status { get; set; }
        public Guid? UserId { get; set; }
        public Guid? OrderId { get; set; }

        public virtual Order? Order { get; set; }
        public virtual User? User { get; set; }
    }
}
