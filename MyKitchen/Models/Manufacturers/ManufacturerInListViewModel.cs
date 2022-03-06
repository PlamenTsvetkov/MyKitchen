namespace MyKitchen.Models.Manufacturers
{
    public class ManufacturerInListViewModel
    {
        public int Id { get; init; }
        public string Name { get; set; }

        public int KitchensCount { get; init; }

        public string AddressName { get; set; }

        public string AddressNumber { get; set; }

        public string AddressCityName { get; set; }

        public string AddressCityCountryName { get; set; }

        public string Email { get; set; }

        public string Website { get; set; }

        public string PhoneNumber { get; set; }
    }
}
