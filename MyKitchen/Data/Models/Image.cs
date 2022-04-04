namespace MyKitchen.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Image
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Id { get; init; }

        public int KitchenId { get; set; }

        public virtual Kitchen Kitchen { get; init; }

        [Required]
        public string AddedByUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }

        public string Extension { get; set; }
    }
}


