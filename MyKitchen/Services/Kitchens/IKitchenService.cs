namespace MyKitchen.Services.Kitchens
{
    using MyKitchen.Models.Kitchens;

    public interface IKitchenService
    {
        Task AddAsync(KitchenFormModel input, string userId, string imagePath);

        IEnumerable<T> GetByCategoryId<T>(int categoryId, int? take = null, int skip = 0);

        int GetCountByCategoryId(int categoryId);
    }
}

