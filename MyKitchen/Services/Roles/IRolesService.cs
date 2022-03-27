namespace MyKitchen.Services.Roles
{
    public interface IRolesService
    {
         void Create(string name);

        IEnumerable<T> GetAll<T>(int? count = null);

        //T GetById<T>(int id);

        //bool CountryExists(int countryId);
    }
}
