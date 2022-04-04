namespace MyKitchen.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using MyKitchen.Models.Home;
    using MyKitchen.Services.Categories;
    using MyKitchen.Services.Comments;
    using MyKitchen.Services.Kitchens;
    using MyKitchen.Services.Manufacturers;
    using MyKitchen.Services.Users;

    public class HomeController : AdminController
    {
        private readonly IKitchenService kitchenService;
        private readonly IManufacturersService manufacturersService;
        private readonly ICommentsService commentsService;
        private readonly IUsersService usersService;
        private readonly ICategoriesService categoriesService;

        public HomeController(IKitchenService kitchenService,
                              IManufacturersService manufacturersService,
                              ICommentsService commentsService,
                              IUsersService usersService,
                              ICategoriesService categoriesService)
        {
            this.kitchenService = kitchenService;
            this.manufacturersService = manufacturersService;
            this.commentsService = commentsService;
            this.usersService = usersService;
            this.categoriesService = categoriesService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexAdminViewModel
            {
                CommentsNumber= this.commentsService.GetCount(),
                KitchensNumber=kitchenService.GetCountAdmin(),
                ManufacturersNumber=manufacturersService.GetCount(),
                UsersNumber=usersService.GetCount(),
                CategoryNumber=categoriesService.GetCount(),
            };

            return this.View(viewModel);
        }
    }
}

