namespace MyKitchen.Test.ServiceTests
{
    using Microsoft.EntityFrameworkCore;
    using MyKitchen.Data;
    using MyKitchen.Services.Addresses;
    using MyKitchen.Services.Manufacturers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class ManufacturersServiceTests
    {
        [Fact]
        public void CreateManufacturerShouldCreateManufacturer()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MyKitchenDbContext>()
                    .UseInMemoryDatabase(databaseName: "Manufacturer_Database")
                    .Options;
            var db = new MyKitchenDbContext(options);

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
            var options = new DbContextOptionsBuilder<MyKitchenDbContext>()
                    .UseInMemoryDatabase(databaseName: "Manufacturer2_Database")
                    .Options;
            var db = new MyKitchenDbContext(options);

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
            var options = new DbContextOptionsBuilder<MyKitchenDbContext>()
                    .UseInMemoryDatabase(databaseName: "Manufacturer2_Database")
                    .Options;
            var db = new MyKitchenDbContext(options);

            var serviceAddresses = new AddressesService(db, null);

            var service = new ManufacturersService(db, null, serviceAddresses);

            service.Create("Plamen", "mail", "Site", "phoneNumber", "userId", 1, 1, "Niderle", "number 5");
            service.Create("Ivanov EOOD", "mail", "Site", "phoneNumber", "userId", 1, 1, "Niderle", "number 5");

            var manufacturer1 = db.Manufacturers.Where(m=>m.Id==1).FirstOrDefault();
            var manufacturer2 = db.Manufacturers.Where(m=>m.Id==2).FirstOrDefault();

            manufacturer1.IsPublic = true;
            manufacturer2.IsPublic = true;

            db.SaveChanges();

            //Act
            var manufacturersCount = service.GetCount();

            //Assert
            Assert.Equal(2, manufacturersCount);
        }

        [Fact]
        public async Task WhenUpdateManufacturersMustGiveTheCorrectCount()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MyKitchenDbContext>()
                    .UseInMemoryDatabase(databaseName: "Manufacturer2_Database")
                    .Options;
            var db = new MyKitchenDbContext(options);

            var serviceAddresses = new AddressesService(db, null);

            var service = new ManufacturersService(db, null, serviceAddresses);

            service.Create("Plamen", "mail", "Site", "phoneNumber", "userId", 1, 1, "Niderle", "number 5");
            service.Create("Ivanov EOOD", "mail", "Site", "phoneNumber", "userId", 1, 1, "Niderle", "number 5");
           

            var manufacturer1 = db.Manufacturers.Where(m => m.Id == 1).FirstOrDefault();
            var manufacturer2 = db.Manufacturers.Where(m => m.Id == 2).FirstOrDefault();

            manufacturer1.IsPublic = true;
            manufacturer2.IsPublic = true;

            db.SaveChanges();

            string fixAddres = "number 1";
            await service.UpdateAsync(1, "Plamen", "mail", "Site", "phoneNumber", "userId", 1, 1, "Niderle", fixAddres, true);

            db.SaveChanges();

            //Act
            var manufacturerAdressNumber = db.Manufacturers.Where(m=>m.Id==1).Select(m=>m.Address.Number).FirstOrDefault();

            //Assert
            Assert.Equal(fixAddres, manufacturerAdressNumber);
        }
    }
}
