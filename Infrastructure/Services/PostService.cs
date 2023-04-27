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

            response.Username = post.Account.Username;

            return response;
        }
    }
}
