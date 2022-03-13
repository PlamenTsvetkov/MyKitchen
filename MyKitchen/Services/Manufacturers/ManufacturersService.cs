﻿namespace MyKitchen.Services.Manufacturers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MyKitchen.Data;
    using MyKitchen.Data.Models;
    using MyKitchen.Models.Countries;
    using MyKitchen.Services.Addresses;

    public class ManufacturersService : IManufacturersService
    {
        private readonly MyKitchenDbContext db;
        private readonly IMapper mapper;
        private readonly IAddressesService addressesService;

        public ManufacturersService(
            MyKitchenDbContext db,
            IMapper mapper,
            IAddressesService addressesService
            )
        {
            this.db = db;
            this.mapper = mapper;
            this.addressesService = addressesService;
        }

        public void ChangeVisility(int manufacturerId)
        {
                var manufacturer = this.db.Manufacturers.Find(manufacturerId);

                manufacturer.IsPublic = !manufacturer.IsPublic;

                this.db.SaveChanges();
        }

        public void Create(string name, string email, string website, string phoneNumber, string userId, int countryId, int cityId, string addressName, string addressNumber)
        {
            var address = addressesService.Create(addressName, addressNumber, cityId, userId);
            var manufacturerData = new Manufacturer
            {
                Name = name,
                Email = email,
                Website = website,
                PhoneNumber = phoneNumber,
                AddedByUserId = userId,
                AddressId = address.Id,
                IsPublic = false,
            };

            this.db.Manufacturers.Add(manufacturerData);
            this.db.SaveChanges();
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Manufacturer> query =
                this.db.Manufacturers
                .Where(m => m.IsPublic)
                .OrderBy(x => x.Name);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.ProjectTo<T>(this.mapper.ConfigurationProvider).ToList();
        }

        public IEnumerable<T> GetAllWithPaging<T>(int page, int itemsPerPage = 12 , bool publicOnly = true)
        {
            var manufacturers = this.db.Manufacturers
               .OrderByDescending(x => x.Id)
               .Where(m => !publicOnly || m.IsPublic)
               .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
               .ProjectTo<T>(this.mapper.ConfigurationProvider)
               .ToList();
            return manufacturers;
        }

        public int GetCount(bool publicOnly = true)
            => this.db.Manufacturers
                .Count(m => !publicOnly || m.IsPublic);

        public async Task UpdateAsync(int id, string name, string email, string website, string phoneNumber, string userId, int countryId, int cityId, string addressName, string addressNumber, bool isPublic)
        {
            var manufacturer = this.db.Manufacturers.FirstOrDefault(x => x.Id == id);
            manufacturer.Name = name;
            manufacturer.Email = email;
            manufacturer.Website = website;
            manufacturer.PhoneNumber = phoneNumber;
            manufacturer.AddedByUserId = userId;
            manufacturer.IsPublic = isPublic;

            if (addressName!= manufacturer.Address.Name || addressNumber != manufacturer.Address.Number)
            {
                var address = addressesService.Create(addressName, addressNumber, cityId, userId);
                manufacturer.AddressId = address.Id;
            }
            await this.db.SaveChangesAsync();
        }

    }
}

