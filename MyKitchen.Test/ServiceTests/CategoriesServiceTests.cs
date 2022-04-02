namespace MyKitchen.Test.ServiceTests
{
    using Microsoft.EntityFrameworkCore;
    using MyKitchen.Data;
    using MyKitchen.Data.Models;
    using MyKitchen.Models.Categories;
    using MyKitchen.Services.Categories;
    using MyKitchen.Test.Mocks;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class CategoriesServiceTests
    {
        [Fact]
        public void WhenCreateCategoryAndCheckIfThereIsAnCategoryShouldWorkAndReturnTheCorrectResult()
        {
            //Arrange
            var db = DatabaseMock.Instance;

            var service = new CategoriesService(db, null);


            service.Create
            (
              "L-Shaped kitchen",
              "https://www.cliqstudios.com/media/cms-pages/l-shape-cliqstudios-kitchen-shape1.jpg",
              "The L-shaped kitchen is the most popular design, and is appropriate for any size kitchen. It includes work spaces on two adjoining walls running perpendicular to each other. This layout works well for two cooks working at the same time, since no traffic lanes flow through the work area. If space allows, it is possible to incorporate a center island that doubles as a work space or eating area. The L-Shape kitchen typically opens into another room which makes a great layout for entertaining"
            );


            //Act
            var resultTrue = service.CategoryExists(1);
            var resultFalse = service.CategoryExists(2);

            //Assert
            Assert.True(resultTrue);
            Assert.False(resultFalse);
        }

        [Fact]
        public void GetAllCategoriesShouldReturnAllCategories()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;

            var service = new CategoriesService(db, mapper);

            db.Categories.AddRange(Enumerable.Range(0, 3).Select(i => new Category
            {
                Description = i.ToString() + "Description {i} Description {i}",
                ImageUrl = i.ToString(),
                Name = i.ToString(),
            }));

            db.SaveChanges();

            //Act
            var result = service.GetAll<CategoryViewModel>();
            var result2 = service.GetAll<CategoryViewModel>(2);

            //Assert
            Assert.Equal(3, result.Count());
            Assert.Equal(2, result2.Count());
        }

        [Fact]
        public void GetByNameShouldReturnCorrectedName()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;

            var service = new CategoriesService(db, mapper);

            db.Categories.AddRange(Enumerable.Range(0, 3).Select(i => new Category
            {
                Description = i.ToString() + "Description {i} Description {i}",
                ImageUrl = i.ToString(),
                Name = "Ala Bala "+ i.ToString(),
            }));

            db.SaveChanges();

            //Act
            var result = service.GetByName<CategoryViewModel>("Ala-Bala-1");

            //Assert
            Assert.Equal("Ala Bala 1", result.Name);
        }
        [Fact]
        public void WhenCreateCategoryAndCheckGetCountShouldWorkAndReturnTheCorrectResult()
        {
            //Arrange
            var db = DatabaseMock.Instance;

            var service = new CategoriesService(db, null);

            var name = "L-Shaped kitchen";
            var url = "https://www.cliqstudios.com/media/cms-pages/l-shape-cliqstudios-kitchen-shape1.jpg";
            var description = "The L-shaped kitchen is the most popular design, and is appropriate for any size kitchen. It includes work spaces on two adjoining walls running perpendicular to each other. This layout works well for two cooks working at the same time, since no traffic lanes flow through the work area. If space allows, it is possible to incorporate a center island that doubles as a work space or eating area. The L-Shape kitchen typically opens into another room which makes a great layout for entertaining";

            service.Create
            (
              name,
              description,
              url
            );

            //Act
            var countResult = service.GetCount();

            //Assert
            Assert.Equal(1, countResult);
        }

        [Fact]
        public async void WhenCreateCategoryAndUpdateShouldWork()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;

            var service = new CategoriesService(db, mapper);


            var name = "L-Shaped kitchen";
            
            var url = "https://www.cliqstudios.com/media/cms-pages/l-shape-cliqstudios-kitchen-shape1.jpg";
            var description = "The L-shaped kitchen is the most popular design, and is appropriate for any size kitchen. It includes work spaces on two adjoining walls running perpendicular to each other. This layout works well for two cooks working at the same time, since no traffic lanes flow through the work area. If space allows, it is possible to incorporate a center island that doubles as a work space or eating area. The L-Shape kitchen typically opens into another room which makes a great layout for entertaining";

            service.Create
            (
              name,
              description,
              url
            );

            var nameUpdate = "UpdateName";
            var descriptionUpdate = "Update";
            await service.UpdateAsync(1,nameUpdate,descriptionUpdate,url);

            //Act
            var result = service.GetByName<CategoryViewModel>(nameUpdate);

            //Assert
            Assert.Equal(descriptionUpdate, result.Description);
        }

        [Fact]
        public async void WhenCreateCategoryAndDeleteShouldWork()
        {
            //Arrange
            var db = DatabaseMock.Instance;

            var service = new CategoriesService(db, null);


            var name = "L-Shaped kitchen";

            var url = "https://www.cliqstudios.com/media/cms-pages/l-shape-cliqstudios-kitchen-shape1.jpg";
            var description = "The L-shaped kitchen is the most popular design, and is appropriate for any size kitchen. It includes work spaces on two adjoining walls running perpendicular to each other. This layout works well for two cooks working at the same time, since no traffic lanes flow through the work area. If space allows, it is possible to incorporate a center island that doubles as a work space or eating area. The L-Shape kitchen typically opens into another room which makes a great layout for entertaining";

            service.Create
            (
              name,
              description,
              url
            );

            //Act
            var resultBeforeDelete = service.GetCount();
            await service.DeleteAsync(1);
            var resultAfterDelete = service.GetCount();

            //Assert
            Assert.Equal(1, resultBeforeDelete);
            Assert.Equal(0, resultAfterDelete);
        }

        [Fact]
        public void WhenCreateCategoryAndCheckGetByIdShouldWorkAndReturnTheCorrectResult()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;

            var service = new CategoriesService(db, mapper);

            var name = "L-Shaped kitchen";
            var url = "https://www.cliqstudios.com/media/cms-pages/l-shape-cliqstudios-kitchen-shape1.jpg";
            var description = "The L-shaped kitchen is the most popular design, and is appropriate for any size kitchen. It includes work spaces on two adjoining walls running perpendicular to each other. This layout works well for two cooks working at the same time, since no traffic lanes flow through the work area. If space allows, it is possible to incorporate a center island that doubles as a work space or eating area. The L-Shape kitchen typically opens into another room which makes a great layout for entertaining";

            service.Create
            (
              name,
              description,
              url
            );

            //Act
            var result = service.GetById<CategoryViewModel>(1);

            //Assert
            Assert.Equal(name, result.Name);
            Assert.Equal(url, result.ImageUrl);
            Assert.Equal(description, result.Description);
        }

        [Fact]
        public void GetAllByPagingShouldReturnCorrectedCategoryAndCount()
        {
            //Arrange
            var db = DatabaseMock.Instance;
            var mapper = AutoMapperMock.Instance;

            var service = new CategoriesService(db, mapper);

            db.Categories.AddRange(Enumerable.Range(0, 5).Select(i => new Category
            {
                Description = i.ToString() + "Description {i} Description {i}",
                ImageUrl = i.ToString(),
                Name = "Ala Bala " + i.ToString(),
            }));

            db.SaveChanges();

            //Act
            var result = service.GetAllWithPaging<CategoryViewModel>(1,2);
            var result2 = service.GetAllWithPaging<CategoryViewModel>(2,2);
            var result3 = service.GetAllWithPaging<CategoryViewModel>(3,2);

            //Assert
            Assert.Equal(2, result.Count());
            Assert.Equal(2, result2.Count());
            Assert.Single(result3);
        }
    }
}
