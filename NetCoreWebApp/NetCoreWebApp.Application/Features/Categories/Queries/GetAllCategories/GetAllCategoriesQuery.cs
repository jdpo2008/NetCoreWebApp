using AutoMapper;
using MediatR;
using NetCoreWebApp.Application.Interfaces.Repositories;
using NetCoreWebApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreWebApp.Application.Features.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQuery : IRequest<PagedResponse<IEnumerable<GetAllCategoriesDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, PagedResponse<IEnumerable<GetAllCategoriesDto>>>
    {

        private readonly ICategoryRepositoryAsync _CategoryRepository;
        private readonly IMapper _mapper;
        public GetAllCategoriesQueryHandler(ICategoryRepositoryAsync categoryRepository, IMapper mapper)
        {
            _CategoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<PagedResponse<IEnumerable<GetAllCategoriesDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllCategoriesRequest>(request);
            var categories = await _CategoryRepository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var response = _mapper.Map<IEnumerable<GetAllCategoriesDto>>(categories);
            return new PagedResponse<IEnumerable<GetAllCategoriesDto>>(response, validFilter.PageNumber, validFilter.PageSize);
        }
    }

}
