namespace MyKitchen.Data.Models
{
    public class Dimensions
    {
        public int Id { get; init; }

        public double LenghtCenter { get; set; }

        public double LenghtLeft { get; set; }

        public double LenghtRight { get; set; }

        public double LenghtG { get; set; }

        public double LenghtIsland { get; set; }

        public int KitchenId { get; set; }

        public virtual Kitchen Kitchen { get; set; }
    }
}


