namespace MyKitchen.Infrastructure
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
    using MyKitchen.Models.Colors;
    using MyKitchen.Models.Comments;
    using MyKitchen.Models.Manufacturers;
    using MyKitchen.Services.Categories.Models;
    using MyKitchen.Models.Users;
    using MyKitchen.Models.Roles;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Category, IndexCategoryViewModel>()
                 .ForMember(x => x.KitchensCount, opt =>
                    opt.MapFrom(x => x.Kitchens.Count(k=>k.IsDeleted==false && k.IsPublic)));
            this.CreateMap<Category, CategoryViewModel>();
            this.CreateMap<ApplicationUser, UserInListViewModel>();
            this.CreateMap<ApplicationUser, EditUserViewModel>();
            this.CreateMap<ApplicationUser, ApplicationUser>();
            this.CreateMap<Category, KitchenCategoriesServiceModel>();
            this.CreateMap<Kitchen, KitchensInCategoryViewModel>()
                 .ForMember(x => x.ImageUrl,
               opt => opt.MapFrom(x => "/images/kitchens/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
            this.CreateMap<Manufacturer, KitchenManufacturerServiceModel>();
            this.CreateMap<Manufacturer, EditManufacturerInputModel>();
            this.CreateMap<Address, ManufacturerAddressFormModel>();
            this.CreateMap<Color, KitchenColorServiceModel>();
            this.CreateMap<Country, AllCountryModel>();
            this.CreateMap<City, AllCityModel>();
            this.CreateMap<Color, ColorsViewModel>();
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
            this.CreateMap<Comment, PostCommentViewModel>();
            this.CreateMap<Manufacturer, ManufacturerInListViewModel>()
                .ForMember(x => x.KitchensCount, opt =>
                    opt.MapFrom(x => x.Kitchens.Count(k => k.IsDeleted == false && k.IsPublic)));
            this.CreateMap<Kitchen, HomeKitchensViewModel>()
             .ForMember(x => x.ImageUrl,
             opt => opt.MapFrom(x => "/images/kitchens/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension))
             .ForMember(x => x.AverageVote, opt =>
                  opt.MapFrom(x => x.Votes.Count() == 0 ? 0 : x.Votes.Average(v => v.Value)));


        }
    }
}

