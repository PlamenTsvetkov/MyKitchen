namespace MyKitchen.Test.ServiceTests
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Moq;
    using Xunit;

    using MyKitchen.Test.Mocks;
    using Microsoft.AspNetCore.Http;
    using MyKitchen.Models.Kitchens;
    using MyKitchen.Services.Kitchens;
    using MyKitchen.Services.Addresses;
    using MyKitchen.Services.Manufacturers;
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
            var service = new KitchenService(db, mapper);

           AddOneKitchen(service, file);

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

            AddOneKitchen(service, file);

            service.ChangeVisility(1);

            //Act
            var kitchensCount = service.GetCount();

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

            AddTwoKitchen(service, file);

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


            AddTwoKitchen(service, file);

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

            AddTwoKitchen(service, file);

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


            AddTwoKitchen(service, file);

            service.ChangeVisility(1);

            //Act
            var kitchensCount = service.GetCountByUserId("1");

            //Assert
            Assert.Equal(1, kitchensCount);
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

            AddOneKitchen(service, file);

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

            AddOneKitchen(service, file);

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
        public void AddKitchenToUserCollectionShouldWork()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;
            var fileMock = new Mock<IFormFile>();
            var file = fileMock.Object;
            var fileName = "test.png";
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            var service = new KitchenService(db, mapper);

            AddOneKitchen(service, file);

            //Act
            service.AddKitchenToUserCollection(1, "1");

            var result = db.KitchensUsers.Where(ku => ku.KitchenId == 1).ToList().Count;
            var resultCount = service.GetCollectionCountByUserId("1");


            //Assert
            Assert.Equal(1, result);
            Assert.Equal(1, resultCount);
        }

        [Fact]
        public void GetAllToUserCollectionShouldWork()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;
            var fileMock = new Mock<IFormFile>();
            var file = fileMock.Object;
            var fileName = "test.png";
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            var service = new KitchenService(db, mapper);

            AddTwoKitchen(service, file);

            //Act
            service.AddKitchenToUserCollection(1, "1");
            service.AddKitchenToUserCollection(2, "1");

            var result = service.GetAllInCollectionByUserId<TestKitchenViewModel>("1", 1, 3);


            //Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void RemoveKitchenToUserCollectionShouldWork()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;
            var fileMock = new Mock<IFormFile>();
            var file = fileMock.Object;
            var fileName = "test.png";
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            var service = new KitchenService(db, mapper);

            AddOneKitchen(service, file);

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
        public void IsByUserShouldWork()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;
            var fileMock = new Mock<IFormFile>();
            var file = fileMock.Object;
            var fileName = "test.png";
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            var service = new KitchenService(db, mapper);

            AddOneKitchen(service, file);

            //Act
            var isByUserTrue = service.IsByUser(1, "1");
            var isByUserFalse = service.IsByUser(1, "2");

            //Assert
            Assert.True(isByUserTrue);
            Assert.False(isByUserFalse);
        }

        [Fact]
        public void GetLastKitchenIdByUserIdShouldWork()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;
            var fileMock = new Mock<IFormFile>();
            var file = fileMock.Object;
            var fileName = "test.png";
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            var service = new KitchenService(db, mapper);

            AddOneKitchen(service, file);

            //Act
            var lastKitchenIdByUser = service.GetLastKitchenIdByUserId("1");

            //Assert
            Assert.Equal(1, lastKitchenIdByUser);
        }

        [Fact]
        public void GetАllKitchenByManufacturerNameShouldWork()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;
            var fileMock = new Mock<IFormFile>();
            var file = fileMock.Object;
            var fileName = "test.png";
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            var service = new KitchenService(db, mapper);
            var serviceAddresses = new AddressesService(db);

            var serviceManufacturer = new ManufacturersService(db, null, serviceAddresses);

            serviceManufacturer.Create("Plamen", "mail", "Site", "phoneNumber", "userId", 1, 1, "Niderle", "number 5");

            AddTwoKitchen(service, file);


            service.ChangeVisility(1);


            //Act
            var publicKitchenCount = serviceManufacturer.GetPublicKitchenCountByName("Plamen");
            var nonPublicKitchenCount = serviceManufacturer.GetNotPublicKitchenCountByName("Plamen");

            //Assert
            Assert.Equal(1, publicKitchenCount);
            Assert.Equal(1, nonPublicKitchenCount);
        }

        [Fact]
        public void GetАllAdminKitchenShouldWork()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;
            var fileMock = new Mock<IFormFile>();
            var file = fileMock.Object;
            var fileName = "test.png";
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            var service = new KitchenService(db, mapper);

            AddTwoKitchen(service, file);

            service.ChangeVisility(1);
            service.ChangeVisility(2);


            //Act
            var kitchens = service.GetAllA<TestKitchenViewModel>(1, 2);

            //Assert
            Assert.Equal(2, kitchens.Count());
        }

        [Fact]
        public void GetАllKitchenShouldWork()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;
            var fileMock = new Mock<IFormFile>();
            var file = fileMock.Object;
            var fileName = "test.png";
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            var service = new KitchenService(db, mapper);

            AddTwoKitchen(service, file);

            service.ChangeVisility(1);


            //Act
            var kitchens = service.GetAll<TestKitchenViewModel>(1, 2);

            //Assert
            Assert.Single(kitchens);
        }

        [Fact]
        public void GetByIdShouldWork()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;
            var fileMock = new Mock<IFormFile>();
            var file = fileMock.Object;
            var fileName = "test.png";
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            var service = new KitchenService(db, mapper);

            AddTwoKitchen(service, file);

            service.ChangeVisility(1);


            //Act
            var kitcheResults = service.GetById<TestKitchenViewModel>(2);

            //Assert
            Assert.Equal("2", kitcheResults.Description);
        }

        [Fact]
        public void GetAllByManufacturerIdShouldWork()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;
            var fileMock = new Mock<IFormFile>();
            var file = fileMock.Object;
            var fileName = "test.png";
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            var service = new KitchenService(db, mapper);

            AddTwoKitchen(service, file);

            service.ChangeVisility(1);


            //Act
            var kitchens1Results = service.GetAllByManufacturerId<TestKitchenViewModel>(1, 1, 1);
            var kitchens2Results = service.GetAllByManufacturerId<TestKitchenViewModel>(2, 1, 1);

            //Assert
            Assert.Single(kitchens1Results);
            Assert.Empty(kitchens2Results);
        }

        [Fact]
        public void GetAllByCategoryIdShouldWork()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;
            var fileMock = new Mock<IFormFile>();
            var file = fileMock.Object;
            var fileName = "test.png";
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            var service = new KitchenService(db, mapper);

            AddTwoKitchen(service, file);

            service.ChangeVisility(1);


            //Act
            var kitchens1Results = service.GetAllByCategoryId<TestKitchenViewModel>(1, 1, 1);
            var kitchens2Results = service.GetAllByCategoryId<TestKitchenViewModel>(2, 1, 1);

            //Assert
            Assert.Single(kitchens1Results);
            Assert.Empty(kitchens2Results);
        }

        [Fact]
        public void GetAllByUserIdShouldWork()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;
            var fileMock = new Mock<IFormFile>();
            var file = fileMock.Object;
            var fileName = "test.png";
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            var service = new KitchenService(db, mapper);

            AddTwoKitchen(service, file);

            service.ChangeVisility(1);


            //Act
            var kitchens1Results = service.GetAllByUserId<TestKitchenViewModel>("1", 1, 1);
            var kitchens2Results = service.GetAllByUserId<TestKitchenViewModel>("2", 1, 1);

            //Assert
            Assert.Single(kitchens1Results);
            Assert.Single(kitchens2Results);
        }

        [Fact]
        public void GetRandomShouldWork()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;
            var fileMock = new Mock<IFormFile>();
            var file = fileMock.Object;
            var fileName = "test.png";
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            var service = new KitchenService(db, mapper);

            AddTwoKitchen(service, file);

            service.ChangeVisility(1);
            service.ChangeVisility(2);


            //Act
            var kitchens1Results = service.GetRandom<TestKitchenViewModel>(1);
            var kitchens2Results = service.GetRandom<TestKitchenViewModel>(2);

            //Assert
            Assert.Single(kitchens1Results);
            Assert.Equal(2, kitchens2Results.Count());
        }

        [Fact]
        public void GetAllByManufacturerNameShouldWork()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;
            var fileMock = new Mock<IFormFile>();
            var file = fileMock.Object;
            var fileName = "test.png";
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            var service = new KitchenService(db, mapper);

            var serviceAddresses = new AddressesService(db);

            var serviceManufacturer = new ManufacturersService(db, mapper, serviceAddresses);

            serviceManufacturer.Create("Plamen", "mail", "Site", "phoneNumber", "userId", 1, 1, "Niderle", "number 5");

            AddTwoKitchen(service, file);

            service.ChangeVisility(1);
            service.ChangeVisility(2);


            //Act
            var kitchens1Results = service.GetAllManufacturerName<TestKitchenViewModel>(1, "Plamen", 5);

            //Assert
            Assert.Equal(2, kitchens1Results.Count());
        }

        private async void AddTwoKitchen(IKitchenService service, IFormFile file)
        {
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
                CategoryId = 2,
                Description = "2",
                KitchenMeter = 4,
                Price = 12000,
                МanufacturerId = 1,
                ColorsId = new List<int> { 1, 2 },
                Images = new List<IFormFile> { file }
            };

            await service.AddAsync(kitchen, "1", "Kitchen");
            await service.AddAsync(kitchen2, "2", "Kitchen2");
        }

        private async void AddOneKitchen(IKitchenService service, IFormFile file)
        {
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
        }

    }
}
