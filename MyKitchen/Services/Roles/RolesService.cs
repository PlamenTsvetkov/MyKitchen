namespace MyKitchen.Services.Roles
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class RolesService : IRolesService
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;

        public RolesService(RoleManager<IdentityRole> _roleManager,
              IMapper mapper)
        {
            roleManager = _roleManager;
            this.mapper = mapper;
        }
        public async void Create(string name)
        {
            await roleManager.CreateAsync(new IdentityRole()
            {
                Name = name
            });
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            var query =this.roleManager.Roles;
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.ProjectTo<T>(this.mapper.ConfigurationProvider).ToList();
        }
    }
}
