﻿using Application.Models.Accounts;
using Application.Models.Comments;
using Application.Models.Posts;
using Application.Models.Votes;
using AutoMapper;
using Domain.Entities;

namespace Application.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Account, AccountResponse>();

            CreateMap<Account, AuthenticateResponse>();

            CreateMap<TextPost, PostResponse>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Account.Username));

            CreateMap<LinkPost, PostResponse>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Account.Username));

            CreateMap<Comment, CommentResponse>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Account.Username));

            CreateMap<PostVote, VoteResponse>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Account.Username));

            CreateMap<CommentVote, VoteResponse>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Account.Username));

            //CreateMap<RegisterRequest, Account>();

            //CreateMap<CreateRequest, Account>();

            //CreateMap<UpdateRequest, Account>()
            //    .ForAllMembers(x => x.Condition((src, dest, prop) =>
            //    {
            //        // ignore null & empty string properties
            //        if (prop == null) return false;

            //        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

            //        // ignore null role
            //        if (x.DestinationMember.Name == "Role" && src.Role == null) return false;

            //        return true;
            //    }
            //));
        }
    }
}
