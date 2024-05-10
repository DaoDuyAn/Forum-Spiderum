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
    public class GetPostsByCategoryQuery : IRequest<PostPaginationDTO>
    {
        public string Sort { get; set; }
        // hot, new, controversial, top
        public int PageIndex { get; set; }
        public string CategorySlug { get; set; }
    }


    public class GetPostsByCategoryQueryHandler : IRequestHandler<GetPostsByCategoryQuery, PostPaginationDTO>
    {
        private readonly IPostRepository postRepo;
        private readonly IMapper _mapper;

        public GetPostsByCategoryQueryHandler(IPostRepository postRepo, IMapper mapper)
        {
            this.postRepo = postRepo;
            _mapper = mapper;
        }

        public async Task<PostPaginationDTO> Handle(GetPostsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var (posts, rowCount, pageCount) = await postRepo.GetPostsByCategorySlugAsync(request.Sort, request.PageIndex, request.CategorySlug);
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