namespace MyKitchen.Data.Models
{
    using MyKitchen.Data.Models.Enum;
    using System.ComponentModel.DataAnnotations;

    public class Kitchen
    {
        public Kitchen()
        {
            this.Images = new HashSet<Image>();
            this.KitchensColors = new HashSet<KitchensColors>();
            this.Votes = new HashSet<Vote>();
            this.Comments = new HashSet<Comment>();
            this.CreatedOn = DateTime.UtcNow;
        }
        public int Id { get; init; }

        public DateTime CreatedOn { get; set; }
        [Required]
        public string Description { get; set; }

        public int МanufacturerId { get; set; }

        public Manufacturer Мanufacturer { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Image> Images { get; init; }

        public decimal Price { get; set; }

        public int PreparationTime { get; set; }

        public double KitchenMeter { get; set; }

        public ICollection<KitchensUsers> KitchensUsers { get; init; }

        public virtual ICollection<KitchensColors> KitchensColors { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public TypeOfDoorMaterial TypeOfDoorMaterial { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}


