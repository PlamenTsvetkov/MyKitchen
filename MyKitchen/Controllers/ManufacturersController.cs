namespace MyKitchen.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;

    using MyKitchen.Data.Models;
    using MyKitchen.Infrastructure.Extensions;
    using MyKitchen.Models.Cityes;
    using MyKitchen.Models.Countries;
    using MyKitchen.Models.Kitchens;
    using MyKitchen.Models.Manufacturers;
    using MyKitchen.Services.Cities;
    using MyKitchen.Services.Countries;
    using MyKitchen.Services.Kitchens;
    using MyKitchen.Services.Manufacturers;

    public class ManufacturersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICountriesService countryService;
        private readonly ICitiesService citiesService;
        private readonly IKitchenService kitchenService;
        private readonly IManufacturersService manufacturersService;

        public const int manufacturerPerPage = 6;
        public ManufacturersController(ICountriesService countryService,
                                       ICitiesService citiesService,
                                       IKitchenService kitchenService,
                                       UserManager<ApplicationUser> userManager,
                                       IManufacturersService manufacturersService)
        {
            this.countryService = countryService;
            this.citiesService = citiesService;
            this.kitchenService = kitchenService;
            this.userManager = userManager;
            this.manufacturersService = manufacturersService;
        }

        [Authorize]
        public IActionResult Add()
        {
            return this.View(new ManufacturerFormModel
            {
                Countries = countryService.GetAll<AllCountryModel>(),
                Cities = citiesService.GetAll<AllCityModel>(),
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(ManufacturerFormModel manufacturer)
        {
            if (!this.ModelState.IsValid)
            {
                manufacturer.Countries = this.countryService.GetAll<AllCountryModel>();
                manufacturer.Cities = this.citiesService.GetAll<AllCityModel>();
                return this.View(manufacturer);
            }

            var userId = this.userManager.GetUserId(this.User);

            manufacturersService.Create(manufacturer.Name, manufacturer.Email, manufacturer.Website, manufacturer.PhoneNumber, userId, manufacturer.CountryId, manufacturer.CityId, manufacturer.Address.Name, manufacturer.Address.Number);

            this.TempData["Message"] = "Manufacturer added successfully.";

            return this.RedirectToAction("All");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var manufacturer = this.manufacturersService.GetById<EditManufacturerInputModel>(id);
            var userId = this.User.Id();

            if (!this.manufacturersService.IsByUser(id, userId) && !User.IsAdmin())
            {
                return Unauthorized();
            }

            manufacturer.Countries = countryService.GetAll<AllCountryModel>();
            manufacturer.Cities = citiesService.GetAll<AllCityModel>();

            return this.View(manufacturer);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ManufacturerFormModel manufacturer)
        {
            var userId = this.User.Id();

            if (!this.manufacturersService.IsByUser(id, userId) && !User.IsAdmin())
            {
                return Unauthorized();
            }

            if (!this.ModelState.IsValid)
            {
                manufacturer.Countries = this.countryService.GetAll<AllCountryModel>();
                manufacturer.Cities = this.citiesService.GetAll<AllCityModel>();
                return this.View(manufacturer);
            }
            if (!this.manufacturersService.IsByUser(id, userId) && !User.IsAdmin() && !User.IsAManufacturer())
            {
                return Unauthorized();
            }

            await manufacturersService.UpdateAsync(id,
                                            manufacturer.Name,
                                            manufacturer.Email,
                                            manufacturer.Website,
                                            manufacturer.PhoneNumber,
                                            userId,
                                            manufacturer.CountryId,
                                            manufacturer.CityId,
                                            manufacturer.Address.Name,
                                            manufacturer.Address.Number,
                                            this.User.IsAdmin());

            this.TempData["Message"] = "Manufacturer edited successfully.";

            if (User.IsAdmin())
            {
                return this.RedirectToAction("All", "Manufacturers", new { area = "Admin" });
            }
            else
            {
                return this.RedirectToAction(nameof(this.All));
            }
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = manufacturerPerPage;
            var viewModel = new ManufacturersListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                ItemsCount = this.manufacturersService.GetCount(),
                Manufacturers = this.manufacturersService.GetAllWithPaging<ManufacturerInListViewModel>(id, ItemsPerPage),
                Action = nameof(All),
            };
            return this.View(viewModel);
        }

        public IActionResult AllKitchens(int manufacturerId, int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            ViewBag.ManufacturerName = manufacturersService.GetById<ManufacturerInListViewModel>(manufacturerId).Name;

        var viewModel = new KitchensListViewModel
            {
                ItemsPerPage = manufacturerPerPage,
                PageNumber = id,
                ItemsCount = this.kitchenService.GetCountByManufacturerId(manufacturerId),
                Kitchens = this.kitchenService.GetAllByManufacturerId<KitchenInListViewModel>(manufacturerId, id, manufacturerPerPage),
                Action = nameof(AllKitchens),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = this.User.Id();

            if (!this.manufacturersService.IsByUser(id, userId) && !User.IsAdmin())
            {
                return Unauthorized();
            }

            await this.manufacturersService.DeleteAsync(id);

            if (User.IsAdmin())
            {
                return this.RedirectToAction("All", "Manufacturers", new { area = "Admin" });
            }
            else
            {
                return this.RedirectToAction(nameof(this.All));
            }

        }
    }
}
