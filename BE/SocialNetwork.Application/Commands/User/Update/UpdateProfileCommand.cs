using MediatR;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Commands.User.Update
{
    public class UpdateProfileCommand : IRequest<string>
    {
        public string Id { get; set; }
        public string FullName { get; set; } = "";
        public string BirthDate { get; set; }
        public string Description { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Address { get; set; } = "";
        public int Gender { get; set; }
        public string AvatarImagePath { get; set; } = "";
        public string CoverImagePath { get; set; } = "";
    }

    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, string>
    {

        private readonly IUserRepository repo;

        public UpdateProfileCommandHandler(IUserRepository repo)
        {
            this.repo = repo;
        }
        public async Task<string> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var newProfile = new UserEntity
            {
                FullName = request.FullName,
                Description = request.Description,
                Email = request.Email,
                Phone = request.Phone,
                Address = request.Address,
                Gender = request.Gender,
                AvatarImagePath = request.AvatarImagePath,
                CoverImagePath = request.CoverImagePath,
                BirthDate = DateTime.ParseExact(request.BirthDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
        };

            return await repo.UpdateProfileAsync(newProfile, Guid.Parse(request.Id));

        }
    }
}
