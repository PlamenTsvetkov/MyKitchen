namespace MyKitchen.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyKitchen.Data.Models;
    using MyKitchen.Models.Kitchens;
    using MyKitchen.Services.Categories;
    using MyKitchen.Services.Colors;
    using MyKitchen.Services.Colors.Models;
    using MyKitchen.Services.Kitchens;
    using MyKitchen.Services.Manufacturers;
    using MyKitchen.Services.Manufacturers.Models;

    public class KitchensController : Controller
    {
        private readonly IManufacturersService manufacturersService;
        private readonly ICategoriesService categoriesService;
        private readonly IKitchenService kitchenService;
        private readonly IColorsService colorService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public KitchensController(IManufacturersService manufacturersService,
            ICategoriesService categoriesService,
            IKitchenService kitchenService,
            IColorsService colorService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.manufacturersService = manufacturersService;
            this.categoriesService = categoriesService;
            this.kitchenService = kitchenService;
            this.colorService = colorService;
            this.userManager = userManager;
            this.environment = environment;
        }
        public IActionResult Add(int id)
        {
            return View(new KitchenFormModel
            {
                Manufacturers = this.manufacturersService.GetAll<KitchenManufacturerServiceModel>(),
                Colors = this.colorService.GetAll<KitchenColorServiceModel>(),
                CategoryId = id
            });
        }
        [HttpPost]
        public async Task<IActionResult> Add(KitchenFormModel kitchen)
        {
            if (!this.categoriesService.CategoryExists(kitchen.CategoryId))
            {
                this.ModelState.AddModelError(nameof(kitchen.CategoryId), "Category does not exist.");
            }
            if (!ModelState.IsValid)
            {
                kitchen.Manufacturers = this.manufacturersService.GetAll<KitchenManufacturerServiceModel>();

                return View(kitchen);
            }
            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.kitchenService.AddAsync(kitchen, user.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                kitchen.Manufacturers = this.manufacturersService.GetAll<KitchenManufacturerServiceModel>();

                return View(kitchen);
            }

            this.TempData["Message"] = "Kitchen added successfully.";

            // TODO: Redirect to recipe info page
            return this.RedirectToAction("All");

        }
    }
}

