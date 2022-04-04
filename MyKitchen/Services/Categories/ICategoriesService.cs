namespace MyKitchen.Services.Categories
{
    using System.Collections.Generic;

    public interface ICategoriesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name);

        T GetById<T>(int id);

        bool CategoryExists(int categoryId);

        IEnumerable<T> GetAllWithPaging<T>(int page, int itemsPerPage = 12);

        int GetCount();

        void Create(string name, 
                    string description, 
                    string imageUrl);

        Task UpdateAsync(int id,
                         string name,
                         string description,
                         string imageUrl);

        Task DeleteAsync(int id);
    }
}

