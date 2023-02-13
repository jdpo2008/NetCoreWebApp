using AutoMapper;
using MediatR;
using NetCoreWebApp.Application.Features.SubCategories.Commands.CreateSubCategory;
using NetCoreWebApp.Application.Interfaces.Repositories;
using NetCoreWebApp.Application.Wrappers;
using NetCoreWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreWebApp.Application.Features.SubCategories.Commands.CreateSubCategory
{
    public class CreateSubCategoryCommand : IRequest<Response<CreateSubCategoryDto>>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateSubCategoryCommand, Response<CreateSubCategoryDto>>
    {
        private readonly ICategoryRepositoryAsync _CategoryRepository;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(ICategoryRepositoryAsync CategoryRepository, IMapper mapper)
        {
            _CategoryRepository = CategoryRepository;
            _mapper = mapper;
        }
        public async Task<Response<CreateSubCategoryDto>> Handle(CreateSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = await _CategoryRepository.AddAsync((_mapper.Map<Category>(request)));

            return new Response<CreateSubCategoryDto>(_mapper.Map<CreateSubCategoryDto>(response), "Category created successfull");
        }
    }
}
