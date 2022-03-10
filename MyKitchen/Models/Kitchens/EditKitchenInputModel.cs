namespace MyKitchen.Models.Kitchens
{
    using MyKitchen.Data.Models;
    using MyKitchen.Services.Categories.Models;

    public class EditKitchenInputModel : BaseKitchenModel
    { 
        public int Id { get; set; }
        public virtual ICollection<KitchensColors> KitchensColors { get; set; }

        public IEnumerable<KitchenCategoriesServiceModel> Categories { get; set; }
    }
}
