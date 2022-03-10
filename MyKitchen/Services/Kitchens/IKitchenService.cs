namespace MyKitchen.Services.Kitchens
{
    using MyKitchen.Models.Kitchens;

    public interface IKitchenService
    {
        Task AddAsync(KitchenFormModel input, string userId, string imagePath);

        IEnumerable<T> GetByCategoryId<T>(int categoryId, int? take = null, int skip = 0);

        IEnumerable<T> GetAllByCategoryId<T>(int categoryId, int page, int itemsPerPage = 12);

        int GetCountByManufacturerId(int manufacturerId);

        int GetCountByCategoryId(int categoryId);

        IEnumerable<T> GetAllByManufacturerId<T>(int manufacturerId, int page, int itemsPerPage = 12);

        IEnumerable<T> GetAllByUserId<T>(string userId, int page, int itemsPerPage = 12);

        int GetCountByUserId(string userId);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        int GetCount();

        T GetById<T>(int id);

        Task UpdateAsync(int id, EditKitchenInputModel input);

        Task DeleteAsync(int id);

        void AddKitchenToUserCollection(int kitchenId, string userId);

        IEnumerable<T> GetAllInCollectionByUserId<T>(string userId, int page, int itemsPerPage = 12);
    }
}

