using AutoMapper;
using MediatR;
using SocialNetwork.Application.DTOs.Post;
using SocialNetwork.Application.DTOs.User;
using SocialNetwork.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Queries.User
{


    public class SearchUserByValueQuery : IRequest<UserPaginationDTO>
    {
        public int Page { get; set; }
        public string SearchValue { get; set; }
    }


    public class SearchUserByValueQueryHandler : IRequestHandler<SearchUserByValueQuery, UserPaginationDTO>
    {
        private readonly IUserRepository userRepo;
        private readonly IMapper _mapper;

        public SearchUserByValueQueryHandler(IUserRepository userRepo, IMapper mapper)
        {
            this.userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<UserPaginationDTO> Handle(SearchUserByValueQuery request, CancellationToken cancellationToken)
        {
            var (users, rowCount, pageCount) = await userRepo.SearchUserByValueAsync(request.SearchValue, request.Page);
            var lstUserResponseDTO = _mapper.Map<List<UserSearchResponseDTO>>(users);
            return new UserPaginationDTO
            {
                userResponse = lstUserResponseDTO,
                RowCount = rowCount,
                PageCount = pageCount
            };
        }
    }
}
