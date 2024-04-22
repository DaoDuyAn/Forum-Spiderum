using AutoMapper;
using MediatR;
using SocialNetwork.Application.DTOs.Category;
using SocialNetwork.Application.DTOs.Post;
using SocialNetwork.Application.Queries.Post;
using SocialNetwork.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Queries.Category
{
    public class GetAllCategoriesQuery : IRequest<List<CategoryResponseDTO>>
    {
    }

    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<CategoryResponseDTO>>
    {
        private readonly ICategoryRepository cateRepo;
        private readonly IMapper _mapper;

        public GetAllCategoriesQueryHandler(ICategoryRepository cateRepo, IMapper mapper)
        {
            this.cateRepo = cateRepo;
            _mapper = mapper;
        }

        public async Task<List<CategoryResponseDTO>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await cateRepo.GetAllAsync();
            if (categories.Count > 0)
            {
                var catesDto = _mapper.Map<List<CategoryResponseDTO>>(categories);
                return catesDto;
            }

            return new List<CategoryResponseDTO>();
        }
    }
}
