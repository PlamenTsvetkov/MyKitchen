namespace MyKitchen.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyKitchen.Models.Manufacturers;
    using MyKitchen.Services.Manufacturers;

    public class ManufacturersController : AdminController
    {
        private readonly IManufacturersService manufacturersService;

        public ManufacturersController(IManufacturersService manufacturersService)
        {
            this.manufacturersService = manufacturersService;
        }
        public IActionResult All(int id = 1)
        {
            //const int ItemsPerPage = 12;

            //var viewModel = new KitchensListViewModel
            //{
            //    ItemsPerPage = ItemsPerPage,
            //    PageNumber = id,
            //    ItemsCount = this.kitchenService.GetCountAdmin(),
            //    Kitchens = this.kitchenService.GetAllA<KitchenInListViewModel>(id, ItemsPerPage, publicOnly: false),
            //    Action = nameof(All),
            //};

            //return this.View(viewModel);

            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 12;
            var viewModel = new ManufacturersListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                ItemsCount = this.manufacturersService.GetCount(),
                Manufacturers = this.manufacturersService.GetAllWithPaging<ManufacturerInListViewModel>(id, ItemsPerPage, publicOnly: false),
                Action = nameof(All),
            };
            return this.View(viewModel);
        }

        public IActionResult ChangeVisibility(int id)
        {
            this.manufacturersService.ChangeVisility(id);

            return RedirectToAction(nameof(All));
        }
    }
}
