namespace MyKitchen.Models.Kitchens
{
    using MyKitchen.Data.Models.Enum;
    using MyKitchen.Services.Categories.Models;
    using MyKitchen.Services.Colors.Models;
    using MyKitchen.Services.Manufacturers.Models;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Kitchen;

    public class BaseKitchenModel
    {
        [Required]
        [StringLength(
          int.MaxValue,
          MinimumLength = DescriptionMinLength,
          ErrorMessage = "The field Description must be a string with a minimum length of {2}.")]
        public string Description { get; set; }

        [Display(Name = "Мanufacturer")]
        public int МanufacturerId { get; set; }
        public IEnumerable<KitchenManufacturerServiceModel> Manufacturers { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<KitchenCategoriesServiceModel> Categories { get; set; }

        public decimal Price { get; set; }

        [Display(Name = "Preparation Time")]
        public int PreparationTime { get; set; }

        //public IEnumerable<IFormFile> Images { get; set; }

        [Required]
        public TypeOfDoorMaterial TypeOfDoorMaterial { get; set; }

        [Display(Name = "Kitchen Meter")]
        public double KitchenMeter { get; set; }

        [Display(Name = "Colors")]

        public IEnumerable<int> ColorsId { get; init; }

        public IEnumerable<KitchenColorServiceModel> Colors { get; set; }
    }
}
