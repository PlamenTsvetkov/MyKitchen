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
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif", "jpeg" };
        private readonly MyKitchenDbContext db;
        private readonly IMapper mapper;

        public KitchenService(MyKitchenDbContext db,
            IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public int GetCount()
        {
            return this.db.Kitchens.Count(k=>k.IsDeleted==false);
        }

        public async Task AddAsync(KitchenFormModel input, string userId, string imagePath)
        {
            var kitchen = new Kitchen
            {
                CategoryId = input.CategoryId,
                UserId = userId,
                CreatedOn = DateTime.UtcNow,
                Description = input.Description,
                PreparationTime = input.PreparationTime,
                Price = input.Price,
                TypeOfDoorMaterial = input.TypeOfDoorMaterial,
                МanufacturerId = input.МanufacturerId,
                KitchenMeter = input.KitchenMeter,
            };

            foreach (var color in input.ColorsId)
            {

                kitchen.KitchensColors.Add(new KitchensColors
                {
                    KitchenId = kitchen.Id,
                    ColorId = color,
                });
            }

            // /wwwroot/images/kitchens/jhdsi-343g3h453-=g34g.jpg
            Directory.CreateDirectory($"{imagePath}/kitchens/");
            foreach (var image in input.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');
                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extension}");
                }

                var dbImage = new Image
                {
                    AddedByUserId = userId,
                    Extension = extension,
                };
                kitchen.Images.Add(dbImage);

                var physicalPath = $"{imagePath}/kitchens/{dbImage.Id}.{extension}";
                using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                await image.CopyToAsync(fileStream);
            }

            await this.db.Kitchens.AddAsync(kitchen);
            await this.db.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12)
        {
            var kitchens = this.db.Kitchens
                .Where(k => k.IsDeleted == false)
               .OrderByDescending(x => x.Id)
               .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
               .ProjectTo<T>(this.mapper.ConfigurationProvider)
               .ToList();
            return kitchens;
        }

        public IEnumerable<T> GetByCategoryId<T>(int categoryId, int? take = null, int skip = 0)
        {
            var query = this.db.Kitchens
                .OrderByDescending(x => x.CreatedOn)
                .Where(x => x.CategoryId == categoryId && x.IsDeleted == false).Skip(skip);
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query
                .ProjectTo<T>(this.mapper.ConfigurationProvider)
                .ToList();
        }

        public T GetById<T>(int id)
        {
            var kitchen = this.db.Kitchens
                .Where(x => x.Id == id)
               .ProjectTo<T>(this.mapper.ConfigurationProvider)
               .FirstOrDefault();

            return kitchen;
        }

        public int GetCountByCategoryId(int categoryId)
        => this.db.Kitchens
                .Count(x => x.CategoryId == categoryId && x.IsDeleted == false);

        public IEnumerable<T> GetAllByManufacturerId<T>(int manufacturerId, int page, int itemsPerPage = 12)
        {
            var kitchens = this.db.Kitchens
              .OrderByDescending(x => x.Id)
              .Where(x => x.МanufacturerId == manufacturerId && x.IsDeleted == false)
              .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
              .ProjectTo<T>(this.mapper.ConfigurationProvider)
              .ToList();
            return kitchens;
        }

        public int GetCountByManufacturerId(int manufacturerId)
            => this.db.Kitchens
                .Count(x => x.МanufacturerId == manufacturerId && x.IsDeleted == false);

        public IEnumerable<T> GetAllByCategoryId<T>(int categoryId, int page, int itemsPerPage = 12)
        {
            var kitchens = this.db.Kitchens
             .OrderByDescending(x => x.Id)
             .Where(x => x.CategoryId == categoryId && x.IsDeleted == false)
             .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
             .ProjectTo<T>(this.mapper.ConfigurationProvider)
             .ToList();
            return kitchens;
        }

        public async Task UpdateAsync(int id, EditKitchenInputModel input)
        {
            var kitchen = this.db.Kitchens.FirstOrDefault(x => x.Id == id);
            kitchen.CategoryId = input.CategoryId;
            kitchen.МanufacturerId = input.МanufacturerId;
            kitchen.PreparationTime = input.PreparationTime;
            kitchen.Description = input.Description;
            kitchen.Price = input.Price;
            kitchen.TypeOfDoorMaterial = input.TypeOfDoorMaterial;
            kitchen.KitchenMeter = input.KitchenMeter;

           var kitchenColors = this.db.KitchensColors.Where(k=>k.KitchenId==id).ToList();
            this.db.RemoveRange(kitchenColors);

            foreach (var color in input.ColorsId)
            {

                kitchen.KitchensColors.Add(new KitchensColors
                {
                    KitchenId = id,
                    ColorId = color,
                });
            }
            await this.db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var kitchen = this.db.Kitchens.FirstOrDefault(x => x.Id == id);
            kitchen.IsDeleted = true;
            kitchen.DeletedOn = DateTime.Now;
            await this.db.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllByUserId<T>(string userId, int page, int itemsPerPage = 12)
        {
            var kitchens = this.db.Kitchens
             .OrderByDescending(x => x.Id)
             .Where(x => x.UserId == userId && x.IsDeleted == false)
             .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
             .ProjectTo<T>(this.mapper.ConfigurationProvider)
             .ToList();
            return kitchens;
        }

        public int GetCountByUserId(string userId)
        => this.db.Kitchens
                .Count(x => x.UserId == userId && x.IsDeleted == false);

        public void AddKitchenToUserCollection(int kitchenId, string userId)
        {
            if (this.db.KitchensUsers.Any(ku => ku.UserId == userId && ku.KitchenId == kitchenId))
            {
                return;
            }

            this.db.KitchensUsers.Add(new KitchensUsers
            {
                KitchenId = kitchenId,
                UserId = userId,
            });

            this.db.SaveChanges();
        }

        public IEnumerable<T> GetAllInCollectionByUserId<T>(string userId, int page, int itemsPerPage = 12)
        {
            var kitchens = this.db.Users
             .Where(u=>u.Id==userId)
             .Select(u=>u.KitchensUsers.Select(u=>u.Kitchen))
             .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
             .ProjectTo<T>(this.mapper.ConfigurationProvider)
             .ToList();
            return kitchens;
        }
    }
}

