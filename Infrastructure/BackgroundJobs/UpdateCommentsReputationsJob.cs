using Application.Common.Interfaces;
using Quartz;

namespace Infrastructure.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class UpdateCommentsReputationsJob : IJob
    {
        private readonly IReputationService reputationService;

        public UpdateCommentsReputationsJob(IReputationService reputationService)
        {
            this.reputationService = reputationService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await reputationService.UpdateAllCommentsReputationsAsync();
        }
    }
}
