using AutoMapper;
using MediatR;
using NetCoreWebApp.Application.Interfaces.Repositories;
using NetCoreWebApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreWebApp.Application.Features.SubCategories.Queries.GetSubCategoryById
{
    public class GetSubCategoryByIdQuery : IRequest<Response<GetSubCategoryByIdDto>>
    {
        [Required]
        public Guid Id { get; set; }
    }

    public class GetSubCategoryByIdQueryHandler : IRequestHandler<GetSubCategoryByIdQuery, Response<GetSubCategoryByIdDto>>
    {

        private readonly ICategoryRepositoryAsync _CategoryRepository;
        private readonly IMapper _mapper;
        public GetSubCategoryByIdQueryHandler(ICategoryRepositoryAsync CategoryRepository, IMapper Mapper)
        {
            _CategoryRepository = CategoryRepository;
            _mapper = Mapper;
        }
        public async Task<Response<GetSubCategoryByIdDto>> Handle(GetSubCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var rq = _mapper.Map<GetSubCategoryByIdRequest>(request);
            var category   = await _CategoryRepository.GetByIdAsync(rq.Id);
            var response = _mapper.Map<GetSubCategoryByIdDto>(category);
            return new Response<GetSubCategoryByIdDto>(response);
        }
    }
}
