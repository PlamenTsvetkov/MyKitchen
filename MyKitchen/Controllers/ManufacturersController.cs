namespace MyKitchen.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using MyKitchen.Models.Manufacturers;
    using MyKitchen.Services.Cities;
    using MyKitchen.Services.Countries;

    public class ManufacturersController : Controller
    {
        private readonly ICountriesService countryService;
        private readonly ICitiesService citiesService;

        public ManufacturersController(ICountriesService countryService,
            ICitiesService citiesService)
        {
            this.countryService = countryService;
            this.citiesService = citiesService;
        }
        public IActionResult Add()
        {
            return View(new ManufacturerFormModel
            {
                Countries = this.countryService.GetAll<ManucafturerCountryFormModel>()
            });
        }
        public JsonResult LoadCity (int Id)
        {
            var cities = citiesService.GetByCountryId<ManufacturerCityFormModel>(Id);
            return Json(new SelectList(cities, "Id", "Name"));
        }
    }
}
