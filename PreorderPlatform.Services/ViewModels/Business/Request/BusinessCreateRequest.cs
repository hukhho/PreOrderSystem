using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using PreorderPlatform.Services.Utility;
using Swashbuckle.AspNetCore.Annotations;

namespace PreorderPlatform.Service.ViewModels.Business.Request
{
    public class BusinessCreateRequest
    {

        [JsonIgnore]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special characters are not allowed.")]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Phone]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [JsonIgnore]
        public Guid OwnerId { get; set; }

        [JsonIgnore]
        public bool Status { get; set; }
    }
}