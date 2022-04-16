namespace MyKitchen.Services.Kitchens
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using System.Collections.Generic;

    using MyKitchen.Data;
    using MyKitchen.Data.Models;
    using MyKitchen.Models.Kitchens;

    public class KitchenService : IKitchenService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif", "jpeg", "webp" };
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
            return this.db.Kitchens.Count(k => k.IsDeleted == false && k.IsPublic);
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
                IsPublic = false,
                IsDeleted = false,
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

        public IEnumerable<T> GetAll<T>(
            int page,
            int itemsPerPage = 12,
            bool publicOnly = true)
        {
            var kitchens = this.db.Kitchens
                               .Where(k => k.IsPublic && k.IsDeleted == false)
                               .OrderByDescending(k => k.Id)
                               .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                               .ProjectTo<T>(this.mapper.ConfigurationProvider)
                               .ToList();
            return kitchens;
        }

        public int GetCountAdmin()
        {
            return this.db.Kitchens.Count();
        }

        public IEnumerable<T> GetAllA<T>(int page, int itemsPerPage = 12, bool publicOnly = true)
        {
            var kitchens = this.db.Kitchens
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
                .Where(k => k.CategoryId == categoryId && k.IsDeleted == false && k.IsPublic).Skip(skip);

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
                .Count(k => k.CategoryId == categoryId && k.IsDeleted == false && k.IsPublic);

        public IEnumerable<T> GetAllByManufacturerId<T>(int manufacturerId, int page, int itemsPerPage = 12)
        {
            var kitchens = this.db.Kitchens
                           .OrderByDescending(x => x.Id)
                           .Where(k => k.МanufacturerId == manufacturerId && k.IsDeleted == false && k.IsPublic)
                           .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                           .ProjectTo<T>(this.mapper.ConfigurationProvider)
                           .ToList();
            return kitchens;
        }

        public int GetCountByManufacturerId(int manufacturerId)
            => this.db.Kitchens
                .Count(k => k.МanufacturerId == manufacturerId && k.IsDeleted == false && k.IsPublic);

        public IEnumerable<T> GetAllByCategoryId<T>(int categoryId, int page, int itemsPerPage = 12)
        {
            var kitchens = this.db.Kitchens
                             .OrderByDescending(x => x.Id)
                             .Where(k => k.CategoryId == categoryId && k.IsDeleted == false && k.IsPublic)
                             .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                             .ProjectTo<T>(this.mapper.ConfigurationProvider)
                             .ToList();

            return kitchens;
        }

        public async Task UpdateAsync(int id, EditKitchenInputModel input, bool isPublic)
        {
            var kitchen = this.db.Kitchens.FirstOrDefault(x => x.Id == id);
            kitchen.CategoryId = input.CategoryId;
            kitchen.МanufacturerId = input.МanufacturerId;
            kitchen.PreparationTime = input.PreparationTime;
            kitchen.Description = input.Description;
            kitchen.Price = input.Price;
            kitchen.TypeOfDoorMaterial = input.TypeOfDoorMaterial;
            kitchen.KitchenMeter = input.KitchenMeter;
            kitchen.IsPublic = isPublic;

            var kitchenColors = this.db.KitchensColors.Where(k => k.KitchenId == id).ToList();
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
                             .Where(k => k.UserId == userId && k.IsDeleted == false)
                             .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                             .ProjectTo<T>(this.mapper.ConfigurationProvider)
                             .ToList();
                    
            return kitchens;
        }

        public int GetCountByUserId(string userId)
        => this.db.Kitchens
                .Count(k => k.UserId == userId && k.IsDeleted == false);

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
            var kitchens = this.db.Kitchens
                            .SelectMany(k => k.KitchensUsers)
                            .Where(k => k.UserId == userId)
                            .Select(k => k.Kitchen)
                            .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                            .ProjectTo<T>(this.mapper.ConfigurationProvider)
                            .ToList();

            return kitchens;
        }

        public int GetCollectionCountByUserId(string userId)
          => this.db.KitchensUsers
            .Where(x => x.UserId == userId)
            .Count();

        public void RemoveKitchenToUserCollection(int kitchenId, string userId)
        {
            var kitchenUser = this.db
                 .KitchensUsers
                 .Where(up => up.UserId == userId &&
                        up.KitchenId == kitchenId)
                 .FirstOrDefault();

            if (kitchenUser == null)
            {
                return;
            }

            this.db.KitchensUsers.Remove(kitchenUser);
            this.db.SaveChanges();
        }

        public bool IsByUser(int kitchenId, string userId)
         => this.db
                .Kitchens
                .Any(k => k.Id == kitchenId && k.UserId == userId);

        public int GetLastKitchenIdByUserId(string userId)
        => this.db.Kitchens
                .Where(k => k.UserId == userId)
                .OrderByDescending(k => k.Id)
                .FirstOrDefault().Id;



        public void ChangeVisility(int kitchenId)
        {
            var kitchen = this.db.Kitchens.Find(kitchenId);

            kitchen.IsPublic = !kitchen.IsPublic;

            this.db.SaveChanges();
        }

        public IEnumerable<T> GetRandom<T>(int count)
        {
            return this.db.Kitchens
                        .Where(k => k.IsPublic)
                        .OrderBy(x => Guid.NewGuid())
                        .Take(count)
                        .ProjectTo<T>(this.mapper.ConfigurationProvider)
                        .ToList();
        }

        public IEnumerable<T> GetAllManufacturerName<T>(int page, string manufacturerName, int itemsPerPage = 12)
        {
            var kitchens = this.db.Kitchens
                            .Where(k => k.Мanufacturer.Name == manufacturerName)
                            .OrderByDescending(x => x.Id)
                            .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                            .ProjectTo<T>(this.mapper.ConfigurationProvider)
                            .ToList();

            return kitchens;
        }
    }
}

