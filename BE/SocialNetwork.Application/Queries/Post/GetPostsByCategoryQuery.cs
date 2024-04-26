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
    public class GetPostsByCategoryQuery : IRequest<List<PostResponseDTO>>
    {
        public string CategorySlug { get; set; }
    }


    public class GetPostsByCategoryQueryHandler : IRequestHandler<GetPostsByCategoryQuery, List<PostResponseDTO>>
    {
        private readonly IPostRepository postRepo;
        private readonly IMapper _mapper;

        public GetPostsByCategoryQueryHandler(IPostRepository postRepo, IMapper mapper)
        {
            this.postRepo = postRepo;
            _mapper = mapper;
        }

        public async Task<List<PostResponseDTO>> Handle(GetPostsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var lstPosts = await postRepo.GetPostsByCategorySlugAsync(request.CategorySlug);
            var lstPostResponseDTO = _mapper.Map<List<PostResponseDTO>>(lstPosts);
            return lstPostResponseDTO;
        }
    }
}