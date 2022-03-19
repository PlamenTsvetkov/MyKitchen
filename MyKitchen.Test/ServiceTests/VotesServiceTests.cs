namespace MyKitchen.Test.ServiceTests
{
    using Microsoft.EntityFrameworkCore;
    using MyKitchen.Data;
    using MyKitchen.Services.Vote;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class VotesServiceTests
    {
        [Fact]
        public async Task WhenUserVotes2TimesOnly1VoteShouldBeCounted()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MyKitchenDbContext>()
                   .UseInMemoryDatabase(databaseName: "Vote_Database")
                   .Options;
            var db = new MyKitchenDbContext(options);

            var service = new VotesService(db);
            
            await service.SetVoteAsync(1, "1", 1);
            await service.SetVoteAsync(1, "1", 2);
            await service.SetVoteAsync(1, "1", 3);
            await service.SetVoteAsync(1, "1", 4);
            await service.SetVoteAsync(1, "1", 5);

            //Act
            var voteCount = db.Votes.ToArray().Count();
            var firstVote = db.Votes.First().Value;

            //Assert
            Assert.Equal(1, voteCount);
            Assert.Equal(5, firstVote);
        }

        [Fact]
        public async Task When2UsersVoteForTheSameRecipeTheAverageVoteShouldBeCorrect()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<MyKitchenDbContext>()
                  .UseInMemoryDatabase(databaseName: "Vote2_Database")
                  .Options;
            var db = new MyKitchenDbContext(options);

            var service = new VotesService(db);

            //Act
            await service.SetVoteAsync(2, "Plamen", 4);
            await service.SetVoteAsync(2, "Mila", 1);
            await service.SetVoteAsync(2, "Plamen", 3);

            //Assert
            var voteCount = db.Votes.ToArray().Count();
            var averageVote = service.GetAverageVotes(2);

            Assert.Equal(2, voteCount);
            Assert.Equal(2, averageVote);
        }
    }
}
