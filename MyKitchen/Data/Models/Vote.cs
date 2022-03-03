namespace MyKitchen.Data.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public int KitchenId { get; set; }

        public virtual Kitchen Kitchen { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public byte Value { get; set; }
    }
}
