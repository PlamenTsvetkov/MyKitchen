namespace MyKitchen.Models.Colors
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Color;
    public class ColorFormModel
    {
        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; init; }
    }
}
