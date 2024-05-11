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
    public class GetPostsByUserNameQuery : IRequest<PostPaginationDTO>
    {
        public string Tab { get; set; }
        // createdPosts, savedPosts
        public int Page { get; set; }
        public string UserName { get; set; }
    }


    public class GetPostsByUserNameQueryHandler : IRequestHandler<GetPostsByUserNameQuery, PostPaginationDTO>
    {
        private readonly IPostRepository postRepo;
        private readonly IMapper _mapper;

        public GetPostsByUserNameQueryHandler(IPostRepository postRepo, IMapper mapper)
        {
            this.postRepo = postRepo;
            _mapper = mapper;
        }

        public async Task<PostPaginationDTO> Handle(GetPostsByUserNameQuery request, CancellationToken cancellationToken)
        {
            var (posts, rowCount, pageCount) = await postRepo.GetPostsByUserNameAsync(request.Tab, request.Page, request.UserName);
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
