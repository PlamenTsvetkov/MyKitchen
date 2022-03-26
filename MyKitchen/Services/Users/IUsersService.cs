namespace MyKitchen.Services.Users
{
    using MyKitchen.Models.Users;

    public interface IUsersService
    {
        IEnumerable<T> GetAllWithPaging<T>(int page, int itemsPerPage = 12);
        int GetCount();

        bool UpdateUser(EditUserViewModel model);

        T GetUserById<T>(string id);
    }
}
