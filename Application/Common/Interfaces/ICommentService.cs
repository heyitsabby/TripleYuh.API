using Application.Models.Comments;

namespace Application.Common.Interfaces
{
    public interface ICommentService
    {
        Task<CommentResponse> CreateAsync(int postId, string username, int? parentId, string body);

        Task<CommentResponse> UpdateAsync(int id, string username, string body);

        Task DeleteAsync(int commentId, string username);

        Task<CommentResponse> GetAsync(int id);

        Task<IEnumerable<CommentResponse>> GetAllByPostAsync(int postId);

        Task<IEnumerable<CommentResponse>> GetAllAsync();
    }
}
