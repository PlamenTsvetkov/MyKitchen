namespace MyKitchen.Models.Categories
{
    using MyKitchen.Models.Kitchens;

    public class CategoryViewModel : PagingViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<KitchenInListViewModel> Kitchens { get; set; }
    }
}

