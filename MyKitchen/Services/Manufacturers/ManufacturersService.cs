namespace MyKitchen.Services.Manufacturers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MyKitchen.Data;
    using MyKitchen.Data.Models;
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
                AddressId = address.Id
            };

            this.db.Manufacturers.Add(manufacturerData);
            this.db.SaveChanges();
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Manufacturer> query =
                this.db.Manufacturers
                .OrderBy(x => x.Name);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.ProjectTo<T>(this.mapper.ConfigurationProvider).ToList();
        }

        public IEnumerable<T> GetAllWithPaging<T>(int page, int itemsPerPage = 12)
        {
            var manufacturers = this.db.Manufacturers
               .OrderByDescending(x => x.Id)
               .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
               .ProjectTo<T>(this.mapper.ConfigurationProvider)
               .ToList();
            return manufacturers;
        }

        public int GetCount()
            => this.db.Manufacturers
                .Count();
    }
}

