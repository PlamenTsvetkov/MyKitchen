namespace MyKitchen.Test.ServiceTests
{
    using System.Linq;
    using Xunit;

    using MyKitchen.Test.Mocks;
    using MyKitchen.Models.Countries;
    using MyKitchen.Services.Countries;

    public class CountriesServiceTests
    {
        [Fact]
        public void CreateCountryShouldCreateCountry()
        {
            //Arrange
            var db = DatabaseMock.Instance;

            var service = new CountriesService(db,null);

            CreateCountries(service);

            //Act
            var countriesCount = db.Countries.ToArray().Count();

            //Assert
            Assert.Equal(3, countriesCount);
        }

        [Fact]
        public void WhenCheckIfThereIsAnCountryТoReturnTheCorrectResult()
        {
            //Arrange
            var db = DatabaseMock.Instance;

            var service = new CountriesService(db, null);

            CreateCountries(service);

            //Act
            var resultTrue = service.CountryExists(1);
            var resultFalse = service.CountryExists(4);

            //Assert
            Assert.True(resultTrue);
            Assert.False(resultFalse);
        }

        [Fact]
        public void GetAllCountriesShouldReturnAllCountries()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;

            var service = new CountriesService(db, mapper);

            CreateCountries(service);

            //Act
            var result = service.GetAll<AllCountryModel>();

            //Assert
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void GetCountryByIdShouldReturnCurrectCountry()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;

            var service = new CountriesService(db, mapper);

            CreateCountries(service);

            //Act
            var result = service.GetById<AllCountryModel>(3).Name;

            //Assert
            Assert.Equal("Ukraine", result);
        }

        private void CreateCountries(ICountriesService service)
        {
            service.Create("Bulgaria");
            service.Create("Rusia");
            service.Create("Ukraine");
            service.Create("Bulgaria");
        }
    }
}
