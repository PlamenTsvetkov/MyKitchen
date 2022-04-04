namespace MyKitchen.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using MyKitchen.Models.Categories;
    using MyKitchen.Services.Categories;

    public class CategoriesController : AdminController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }
        public IActionResult All(int id = 1)
        {

            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 12;
            var viewModel = new CategoriesListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                ItemsCount = this.categoriesService.GetCount(),
                Categories = this.categoriesService.GetAllWithPaging<CategoriesInListViewModel>(id),
                Action = nameof(All),
            };
            return this.View(viewModel);
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(CategoryFormModel category)
        {

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            this.categoriesService.Create(category.Name, category.Description, category.ImageUrl);

            this.TempData["Message"] = "Category added successfully.";

            return RedirectToAction(nameof(ManufacturersController.All), "Categories");

        }

        public IActionResult Edit(int id)
        {
            var category = this.categoriesService.GetById<CategoryFormModel>(id);
            
            return this.View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CategoryFormModel category)
        {

            if (!this.ModelState.IsValid)
            {
                return this.View(category);
            }

            await categoriesService.UpdateAsync(id,
                                            category.Name,
                                           category.Description,
                                           category.ImageUrl);

            this.TempData["Message"] = "Category edited successfully.";

            return this.RedirectToAction(nameof(ManufacturersController.All), "Categories");
            
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.categoriesService.DeleteAsync(id);

            this.TempData["Message"] = "Category deleted successfully.";

            return this.RedirectToAction(nameof(ManufacturersController.All), "Categories");

        }
    }
}
