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

namespace NetCoreWebApp.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<PagedResponse<IEnumerable<GetAllProductsDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PagedResponse<IEnumerable<GetAllProductsDto>>>
    {

        private readonly IProductRepositoryAsync _ProductRepository;
        private readonly IMapper _mapper;
        public GetAllProductsQueryHandler(IProductRepositoryAsync productRepository, IMapper mapper)
        {
            _ProductRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<PagedResponse<IEnumerable<GetAllProductsDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllProductsRequest>(request);
            var products = await _ProductRepository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var response = _mapper.Map<IEnumerable<GetAllProductsDto>>(products);
            return new PagedResponse<IEnumerable<GetAllProductsDto>>(response, validFilter.PageNumber, validFilter.PageSize);
        }
    }

}