namespace MyKitchen.Data.Models
{
    public class KitchensUsers
    {
        public int KitchenId { get; set; }

        public virtual Kitchen Kitchen { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
