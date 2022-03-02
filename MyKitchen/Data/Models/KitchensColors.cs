namespace MyKitchen.Data.Models
{
    public class KitchensColors
    {

        public int KitchenId { get; set; }

        public virtual Kitchen Kitchen { get; set; }

        public int ColorId { get; set; }

        public virtual Color Color { get; set; }
    }
}


