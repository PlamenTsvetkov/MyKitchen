namespace MyKitchen.Services.Cities
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using System.Collections.Generic;

    using MyKitchen.Data;
    using MyKitchen.Data.Models;

    public class CitiesService : ICitiesService
    {
        private readonly MyKitchenDbContext db;
        private readonly IMapper mapper;

        public CitiesService(MyKitchenDbContext db,
                             IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public void Create(string name, int countryId)
        {
            var city = this.db.Cities.FirstOrDefault(x => x.Name == name && x.CountryId == countryId);

            if (city != null)
            {
                return;
            }
            city = new City
            {
                Name = name,
                CountryId = countryId,
            };

            this.db.Cities.Add(city);
            this.db.SaveChanges();
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<City> query =
              this.db.Cities
              .OrderBy(x => x.Name);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.ProjectTo<T>(this.mapper.ConfigurationProvider).ToList();
        }

        public IEnumerable<T> GetByCountryId<T>(int countryId, int? take = null, int skip = 0)
        {
            var query = this.db.Cities
              .OrderByDescending(x => x.Name)
              .Where(x => x.CountryId == countryId).Skip(skip);
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query
                .ProjectTo<T>(this.mapper.ConfigurationProvider)
                .ToList();
        }
    }

}
