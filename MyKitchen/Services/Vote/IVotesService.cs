namespace MyKitchen.Services.Vote
{
    public interface IVotesService
    {
        Task SetVoteAsync(int kitchenId, string userId, byte value);

        double GetAverageVotes(int kitchenId);
    }
}
