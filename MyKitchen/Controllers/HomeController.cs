﻿namespace MyKitchen.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using MyKitchen.Models;
    using MyKitchen.Models.Home;
    using MyKitchen.Models.Kitchens;
    using MyKitchen.Services.Categories;
    using MyKitchen.Services.Kitchens;

    using static WebConstants.Cache;

    public class HomeController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IKitchenService kitchenService;
        private readonly IMemoryCache cache;

        public const int kitchenPerHome = 3;

        public HomeController(ICategoriesService categoriesService,
            IKitchenService kitchenService,
            IMemoryCache cache)
        {
            this.categoriesService = categoriesService;
            this.kitchenService = kitchenService;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            var indexViewModel = this.cache.Get<IndexViewModel>(IndexViewCasheKey);

            if (indexViewModel == null)
            {
                indexViewModel = new IndexViewModel
                {
                    Categories =
                   this.categoriesService.GetAll<IndexCategoryViewModel>(),
                    Kitchens = this.kitchenService.GetRandom<HomeKitchensViewModel>(kitchenPerHome).ToList(),

                };

                var cacheOptions = new MemoryCacheEntryOptions()
                   .SetAbsoluteExpiration(TimeSpan.FromMinutes(3));

                this.cache.Set(IndexViewCasheKey, indexViewModel, cacheOptions);
            }
           
            return this.View(indexViewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}