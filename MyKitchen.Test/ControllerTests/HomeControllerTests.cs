namespace MyKitchen.Test.ControllerTests
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using MyKitchen.Controllers;
    using MyKitchen.Data.Models;
    using MyKitchen.Models.Home;
    using MyKitchen.Services.Categories;
    using MyKitchen.Services.Kitchens;
    using MyKitchen.Test.Mocks;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class HomeControllerTests
    {
        //[Fact]
        //public void CreateAddressShouldCreateAddress()
        //{
        //    //Arrange
        //    var db = DatabaseMock.Instance;
        //    var mapper = AutoMapperMock.Instance;

        //    db.Kitchens.AddRange(Enumerable.Range(0, 10).Select(i => new Kitchen
        //    {
        //        Description = i.ToString() + "Description {i} Description {i}",
        //        UserId = i.ToString()
        //    }));
        //    db.Categories.AddRange(Enumerable.Range(0, 3).Select(i => new Category
        //    {
        //        Description = i.ToString() + "Description {i} Description {i}",
        //        ImageUrl = i.ToString(),
        //        Name = i.ToString(),
        //    }));
        //    db.SaveChanges();
        //    foreach (var kitchen in db.Kitchens)
        //    {
        //        kitchen.IsPublic = true;
        //    }
        //    db.SaveChanges();
        //    var categoryService = new CategoriesService(db, mapper);
        //    var kitchenService = new KitchenService(db, mapper);

        //    var homeController = new HomeController(categoryService, kitchenService);

        //    //Act
        //    var result = homeController.Index();

        //    //Assert
        //    Assert.NotNull(result);

        //    var viewResult = Assert.IsType<ViewResult>(result);

        //    var model = viewResult.Model;

        //    var indexViewModel = Assert.IsType<IndexViewModel>(model);

        //    Assert.Equal(3, indexViewModel.Categories.Count());
        //}
    }
}
