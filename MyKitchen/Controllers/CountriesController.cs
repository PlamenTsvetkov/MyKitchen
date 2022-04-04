namespace MyKitchen.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using MyKitchen.Models.Countries;
    using MyKitchen.Services.Countries;

    [Authorize]
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
        public IActionResult Add(CountryFormModel country)
        {
           
            if (!ModelState.IsValid)
            {
                return View(country);
            }

            this.countriesService.Create(country.Name);

            this.TempData["Message"] = "Country added successfully.";

            return this.RedirectToAction(nameof(ManufacturersController.Add), "Manufacturers");
        }
    }
}
