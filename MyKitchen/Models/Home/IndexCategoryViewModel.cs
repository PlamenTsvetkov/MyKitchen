namespace MyKitchen.Models.Home
{

    public class IndexCategoryViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int KitchensCount { get; set; }

        public string Url => $"/f/{this.Name.Replace(' ', '-')}";
    }
}

