namespace MyKitchen.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using MyKitchen.Data.Models;
    using MyKitchen.Models.Cityes;
    using MyKitchen.Models.Countries;
    using MyKitchen.Services.Cities;
    using MyKitchen.Services.Countries;
    using MyKitchen.Services.Manufacturers;

    public class ManufacturersController : Controller
    {
        private readonly ICountriesService countryService;
        private readonly ICitiesService citiesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IManufacturersService manufacturersService;

        public ManufacturersController(ICountriesService countryService,
            ICitiesService citiesService,
            UserManager<ApplicationUser> userManager,
            IManufacturersService manufacturersService)
        {
            this.countryService = countryService;
            this.citiesService = citiesService;
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
    }
}
