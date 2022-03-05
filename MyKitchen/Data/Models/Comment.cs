namespace MyKitchen.Data.Models
{
    public class Comment
    {
        public Comment()
        {
            this.CreatedOn=DateTime.UtcNow;
        }
        public int Id { get; set; }

        public int KitchenId { get; set; }

        public virtual Kitchen Kitchen { get; set; }

        public int? ParentId { get; set; }

        public virtual Comment Parent { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
