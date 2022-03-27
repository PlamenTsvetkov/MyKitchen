namespace MyKitchen.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult Roles(string id)
        {
            var user = usersService.GetUserById<ApplicationUser>(id);
            var model = new UsersRolesViewModel()
            {
                UserId = user.Id,
                Name = user.Name
            };


            ViewBag.RoleItems = roleManager.Roles
                .ToList()
                .Select(r => new SelectListItem()
                {
                    Text = r.Name,
                    Value = r.Name,
                    Selected = userManager.IsInRoleAsync(user, r.Name).Result
                }).ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Roles(UsersRolesViewModel model)
        {
            var user = usersService.GetUserById<ApplicationUser>(model.UserId);
            var userRoles = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, userRoles);

            if (model.RoleNames?.Length > 0)
            {
                await userManager.AddToRolesAsync(user, model.RoleNames);
            }

            return this.RedirectToAction("All", "Users", new { area = "Admin" });
        }


    }
}
