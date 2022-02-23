namespace MyKitchen.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Category;
    public class Category
    {
        public Category()
        {
            this.Kitchens = new HashSet<Kitchen>();
        }
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public virtual ICollection<Kitchen> Kitchens { get; set; }
    }
}

