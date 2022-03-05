namespace MyKitchen.Models.Comments
{
    public class CreateCommentInputModel
    {
        public int KitchenId { get; set; }

        public int ParentId { get; set; }

        public string Content { get; set; }
    }
}
