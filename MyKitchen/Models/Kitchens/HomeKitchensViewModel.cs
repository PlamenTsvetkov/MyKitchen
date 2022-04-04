namespace MyKitchen.Models.Kitchens
{
    using MyKitchen.Data.Models.Enum;

    public class HomeKitchensViewModel : IKitchenModel
    {
        public int Id { get; init; }

        public DateTime CreatedOn { get; set; }

        public string МanufacturerName { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public decimal Price { get; set; }

        public int PreparationTime { get; set; }

        public double KitchenMeter { get; set; }

        public TypeOfDoorMaterial TypeOfDoorMaterial { get; set; }

        public double AverageVote { get; set; }

        public int CommentsCount { get; set; }
    }
}
