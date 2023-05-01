using Domain.Rules;

namespace Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        public string Body { get; set; } = string.Empty;

        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }

        public int Reputation { get; set; } = CommentRules.DefaultReputation;

        public Account Account { get; set; } = new Account();

        public int AccountId { get; set; }

        public Post Post { get; set; } = new TextPost();

        public int PostId { get; set; }

        public Comment? Parent { get; set; }

        public int? ParentId { get; set; }

    }
}
