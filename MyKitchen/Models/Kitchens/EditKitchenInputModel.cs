namespace MyKitchen.Models.Kitchens
{
    using MyKitchen.Services.Categories.Models;

    public class EditKitchenInputModel : BaseKitchenModel
    { 
        public int Id { get; set; }

        public IEnumerable<KitchenCategoriesServiceModel> Categories { get; set; }
    }
}
