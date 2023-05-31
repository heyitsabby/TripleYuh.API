using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Models.Votes;
using AutoMapper;
using Domain.Entities;
using Domain.Rules;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class VoteService : IVoteService
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public VoteService(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<VoteResponse> RemoveCommentVoteAsync(int commentId, string username)
        {
            var vote = context.CommentVotes
                .Where(vote => vote.CommentId == commentId && vote.Account.Username == username)
                .SingleOrDefault() ?? throw new NotFoundResourceException("Vote can't be found.");

            var account = context.Accounts
                .Where(account => account.Username == username)
                .SingleOrDefault() ?? throw new NotFoundResourceException($"Can't find user '{username}'.");

            vote.Value = VoteRules.RemoveVote;

            vote.DeletedBy = account.Username;

            context.CommentVotes.Update(vote);

            context.CommentVotes.Remove(vote);

            await context.SaveChangesAsync();

            var response = mapper.Map<VoteResponse>(vote);

            return response;
        }

        public async Task<VoteResponse> RemovePostVoteAsync(int postId, string username)
        {
            var vote = context.PostVotes
                .Where(vote => vote.PostId == postId && vote.Account.Username == username)
                .SingleOrDefault() ?? throw new NotFoundResourceException("Vote can't be found");

            var account = context.Accounts
               .Where(account => account.Username == username)
               .SingleOrDefault() ?? throw new NotFoundResourceException($"Can't find user '{username}'.");

            vote.Value = VoteRules.RemoveVote;

            vote.DeletedBy = account.Username;
            
            context.PostVotes.Update(vote);

            context.PostVotes.Remove(vote);

            await context.SaveChangesAsync();

            var response = mapper.Map<VoteResponse>(vote);

            return response;

        }

        public async Task<VoteResponse> VoteOnCommentAsync(int value, int commentId, string username)
        {
            if (value != VoteRules.Upvote && value != VoteRules.Downvote)
            {
                throw new VoteException("Invalid value");
            }

            var account = context.Accounts.Where(account => account.Username == username)
                    .SingleOrDefault() ?? throw new NotFoundResourceException($"Can't find username '{username}'");

            var comment = context.Comments
                .Where(comment => comment.Id == commentId)
                .SingleOrDefault() ?? throw new NotFoundResourceException($"Can't find comment with id '{commentId}'.");

            var vote = context.CommentVotes
               .Where(vote => vote.CommentId == commentId && vote.Account.Username == username)
               .SingleOrDefault();

            // Create new vote if none already exists, otherwise update vote
            if (vote == null)
            {
                vote = new Domain.Entities.CommentVote
                {
                    Value = value,
                    Account = account,
                    CreatedBy = account.Username,
                    Comment = comment,
                    CommentId = commentId
                };

                context.CommentVotes.Add(vote);
            } else
            {
                vote.Value = value;
                vote.UpdatedBy = account.Username;
                context.CommentVotes.Update(vote);
            }

            await context.SaveChangesAsync();

            var response = mapper.Map<VoteResponse>(vote);

            return response;

        }

        public async Task<VoteResponse> VoteOnPostAsync(int value, int postId, string username)
        {
            if (value != VoteRules.Upvote && value != VoteRules.Downvote)
            {
                throw new VoteException("Invalid value");
            }

            var account = context.Accounts.Where(account => account.Username == username)
                    .SingleOrDefault() ?? throw new NotFoundResourceException($"Can't find username '{username}'");

            var post = context.Posts
                .Where(post => post.Id == postId)
                .SingleOrDefault() ?? throw new NotFoundResourceException($"Can't find post with id '{postId}'.");

            var vote = context.PostVotes
               .Where(vote => vote.PostId == postId && vote.Account.Username == username)
               .SingleOrDefault();

            // Create new vote if none already exists, otherwise update vote
            if (vote == null)
            {
                vote = new Domain.Entities.PostVote
                {
                    Value = value,
                    Account = account,
                    CreatedBy = account.Username,
                    Post = post,
                    PostId = postId
                };

                context.PostVotes.Add(vote);
            }
            else
            {
                vote.Value = value;
                vote.UpdatedBy = account.Username;
                context.PostVotes.Update(vote);
            }

            await context.SaveChangesAsync();

            var response = mapper.Map<VoteResponse>(vote);

            return response;
        }
    }
}
