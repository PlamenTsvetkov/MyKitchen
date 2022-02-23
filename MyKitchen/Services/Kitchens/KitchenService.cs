namespace MyKitchen.Services.Kitchens
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MyKitchen.Data;
    using MyKitchen.Data.Models;
    using MyKitchen.Models.Kitchens;
    using System.Collections.Generic;

    public class KitchenService : IKitchenService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly MyKitchenDbContext db;
        private readonly IMapper mapper;

        public KitchenService(MyKitchenDbContext db,
            IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task AddAsync(KitchenFormModel input, string userId, string imagePath)
        {
            //var recipe = new Recipe
            //{
            //    CategoryId = input.CategoryId,
            //    CookingTime = TimeSpan.FromMinutes(input.CookingTime),
            //    Instructions = input.Instructions,
            //    Name = input.Name,
            //    PortionsCount = input.PortionsCount,
            //    PreparationTime = TimeSpan.FromMinutes(input.PreparationTime),
            //    AddedByUserId = userId,
            //};

            //foreach (var inputIngredient in input.Ingredients)
            //{
            //    var ingredient = this.ingredientsRespository.All().FirstOrDefault(x => x.Name == inputIngredient.IngredientName);
            //    if (ingredient == null)
            //    {
            //        ingredient = new Ingredient { Name = inputIngredient.IngredientName };
            //    }

            //    recipe.Ingredients.Add(new RecipeIngredient
            //    {
            //        Ingredient = ingredient,
            //        Quantity = inputIngredient.Quantity,
            //    });
            //}

            //// /wwwroot/images/kitchens/jhdsi-343g3h453-=g34g.jpg
            //Directory.CreateDirectory($"{imagePath}/kitchens/");
            //foreach (var image in input.Images)
            //{
            //    var extension = Path.GetExtension(image.FileName).TrimStart('.');
            //    if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            //    {
            //        throw new Exception($"Invalid image extension {extension}");
            //    }

            //    var dbImage = new Image
            //    {
            //        AddedByUserId = userId,
            //        Extension = extension,
            //    };
            //    recipe.Images.Add(dbImage);

            //    var physicalPath = $"{imagePath}/kitchens/{dbImage.Id}.{extension}";
            //    using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            //    await image.CopyToAsync(fileStream);
            //}

            //await this.db.Kitchens.AddAsync(recipe);
            //await this.db.SaveChangesAsync();
        }
        public IEnumerable<T> GetByCategoryId<T>(int categoryId, int? take = null, int skip = 0)
        {
            var query = this.db.Kitchens
                .OrderByDescending(x => x.CreatedOn)
                .Where(x => x.CategoryId == categoryId).Skip(skip);
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query
                .ProjectTo<T>(this.mapper.ConfigurationProvider)
                .ToList();
        }

        public int GetCountByCategoryId(int categoryId)
        => this.db.Kitchens
                .Count(x => x.CategoryId == categoryId);
    }
}

