namespace MyKitchen.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyKitchen.Models.Roles;
    using MyKitchen.Services.Roles;

    public class RolesController : AdminController
    {
        private readonly IRolesService rolesService;
        private readonly RoleManager<IdentityRole> roleManager;

        public RolesController(IRolesService rolesService,
                                RoleManager<IdentityRole> _roleManager)
        {
            this.rolesService = rolesService;
            roleManager = _roleManager;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(RoleFormModel role)
        {

            if (!ModelState.IsValid)
            {
                return View(role);
            }

            await roleManager.CreateAsync(new IdentityRole()
            {
                Name = role.Name
            });

            this.TempData["Message"] = "Role added successfully.";

            return RedirectToAction(nameof(HomeController.Index), "Home", new { area = "Admin" });

        }
    }
}
