namespace MyKitchen.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyKitchen.Data.Models;
    using MyKitchen.Models.Users;
    using MyKitchen.Services.Users;

    using static MyKitchen.Areas.Admin.AdminConstants;
    public class UsersController : AdminController
    {
        private readonly IUsersService usersService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(IUsersService usersService,
                                RoleManager<IdentityRole> _roleManager,
                                UserManager<ApplicationUser> _userManager)
        {
            this.usersService = usersService;
            roleManager = _roleManager;
            userManager = _userManager;
        }
        public IActionResult All(int id = 1)
        {

            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 12;
            var viewModel = new UsersListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                ItemsCount = this.usersService.GetCount(),
                Users = this.usersService.GetAllWithPaging<UserInListViewModel>(id, ItemsPerPage),
                Action = nameof(All),
            };
            return this.View(viewModel);
        }

        public IActionResult Edit(string id)
        {
            var user =  usersService.GetUserById<EditUserViewModel>(id);

            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (usersService.UpdateUser(model))
            {
                this.TempData["Message"] = "User was edited!";
            }
            else
            {
                this.TempData["Message"] = "Error!";
            }

            return this.RedirectToAction("All", "Users", new { area = "Admin" });
        }


    }
}
