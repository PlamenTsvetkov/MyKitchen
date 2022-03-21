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
        public void WhenCheckIfThereIsAnCategoryТoReturnTheCorrectResult()
        {
            //Arrange
            var db = DatabaseMock.Instance;

            var service = new CategoriesService(db, null);


            var category = new Category
            {
                Name = "L-Shaped kitchen",
                ImageUrl = "https://www.cliqstudios.com/media/cms-pages/l-shape-cliqstudios-kitchen-shape1.jpg",
                Description = "The L-shaped kitchen is the most popular design, and is appropriate for any size kitchen. It includes work spaces on two adjoining walls running perpendicular to each other. This layout works well for two cooks working at the same time, since no traffic lanes flow through the work area. If space allows, it is possible to incorporate a center island that doubles as a work space or eating area. The L-Shape kitchen typically opens into another room which makes a great layout for entertaining"
            };

            db.Categories.Add(category);
            db.SaveChanges();

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
    }
}
