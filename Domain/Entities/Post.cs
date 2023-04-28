using Domain.Rules;

namespace Domain.Entities
{
    public abstract class Post
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public int Reputation { get; set; } = PostRules.DefaultReputation;

        public Account Account { get; set; } = new Account();

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public PostType Type { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
