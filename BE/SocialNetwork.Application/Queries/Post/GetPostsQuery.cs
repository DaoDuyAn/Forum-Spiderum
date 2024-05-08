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

    public class GetPostsQuery : IRequest<PostPaginationDTO>
    {
        public string Sort { get; set; }
        // hot, follow, new, controversial, top
        public int PageIndex { get; set; }
        public string UserId { get; set; }
    }

    public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, PostPaginationDTO>
    {
        private readonly IPostRepository postRepo;
        private readonly IMapper _mapper;

        public GetPostsQueryHandler(IPostRepository postRepo, IMapper mapper)
        {
            this.postRepo = postRepo;
            _mapper = mapper;
        }

        public async Task<PostPaginationDTO> Handle(GetPostsQuery request, CancellationToken cancellationToken)
        {
            var (posts, rowCount, pageCount) = await postRepo.GetPostsAsync(request.Sort, request.PageIndex, Guid.Parse(request.UserId));
            var lstPostResponseDTO = _mapper.Map<List<PostResponseDTO>>(posts);
            return new PostPaginationDTO
            {
               postResponse =  lstPostResponseDTO,
               RowCount = rowCount,
               PageCount = pageCount
            };
        }
    }
}
