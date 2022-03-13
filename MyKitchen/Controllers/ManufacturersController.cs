﻿namespace MyKitchen.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using MyKitchen.Data.Models;
    using MyKitchen.Infrastructure.Extensions;
    using MyKitchen.Models.Cityes;
    using MyKitchen.Models.Countries;
    using MyKitchen.Models.Kitchens;
    using MyKitchen.Models.Manufacturers;
    using MyKitchen.Services.Cities;
    using MyKitchen.Services.Countries;
    using MyKitchen.Services.Kitchens;
    using MyKitchen.Services.Manufacturers;

    public class ManufacturersController : Controller
    {
        private readonly ICountriesService countryService;
        private readonly ICitiesService citiesService;
        private readonly IKitchenService kitchenService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IManufacturersService manufacturersService;

        public ManufacturersController(ICountriesService countryService,
            ICitiesService citiesService,
            IKitchenService kitchenService,
            UserManager<ApplicationUser> userManager,
            IManufacturersService manufacturersService)
        {
            this.countryService = countryService;
            this.citiesService = citiesService;
            this.kitchenService = kitchenService;
            this.userManager = userManager;
            this.manufacturersService = manufacturersService;
        }
        public IActionResult Add()
        {
            return this.View(new ManufacturerFormModel
            {
                Countries = countryService.GetAll<AllCountryModel>(),
                Cities = citiesService.GetAll<AllCityModel>(),
            });
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(ManufacturerFormModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Countries = this.countryService.GetAll<AllCountryModel>();
                input.Cities = this.citiesService.GetAll<AllCityModel>();
                return this.View(input);
            }

            var userId =  this.userManager.GetUserId(this.User);

            manufacturersService.Create(input.Name, input.Email, input.Website, input.PhoneNumber, userId , input.CountryId, input.CityId, input.Address.Name, input.Address.Number);

            this.TempData["Message"] = "Manufacturer added successfully.";

            return this.RedirectToAction("All");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            return this.View(new ManufacturerFormModel
            {
                Countries = countryService.GetAll<AllCountryModel>(),
                Cities = citiesService.GetAll<AllCityModel>(),
            });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, ManufacturerFormModel input)
        {
            var userId = this.User.Id();

            if (!this.ModelState.IsValid)
            {
                input.Countries = this.countryService.GetAll<AllCountryModel>();
                input.Cities = this.citiesService.GetAll<AllCityModel>();
                return this.View(input);
            }
            if (!User.IsAdmin())
            {
                return Unauthorized();
            }

            await manufacturersService.UpdateAsync(id,
                                            input.Name, 
                                            input.Email, 
                                            input.Website, 
                                            input.PhoneNumber, 
                                            userId, 
                                            input.CountryId, 
                                            input.CityId, 
                                            input.Address.Name, 
                                            input.Address.Number, 
                                            this.User.IsAdmin());

            this.TempData["Message"] = "Manufacturer edited successfully.";

            if (User.IsAdmin())
            {
                return this.RedirectToAction("All", "Kitchens", new { area = "Admin" });
            }
            else
            {
                return this.RedirectToAction(nameof(this.All));
            }
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 6;
            var viewModel = new ManufacturersListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                ItemsCount = this.manufacturersService.GetCount(),
                Manufacturers = this.manufacturersService.GetAllWithPaging<ManufacturerInListViewModel>(id, ItemsPerPage),
                Action = nameof(All),
            };
            return this.View(viewModel);
        }

        public IActionResult AllKitchens(int manufacturerId, int id=1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 3;
            var viewModel = new KitchensListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                ItemsCount = this.kitchenService.GetCountByManufacturerId(manufacturerId),
                Kitchens = this.kitchenService.GetAllByManufacturerId<KitchenInListViewModel>(manufacturerId, id, ItemsPerPage),
                Action = nameof(AllKitchens),
            };
            return this.View(viewModel);
        }
    }
}
