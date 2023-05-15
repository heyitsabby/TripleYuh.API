using Domain.Entities;

namespace Application.Models.Votes
{
    public class VoteResponse
    {
        public int Id { get; set; }
        public int Value { get; set; }

        public int? CommentId { get; set; }

        public int? PostId { get; set; }

        public string Username { get; set; } = string.Empty;
    }
}
