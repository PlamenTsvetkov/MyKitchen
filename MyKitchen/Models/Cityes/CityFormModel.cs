namespace MyKitchen.Models.Cityes
{
    using MyKitchen.Models.Countries;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.City;
    public class CityFormModel
    {
        [Display(Name = "Country")]
        public int CountryId { get; init; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength =NameMinLength, ErrorMessage = "City {0} must be between {2} and {1} characters long")]
        public string Name { get; init; }

        public IEnumerable<AllCountryModel>? Countries { get; set; }
    }
}
