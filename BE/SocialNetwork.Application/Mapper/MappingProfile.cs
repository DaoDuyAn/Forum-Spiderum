using AutoMapper;
using SocialNetwork.Application.DTOs.Account;
using SocialNetwork.Application.DTOs.Category;
using SocialNetwork.Application.DTOs.Post;
using SocialNetwork.Application.DTOs.User;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryResponseDTO, CategoryEntity>().ReverseMap();
            CreateMap<AuthResponseDTO, ApiResponse>().ReverseMap();
            CreateMap<UserEntity, UserResponseDTO>();
               
            CreateMap<PostEntity, PostResponseDTO>()
                .ForMember(dest => dest.PostInfo, opt => opt.MapFrom(src => new PostDetailInfo
                {
                    Id = src.Id,
                    Title = src.Title,
                    Description = src.Description,
                    CreationDate = src.CreationDate.ToString("dd/MM/yyyy"),
                    ThumbnailImagePath = src.ThumbnailImagePath,
                    Slug = src.Slug,
                    LikesCount = src.LikesCount,
                    CommentsCount = src.CommentsCount
                }))
                .ForMember(dest => dest.UserInfo, opt => opt.MapFrom(src => new UserDetailInfo
                {
                    FullName = src.User.FullName,
                    UserName = src.User.UserName,
                    AvatarImagePath = src.User.AvatarImagePath
                }))
                .ForMember(dest => dest.PostCategoryInfo, opt => opt.MapFrom(src => new PostCategoryDetailInfo
                {
                    CategoryName = src.Category.CategoryName,
                    Slug = src.Category.Slug
                }));
        }
    }
}
