namespace MyKitchen.Models.Users
{
    public class UsersListViewModel : PagingViewModel
    {
        public IEnumerable<UserInListViewModel> Users { get; set; }
    }
}
