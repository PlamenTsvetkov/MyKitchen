namespace MyKitchen.Services.Comments
{
    using MyKitchen.Data;
    using MyKitchen.Data.Models;

    public class CommentsService : ICommentsService
    {
        private readonly MyKitchenDbContext db;

        public CommentsService(MyKitchenDbContext db)
        {
            this.db = db;
        }

        public async Task Create(int kitchenId, 
                                 string userId, 
                                 string content, 
                                 int? parentId = null)
        {
            var comment = new Comment
            {
                Content = content,
                ParentId = parentId,
                KitchenId = kitchenId,
                UserId = userId,
            };
            await this.db.Comments.AddAsync(comment);
            await this.db.SaveChangesAsync();
        }

        public int GetCount()
        {
            return this.db.Comments.Count();
        }

        public bool IsInPostId(int commentId, int kitchenId)
        {
            var commentKitchenId = this.db
                                .Comments.Where(x => x.Id == commentId)
                                .Select(x => x.KitchenId).FirstOrDefault();

            return commentKitchenId == kitchenId;
        }
    }
}
