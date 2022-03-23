namespace MyKitchen.Test.ServiceTests
{
    using Microsoft.AspNetCore.Http;
    using Moq;
    using MyKitchen.Data.Models;
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

            await service.AddAsync(kitchen, "1", "Kitchen");

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

            await service.AddAsync(kitchen, "1", "Kitchen");

            service.ChangeVisility(1);

            //Act
            var kitchensCount = service.GetCount();

            //Assert
            Assert.Equal(1, kitchensCount);
        }

        [Fact]
        public async Task GetAllKitchenShouldReturnAllPublicKitchen()
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
            var kitchen2 = new KitchenFormModel
            {
                CategoryId = 1,
                Description = "1",
                KitchenMeter = 3,
                Price = 15000,
                МanufacturerId = 1,
                ColorsId = new List<int> { 1, 2 },
                Images = new List<IFormFile> { file }
            };

            await service.AddAsync(kitchen, "1", "Kitchen");
            await service.AddAsync(kitchen2, "1", "Kitchen2");
            var kitchen1 = db.Kitchens.Where(k => k.Id == 1).FirstOrDefault();
            kitchen1.IsPublic = true;
            db.SaveChanges();

            //Act
            var kitchensCount = service.GetAll<KitchenInListViewModel>(1).Count();

            //Assert
            Assert.Equal(1, kitchensCount);
        }

        [Fact]
        public async Task GetCountAdminShouldReturnAllKitchensCount()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;
            var service = new KitchenService(db, mapper);
            var fileMock = new Mock<IFormFile>();
            var file = fileMock.Object;
            var fileName = "test.png";
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            

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
            var kitchen2 = new KitchenFormModel
            {
                CategoryId = 1,
                Description = "1",
                KitchenMeter = 3,
                Price = 15000,
                МanufacturerId = 1,
                ColorsId = new List<int> { 1, 2 },
                Images = new List<IFormFile> { file }
            };

            await service.AddAsync(kitchen, "1", "Kitchen");
            await service.AddAsync(kitchen2, "1", "Kitchen2");

            service.ChangeVisility(1);

            //Act
            var kitchensCount = service.GetCountAdmin();

            //Assert
            Assert.Equal(2, kitchensCount);
        }

        [Fact]
        public async Task GetCountByCategoryShouldReturnAllKitchensCount()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;
            var service = new KitchenService(db, mapper);
            var fileMock = new Mock<IFormFile>();
            var file = fileMock.Object;
            var fileName = "test.png";
            fileMock.Setup(_ => _.FileName).Returns(fileName);


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
            var kitchen2 = new KitchenFormModel
            {
                CategoryId = 1,
                Description = "1",
                KitchenMeter = 3,
                Price = 15000,
                МanufacturerId = 1,
                ColorsId = new List<int> { 1, 2 },
                Images = new List<IFormFile> { file }
            };

            await service.AddAsync(kitchen, "1", "Kitchen");
            await service.AddAsync(kitchen2, "1", "Kitchen2");

            service.ChangeVisility(1);

            //Act
            var kitchensCount = service.GetCountByCategoryId(1);

            //Assert
            Assert.Equal(1, kitchensCount);
        }

        [Fact]
        public async Task GetCountByManufacturerIdShouldReturnAllPublicKitchensByManufacturer()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;
            var service = new KitchenService(db, mapper);
            var fileMock = new Mock<IFormFile>();
            var file = fileMock.Object;
            var fileName = "test.png";
            fileMock.Setup(_ => _.FileName).Returns(fileName);


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
            var kitchen2 = new KitchenFormModel
            {
                CategoryId = 1,
                Description = "1",
                KitchenMeter = 3,
                Price = 15000,
                МanufacturerId = 1,
                ColorsId = new List<int> { 1, 2 },
                Images = new List<IFormFile> { file }
            };

            await service.AddAsync(kitchen, "1", "Kitchen");
            await service.AddAsync(kitchen2, "1", "Kitchen2");

            service.ChangeVisility(1);

            //Act
            var kitchensCount = service.GetCountByManufacturerId(1);

            //Assert
            Assert.Equal(1, kitchensCount);
        }

        [Fact]
        public async Task GetCountByUserIdShouldReturnAllPublicKitchensByUser()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;
            var service = new KitchenService(db, mapper);
            var fileMock = new Mock<IFormFile>();
            var file = fileMock.Object;
            var fileName = "test.png";
            fileMock.Setup(_ => _.FileName).Returns(fileName);


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
            var kitchen2 = new KitchenFormModel
            {
                CategoryId = 1,
                Description = "1",
                KitchenMeter = 3,
                Price = 15000,
                МanufacturerId = 1,
                ColorsId = new List<int> { 1, 2 },
                Images = new List<IFormFile> { file }
            };

            await service.AddAsync(kitchen, "1", "Kitchen");
            await service.AddAsync(kitchen2, "1", "Kitchen2");

            service.ChangeVisility(1);

            //Act
            var kitchensCount = service.GetCountByUserId("1");

            //Assert
            Assert.Equal(2, kitchensCount);
        }

        [Fact]
        public async Task WhenUpdateKitchensShouldUpdateKitchens()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;
            var service = new KitchenService(db, mapper);
            var fileMock = new Mock<IFormFile>();
            var file = fileMock.Object;
            var fileName = "test.png";
            fileMock.Setup(_ => _.FileName).Returns(fileName);


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
           

            await service.AddAsync(kitchen, "1", "Kitchen");

            var newCategoryId = 2;
            var newDescriptionId = "2";
            var newKitchenMeter = 2;
            var newPrice = 2;
            var newManufacturer = 2;

            var kitchen2 = new EditKitchenInputModel
            {
                CategoryId = newCategoryId,
                Description = newDescriptionId,
                KitchenMeter = newKitchenMeter,
                Price = newPrice,
                МanufacturerId = newManufacturer,
                ColorsId = new List<int> { 1, 2 },
            };
            await service.UpdateAsync(1, kitchen2, false);


            //Act
            var result = db.Kitchens.Where(k => k.Id == 1).FirstOrDefault();

            //Assert
            Assert.Equal(newCategoryId, result.CategoryId);
            Assert.Equal(newDescriptionId, result.Description);
            Assert.Equal(newKitchenMeter, result.KitchenMeter);
            Assert.Equal(newPrice, result.Price);
            Assert.Equal(newManufacturer, result.МanufacturerId);
        }

        [Fact]
        public async Task DeleteKitchenShouldMakeIsDeleteTrue()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;
            var service = new KitchenService(db, mapper);
            var fileMock = new Mock<IFormFile>();
            var file = fileMock.Object;
            var fileName = "test.png";
            fileMock.Setup(_ => _.FileName).Returns(fileName);


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

            await service.AddAsync(kitchen, "1", "Kitchen");

            service.ChangeVisility(1);

            await service.DeleteAsync(1);


            //Act
            var result = db.Kitchens.Where(k => k.Id == 1).FirstOrDefault();
            var kitchensCountByManufacturer = service.GetCountByManufacturerId(1);
            var kitchensCountByCategory = service.GetCountByManufacturerId(1);
            var kitchensCountByUserId = service.GetCountByUserId("1");
            var kitchensCountAdmin = service.GetCountAdmin();
            var kitchensCount = service.GetCount();

            //Assert
            Assert.True(result.IsDeleted);
            Assert.Equal(0, kitchensCountByCategory);
            Assert.Equal(0, kitchensCountByManufacturer);
            Assert.Equal(1, kitchensCountAdmin);
            Assert.Equal(0, kitchensCount);
            Assert.Equal(0, kitchensCountByUserId);
        }

        [Fact]
        public async Task AddKitchenToUserCollectionShouldWork()
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

            await service.AddAsync(kitchen, "1", "Kitchen");

            //Act
            service.AddKitchenToUserCollection(1, "1");

            var result = db.KitchensUsers.Where(ku=>ku.KitchenId==1).ToList().Count;
            var resultCount = service.GetCollectionCountByUserId("1");


            //Assert
            Assert.Equal(1, result);
            Assert.Equal(1, resultCount);
        }

        [Fact]
        public async Task RemoveKitchenToUserCollectionShouldWork()
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

            await service.AddAsync(kitchen, "1", "Kitchen");

            //Act
            service.AddKitchenToUserCollection(1, "1");
            service.RemoveKitchenToUserCollection(1, "1");

            var result = db.KitchensUsers.Where(ku => ku.KitchenId == 1).ToList().Count;
            var resultCount = service.GetCollectionCountByUserId("1");


            //Assert
            Assert.Equal(0, result);
            Assert.Equal(0, resultCount);
        }

        [Fact]
        public async Task IsByUserShouldWork()
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

            await service.AddAsync(kitchen, "1", "Kitchen");

            //Act
            var isByUserTrue = service.IsByUser(1, "1");
            var isByUserFalse = service.IsByUser(1, "2");

            //Assert
            Assert.True(isByUserTrue);
            Assert.False(isByUserFalse);
        }

        [Fact]
        public async Task GetLastKitchenIdByUserIdShouldWork()
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

            await service.AddAsync(kitchen, "1", "Kitchen");

            //Act
            var lastKitchenIdByUser = service.GetLastKitchenIdByUserId("1");

            //Assert
            Assert.Equal(1,lastKitchenIdByUser);
        }

        [Fact]
        public async Task GetRandomShouldWork()
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

            await service.AddAsync(kitchen, "1", "Kitchen");
            await service.AddAsync(kitchen, "2", "Kitchen");
            await service.AddAsync(kitchen, "3", "Kitchen");

            //Act
            var randomKitchensCount = service.GetRandom<HomeKitchensViewModel>(2).Count();

            //Assert
            Assert.Equal(2, randomKitchensCount);
        }
    }
}
