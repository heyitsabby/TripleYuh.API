﻿using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Models.Comments;
using AutoMapper;
using Domain.Entities;
using Domain.Rules;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class CommentService : ICommentService
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public CommentService(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<CommentResponse> CreateAsync(int postId, string username, int? parentId, string body)
        {
            var account = await context.Accounts
                .Where(account => account.Username == username)
                .SingleOrDefaultAsync() ?? throw new NotFoundResourceException($"Can't find account '{username}'");

            var post = await context.Posts.FindAsync(postId)
                ?? throw new NotFoundResourceException($"Can't find post with id '{postId}'.");

            var parentComment = await context.Comments.FindAsync(parentId);

            var comment = new Comment
            {
                Account = account,
                Body = body,
                CreatedBy = account.Username,
                Reputation = CommentRules.DefaultReputation,
                Post = post,
                Parent = parentComment
            };

            if (parentComment != null && parentComment.PostId != post.Id)
            {
                throw new CreateResourceException("Posts don't match.");
            }

            if (parentComment != null)
            {
                comment.Path = parentComment.Path.ToList();

            }

            await context.Comments.AddAsync(comment);

            await context.SaveChangesAsync();

            comment.Path.Add(comment.Id);

            await context.SaveChangesAsync();

            return mapper.Map<CommentResponse>(comment);
        }

        public async Task DeleteAsync(int commentId, string username)
        {
            var account = await context.Accounts
                .Where(account => account.Username == username)
                .SingleOrDefaultAsync() ?? throw new NotFoundResourceException($"Can't find account '{username}'.");

            var comment = await context.Comments.FindAsync(commentId)
                ?? throw new NotFoundResourceException($"Can't find comment with id '{commentId}'.");
            
            if (comment.Account.Username != account.Username && account.Role != Role.Admin)
            {
                throw new UnauthorizedException("Unauthorized to perform deletion.");
            }

            comment.DeletedBy = account.Username;

            context.Comments.Remove(comment);

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CommentResponse>> GetAllAsync()
        {
            var comments = await context.Comments
                .Include(comment => comment.Account)
                .Include(comment => comment.Post)
                .ToListAsync();

            return mapper.Map<IList<CommentResponse>>(comments);
        }

        public async Task<CommentResponse> GetAsync(int id)
        {
            var comment = await context.Comments.FindAsync(id)
                ?? throw new NotFoundResourceException($"Can't find comment with id '{id}'.");

            if (comment.Deleted != null)
            {
                throw new NotFoundResourceException($"Comment has been deleted.");
            }

            return mapper.Map<CommentResponse>(comment);
        }

        public async Task<IEnumerable<CommentResponse>> GetAllByPostAsync(int postId)
        {
            var post = await context.Posts
                .Include(p => p.Comments)
                .ThenInclude(c => c.Account)
                .SingleOrDefaultAsync(p => p.Id == postId)
                ?? throw new NotFoundResourceException($"Can't find post with id '{postId}'.");

            var comments = post.Comments;

            return mapper.Map<IList<CommentResponse>>(comments);
        }

        public async Task<CommentResponse> UpdateAsync(int id, string username, string body)
        {
            var account = await context.Accounts
                .Where(account => account.Username == username)
                .SingleOrDefaultAsync() ?? throw new NotFoundResourceException($"Can't find account '{username}'");

            var comment = await context.Comments.FindAsync(id)
                ?? throw new NotFoundResourceException($"Can't find comment with id '{id}'.");

            if (comment.Deleted != null)
            {
                throw new NotFoundResourceException($"Comment has been deleted.");
            }

            if (comment.Account.Username != account.Username && account.Role != Role.Admin)
            {
                throw new UnauthorizedException("Unauthorized to perform update.");
            }

            comment.Body = body;

            comment.UpdatedBy = account.Username;

            context.Comments.Update(comment);

            await context.SaveChangesAsync();

            return mapper.Map<CommentResponse>(comment);
            
        }

        public async Task<IEnumerable<CommentResponse>> GetAllByUsernameAsync(string username)
        {
            var account = await context.Accounts
                .Include(account => account.Comments)
                .Where(a => a.Username == username)
                .SingleOrDefaultAsync() ?? throw new NotFoundResourceException($"Can't find account '{username}'.");

            var comments = account.Comments;

            return mapper.Map<IList<CommentResponse>>(comments);
        }
    }
}
