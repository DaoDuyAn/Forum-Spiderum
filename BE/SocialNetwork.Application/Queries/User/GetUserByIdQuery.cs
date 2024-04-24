using AutoMapper;
using MediatR;
using SocialNetwork.Application.DTOs.Category;
using SocialNetwork.Application.DTOs.User;
using SocialNetwork.Application.Queries.Category;
using SocialNetwork.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Queries.User
{
    public class GetUserByIdQuery : IRequest<GetUserByIdResponseDTO>
    {
        public string Id { get; set; }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdResponseDTO>
    {
        private readonly IUserRepository userRepo;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUserRepository userRepo, IMapper mapper)
        {
            this.userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<GetUserByIdResponseDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepo.GetUserAsync(p => p.Id == Guid.Parse(request.Id));
            if (user != null)
            {
                var userDto = _mapper.Map<GetUserByIdResponseDTO>(user);
                return userDto;
            }

            return new GetUserByIdResponseDTO();
        }
    }
}
