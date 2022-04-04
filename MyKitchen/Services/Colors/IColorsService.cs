namespace MyKitchen.Services.Colors
{
    using System.Collections.Generic;
    public interface IColorsService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        void Create(string name);
    }
}
