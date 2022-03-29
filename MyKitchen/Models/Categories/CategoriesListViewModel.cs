namespace MyKitchen.Models.Categories
{
    public class CategoriesListViewModel : PagingViewModel
    {
        public IEnumerable<CategoriesInListViewModel> Categories { get; set; }
    }
}
