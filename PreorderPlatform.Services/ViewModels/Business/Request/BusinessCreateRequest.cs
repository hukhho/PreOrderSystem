using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using PreorderPlatform.Services.Utility;
using Swashbuckle.AspNetCore.Annotations;

namespace PreorderPlatform.Service.ViewModels.Business.Request
{
    public class BusinessCreateRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Phone]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// This property should be ignored by Swashbuckle/Swagger.
        /// </summary>
        [JsonIgnore]
        public int OwnerId { get; set; }

        /// <summary>
        /// This property should be ignored by Swashbuckle/Swagger.
        /// </summary>
        [JsonIgnore]
        public bool Status { get; set; }
    }
}