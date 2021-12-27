using AutoMapper;
using MediatR;
using NetCoreWebApp.Application.Exceptions;
using NetCoreWebApp.Application.Interfaces.Repositories;
using NetCoreWebApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreWebApp.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<Response<UpdateCategoryDto>>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Response<UpdateCategoryDto>>
    {
        private readonly ICategoryRepositoryAsync _CategoryRepository;
        private readonly IMapper _mapper;
        public UpdateCategoryCommandHandler(ICategoryRepositoryAsync CategoryRepository, IMapper Mapper)
        {
            _CategoryRepository = CategoryRepository;
            _mapper = Mapper;
        }

        public async Task<Response<UpdateCategoryDto>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateCategoryRequest>(request);
            var category = await _CategoryRepository.GetByIdAsync(command.Id);
            if(category == null)
            {
                throw new ApiException($"Category Not Found.");
            }
            else
            {
                category.Name = command.Name;
                category.Description = command.Description;
                await _CategoryRepository.UpdateAsync(category);
                return new Response<UpdateCategoryDto>(_mapper.Map<UpdateCategoryDto>(category), "Category Update Successufull");
            }
        }
    }
}
