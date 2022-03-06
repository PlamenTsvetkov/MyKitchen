﻿namespace MyKitchen.Services.Manufacturers
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
        IEnumerable<T> GetAllWithPaging<T>(int page, int itemsPerPage = 12);

        int GetCount();
    }
}
   

