namespace MyKitchen.Models.Manufacturers
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Manufacturer;
    public class ManufacturerFormModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Website { get; set; }

        public string PhoneNumber { get; set; }

        public string AddedByUserId { get; set; }

        public int CountryId { get; init; }

        public IEnumerable<ManucafturerCountryFormModel> Countries { get; set; }

        public int CityId { get; init; }

        public IEnumerable<ManufacturerCityFormModel> Cities { get; set; }
        public ManufacturerAddressFormModel Address { get; set; }
    }
}
