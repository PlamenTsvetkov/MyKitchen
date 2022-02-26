namespace MyKitchen.Services.Addresses
{
    using MyKitchen.Data.Models;

    public interface IAddressesService
    {
        Address Create(string name, string number, int cityId, string userId);
    }
}
