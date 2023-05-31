using Application.Common.Interfaces;
using Domain.Rules;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class ReputationService : IReputationService
    {
        private readonly DataContext context;

        public ReputationService(DataContext context) 
        {
            this.context = context;
        }

        public async Task UpdateAllPostsReputationsAsync()
        {

            foreach (var post in await context.Posts.ToListAsync())
            {
                post.Reputation = PostRules.DefaultReputation + await context.PostVotes
                    .Where(vote => vote.PostId == post.Id)
                    .SumAsync(vote => vote.Value);

                context.Posts.Update(post);
            }

            await context.SaveChangesAsync();
        }

        public async Task UpdateAllCommentsReputationsAsync()
        {

            foreach (var comment in await context.Comments.ToListAsync())
            {
                comment.Reputation = CommentRules.DefaultReputation + await context.CommentVotes
                    .Where(vote => vote.CommentId == comment.Id)
                    .SumAsync(vote => vote.Value);

                context.Comments.Update(comment);
            }

            await context.SaveChangesAsync();
        }
    }
}
