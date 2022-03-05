namespace MyKitchen.Models.Comments
{
    using Ganss.XSS;

    public class PostCommentViewModel
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string UserName { get; set; }
    }
}
