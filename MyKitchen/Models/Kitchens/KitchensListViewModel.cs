namespace MyKitchen.Models.Kitchens
{
    public class KitchensListViewModel : PagingViewModel
    {
        public IEnumerable<KitchenInListViewModel> Kitchens { get; set; }
    }
}
