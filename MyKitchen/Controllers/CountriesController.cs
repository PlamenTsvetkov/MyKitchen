namespace MyKitchen.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyKitchen.Models.Countries;
    using MyKitchen.Services.Countries;

    public class CountriesController : Controller
    {
        private readonly ICountriesService countriesService;

        public CountriesController(ICountriesService countriesService)
        {
            this.countriesService = countriesService;
        }
        public IActionResult Add()
        {
            return this.View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(CountryFormModel country)
        {
           
            if (!ModelState.IsValid)
            {
                return View(country);
            }

            this.countriesService.Create(country.Name);

            this.TempData["Message"] = "Country added successfully.";

            return RedirectToAction(nameof(ManufacturersController.Add), "Manufacturers");

        }
    }
}
