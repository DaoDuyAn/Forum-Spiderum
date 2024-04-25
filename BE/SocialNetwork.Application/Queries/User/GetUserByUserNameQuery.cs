using AutoMapper;
using MediatR;
using SocialNetwork.Application.DTOs.User;
using SocialNetwork.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Queries.User
{
   

    public class GetUserByUserNameQuery : IRequest<UserResponseDTO>
    {
        public string UserName { get; set; }
    }

    public class GetUserByUserNameQueryHandler : IRequestHandler<GetUserByUserNameQuery, UserResponseDTO>
    {
        private readonly IUserRepository userRepo;
        private readonly IMapper _mapper;

        public GetUserByUserNameQueryHandler(IUserRepository userRepo, IMapper mapper)
        {
            this.userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<UserResponseDTO> Handle(GetUserByUserNameQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepo.GetUserAsync(p => p.UserName == request.UserName);
            if (user != null)
            {
                var userDto = _mapper.Map<UserResponseDTO>(user);
                return userDto;
            }

            return new UserResponseDTO();
        }
    }
}
