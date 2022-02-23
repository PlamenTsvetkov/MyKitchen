namespace MyKitchen.Models.Categories
{
    public class KitchensInCategoryViewModel
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Description { get; set; }

        public string ShortDescription => this.Description.Length > 300
                        ? this.Description.Substring(0, 300) + "..."
                        : this.Description;

        public decimal Price { get; set; }

        public int PreparationTime { get; set; }

        public int DimensionId { get; set; }

        public string UserName { get; set; }

        public string МanufacturerName { get; set; }

        public int CommentsCount { get; set; }
    }
}


