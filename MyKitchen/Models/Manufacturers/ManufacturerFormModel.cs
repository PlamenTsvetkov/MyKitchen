namespace MyKitchen.Models.Countries
{
    using MyKitchen.Models.Cityes;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Manufacturer;
    public class ManufacturerFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "Manufacturer {0} must be between {2} and {1} characters long")]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Url]
        public string Website { get; set; }

        [Display(Name = "Phone Number")]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "Manufacturer {0} must be between {2} and {1} characters long")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Country")]
        public int CountryId { get; init; }

        public IEnumerable<AllCountryModel> Countries { get; set; }

        [Display(Name = "City")]
        public int CityId { get; init; }

        public IEnumerable<AllCityModel> Cities { get; set; }

        public ManufacturerAddressFormModel Address { get; set; }
    }
}
