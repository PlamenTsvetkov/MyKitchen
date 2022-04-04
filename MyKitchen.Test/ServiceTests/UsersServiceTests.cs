namespace MyKitchen.Test.ServiceTests
{
    using Moq;
    using MyKitchen.Data.Models;
    using System.Linq;
    using Xunit;
    using MyKitchen.Models.Users;
    using MyKitchen.Services.Users;
    using MyKitchen.Test.Mocks;

    public class UsersServiceTests
    {
        [Fact]
        public void GetCountShouldReturnCorrectAnswer()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;

            var service = new UsersService(db, mapper);

            var mockUser = new Mock<ApplicationUser>();

            var usersService = new UsersService(db, mapper);

            var user = new ApplicationUser
            {
                UserName="Plamen",
                Email="plamen@abv.bg",
                Name="Plamen",
            };
            db.Users.Add(user);
            db.SaveChanges();

            //Act
            var usersCount = service.GetCount();

            //Assert
            Assert.Equal(1, usersCount);
        }

        [Fact]
        public void GetAllShouldWork()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;

            var service = new UsersService(db, mapper);

            var mockUser = new Mock<ApplicationUser>();

            var usersService = new UsersService(db, mapper);

            var user = new ApplicationUser
            {
                UserName = "Plamen",
                Email = "plamen@abv.bg",
                Name = "Plamen",
            };
            var user2 = new ApplicationUser
            {
                UserName = "Mila",
                Email = "mila@abv.bg",
                Name = "Mila",
            };
            db.Users.Add(user);
            db.Users.Add(user2);
            db.SaveChanges();

            //Act
            var users1 = service.GetAllWithPaging<EditUserViewModel>(1,1);
            var users2 = service.GetAllWithPaging<EditUserViewModel>(1,2);

            //Assert
            Assert.Single(users1);
            Assert.Equal(2, users2.Count());
        }

        [Fact]
        public void GetUserByIdShouldWork()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;

            var service = new UsersService(db, mapper);

            var mockUser = new Mock<ApplicationUser>();

            var usersService = new UsersService(db, mapper);

            var user = new ApplicationUser
            {
                Id="1",
                UserName = "Plamen",
                Email = "plamen@abv.bg",
                Name = "Plamen",
            };
            var user2 = new ApplicationUser
            {
                Id= "2",
                UserName = "Mila",
                Email = "mila@abv.bg",
                Name = "Mila",
            };
            db.Users.Add(user);
            db.Users.Add(user2);
            db.SaveChanges();

            //Act
            var users1 = service.GetUserById("1");
            var users2 = service.GetUserById("2");

            //Assert
            Assert.Equal("Plamen", users1.Name);
            Assert.Equal("mila@abv.bg", users2.Email);
        }

        [Fact]
        public void UpdateShouldUpdateUser()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;

            var service = new UsersService(db, mapper);

            var usersService = new UsersService(db, mapper);

            var user = new ApplicationUser
            {
                Id= "1",
                UserName = "Plamen",
                Email = "plamen@abv.bg",
                Name = "Plamen",
            };
            db.Users.Add(user);
            db.SaveChanges();

            //Act
            var users1 = service.GetUserById("1");
            users1.Name = "Mitko";
            service.UpdateUser(users1);
            var resultUser = service.GetUserById("1");

            //Assert
            Assert.Equal("Mitko", resultUser.Name);
        }
    }
}
