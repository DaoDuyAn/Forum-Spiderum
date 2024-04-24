using AutoMapper;
using SocialNetwork.Application.DTOs.Account;
using SocialNetwork.Application.DTOs.Category;
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
            CreateMap<UserEntity, GetUserByIdResponseDTO>()
                .ForMember(dest => dest.BirthDate,
                           opt => opt.MapFrom(src => src.BirthDate.ToString("dd/MM/yyyy")))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
