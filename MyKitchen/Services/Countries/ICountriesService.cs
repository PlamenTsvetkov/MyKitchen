namespace MyKitchen.Services.Countries
{
    public interface ICountriesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);
    }
}
