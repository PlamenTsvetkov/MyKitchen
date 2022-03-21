namespace MyKitchen.Test.Mocks
{
    using AutoMapper;
    using MyKitchen.Infrastructure;

    public static class AutoMapperMock
    {
        public static IMapper Instance
        {
            get
            {
                var mapperConfiguration = new MapperConfiguration(config =>
                {
                    config.AddProfile<MappingProfile>();
                });
                return new Mapper(mapperConfiguration);

            }
        }
    }
}
