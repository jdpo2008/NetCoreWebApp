using AutoMapper;
using MediatR;
using NetCoreWebApp.Application.Features.Categories.Commands.CreateCategory;
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

namespace NetCoreWebApp.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<Response<CreateCategoryDto>>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Response<CreateCategoryDto>>
    {
        private readonly ICategoryRepositoryAsync _CategoryRepository;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(ICategoryRepositoryAsync CategoryRepository, IMapper mapper)
        {
            _CategoryRepository = CategoryRepository;
            _mapper = mapper;
        }
        public async Task<Response<CreateCategoryDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = await _CategoryRepository.AddAsync((_mapper.Map<Category>(request)));

            return new Response<CreateCategoryDto>(_mapper.Map<CreateCategoryDto>(response), "Category created successfull");
        }
    }
}
