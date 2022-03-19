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

        public void Create(string name)
        {
            var country = this.db.Countries.FirstOrDefault(x => x.Name == name);

            if (country != null)
            {
                return;
            }

            country = new Country { Name = name };
            this.db.Countries.Add(country);
            this.db.SaveChanges();
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Country> query =
              this.db.Countries;
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.ProjectTo<T>(this.mapper.ConfigurationProvider).ToList();
        }

        public T GetById<T>(int id)
        {
            var country = this.db.Countries
              .Where(x => x.Id == id)
              .ProjectTo<T>(this.mapper.ConfigurationProvider).FirstOrDefault();
            return country;
        }

        public bool CountryExists(int countryId)
        => this.db
                .Countries
                .Any(c => c.Id == countryId);
    }
}
