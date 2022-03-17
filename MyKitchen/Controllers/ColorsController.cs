namespace MyKitchen.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyKitchen.Models.Colors;
    using MyKitchen.Services.Colors;

    public class ColorsController : Controller
    {
        private readonly IColorsService colorsService;

        public ColorsController(IColorsService colorsService)
        {
            this.colorsService = colorsService;
        }

        [Authorize]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(ColorFormModel color)
        {

            if (!ModelState.IsValid)
            {
                return View(color);
            }

            this.colorsService.Create(color.Name);

            this.TempData["Message"] = "Color added successfully.";

            return RedirectToAction(nameof(KitchensController.Add), "Kitchens");

        }
    }
}
