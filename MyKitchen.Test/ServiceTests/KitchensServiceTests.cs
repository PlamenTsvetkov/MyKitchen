namespace MyKitchen.Test.ServiceTests
{
    using Microsoft.AspNetCore.Http;
    using Moq;
    using MyKitchen.Models.Kitchens;
    using MyKitchen.Services.Kitchens;
    using MyKitchen.Test.Mocks;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    using static Data.Kitchens;
    public class KitchensServiceTests
    {
        [Fact]
        public async Task CreateKitchenShouldCreateKitchen()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;
            var fileMock = new Mock<IFormFile>();
            var file = fileMock.Object;
            var fileName = "test.png";
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            var service = new KitchenService(db,mapper);

            var kitchen = new KitchenFormModel
            {
                CategoryId = 1,
                Description="1",
                KitchenMeter=3,
                Price=15000,
                МanufacturerId=1,
                ColorsId= new List<int> { 1,2},
                Images=new List<IFormFile> { file }
            };

            await service.AddAsync(kitchen, "1", "Lqlqlq");

            //Act
            var kitchensCount = db.Kitchens.Count();

            //Assert
            Assert.Equal(1, kitchensCount);
        }

        [Fact]
        public async Task GetCountShouldReturnCurrectCount()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;
            var fileMock = new Mock<IFormFile>();
            var file = fileMock.Object;
            var fileName = "test.png";
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            var service = new KitchenService(db, mapper);

            var kitchen = new KitchenFormModel
            {
                CategoryId = 1,
                Description = "1",
                KitchenMeter = 3,
                Price = 15000,
                МanufacturerId = 1,
                ColorsId = new List<int> { 1, 2 },
                Images = new List<IFormFile> { file }
            };

            await service.AddAsync(kitchen, "1", "Lqlqlq");
            var kitchen1 = service.GetById<KitchenFormModel>(1);
            kitchen1.IsPublic = true;
            db.SaveChanges();

            //Act
            var kitchensCount = service.GetCount();

            //Assert
            Assert.Equal(1, kitchensCount);
        }
    }
}
