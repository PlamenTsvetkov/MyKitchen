namespace MyKitchen.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Color;
    public class Color
    {
        public Color()
        {
            this.KitchensColors = new HashSet<KitchensColors>();
        }
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public virtual ICollection<KitchensColors> KitchensColors { get; init; }
    }
}

