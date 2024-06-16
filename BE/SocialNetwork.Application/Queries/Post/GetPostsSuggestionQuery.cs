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
    public class GetPostsSuggestionQuery : IRequest<PostPaginationDTO>
    {
        public string PostSlug { get; set; }
    }

    public class GetPostsSuggestionQueryHandler : IRequestHandler<GetPostsSuggestionQuery, PostPaginationDTO>
    {
        private readonly IPostRepository postRepo;
        private readonly IMapper _mapper;

        public GetPostsSuggestionQueryHandler(IPostRepository postRepo, IMapper mapper)
        {
            this.postRepo = postRepo;
            _mapper = mapper;
        }

        public async Task<PostPaginationDTO> Handle(GetPostsSuggestionQuery request, CancellationToken cancellationToken)
        {
            var (posts, rowCount, pageCount) = await postRepo.GetPostsSuggestionAsync(request.PostSlug);
            var lstPostResponseDTO = _mapper.Map<List<PostResponseDTO>>(posts);
            return new PostPaginationDTO
            {
                postResponse = lstPostResponseDTO,
                RowCount = rowCount,
                PageCount = pageCount
            };
        }
    }
}
