namespace MyKitchen.Models.Kitchens
{
    using MyKitchen.Models.Dimension;
    using MyKitchen.Services.Colors.Models;
    using System.ComponentModel.DataAnnotations;

    public class KitchenFormModel :BaseKitchenModel
    {

        public IEnumerable<IFormFile> Images { get; set; }

        [Display(Name = "Colors")]

        public IEnumerable<int> ColorsId { get; init; }

        public IEnumerable<KitchenColorServiceModel> Colors { get; set; }


        public DimensionFormModel Dimension { get; set; }


    }
}

