using System.ComponentModel.DataAnnotations;

namespace PreOrderPlatform.Service.ViewModels.Business.Request
{
    public class BusinessPatchRequest
    {
        public Guid Id { get; set; }
        
        [StringLength(100)]
        public string? Name { get; set; }
       
        [StringLength(500)]
        public string? Description { get; set; }
       
        [Phone]
        public string Phone { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
        
        public bool? Status { get; set; }
    }
}
