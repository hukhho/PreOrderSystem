using System.ComponentModel.DataAnnotations;

namespace PreOrderPlatform.Service.ViewModels.Product.Request
{
    public class ProductUpdateRequest
    {
        public Guid Id { get; set; }

        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special characters are not allowed.")]
        public string? Name { get; set; }

        [Url]
        public string? Image { get; set; }

        [StringLength(500)]
        [RegularExpression(@"^[a-zA-Z0-9 .,]*$", ErrorMessage = "Special characters are not allowed.")]
        public string? Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public decimal? Price { get; set; }

        public bool? Status { get; set; }

        public Guid? CategoryId { get; set; }

        public Guid? BusinessId { get; set; }
    }
}
