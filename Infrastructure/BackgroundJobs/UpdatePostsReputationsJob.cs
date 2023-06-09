﻿using Application.Common.Interfaces;
using Quartz;

namespace Infrastructure.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class UpdatePostsReputationsJob : IJob
    {
        private readonly IReputationService reputationService;

        public UpdatePostsReputationsJob(IReputationService reputationService)
        {
            this.reputationService = reputationService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await reputationService.UpdateAllPostsReputationsAsync();
        }
    }
}
