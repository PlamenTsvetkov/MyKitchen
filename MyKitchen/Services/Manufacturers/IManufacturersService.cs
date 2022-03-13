namespace MyKitchen.Services.Manufacturers
{
    public interface IManufacturersService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        void Create(
            string name,
            string email,
            string website,
            string phoneNumber,
            string userId,
            int countryId,
            int cityId,
            string addressName,
            string addressNumber);
        IEnumerable<T> GetAllWithPaging<T>(int page, int itemsPerPage = 12, bool publicOnly = true);

        int GetCount(bool publicOnly = true);

        void ChangeVisility(int manufacturerId);
        Task UpdateAsync(
            int id,
             string name,
            string email,
            string website,
            string phoneNumber,
            string userId,
            int countryId,
            int cityId,
            string addressName,
            string addressNumber,
            bool isPublic);
    }
}
   

