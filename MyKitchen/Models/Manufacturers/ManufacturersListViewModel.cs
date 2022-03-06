namespace MyKitchen.Models.Manufacturers
{
    public class ManufacturersListViewModel : PagingViewModel
    {
        public IEnumerable<ManufacturerInListViewModel> Manufacturers { get; set; }
    }
}