namespace MyKitchen.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using MyKitchen.Data.Models;
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
        private readonly ICountriesService countryService;
        private readonly ICitiesService citiesService;
        private readonly IKitchenService kitchenService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IManufacturersService manufacturersService;

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
        public IActionResult Add()
        {
            return this.View(new ManufacturerFormModel
            {
                Countries = countryService.GetAll<AllCountryModel>(),
                Cities = citiesService.GetAll<AllCityModel>(),
            });
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(ManufacturerFormModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Countries = this.countryService.GetAll<AllCountryModel>();
                input.Cities = this.citiesService.GetAll<AllCityModel>();
                return this.View(input);
            }

            var userId =  this.userManager.GetUserId(this.User);

            manufacturersService.Create(input.Name, input.Email, input.Website, input.PhoneNumber, userId , input.CountryId, input.CityId, input.Address.Name, input.Address.Number);

            this.TempData["Message"] = "Manufacturer added successfully.";

            return this.RedirectToAction("All");
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 6;
            var viewModel = new ManufacturersListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                ItemsCount = this.manufacturersService.GetCount(),
                Manufacturers = this.manufacturersService.GetAllWithPaging<ManufacturerInListViewModel>(id, ItemsPerPage),
            };
            return this.View(viewModel);
        }

        public IActionResult AllKitchens(int manufacturerId, int pageId=1)
        {
            if (pageId <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 12;
            var viewModel = new KitchensListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = pageId,
                ItemsCount = this.kitchenService.GetCountByManufacturerId(manufacturerId),
                Kitchens = this.kitchenService.GetAllByManufacturerId<KitchenInListViewModel>(manufacturerId, pageId, ItemsPerPage),
            };
            return this.View(viewModel);
        }
    }
}
