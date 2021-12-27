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

namespace NetCoreWebApp.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQuery : IRequest<Response<GetCategoryByIdDto>>
    {
        [Required]
        public Guid Id { get; set; }
    }

    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Response<GetCategoryByIdDto>>
    {

        private readonly ICategoryRepositoryAsync _CategoryRepository;
        private readonly IMapper _mapper;
        public GetCategoryByIdQueryHandler(ICategoryRepositoryAsync CategoryRepository, IMapper Mapper)
        {
            _CategoryRepository = CategoryRepository;
            _mapper = Mapper;
        }
        public async Task<Response<GetCategoryByIdDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var rq = _mapper.Map<GetCategoryByIdRequest>(request);
            var category   = await _CategoryRepository.GetByIdAsync(rq.Id);
            var response = _mapper.Map<GetCategoryByIdDto>(category);
            return new Response<GetCategoryByIdDto>(response);
        }
    }
}
