namespace MyKitchen.Models.Countries
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Country;
    public class CountryFormModel
    {
        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; init; }
    }
}
