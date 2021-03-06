namespace MyKitchen.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using MyKitchen.Models.Categories;
    using MyKitchen.Models.Kitchens;
    using MyKitchen.Services.Categories;
    using MyKitchen.Services.Kitchens;

    public class CategoriesController : Controller
    {
        private const int ItemsPerPage = 3;

        private readonly ICategoriesService categoriesService;
        private readonly IKitchenService kitchenService;

        public CategoriesController(ICategoriesService categoriesService,
                                    IKitchenService kitchenService)
        {
            this.categoriesService = categoriesService;
            this.kitchenService = kitchenService;
        }

        public IActionResult ByName(string name, int page = 1)
        {

            var viewModel = this.categoriesService.GetByName<CategoryViewModel>(name);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            viewModel.Kitchens = this.kitchenService.GetByCategoryId<KitchenInListViewModel>(viewModel.Id, ItemsPerPage, (page - 1) * ItemsPerPage);

            var count = this.kitchenService.GetCountByCategoryId(viewModel.Id);

            viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);

            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }
    }
}

