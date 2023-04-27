namespace Application.Models.Posts
{
    public class PostResponse
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public int Reputation { get; set; }

        public string? Url { get; set; }

        public string? Body { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public string Username { get; set; } = string.Empty;
    }
}
