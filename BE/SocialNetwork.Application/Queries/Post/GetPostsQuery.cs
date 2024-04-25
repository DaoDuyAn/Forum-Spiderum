using AutoMapper;
using MediatR;
using SocialNetwork.Application.DTOs.Category;
using SocialNetwork.Application.DTOs.Post;
using SocialNetwork.Application.DTOs.Post.;
using SocialNetwork.Application.Queries.Category;
using SocialNetwork.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Queries.Post
{
    public class GetPostsQuery : IRequest<List<PostResponseDTO>>
    {
    }

    public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, List<PostResponseDTO>>
    {
        private readonly IPostRepository postRepo;
        private readonly IMapper _mapper;

        public GetPostsQueryHandler(IPostRepository postRepo, IMapper mapper)
        {
            this.postRepo = postRepo;
            _mapper = mapper;
        }

        public async Task<List<PostResponseDTO>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
        {
          

            return new List<PostResponseDTO>();
        }
    }
}
