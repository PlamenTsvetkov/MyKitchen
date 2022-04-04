namespace MyKitchen.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using MyKitchen.Models.Kitchens;
    using MyKitchen.Services.Kitchens;

    public class KitchensController : AdminController
    {
        private readonly IKitchenService kitchenService;

        public KitchensController(IKitchenService kitchenService) 
            => this.kitchenService = kitchenService;

        public IActionResult All(int id = 1)
        {
            const int ItemsPerPage = 12;

            var viewModel = new KitchensListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                ItemsCount = this.kitchenService.GetCountAdmin(),
                Kitchens = this.kitchenService.GetAllA<KitchenInListViewModel>(id, ItemsPerPage, publicOnly: false),
                Action = nameof(All),
            };

            return this.View(viewModel);
        }

        public IActionResult ChangeVisibility(int id)
        {
            this.kitchenService.ChangeVisility(id);

            return RedirectToAction(nameof(All));
        }
    }
}

