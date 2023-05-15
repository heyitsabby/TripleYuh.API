using Application.Models.Votes;

namespace Application.Common.Interfaces
{
    public interface IVoteService
    {
        Task<VoteResponse> VoteOnCommentAsync(int value, int commentId, string username);

        Task<VoteResponse> RemoveCommentVoteAsync(int commentId, string username);

        Task<VoteResponse> VoteOnPostAsync(int value, int postId, string username);

        Task<VoteResponse> RemovePostVoteAsync(int postId, string username);
    }
}
