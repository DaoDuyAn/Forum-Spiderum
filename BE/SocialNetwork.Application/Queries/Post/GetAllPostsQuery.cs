using AutoMapper;
using MediatR;
using SocialNetwork.Application.DTOs.Category;
using SocialNetwork.Application.DTOs.Post;
using SocialNetwork.Application.Queries.Category;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Queries.Post
{
    public class GetAllPostsQuery : IRequest<List<PostResponseDTO>>
    {
    }

    public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, List<PostResponseDTO>>
    {
        private readonly IPostRepository postRepo;
        private readonly IMapper _mapper;

        public GetAllPostsQueryHandler(IPostRepository postRepo, IMapper mapper)
        {
            this.postRepo = postRepo;
            _mapper = mapper;
        }

        public async Task<List<PostResponseDTO>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var lstPosts = await postRepo.GetAllPostsAsync();
            var lstPostResponseDTO = _mapper.Map<List<PostResponseDTO>>(lstPosts);
            return lstPostResponseDTO;
        }
    }
}
