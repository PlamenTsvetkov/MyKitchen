namespace MyKitchen.Test.ServiceTests
{
    using Microsoft.EntityFrameworkCore;
    using MyKitchen.Data;
    using MyKitchen.Services.Cities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class CitiesServiceTests
    {
        [Fact]
        public void CreatCityShouldCreateCity()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MyKitchenDbContext>()
                    .UseInMemoryDatabase(databaseName: "CreateCity_Database")
                    .Options;
            var db = new MyKitchenDbContext(options);

            var service = new CitiesService(db, null,null);

            service.Create("Varna", 1);
            service.Create("Sofia", 1);
            service.Create("Sofia", 1);

            //Act
            var citiesCount = db.Cities.ToArray().Count();

            //Assert
            Assert.Equal(2, citiesCount);
        }
    }
}
