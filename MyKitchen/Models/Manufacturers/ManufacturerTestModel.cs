namespace MyKitchen.Models.Manufacturers
{
    public class ManufacturerTestModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string PhoneNumber { get; set; }
        public int CountryId { get; init; }
        public int CityId { get; init; }
    }
}
