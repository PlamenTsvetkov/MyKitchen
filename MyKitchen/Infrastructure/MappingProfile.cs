namespace MyKitchen.Infrastructure
{
    using AutoMapper;

    using MyKitchen.Data.Models;

    using MyKitchen.Models.Home;
    using MyKitchen.Models.Users;
    using MyKitchen.Models.Cityes;
    using MyKitchen.Models.Colors;
    using MyKitchen.Models.Comments;
    using MyKitchen.Models.Kitchens;
    using MyKitchen.Models.Countries;
    using MyKitchen.Models.Categories;
    using MyKitchen.Models.Manufacturers;

    using MyKitchen.Services.Colors.Models;
    using MyKitchen.Services.Manufacturers.Models;
    using MyKitchen.Services.Categories.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<ApplicationUser, UserInListViewModel>();
            this.CreateMap<ApplicationUser, EditUserViewModel>();
            this.CreateMap<ApplicationUser, ApplicationUser>();

            this.CreateMap<Address, ManufacturerAddressFormModel>();

            this.CreateMap<Comment, PostCommentViewModel>();

            this.CreateMap<Category, CategoryFormModel>();
            this.CreateMap<Category, CategoryViewModel>();
            this.CreateMap<Category, CategoriesInListViewModel>();
            this.CreateMap<Category, KitchenCategoriesServiceModel>();
            this.CreateMap<Category, IndexCategoryViewModel>()
                             .ForMember(x => x.KitchensCount, opt =>
                             opt.MapFrom(x => x.Kitchens.Count(k=>k.IsDeleted==false && k.IsPublic)));

            this.CreateMap<Color, KitchenColorServiceModel>();
            this.CreateMap<Color, ColorsViewModel>();

            this.CreateMap<Country, AllCountryModel>();

            this.CreateMap<City, AllCityModel>();

            this.CreateMap<Manufacturer, KitchenManufacturerServiceModel>();
            this.CreateMap<Manufacturer, EditManufacturerInputModel>();
            this.CreateMap<Manufacturer, ManufacturerTestModel>();
            this.CreateMap<Manufacturer, ManufacturerInListViewModel>()
                              .ForMember(x => x.KitchensCount, opt =>
                              opt.MapFrom(x => x.Kitchens.Count(k => k.IsDeleted == false && k.IsPublic)));

            this.CreateMap<Kitchen, KitchensInCategoryViewModel>()
                             .ForMember(x => x.ImageUrl,
                             opt => opt.MapFrom(x => "/images/kitchens/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
            this.CreateMap<Kitchen, TestKitchenViewModel>();
            this.CreateMap<Kitchen, KitchenInListViewModel>()
                             .ForMember(x => x.ImageUrl, 
                             opt => opt.MapFrom(x => "/images/kitchens/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension))
                             .ForMember(x => x.AverageVote, opt =>
                             opt.MapFrom(x => x.Votes.Count() == 0 ? 0 : x.Votes.Average(v => v.Value)));
            this.CreateMap<Kitchen, SingleKitchenViewModel>()
                            .ForMember(x => x.ImageUrl,
                            opt => opt.MapFrom(x => "/images/kitchens/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension))
                            .ForMember(x => x.AverageVote, opt =>
                            opt.MapFrom(x => x.Votes.Count() == 0 ? 0 : x.Votes.Average(v => v.Value)))
                            .ForMember(x => x.KitchensColorsColor,
                            opt => opt.MapFrom(x => x.KitchensColors.Select(c => c.Color)));
            this.CreateMap<Kitchen, EditKitchenInputModel>()
                            .ForMember(x => x.ColorsId, opt =>
                             opt.MapFrom(x => x.KitchensColors.Select(c=>c.ColorId))); 
            this.CreateMap<Kitchen, HomeKitchensViewModel>()
                            .ForMember(x => x.ImageUrl,
                            opt => opt.MapFrom(x => "/images/kitchens/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension))
                            .ForMember(x => x.AverageVote, opt =>
                            opt.MapFrom(x => x.Votes.Count() == 0 ? 0 : x.Votes.Average(v => v.Value)));

        }
    }
}

