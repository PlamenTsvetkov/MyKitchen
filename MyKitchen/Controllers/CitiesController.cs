namespace MyKitchen.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyKitchen.Models.Cityes;
    using MyKitchen.Models.Countries;
    using MyKitchen.Services.Cities;
    using MyKitchen.Services.Countries;

    public class CitiesController : Controller
    {
        private readonly ICountriesService countriesService;
        private readonly ICitiesService citiesService;

        public CitiesController(ICountriesService countriesService,
            ICitiesService citiesService)
        {
            this.countriesService = countriesService;
            this.citiesService = citiesService;
        }

        public IActionResult Add()
        {
            return this.View(new CityFormModel
            {
                Countries = this.countriesService.GetAll<AllCountryModel>(),
            });
        }
        [HttpPost]
        public IActionResult Add(CityFormModel city)
        {
            if (!this.countriesService.CountryExists(city.CountryId))
            {
                this.ModelState.AddModelError(nameof(city.CountryId), "Country does not exist.");
            }
            city.Countries = this.countriesService.GetAll<AllCountryModel>();
            if (!ModelState.IsValid)
            {
                return View(city);
            }

            this.citiesService.Create(city.Name, city.CountryId);

            this.TempData["Message"] = "City added successfully.";

            return RedirectToAction(nameof(ManufacturersController.Add), "Manufacturers");

        }
    }
}
