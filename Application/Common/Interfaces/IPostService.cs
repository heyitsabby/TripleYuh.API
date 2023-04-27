using Application.Models.Accounts;

namespace Application.Common.Interfaces
{
    public interface IPostService
    {
        Task<PostResponse> CreateTextPostAsync(string? username, string title, string? body);

        Task<PostResponse> CreateLinkPostAsync(string? username, string title, string url);

        Task<PostResponse> UpdatePostAsync(int id, string? username, string? body);

        Task DeleteAsync(int id, string? username);
    }
}
