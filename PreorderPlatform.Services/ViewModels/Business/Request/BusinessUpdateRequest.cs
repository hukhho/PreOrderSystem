﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using PreorderPlatform.Services.Utility;
using Swashbuckle.AspNetCore.Annotations;

namespace PreorderPlatform.Service.ViewModels.Business.Request
{
    public class BusinessUpdateRequest
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required]
        [StringLength(500)]
        public string? Description { get; set; }

        [Phone]
        [Required]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public bool? Status { get; set; }
    }
}
