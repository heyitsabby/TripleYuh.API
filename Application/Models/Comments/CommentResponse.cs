namespace Application.Models.Comments
{
    public class CommentResponse
    {
        public int Id { get; set; }

        public int Reputation { get; set; }

        public string Body { get; set; } = string.Empty;

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public string Username { get; set; } = string.Empty;

        public int PostId { get; set; }

        public int? ParentId { get; set; }

        public List<int> ChildrenIds { get; set; } = new List<int>();
    }
}
