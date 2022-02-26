﻿namespace MyKitchen.Infrastructure
{
    using AutoMapper;
    using MyKitchen.Data.Models;
    using MyKitchen.Models.Categories;
    using MyKitchen.Models.Home;
    using MyKitchen.Models.Kitchens;
    using MyKitchen.Models.Countries;
    using MyKitchen.Services.Colors.Models;
    using MyKitchen.Services.Manufacturers.Models;
    using MyKitchen.Models.Cityes;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Category, IndexCategoryViewModel>();
            this.CreateMap<Category, KitchenCategoryFormModel>();
            this.CreateMap<Category, CategoryViewModel>();
            this.CreateMap<Kitchen, KitchensInCategoryViewModel>();
            this.CreateMap<Manufacturer, KitchenManufacturerServiceModel>();
            this.CreateMap<Color, KitchenColorServiceModel>();
            this.CreateMap<Country, AllCountryModel>();
            this.CreateMap<City, AllCityModel>();
        }
    }
}

