namespace MyKitchen.Areas.Manufacturer.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyKitchen.Data.Models;
    using MyKitchen.Models.Home;
    using MyKitchen.Services.Manufacturers;
    using MyKitchen.Services.Users;

    public class HomeController : ManufacturerController
    {
        private readonly IManufacturersService manufacturersService;
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(IManufacturersService manufacturersService,
                              IUsersService usersService,
                              UserManager<ApplicationUser> userManager)
        {
            this.manufacturersService = manufacturersService;
            this.usersService = usersService;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var currentUser = usersService.GetUserById(user.Id);
            var viewModel = new IndexManufacturerViewModel
            {
                PublicKitchensNumber = manufacturersService.GetPublicKitchenCountByName(currentUser.Name),
                NotPublicKitchensNumber = manufacturersService.GetNotPublicKitchenCountByName(currentUser.Name)
            };
            return this.View(viewModel);
        }
    }
}
