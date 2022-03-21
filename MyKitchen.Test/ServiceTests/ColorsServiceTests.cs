namespace MyKitchen.Test.ServiceTests
{
    using Microsoft.EntityFrameworkCore;
    using MyKitchen.Data;
    using MyKitchen.Services.Colors;
    using MyKitchen.Services.Colors.Models;
    using MyKitchen.Test.Mocks;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class ColorsServiceTests
    {
        [Fact]
        public void CreateColorShouldCreateColor()
        {
            //Arrange
            var db = DatabaseMock.Instance;

            var service = new ColorsService(db, null);

            service.Create("White");
            service.Create("Black");
            service.Create("White");

            //Act
            var colorsCount = db.Colors.ToArray().Count();

            //Assert
            Assert.Equal(2, colorsCount);
        }

        [Fact]
        public void GetAllColorsShouldReturnAllColors()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;

            var service = new ColorsService(db, mapper);

            service.Create("White");
            service.Create("Black");
            service.Create("White");

            db.SaveChanges();

            //Act
            var result = service.GetAll<KitchenColorServiceModel>().Count();

            //Assert
            Assert.Equal(2, result);
        }
    }
}
