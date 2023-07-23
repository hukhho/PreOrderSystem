using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PreOrderPlatform.Service.ViewModels.Product.Request
{
    public class ProductCreateRequest
    {
        //[JsonIgnore]
        //public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special characters are not allowed.")]
        public string Name { get; set; }

        [Url]
        public string? Image { get; set; }

        [Required]
        [StringLength(500)]
        [RegularExpression(@"^[a-zA-Z0-9 .,]*$", ErrorMessage = "Special characters are not allowed.")]
        public string Description { get; set; }

        public Guid CategoryId { get; set; }

        [JsonIgnore]
        public Guid BusinessId { get; set; }
    }
}
