namespace MyKitchen.Test.ServiceTests
{
    using Microsoft.EntityFrameworkCore;
    using MyKitchen.Data;
    using MyKitchen.Services.Addresses;
    using MyKitchen.Test.Mocks;
    using System.Linq;
    using Xunit;

    public class AddressesServiceTests
    {
        [Fact]
        public void CreateAddressShouldCreateAddress()
        {
            //Arrange
            var db = DatabaseMock.Instance;

            var service = new AddressesService(db, null);
          
            service.Create("Niderle", "1", 1, "4b38713f-6a4c-49fc-8641-8c787c9d2821");
            service.Create("Osmi Mart", "15", 1, "4b38713f-6a4c-49fc-8641-8c787c9d2821");

            //Act
            var addressCount = db.Addresses.ToArray().Count();

            //Assert
            Assert.Equal(2, addressCount);
        }

        [Fact]
        public void WhenCreateAddressWithTheSameNameDoNotCreateIt()
        {
            //Arrange
            var db =  DatabaseMock.Instance;

            var service = new AddressesService(db, null);

           
            service.Create("Niderle", "1", 1, "4b38713f-6a4c-49fc-8641-8c787c9d2821");
            service.Create("Osmi Mart", "15", 1, "4b38713f-6a4c-49fc-8641-8c787c9d2821");
            service.Create("Osmi Mart", "15", 1, "4b38713f-6a4c-49fc-8641-8c787c9d2821");
            service.Create("Osmi Mart", "15", 1, "4b38713f-6a4c-49fc-8641-8c787c9d2821");

            //Act
            var addressCount = db.Addresses.ToArray().Count();

            //Assert
            Assert.Equal(2, addressCount);
        }
    }
}
