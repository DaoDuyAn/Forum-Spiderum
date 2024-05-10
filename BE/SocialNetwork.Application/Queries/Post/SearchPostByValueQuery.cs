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

    public class SearchPostByValueQuery : IRequest<PostPaginationDTO>
    {
        public int Page{ get; set; }
        public string SearchValue { get; set; }
    }


    public class SearchPostByValueQueryHandler : IRequestHandler<SearchPostByValueQuery, PostPaginationDTO>
    {
        private readonly IPostRepository postRepo;
        private readonly IMapper _mapper;

        public SearchPostByValueQueryHandler(IPostRepository postRepo, IMapper mapper)
        {
            this.postRepo = postRepo;
            _mapper = mapper;
        }

        public async Task<PostPaginationDTO> Handle(SearchPostByValueQuery request, CancellationToken cancellationToken)
        {
            var (posts, rowCount, pageCount) = await postRepo.SearchPostByValueAsync(request.SearchValue, request.Page);
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
