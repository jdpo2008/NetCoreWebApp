using AutoMapper;
using MediatR;
using NetCoreWebApp.Application.Exceptions;
using NetCoreWebApp.Application.Interfaces.Repositories;
using NetCoreWebApp.Application.Mappings;
using NetCoreWebApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreWebApp.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<Response<DeleteCategoryDto>>
    {
        [Required]
        public Guid Id { get; set; }
    }

    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Response<DeleteCategoryDto>>
    {
        private readonly ICategoryRepositoryAsync _CategoryRespository;
        private readonly IMapper _mapper;

        public DeleteCategoryHandler(ICategoryRepositoryAsync CategoryRepository, IMapper Mapper)
        {
            _CategoryRespository = CategoryRepository;
            _mapper = Mapper;
        }
        public async Task<Response<DeleteCategoryDto>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _CategoryRespository.GetByIdAsync(request.Id);
            if (category == null) throw new ApiException($"Category Not Found.");
            await _CategoryRespository.RemoveAsync(category);

            var response = _mapper.Map<DeleteCategoryDto>(category);

            return new Response<DeleteCategoryDto>(_mapper.Map<DeleteCategoryDto>(category), "Category remove successful");
        }
    }
}
