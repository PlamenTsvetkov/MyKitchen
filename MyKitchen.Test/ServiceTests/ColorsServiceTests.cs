namespace MyKitchen.Test.ServiceTests
{
    using Microsoft.EntityFrameworkCore;
    using MyKitchen.Data;
    using MyKitchen.Services.Colors;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class ColorsServiceTests
    {
        [Fact]
        public void CreateColorShouldCreateColor()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MyKitchenDbContext>()
                    .UseInMemoryDatabase(databaseName: "CreateColor_Database")
                    .Options;
            var db = new MyKitchenDbContext(options);

            var service = new ColorsService(db, null);

            service.Create("White");
            service.Create("Black");
            service.Create("White");

            //Act
            var colorsCount = db.Colors.ToArray().Count();

            //Assert
            Assert.Equal(2, colorsCount);
        }
    }
}
