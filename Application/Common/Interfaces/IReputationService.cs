namespace Application.Common.Interfaces
{
    public interface IReputationService
    {
        Task UpdateAllPostsReputationsAsync();

        Task UpdateAllCommentsReputationsAsync();
    }
}
