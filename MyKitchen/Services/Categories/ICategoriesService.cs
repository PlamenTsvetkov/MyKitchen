namespace MyKitchen.Services.Categories
{
    using System.Collections.Generic;

    public interface ICategoriesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name);

        bool CategoryExists(int categoryId);
        int GetCount();
    }
}

