namespace MyKitchen.Services.Cities
{
    public interface ICitiesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);
        IEnumerable<T> GetByCountryId<T>(int countryId, int? take = null, int skip = 0);
        void Create(string name, int countryId);

    }
}
