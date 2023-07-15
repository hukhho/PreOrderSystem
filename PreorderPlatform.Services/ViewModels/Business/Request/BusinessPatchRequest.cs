using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using PreorderPlatform.Services.Utility;
using Swashbuckle.AspNetCore.Annotations;

namespace PreorderPlatform.Service.ViewModels.Business.Request
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
