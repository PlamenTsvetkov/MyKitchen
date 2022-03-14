namespace MyKitchen.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using MyKitchen.Models;
    using MyKitchen.Models.Home;
    using MyKitchen.Models.Kitchens;
    using MyKitchen.Services.Categories;
    using MyKitchen.Services.Kitchens;

    public class HomeController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IKitchenService kitchenService;

        public const int kitchenPerHome = 3;
        public HomeController(ICategoriesService categoriesService,
            IKitchenService kitchenService)
        {
            this.categoriesService = categoriesService;
            this.kitchenService = kitchenService;
        }
        public IActionResult Index()
        {
            this.TempData["InfoMessage"] = "Thank you for visiting home page.";
            var viewModel = new IndexViewModel
            {
                Categories =
                    this.categoriesService.GetAll<IndexCategoryViewModel>(),
                Kitchens = this.kitchenService.GetRandom<HomeKitchensViewModel>(kitchenPerHome).ToList(),

            };
            return this.View(viewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}