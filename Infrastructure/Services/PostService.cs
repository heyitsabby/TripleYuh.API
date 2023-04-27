using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Models.Accounts;
using AutoMapper;
using Domain.Entities;
using Domain.Rules;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class PostService : IPostService
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public PostService(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<PostResponse> CreateLinkPostAsync(string? username, string title, string url)
        {
            var account = await context.Accounts
                .Where(account => account.Username == username)
                .SingleOrDefaultAsync() ?? throw new NotFoundResourceException($"Can't find account '{username}'");

            var post = new LinkPost 
            {
                Title = title,
                Url = url,
                Account = account,
                Reputation = PostRules.DefaultReputation,
                Created = DateTime.UtcNow
            };

            await context.Posts.AddAsync(post);
            
            await context.SaveChangesAsync();

            return mapper.Map<PostResponse>(post);
        }

        public async Task<PostResponse> CreateTextPostAsync(string? username, string title, string? body)
        {
            var account = await context.Accounts
                .Where(account => account.Username == username)
                .SingleOrDefaultAsync() ?? throw new NotFoundResourceException($"Can't find account '{username}'");

            var post = new TextPost
            {
                Title = title,
                Body = body,
                Account = account,
                Reputation = PostRules.DefaultReputation,
                Created = DateTime.UtcNow
            };

            await context.Posts.AddAsync(post);

            await context.SaveChangesAsync();

            var response = mapper.Map<PostResponse>(post);

            return response;
        }

        public async Task DeleteAsync(int id, string? username)
        {
            var account = await context.Accounts
              .Where(account => account.Username == username)
              .SingleOrDefaultAsync() ?? throw new NotFoundResourceException($"Can't find account '{username}'");

            var post = await context.Posts.FindAsync(id)
                ?? throw new NotFoundResourceException($"Can't find post with id '{id}'.");

            context.Posts.Remove(post);

            await context.SaveChangesAsync();
        }

        public Task<IEnumerable<PostResponse>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<PostResponse> GetAsync(int id)
        {
            var post = await context.Posts
                .Include(p => p.Account)
                .SingleOrDefaultAsync(p => p.Id == id)
                ?? throw new NotFoundResourceException($"Can't find post with id '{id}'.");

            return mapper.Map<PostResponse>(post);
        }

        public async Task<PostResponse> UpdatePostAsync(int id, string? username, string? body)
        {
            var account = await context.Accounts
               .Where(account => account.Username == username)
               .SingleOrDefaultAsync() ?? throw new NotFoundResourceException($"Can't find account '{username}'");

            var post = await context.Posts.FindAsync(id) 
                ?? throw new NotFoundResourceException($"Can't find post with id '{id}'.");

            if (post.Type == PostType.Link)
            {
                throw new UpdateResourceException($"Can't update post '{id}'.");
            }

            var textPost = (TextPost)post;

            textPost.Body = string.IsNullOrEmpty(body) ? null : body;

            textPost.Updated = DateTime.UtcNow;

            context.Posts.Update(textPost);

            await context.SaveChangesAsync();

            return mapper.Map<PostResponse>(textPost);
        }
    }
}
