namespace MyKitchen.Models.Vote
{
    using System.ComponentModel.DataAnnotations;

    public class PostVoteInputModel
    {
        public int KitchenId { get; set; }

        [Range(1, 5)]
        public byte Value { get; set; }
    }
}
