namespace MyKitchen.Services.Comments
{
    public interface ICommentsService
    {
        Task Create(int kitchenId, string userId, string content, int? parentId = null);

        bool IsInPostId(int commentId, int kitchenId);

        int GetCount();
    }
}
