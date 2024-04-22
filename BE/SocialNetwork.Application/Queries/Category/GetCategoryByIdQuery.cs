using AutoMapper;
using MediatR;
using SocialNetwork.Application.DTOs.Category;
using SocialNetwork.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Queries.Category
{

    public class GetCategoryByIdQuery : IRequest<CategoryResponseDTO>
    {
        public string Id { get; set; }
    }

    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryResponseDTO>
    {
        private readonly ICategoryRepository cateRepo;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(ICategoryRepository cateRepo, IMapper mapper)
        {
            this.cateRepo = cateRepo;
            _mapper = mapper;
        }

        public async Task<CategoryResponseDTO> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await cateRepo.GetCategoryAsync(p => p.Id == Guid.Parse(request.Id));
            if (category != null)
            {
                var cateDto = _mapper.Map<CategoryResponseDTO>(category);
                return cateDto;
            }

            return new CategoryResponseDTO();
        }
    }
}
