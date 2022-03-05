namespace MyKitchen.Models.Home
{

    public class IndexCategoryViewModel
    {
        public string Name { get; init; }

        public string Description { get; init; }

        public string ImageUrl { get; init; }

        public int KitchensCount { get; init; }

        public string Url => $"/k/{this.Name.Replace(' ', '-')}";
    }
}

