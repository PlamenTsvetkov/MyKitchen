namespace MyKitchen.Services.Manufacturers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MyKitchen.Data;
    using MyKitchen.Data.Models;

    public class ManufacturersService : IManufacturersService
    {
        private readonly MyKitchenDbContext db;
        private readonly IMapper mapper;

        public ManufacturersService(
            MyKitchenDbContext db,
            IMapper mapper
            )
        {
            this.db = db;
            this.mapper = mapper;
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
    }
}

