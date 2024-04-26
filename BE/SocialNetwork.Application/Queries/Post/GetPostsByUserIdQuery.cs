using AutoMapper;
using MediatR;
using SocialNetwork.Application.DTOs.Post;
using SocialNetwork.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Queries.Post
{
    public class GetPostsByUserIdQuery : IRequest<List<PostResponseDTO>>
    {
        public string UserId { get; set; }
    }

    public class GetPostsByUserIdQueryHandler : IRequestHandler<GetPostsByUserIdQuery, List<PostResponseDTO>>
    {
        private readonly IPostRepository postRepo;
        private readonly IMapper _mapper;

        public GetPostsByUserIdQueryHandler(IPostRepository postRepo, IMapper mapper)
        {
            this.postRepo = postRepo;
            _mapper = mapper;
        }

        public async Task<List<PostResponseDTO>> Handle(GetPostsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var lstPosts = await postRepo.GetPostsByUserIdAsync(Guid.Parse(request.UserId));
            var lstPostResponseDTO = _mapper.Map<List<PostResponseDTO>>(lstPosts);
            return lstPostResponseDTO;
        }
    }
}
