namespace MyKitchen.Models.Kitchens
{
    public class TestKitchenViewModel
    {
        public string Description { get; set; }

        public int МanufacturerId { get; set; }

        public string UserId { get; set; }

        public int CategoryId { get; set; }

        public decimal Price { get; set; }

        public int PreparationTime { get; set; }

        public double KitchenMeter { get; set; }

        public IEnumerable<int> ColorsId { get; init; }

        public bool IsPublic { get; set; }
    }
}
