namespace MyKitchen.Models.Kitchens
{

   public class KitchenFormModel : BaseKitchenModel,  IKitchenModel 
    {
        public IEnumerable<IFormFile> Images { get; set; }

    }
}

