namespace MyKitchen.Test.ServiceTests
{
    using System.Linq;
    using Xunit;

    using MyKitchen.Models.Cityes;
    using MyKitchen.Services.Cities;
    using MyKitchen.Test.Mocks;

    public class CitiesServiceTests
    {
        [Fact]
        public void CreatCityShouldCreateCity()
        {
            //Arrange
            var db = DatabaseMock.Instance;

            var service = new CitiesService(db, null);

            service.Create("Varna", 1);
            service.Create("Sofia", 1);
            service.Create("Sofia", 1);

            //Act
            var citiesCount = db.Cities.ToArray().Count();

            //Assert
            Assert.Equal(2, citiesCount);
        }

        [Fact]
        public void GetAllCitiesShouldReturnAllCities()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;

            var service = new CitiesService(db, mapper);

            service.Create("Varna", 1);
            service.Create("Sofia", 1);
            service.Create("Sofia", 1);

            db.SaveChanges();

            //Act
            var result = service.GetAll<AllCityModel>();

            //Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetByCountryIdCitiesShouldReturnAllCitiesByCountry()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;

            var service = new CitiesService(db, mapper);

            service.Create("Varna", 1);
            service.Create("Sofia", 1);
            service.Create("Sofia", 1);
            service.Create("Plovdiv", 1);
            service.Create("Veliko Turnovo", 1);

            db.SaveChanges();

            //Act
            var result = service.GetByCountryId<AllCityModel>(1);

            //Assert
            Assert.Equal(4, result.Count());
        }
    }
}
