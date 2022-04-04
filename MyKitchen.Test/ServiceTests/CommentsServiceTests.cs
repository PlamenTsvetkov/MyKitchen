namespace MyKitchen.Test.ServiceTests
{
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    using MyKitchen.Services.Comments;
    using MyKitchen.Test.Mocks;

    public class CommentsServiceTests
    {
        [Fact]
        public async Task CreateCommentShouldCreateComment()
        {
            //Arrange
            var db = DatabaseMock.Instance;

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
            var db = DatabaseMock.Instance;

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
        [Fact]
        public async Task GetCountShouldReturnCorrectResult()
        {
            //Arrange
            var db = DatabaseMock.Instance;

            var service = new CommentsService(db);

            await service.Create(1, "1", "Woaw!");
            await service.Create(1, "1", "Ehaaa");
            await service.Create(1, "1", "Price?");

            //Act
            var commentsCount = service.GetCount();

            //Assert
            Assert.Equal(3, commentsCount);
        }
    }
}
