namespace MyKitchen.Infrastructure
{
    using AutoMapper;
    using MyKitchen.Data.Models;
    using MyKitchen.Models.Categories;
    using MyKitchen.Models.Home;
    using MyKitchen.Models.Kitchens;
    using MyKitchen.Models.Manufacturers;
    using MyKitchen.Services.Colors.Models;
    using MyKitchen.Services.Manufacturers.Models;

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
            this.CreateMap<Country, ManucafturerCountryFormModel>();
            this.CreateMap<City, ManufacturerCityFormModel>();
        }
    }
}

