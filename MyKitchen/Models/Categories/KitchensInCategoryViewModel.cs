namespace MyKitchen.Models.Categories
{
    using MyKitchen.Data.Models.Enum;

    public class KitchensInCategoryViewModel
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Description { get; set; }

        public string ShortDescription => this.Description.Length > 300
                        ? this.Description.Substring(0, 300) + "..."
                        : this.Description;

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public int PreparationTime { get; set; }

        public double KitchenMeter { get; set; }

        public string UserName { get; set; }

        public string МanufacturerName { get; set; }

        public int CommentsCount { get; set; }

        public TypeOfDoorMaterial TypeOfDoorMaterial { get; set; }
    }
}



