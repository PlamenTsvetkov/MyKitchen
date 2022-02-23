namespace MyKitchen.Services.Countries
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MyKitchen.Data;
    using MyKitchen.Data.Models;
    using System.Collections.Generic;

    public class CountriesService : ICountriesService
    {
        private readonly MyKitchenDbContext db;
        private readonly IMapper mapper;

        public CountriesService(
            MyKitchenDbContext db,
            IMapper mapper
            )
        {
            this.db = db;
            this.mapper = mapper;
        }
        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Country> query =
              this.db.Countries
              .OrderBy(x => x.Name);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.ProjectTo<T>(this.mapper.ConfigurationProvider).ToList();
        }
    }
}
