namespace MyKitchen.Services.Categories
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MyKitchen.Data;
    using MyKitchen.Data.Models;
    using System.Threading.Tasks;

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

        public int GetCount()
        {
            return this.db.Categories.Count();
        }

        public IEnumerable<T> GetAllWithPaging<T>(int page, int itemsPerPage = 12)
        {
            var categories = this.db.Categories
               .OrderByDescending(x => x.Id)
               .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
               .ProjectTo<T>(this.mapper.ConfigurationProvider)
               .ToList();
            return categories;
        }

        public void Create(string name, string description, string imageUrl)
        {
            var category = this.db.Categories.FirstOrDefault(x => x.Name == name);

            if (category != null)
            {
                return;
            }

            category = new Category 
            { 
                Name = name,
                Description= description,
                ImageUrl=imageUrl
            };
            this.db.Categories.Add(category);
            this.db.SaveChanges();
        }

        public T GetById<T>(int id)
        {
            var category = this.db.Categories
               .Where(x => x.Id == id)
              .ProjectTo<T>(this.mapper.ConfigurationProvider)
              .FirstOrDefault();

            return category;
        }

        public async Task UpdateAsync(int id, string name, string description, string imageUrl)
        {
            var category = this.db.Categories.FirstOrDefault(x => x.Id == id);
            category.Name = name;
            category.Description = description;
            category.ImageUrl = imageUrl;

            await this.db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = this.db.Categories.FirstOrDefault(x => x.Id == id);

            await this.db.SaveChangesAsync();
        }
    }
}

