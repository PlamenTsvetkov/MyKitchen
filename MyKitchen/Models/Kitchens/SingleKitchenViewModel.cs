namespace MyKitchen.Models.Kitchens
{
    using MyKitchen.Data.Models.Enum;
    using MyKitchen.Models.Colors;

    public class SingleKitchenViewModel
    {
        public int Id { get; init; }

        public DateTime CreatedOn { get; set; }

        public string МanufacturerName { get; set; }
        public string UserName { get; set; }
        public string ImageUrl { get; set; }

        public string CategoryName { get; set; }

        public decimal Price { get; set; }

        public int PreparationTime { get; set; }

        public int DimensionId { get; set; }

        public  IEnumerable<ColorsViewModel> Colors { get; set; }

        public TypeOfDoorMaterial TypeOfDoorMaterial { get; set; }

        public double AverageVote { get; set; }
    }
}
