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
    

    public class GetCategoryBySlugQuery : IRequest<CategoryResponseDTO>
    {
        public string Slug { get; set; }
    }

    public class GetCategoryBySlugQueryHandler : IRequestHandler<GetCategoryBySlugQuery, CategoryResponseDTO>
    {
        private readonly ICategoryRepository cateRepo;
        private readonly IMapper _mapper;

        public GetCategoryBySlugQueryHandler(ICategoryRepository cateRepo, IMapper mapper)
        {
            this.cateRepo = cateRepo;
            _mapper = mapper;
        }

        public async Task<CategoryResponseDTO> Handle(GetCategoryBySlugQuery request, CancellationToken cancellationToken)
        {
            var category = await cateRepo.GetCategoryAsync(p => p.Slug == request.Slug);
            if (category != null)
            {
                var cateDto = _mapper.Map<CategoryResponseDTO>(category);
                return cateDto;
            }

            return new CategoryResponseDTO();
        }
    }
}
