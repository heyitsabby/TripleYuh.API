namespace Domain.Entities
{
    public class CommentVote : AuditableEntity
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public Account Account { get; set; } = new Account();

        public int CommentId { get; set; }

        public Comment Comment { get; set; } = new Comment();
    }
}
