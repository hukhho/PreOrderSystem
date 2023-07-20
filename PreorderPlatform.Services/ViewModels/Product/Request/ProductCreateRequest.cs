using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.Product.Request
{
    public class ProductCreateRequest
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special characters are not allowed.")]
        public string Name { get; set; }

        [Url]
        public string Image { get; set; }

        [Required]
        [StringLength(500)]
        [RegularExpression(@"^[a-zA-Z0-9 .,]*$", ErrorMessage = "Special characters are not allowed.")]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public decimal Price { get; set; }

        public Guid CategoryId { get; set; }
        [JsonIgnore]
        public Guid BusinessId { get; set; }
    }
}
