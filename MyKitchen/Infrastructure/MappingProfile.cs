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

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Category, IndexCategoryViewModel>();
            this.CreateMap<Category, KitchenCategoryFormModel>();
            this.CreateMap<Category, CategoryViewModel>();
            this.CreateMap<Category, KitchenCategoriesServiceModel>();
            this.CreateMap<Kitchen, KitchensInCategoryViewModel>()
                 .ForMember(x => x.ImageUrl,
               opt => opt.MapFrom(x => "/images/kitchens/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
            this.CreateMap<Manufacturer, KitchenManufacturerServiceModel>();
            this.CreateMap<Color, KitchenColorServiceModel>();
            this.CreateMap<Country, AllCountryModel>();
            this.CreateMap<City, AllCityModel>();
            this.CreateMap<Color, ColorsViewModel>();
            this.CreateMap<Kitchen, KitchenInListViewModel>()
               .ForMember(x => x.ImageUrl, 
               opt => opt.MapFrom(x => "/images/kitchens/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
            this.CreateMap<Kitchen, SingleKitchenViewModel>()
              .ForMember(x => x.ImageUrl,
              opt => opt.MapFrom(x => "/images/kitchens/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension))
              .ForMember(x => x.AverageVote, opt =>
                    opt.MapFrom(x => x.Votes.Count() == 0 ? 0 : x.Votes.Average(v => v.Value)))
               .ForMember(x => x.KitchensColorsColor,
              opt => opt.MapFrom(x => x.KitchensColors.Select(c => c.Color)));
            this.CreateMap<Kitchen, EditKitchenInputModel>();
            this.CreateMap<Comment, PostCommentViewModel>();
            this.CreateMap<Manufacturer, ManufacturerInListViewModel>();


        }
    }
}

