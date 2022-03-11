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
        //public IActionResult Add(int id)
        //{
        //    return View(new KitchenFormModel
        //    {
        //        Manufacturers = this.manufacturersService.GetAll<KitchenManufacturerServiceModel>(),
        //        Colors = this.colorService.GetAll<KitchenColorServiceModel>(),
        //        CategoryId = id
        //    });
        //}
        //[HttpPost]
        //public async Task<IActionResult> Add(int id, KitchenFormModel kitchen)
        //{
        //    kitchen.CategoryId = id;
        //    var user = await this.userManager.GetUserAsync(this.User);
        //    kitchen.UserId = user.Id;
        //    if (!this.categoriesService.CategoryExists(kitchen.CategoryId))
        //    {
        //        this.ModelState.AddModelError(nameof(kitchen.CategoryId), "Category does not exist.");
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        kitchen.Manufacturers = this.manufacturersService.GetAll<KitchenManufacturerServiceModel>();
        //        kitchen.Colors = this.colorService.GetAll<KitchenColorServiceModel>();
        //        return View(kitchen);
        //    }

        //    try
        //    {
        //        await this.kitchenService.AddAsync(kitchen, user.Id, $"{this.environment.WebRootPath}/images");
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ModelState.AddModelError(string.Empty, ex.Message);
        //        kitchen.Manufacturers = this.manufacturersService.GetAll<KitchenManufacturerServiceModel>();
        //        kitchen.Colors = this.colorService.GetAll<KitchenColorServiceModel>();
        //        return View(kitchen);
        //    }

        //    this.TempData["Message"] = "Kitchen added successfully.";

        //    // TODO: Redirect to recipe info page
        //    return this.RedirectToAction("All");

        //}
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

            this.TempData["Message"] = "Kitchen added successfully.";

            // TODO: Redirect to recipe info page
            return this.RedirectToAction("All");

        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }


            const int ItemsPerPage = 3;
            var viewModel = new KitchensListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                ItemsCount = this.kitchenService.GetCount(),
                Kitchens = this.kitchenService.GetAll<KitchenInListViewModel>(id, ItemsPerPage),
                Action = nameof(All),
            };

            return this.View(viewModel);
        }

        public  IActionResult My(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var userId =   this.userManager.GetUserId(this.User);
            const int ItemsPerPage = 3;
            var viewModel = new KitchensListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                ItemsCount = this.kitchenService.GetCountByUserId(userId),
                Kitchens = this.kitchenService.GetAllByUserId<KitchenInListViewModel>(userId , id, ItemsPerPage),
                Action = nameof(My),
            };

            return this.View(viewModel);
        }

        public IActionResult Details(int id)
        {
            var recipe = this.kitchenService.GetById<SingleKitchenViewModel>(id);
            return this.View(recipe);
        }

        public IActionResult Edit(int id)
        {
            var inputModel = this.kitchenService.GetById<EditKitchenInputModel>(id);
            inputModel.Manufacturers = this.manufacturersService.GetAll<KitchenManufacturerServiceModel>();
            inputModel.Categories = this.categoriesService.GetAll<KitchenCategoriesServiceModel>();
            inputModel.Colors = this.colorService.GetAll<KitchenColorServiceModel>();
            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditKitchenInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Manufacturers = this.manufacturersService.GetAll<KitchenManufacturerServiceModel>();
                input.Categories = this.categoriesService.GetAll<KitchenCategoriesServiceModel>();
                input.Colors = this.colorService.GetAll<KitchenColorServiceModel>();
                return this.View(input);
            }

            await this.kitchenService.UpdateAsync(id, input);
            return this.RedirectToAction(nameof(this.Details), new { id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.kitchenService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.All));
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

            const int ItemsPerPage = 3;
            var viewModel = new KitchensListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                ItemsCount = this.kitchenService.GetCollectionCountByUserId(userId),
                Kitchens = this.kitchenService.GetAllInCollectionByUserId<KitchenInListViewModel>(userId, id, ItemsPerPage),
                Action = nameof(Collection),
            };

            return this.View(viewModel);
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

