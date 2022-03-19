namespace MyKitchen.Test.ServiceTests
{
    using Microsoft.EntityFrameworkCore;
    using MyKitchen.Data;
    using MyKitchen.Services.Comments;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class CommentsServiceTests
    {
        [Fact]
        public async Task CreateCommentShouldCreateComment()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MyKitchenDbContext>()
                    .UseInMemoryDatabase(databaseName: "Comment_Database")
                    .Options;
            var db = new MyKitchenDbContext(options);

            var service = new CommentsService(db);

            await service.Create(1,"1","Woaw!");
            await service.Create(1,"1","Ehaaa");
            await service.Create(1,"1","Price?");

            //Act
            var commentsCount = db.Comments.ToArray().Count();

            //Assert
            Assert.Equal(3, commentsCount);
        }

        [Fact]
        public async Task WhenCheckIsIsInKitchensIdShouldТoReturnTheCorrectResult()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MyKitchenDbContext>()
                    .UseInMemoryDatabase(databaseName: "Comment2_Database")
                    .Options;
            var db = new MyKitchenDbContext(options);

            var service = new CommentsService(db);

            await service.Create(1, "1", "Woaw!");
            await service.Create(1, "1", "Ehaaa");
            await service.Create(1, "1", "Price?");

            //Act
            var resultTrue = service.IsInPostId(1, 1);
            var resultFalse = service.IsInPostId(1, 2);

            //Assert
            Assert.True(resultTrue);
            Assert.False(resultFalse);
        }
    }
}
