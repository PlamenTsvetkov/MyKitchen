namespace MyKitchen.Areas.Manufacturer.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using MyKitchen.Data.Models;
    using MyKitchen.Models.Kitchens;
    using MyKitchen.Services.Kitchens;
    using MyKitchen.Services.Manufacturers;
    using MyKitchen.Services.Users;

    public class KitchensController : ManufacturerController
    {
        private readonly IManufacturersService manufacturersService;
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IKitchenService kitchenService;

        public KitchensController(IManufacturersService manufacturersService,
                                  IUsersService usersService,
                                  UserManager<ApplicationUser> userManager, 
                                  IKitchenService kitchenService)
        {
            this.manufacturersService = manufacturersService;
            this.usersService = usersService;
            this.userManager = userManager;
            this.kitchenService = kitchenService;
        }

        public async Task<IActionResult> All(int id = 1)
        {
            const int ItemsPerPage = 12;
            var user = await this.userManager.GetUserAsync(this.User);
            var currentUser = usersService.GetUserById(user.Id);

            var viewModel = new KitchensListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                ItemsCount = manufacturersService.GetPublicKitchenCountByName(currentUser.Name)+ manufacturersService.GetNotPublicKitchenCountByName(currentUser.Name),
                Kitchens = this.kitchenService.GetAllManufacturerName<KitchenInListViewModel>(id, currentUser.Name, ItemsPerPage),
                Action = nameof(All),
            };

            return this.View(viewModel);
        }
    }
}