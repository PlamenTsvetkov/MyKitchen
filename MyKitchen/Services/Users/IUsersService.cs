namespace MyKitchen.Services.Users
{
    using MyKitchen.Data.Models;

    public interface IUsersService
    {
        IEnumerable<T> GetAllWithPaging<T>(int page, int itemsPerPage = 12);

        int GetCount();

        bool UpdateUser(ApplicationUser model);

        ApplicationUser GetUserById(string id);
    }
}
