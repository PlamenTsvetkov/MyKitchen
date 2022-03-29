namespace MyKitchen.Models.Categories
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Category;
    public class CategoryFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "Category {0} must be between {2} and {1} characters long")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }
    }
}
