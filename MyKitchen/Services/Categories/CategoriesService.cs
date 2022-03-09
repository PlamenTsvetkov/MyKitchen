namespace MyKitchen.Services.Categories
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MyKitchen.Data;
    using MyKitchen.Data.Models;

    public class CategoriesService : ICategoriesService
    {
        private readonly MyKitchenDbContext db;
        private readonly IMapper mapper;

        public CategoriesService(
            MyKitchenDbContext db,
            IMapper mapper
            )
        {
            this.db = db;
            this.mapper = mapper;
        }

        public bool CategoryExists(int categoryId)
        => this.db
                .Categories
                .Any(c => c.Id == categoryId);

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Category> query =
                this.db.Categories
                .OrderBy(x => x.Name );
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.ProjectTo<T>(this.mapper.ConfigurationProvider).ToList();
        }

        public T GetByName<T>(string name)
        {
            var category = this.db.Categories
               .Where(x => x.Name.Replace(" ", "-") == name.Replace(" ", "-"))
               .ProjectTo<T>(this.mapper.ConfigurationProvider).FirstOrDefault();
            return category;
        }
    }
}

