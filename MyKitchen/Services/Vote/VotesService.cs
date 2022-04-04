namespace MyKitchen.Services.Vote
{
    using MyKitchen.Data;
    using MyKitchen.Data.Models;

    public class VotesService : IVotesService
    {
        private readonly MyKitchenDbContext db;

        public VotesService(MyKitchenDbContext db)
        {
            this.db = db;
        }

        public double GetAverageVotes(int kitchenId)
        {
            return this.db.Votes
                          .Where(x => x.KitchenId == kitchenId)
                          .Average(x => x.Value);
        }

        public async Task SetVoteAsync(int kitchenId, string userId, byte value)
        {
            var vote = this.db.Votes
                .FirstOrDefault(x => x.KitchenId == kitchenId && x.UserId == userId);

            if (vote == null)
            {
                vote = new Vote
                {
                    KitchenId = kitchenId,
                    UserId = userId,
                };

                await this.db.Votes.AddAsync(vote);
            }

            vote.Value = value;
            await this.db.SaveChangesAsync();
        }
    }
}
