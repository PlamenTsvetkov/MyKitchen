namespace MyKitchen.Models.Users
{
    using MyKitchen.Models.Roles;

    public class UsersRolesViewModel
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public string[] RoleNames { get; set; }
    }
}
