namespace MyKitchen.Services.Countries
{
    public interface ICountriesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);
        void Create(string name);

        T GetById<T>(int id);

        bool CountryExists(int countryId);
    }
}
