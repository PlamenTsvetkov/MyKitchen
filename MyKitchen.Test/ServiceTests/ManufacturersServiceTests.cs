namespace MyKitchen.Test.ServiceTests
{
    using MyKitchen.Models.Manufacturers;
    using MyKitchen.Services.Addresses;
    using MyKitchen.Services.Manufacturers;
    using MyKitchen.Test.Mocks;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class ManufacturersServiceTests
    {
        [Fact]
        public void CreateManufacturerShouldCreateManufacturer()
        {
            //Arrange
            var db = DatabaseMock.Instance;

            var serviceAddresses = new AddressesService(db, null);

            var service = new ManufacturersService(db, null,serviceAddresses);

            service.Create("Plamen","mail","Site", "phoneNumber", "userId",1,1,"Niderle","number 5");
            service.Create("Ivanov EOOD","mail","Site", "phoneNumber", "userId",1,1,"Niderle","number 5");

            //Act
            var manufacturersCount = db.Manufacturers.ToArray().Count();

            //Assert
            Assert.Equal(2, manufacturersCount);
        }

        [Fact]
        public void WhenCreateSeveralManufacturersMustGiveTheCorrectCount()
        {
            //Arrange
            var db = DatabaseMock.Instance;

            var serviceAddresses = new AddressesService(db, null);

            var service = new ManufacturersService(db, null, serviceAddresses);

            service.Create("Plamen", "mail", "Site", "phoneNumber", "userId", 1, 1, "Niderle", "number 5");
            service.Create("Ivanov EOOD", "mail", "Site", "phoneNumber", "userId", 1, 1, "Niderle", "number 5");

            //Act
            var manufacturersCount =service.GetCount();

            //Assert
            Assert.Equal(0, manufacturersCount);
        }

        [Fact]
        public void WhenCreateSeveralPublicManufacturersMustGiveTheCorrectCount()
        {
            //Arrange
            var db = DatabaseMock.Instance;

            var serviceAddresses = new AddressesService(db, null);

            var service = new ManufacturersService(db, null, serviceAddresses);

            service.Create("Plamen", "mail", "Site", "phoneNumber", "userId", 1, 1, "Niderle", "number 5");
            service.Create("Ivanov EOOD", "mail", "Site", "phoneNumber", "userId", 1, 1, "Niderle", "number 5");

            service.ChangeVisility(1);
            service.ChangeVisility(2);

            db.SaveChanges();

            //Act
            var manufacturersCount = service.GetCount();

            //Assert
            Assert.Equal(2, manufacturersCount);
        }

        [Fact]
        public async Task WhenUpdateManufacturersShouldUpdateManufacturer()
        {
            //Arrange
            var db = DatabaseMock.Instance;

            var serviceAddresses = new AddressesService(db, null);

            var service = new ManufacturersService(db, null, serviceAddresses);

            service.Create("Plamen", "mail", "Site", "phoneNumber", "userId", 1, 1, "Niderle", "number 5");
            service.Create("Ivanov EOOD", "mail", "Site", "phoneNumber", "userId", 1, 1, "Niderle", "number 5");


            service.ChangeVisility(1);
            service.ChangeVisility(2);

            string fixAddres = "number 1";
            await service.UpdateAsync(1, "Plamen", "mail", "Site", "phoneNumber", "userId", 1, 1, "Niderle", fixAddres, true);

            db.SaveChanges();

            //Act
            var manufacturerAdressNumber = db.Manufacturers.Where(m=>m.Id==1).Select(m=>m.Address.Number).FirstOrDefault();

            //Assert
            Assert.Equal(fixAddres, manufacturerAdressNumber);
        }

        [Fact]
        public void GetAllShouldGiveTheCorrectAnswer()
        {
            //Arrange
            var db = DatabaseMock.Instance;

            var mapper = AutoMapperMock.Instance;

            var serviceAddresses = new AddressesService(db, mapper);

            var service = new ManufacturersService(db, mapper, serviceAddresses);

            service.Create("Plamen", "mail", "Site", "phoneNumber", "userId", 1, 1, "Niderle", "number 5");
            service.Create("Ivanov EOOD", "mail", "Site", "phoneNumber", "userId", 1, 1, "Niderle", "number 5");

            service.ChangeVisility(1);
            service.ChangeVisility(2);

            //Act
            var allManufacturer = service.GetAllWithPaging<ManufacturerTestModel>(1,2);
            var allManufacturer2 = service.GetAllWithPaging<ManufacturerTestModel>(1,1);

            //Assert
            Assert.Equal(2, allManufacturer.Count());
            Assert.Single(allManufacturer2);
        }

        [Fact]
        public void GetByIdShouldGiveTheCorrectManufacturer()
        {
            //Arrange
            var db = DatabaseMock.Instance;

            var mapper = AutoMapperMock.Instance;

            var serviceAddresses = new AddressesService(db, mapper);

            var service = new ManufacturersService(db, mapper, serviceAddresses);

            service.Create("Plamen", "mail", "Site", "phoneNumber", "userId", 1, 1, "Niderle", "number 5");
            service.Create("Ivanov EOOD", "mail", "Site", "phoneNumber", "userId", 1, 1, "Niderle", "number 5");

            service.ChangeVisility(1);
            service.ChangeVisility(2);

            //Act
            var allManufacturer = service.GetById<ManufacturerTestModel>(1);
            var allManufacturer2 = service.GetById<ManufacturerTestModel>(2);

            //Assert
            Assert.Equal("mail", allManufacturer.Email);
            Assert.Equal("Ivanov EOOD", allManufacturer2.Name);
        }

        [Fact]
        public void IsByUserShouldReturnTheCorrectValue()
        {
            //Arrange
            var db = DatabaseMock.Instance;

            var mapper = AutoMapperMock.Instance;

            var serviceAddresses = new AddressesService(db, mapper);

            var service = new ManufacturersService(db, mapper, serviceAddresses);

            service.Create("Plamen", "mail", "Site", "phoneNumber", "userId", 1, 1, "Niderle", "number 5");
            service.Create("Ivanov EOOD", "mail", "Site", "phoneNumber", "userId", 1, 1, "Niderle", "number 5");

            service.ChangeVisility(1);
            service.ChangeVisility(2);

            //Act
            var isByUser1 = service.IsByUser(1,"userId");
            var isByUser2 = service.IsByUser(1,"anatherUser");

            //Assert
            Assert.True(isByUser1);
            Assert.False(isByUser2);
        }

        [Fact]
        public async void DeleteManufacturerShouldWork()
        {
            //Arrange
            var db = DatabaseMock.Instance;

            var serviceAddresses = new AddressesService(db, null);

            var service = new ManufacturersService(db, null, serviceAddresses);

            service.Create("Plamen", "mail", "Site", "phoneNumber", "userId", 1, 1, "Niderle", "number 5");
            service.Create("Ivanov EOOD", "mail", "Site", "phoneNumber", "userId", 1, 1, "Niderle", "number 5");

            service.ChangeVisility(1);
            service.ChangeVisility(2);

            //Act
            await service.DeleteAsync(1);
            var manufacturersCount = service.GetCount();

            //Assert
            Assert.Equal(1, manufacturersCount);
        }

    }
}
