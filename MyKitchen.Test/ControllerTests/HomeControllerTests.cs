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
    using MyTested.AspNetCore.Mvc;
    using System;
    using Shouldly;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    using static Data.Kitchens;
    using static WebConstants.Cache;
    public class HomeControllerTests
    {
        //[Fact]
        //public void IndexShouldReturnCorrectViewWithModel()
        //  => MyController<HomeController>
        //      .Instance(controller => controller
        //          .WithData(TenPublicKitchens))
        //      .Calling(c => c.Index())
        //      .ShouldHave()
        //      .MemoryCache(cache => cache
        //          .ContainingEntry(entry => entry
        //              .WithKey(IndexViewCasheKey)
        //              .WithAbsoluteExpirationRelativeToNow(TimeSpan.FromMinutes(3))
        //              .WithValueOfType<IndexViewModel>()))
        //      .AndAlso()
        //      .ShouldReturn()
        //      .View(view => view
        //          .WithModelOfType<IndexViewModel>());

        //[Fact]
        //public void ErrorShouldReturnView()
        //    => MyController<HomeController>
        //        .Instance()
        //        .Calling(c => c.Error())
        //        .ShouldReturn()
        //        .View();
    }
}
