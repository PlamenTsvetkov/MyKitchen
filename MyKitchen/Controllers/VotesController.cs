namespace MyKitchen.Controllers
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using MyKitchen.Models.Vote;
    using MyKitchen.Services.Vote;

    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : Controller
    {
        private readonly IVotesService votesService;

        public VotesController(IVotesService votesService)
        {
            this.votesService = votesService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PostVoteResponseModel>> Post(PostVoteInputModel input)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.votesService.SetVoteAsync(input.KitchenId, userId, input.Value);
            var averageVotes = this.votesService.GetAverageVotes(input.KitchenId);
            return new PostVoteResponseModel { AverageVote = averageVotes };
        }
    }
}
