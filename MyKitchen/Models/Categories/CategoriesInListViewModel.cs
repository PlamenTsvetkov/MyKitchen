namespace MyKitchen.Models.Categories
{
    public class CategoriesInListViewModel
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ShortDescription => this.Description.Length > 300
                        ? this.Description.Substring(0, 300) + "..."
                        : this.Description;

        public string ImageUrl { get; set; }

        public int KitchensCount { get; set; }
    }
}
