using Application.Common.Interfaces;
using Quartz;

namespace Infrastructure.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class UpdatePostsReputationsJob : IJob
    {
        private readonly IVoteService voteService;

        public UpdatePostsReputationsJob(IVoteService voteService)
        {
            this.voteService = voteService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await voteService.UpdateAllPostsReputationsAsync();
        }
    }
}
