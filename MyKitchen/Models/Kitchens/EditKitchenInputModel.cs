namespace MyKitchen.Models.Kitchens
{
    using MyKitchen.Data.Models;

    public class EditKitchenInputModel : BaseKitchenModel , IKitchenModel
    { 
        public int Id { get; set; }
        public virtual ICollection<KitchensColors> KitchensColors { get; set; }

    }
}
