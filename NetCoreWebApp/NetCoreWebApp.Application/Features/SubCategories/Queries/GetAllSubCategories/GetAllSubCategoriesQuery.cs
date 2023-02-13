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

namespace NetCoreWebApp.Application.Features.SubCategories.Queries.GetAllSubCategories
{
    public class GetAllSubCategoriesQuery : IRequest<PagedResponse<IEnumerable<GetAllSubCategoriesDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllSubCategoriesQuery, PagedResponse<IEnumerable<GetAllSubCategoriesDto>>>
    {

        private readonly ICategoryRepositoryAsync _CategoryRepository;
        private readonly IMapper _mapper;
        public GetAllCategoriesQueryHandler(ICategoryRepositoryAsync categoryRepository, IMapper mapper)
        {
            _CategoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<PagedResponse<IEnumerable<GetAllSubCategoriesDto>>> Handle(GetAllSubCategoriesQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllSubCategoriesRequest>(request);
            var categories = await _CategoryRepository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var response = _mapper.Map<IEnumerable<GetAllSubCategoriesDto>>(categories);
            return new PagedResponse<IEnumerable<GetAllSubCategoriesDto>>(response, validFilter.PageNumber, validFilter.PageSize);
        }
    }

}
