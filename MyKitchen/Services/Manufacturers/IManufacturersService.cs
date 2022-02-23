namespace MyKitchen.Services.Manufacturers
{
    public interface IManufacturersService
    {
        IEnumerable<T> GetAll<T>(int? count = null);
    }
}

