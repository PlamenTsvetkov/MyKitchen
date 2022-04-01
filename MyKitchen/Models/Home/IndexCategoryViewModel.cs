namespace MyKitchen.Models.Home
{

    public class IndexCategoryViewModel
    {
        public string Name { get; init; }

        public string Description { get; init; }

        public string ShortDescription => this.Description.Length > 300
                        ? this.Description.Substring(0, 300) + "..."
                        : this.Description;

        public string ImageUrl { get; init; }

        public int KitchensCount { get; init; }

        public string Url => $"/k/{this.Name.Replace(' ', '-')}";
    }
}

