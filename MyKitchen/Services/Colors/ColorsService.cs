namespace MyKitchen.Services.Colors
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MyKitchen.Data;
    using MyKitchen.Data.Models;

    public class ColorsService : IColorsService
    {
        private readonly MyKitchenDbContext db;
        private readonly IMapper mapper;

        public ColorsService(
            MyKitchenDbContext db,
            IMapper mapper
            )
        {
            this.db = db;
            this.mapper = mapper;
        }

        public void Create(string name)
        {
            var color = this.db.Colors.FirstOrDefault(x => x.Name == name);

            if (color == null)
            {
                color = new Color { Name = name };
            }

            this.db.Colors.Add(color);
            this.db.SaveChanges();
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Color> query =
                this.db.Colors
                .OrderBy(x => x.Name);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.ProjectTo<T>(this.mapper.ConfigurationProvider).ToList();
        }
    }
}
