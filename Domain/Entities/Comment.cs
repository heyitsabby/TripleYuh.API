using Domain.Rules;

namespace Domain.Entities
{
    public class Comment : AuditableEntity
    {
        public int Id { get; set; }

        public string Body { get; set; } = string.Empty;

        public int Reputation { get; set; } = CommentRules.DefaultReputation;

        public Account Account { get; set; } = new Account();

        public int AccountId { get; set; }

        public Post Post { get; set; } = new TextPost();

        public int PostId { get; set; }

        public Comment? Parent { get; set; }

        public int? ParentId { get; set; }

        public List<int> ChildrenIds { get; set; } = new List<int>();
    }
}
