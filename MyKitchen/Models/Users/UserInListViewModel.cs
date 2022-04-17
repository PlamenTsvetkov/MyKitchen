namespace MyKitchen.Models.Users
{
    public class UserInListViewModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public bool IsAdmin { get; set; }

        public int KitchensCount { get; init; }

        public int ManufacturersCount { get; set; }

        public int VotesCount { get; set; }

        public int CommentsCount { get; set; }
    }
}
