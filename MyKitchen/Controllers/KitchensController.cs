namespace MyKitchen.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyKitchen.Data.Models;
    using MyKitchen.Models.Categories;
    using MyKitchen.Models.Kitchens;
    using MyKitchen.Services.Categories;
    using MyKitchen.Services.Categories.Models;
    using MyKitchen.Services.Colors;
    using MyKitchen.Services.Colors.Models;
    using MyKitchen.Services.Kitchens;
    using MyKitchen.Services.Manufacturers;
    using MyKitchen.Services.Manufacturers.Models;

    using MyKitchen.Infrastructure.Extensions;

    using static WebConstants;
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

        [Authorize]
        public IActionResult Add()
        {
            return View(new KitchenFormModel
            {
                Manufacturers = this.manufacturersService.GetAll<KitchenManufacturerServiceModel>(),
                Colors = this.colorService.GetAll<KitchenColorServiceModel>(),
                Categories= this.categoriesService.GetAll<KitchenCategoriesServiceModel>(),
            });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add( KitchenFormModel kitchen)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            kitchen.UserId = user.Id;
            if (!this.categoriesService.CategoryExists(kitchen.CategoryId))
            {
                this.ModelState.AddModelError(nameof(kitchen.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                kitchen.Manufacturers = this.manufacturersService.GetAll<KitchenManufacturerServiceModel>();
                kitchen.Colors = this.colorService.GetAll<KitchenColorServiceModel>();
                kitchen.Categories = this.categoriesService.GetAll<KitchenCategoriesServiceModel>();
                return View(kitchen);
            }

            try
            {
                await this.kitchenService.AddAsync(kitchen, user.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                kitchen.Manufacturers = this.manufacturersService.GetAll<KitchenManufacturerServiceModel>();
                kitchen.Colors = this.colorService.GetAll<KitchenColorServiceModel>();
                kitchen.Categories = this.categoriesService.GetAll<KitchenCategoriesServiceModel>();
                return View(kitchen);
            }

            this.TempData["Message"] = "Your kitchen was added and is awaiting for approval.";
            var kitchenId = kitchenService.GetLastKitchenIdByUserId(user.Id);

            return RedirectToAction(nameof(Details), new { id = kitchenId, information = kitchen.GetInformation() });

        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }


            const int ItemsPerPage = kitchenPerPage;

            var kitchens = new KitchensListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                ItemsCount = this.kitchenService.GetCount(),
                Kitchens = this.kitchenService.GetAll<KitchenInListViewModel>(id, ItemsPerPage),
                Action = nameof(All),
            };

            return this.View(kitchens);
        }

        [Authorize]
        public  IActionResult My(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var userId =   this.userManager.GetUserId(this.User);
            const int ItemsPerPage = kitchenPerPage;
            var myKitchens = new KitchensListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                ItemsCount = this.kitchenService.GetCountByUserId(userId),
                Kitchens = this.kitchenService.GetAllByUserId<KitchenInListViewModel>(userId , id, ItemsPerPage),
                Action = nameof(My),
            };

            return this.View(myKitchens);
        }

        public IActionResult Details(int id, string information)
        {
            var kitchen = this.kitchenService.GetById<SingleKitchenViewModel>(id);

            if (information != kitchen.GetInformation())
            {
                return BadRequest();
            }

            return this.View(kitchen);
        }

        public IActionResult Edit(int id)
        {
            var kitchen = this.kitchenService.GetById<EditKitchenInputModel>(id);
            var userId = this.User.Id();

            if (!this.kitchenService.IsByUser(id, userId) && !User.IsAdmin())
            {
                return Unauthorized();
            }

            kitchen.Manufacturers = this.manufacturersService.GetAll<KitchenManufacturerServiceModel>();
            kitchen.Categories = this.categoriesService.GetAll<KitchenCategoriesServiceModel>();
            kitchen.Colors = this.colorService.GetAll<KitchenColorServiceModel>();
            return this.View(kitchen);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditKitchenInputModel kitchen)
        {
            var userId = this.User.Id();
            if (!this.categoriesService.CategoryExists(kitchen.CategoryId))
            {
                this.ModelState.AddModelError(nameof(kitchen.CategoryId), "Category does not exist.");
            }
            if (!this.kitchenService.IsByUser(id, userId) && !User.IsAdmin())
            {
                return Unauthorized();
            }

            if (!this.ModelState.IsValid)
            {
                kitchen.Manufacturers = this.manufacturersService.GetAll<KitchenManufacturerServiceModel>();
                kitchen.Categories = this.categoriesService.GetAll<KitchenCategoriesServiceModel>();
                kitchen.Colors = this.colorService.GetAll<KitchenColorServiceModel>();
                return this.View(kitchen);
            }

            await this.kitchenService.UpdateAsync(id, kitchen, this.User.IsAdmin());

            this.TempData["Message"] = $"You kitchen was edited{(this.User.IsAdmin() ? string.Empty : " and is awaiting for approval")}!";

            return RedirectToAction(nameof(Details), new { id = id, information = kitchen.GetInformation() });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = this.User.Id();

            if (!this.kitchenService.IsByUser(id, userId) && !User.IsAdmin())
            {
                return Unauthorized();
            }
            await this.kitchenService.DeleteAsync(id);
            if (User.IsAdmin())
            {
                return this.RedirectToAction("All", "Kitchens", new { area = "Admin" });
            }
            else
            {
                return this.RedirectToAction(nameof(this.All));
            }
            
        }

        [Authorize]
        public IActionResult AddToCollection(int id)
        {
            var userId = this.userManager.GetUserId(this.User);

            this.kitchenService.AddKitchenToUserCollection(id, userId);

            this.TempData["Message"] = "Kitchen added successfully in your collection.";

            return RedirectToAction(nameof(this.Collection));
        }

        [Authorize]
        public IActionResult Collection(int id = 1)
        {
            var userId = this.userManager.GetUserId(this.User);

            const int ItemsPerPage = kitchenPerPage;
            var kitchens = new KitchensListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                ItemsCount = this.kitchenService.GetCollectionCountByUserId(userId),
                Kitchens = this.kitchenService.GetAllInCollectionByUserId<KitchenInListViewModel>(userId, id, ItemsPerPage),
                Action = nameof(Collection),
            };

            return this.View(kitchens);
        }

        [Authorize]
        public IActionResult RemoveFromCollection(int id)
        {
            var userId = this.userManager.GetUserId(this.User);

            this.kitchenService.RemoveKitchenToUserCollection(id, userId);

            return this.RedirectToAction(nameof(this.Collection));
        }


    }
}

