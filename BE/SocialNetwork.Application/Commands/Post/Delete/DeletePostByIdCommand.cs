using MediatR;
using SocialNetwork.Domain.Interfaces;

namespace SocialNetwork.Application.Commands.Post.Delete
{
    public class DeletePostByIdCommand : IRequest<bool>
    {
        public string Id { get; set; }  
    }

    public class DeletePostByIdCommandHandler : IRequestHandler<DeletePostByIdCommand, bool>
    {
        private readonly IPostRepository repo;

        public DeletePostByIdCommandHandler(IPostRepository repo)
        {
            this.repo = repo;
        }


        public async Task<bool> Handle(DeletePostByIdCommand request, CancellationToken cancellationToken)
        {
            return await repo.DeletePostAsync(Guid.Parse(request.Id));
        }
    }

}
