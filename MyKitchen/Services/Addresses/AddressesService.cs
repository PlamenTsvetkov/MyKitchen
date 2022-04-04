namespace MyKitchen.Services.Addresses
{
    using MyKitchen.Data;
    using MyKitchen.Data.Models;

    public class AddressesService : IAddressesService
    {
        private readonly MyKitchenDbContext db;

        public AddressesService(MyKitchenDbContext db)
        {
            this.db = db;
        }

        public Address Create(string name, string number, int cityId, string userId)
        {
            var addressDate = this
                .db
                .Addresses
                .FirstOrDefault(a => a.Name == name && a.Number == number && a.CityId == cityId);
            if (addressDate != null)
            {
                return addressDate;
            }

            addressDate = new Address
            {
                Name = name,
                Number = number,
                CityId = cityId,
                AddedByUserId = userId
            };

            this.db.Addresses.Add(addressDate);
            this.db.SaveChanges();

            return addressDate;
        }
    }
}
