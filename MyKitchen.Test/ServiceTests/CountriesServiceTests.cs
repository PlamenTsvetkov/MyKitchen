namespace MyKitchen.Test.ServiceTests
{
    using Microsoft.EntityFrameworkCore;
    using MyKitchen.Data;
    using MyKitchen.Services.Countries;
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
            var options = new DbContextOptionsBuilder<MyKitchenDbContext>()
                    .UseInMemoryDatabase(databaseName: "Country_Database")
                    .Options;
            var db = new MyKitchenDbContext(options);

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
            var options = new DbContextOptionsBuilder<MyKitchenDbContext>()
                    .UseInMemoryDatabase(databaseName: "Country2_Database")
                    .Options;
            var db = new MyKitchenDbContext(options);

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
    }
}
