namespace Domain.Entities
{
    public class PostVote : AuditableEntity
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public Account Account { get; set; } = new Account();

        public int PostId { get; set; }

        public Post Post { get; set; } = new TextPost();
    }
}
