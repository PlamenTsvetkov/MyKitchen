namespace MyKitchen.Services.Users
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MyKitchen.Data;
    using MyKitchen.Data.Models;
    using System.Collections.Generic;
    public class UsersService : IUsersService
    {
        private readonly MyKitchenDbContext db;
        private readonly IMapper mapper;

        public UsersService(MyKitchenDbContext db,
            IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public int GetCount()
        {
            return this.db.Users.Count();
        }

        public IEnumerable<T> GetAllWithPaging<T>(int page, int itemsPerPage = 12)
        {
            var users = this.db.Users
             .OrderByDescending(x => x.Id)
             .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
             .ProjectTo<T>(this.mapper.ConfigurationProvider)
             .ToList();
            return users;
        }

        public ApplicationUser GetUserById(string id)
        {
                var user = this.db.Users
                   .Where(x => x.Id == id)
                  .FirstOrDefault();

                return user;
        }

        public bool UpdateUser(ApplicationUser model)
        {
            bool result = false;
            var user =  this.db.Users.FirstOrDefault(u=>u.Id==model.Id);

            if (user != null)
            {
                user.Email = model.Email;
                user.Name = model.Name;

              db.SaveChanges();
             result = true;
            }

            return result;
        }
    }
}
