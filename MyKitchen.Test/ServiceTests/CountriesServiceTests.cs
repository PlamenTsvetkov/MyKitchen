namespace MyKitchen.Test.ServiceTests
{
    using Microsoft.EntityFrameworkCore;
    using MyKitchen.Data;
    using MyKitchen.Models.Countries;
    using MyKitchen.Services.Countries;
    using MyKitchen.Test.Mocks;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class CountriesServiceTests
    {
        [Fact]
        public void CreateCountryShouldCreateCountry()
        {
            //Arrange
            var db = DatabaseMock.Instance;

            var service = new CountriesService(db,null);

            service.Create("Bulgaria");
            service.Create("Rusia");
            service.Create("Ukraine");
            service.Create("Bulgaria");

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

            service.Create("Bulgaria");
            service.Create("Rusia");
            service.Create("Ukraine");
            service.Create("Bulgaria");

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

            service.Create("Bulgaria");
            service.Create("Rusia");
            service.Create("Ukraine");
            service.Create("Bulgaria");

            db.SaveChanges();

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

            service.Create("Bulgaria");
            service.Create("Rusia");
            service.Create("Ukraine");
            service.Create("Bulgaria");

            db.SaveChanges();

            //Act
            var result = service.GetById<AllCountryModel>(3).Name;

            //Assert
            Assert.Equal("Ukraine", result);
        }
    }
}
