namespace MyKitchen.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using MyKitchen.Models;
    using MyKitchen.Models.Home;
    using MyKitchen.Services.Categories;

    public class HomeController : Controller
    {
        private readonly ICategoriesService categoriesService;

        public HomeController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }
        public IActionResult Index()
        {
            this.TempData["InfoMessage"] = "Thank you for visiting home page.";
            var viewModel = new IndexViewModel
            {
                Categories =
                    this.categoriesService.GetAll<IndexCategoryViewModel>(),
                
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