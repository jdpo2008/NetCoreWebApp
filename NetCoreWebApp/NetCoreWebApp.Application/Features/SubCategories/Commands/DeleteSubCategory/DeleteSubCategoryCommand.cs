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

namespace NetCoreWebApp.Application.Features.SubCategories.Commands.DeleteSubCategory
{
    public class DeleteSubCategoryCommand : IRequest<Response<DeleteSubCategoryDto>>
    {
        [Required]
        public Guid Id { get; set; }
    }

    public class DeleteSubCategoryHandler : IRequestHandler<DeleteSubCategoryCommand, Response<DeleteSubCategoryDto>>
    {
        private readonly ISubCategoryRepositoryAsync _SubCategoryRespository;
        private readonly IMapper _mapper;

        public DeleteSubCategoryHandler(ISubCategoryRepositoryAsync SubCategoryRepository, IMapper Mapper)
        {
            _SubCategoryRespository = SubCategoryRepository;
            _mapper = Mapper;
        }
        public async Task<Response<DeleteSubCategoryDto>> Handle(DeleteSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _SubCategoryRespository.GetByIdAsync(request.Id);
            if (category == null) throw new ApiException($"Category Not Found.");
            await _SubCategoryRespository.RemoveAsync(category);

            var response = _mapper.Map<DeleteSubCategoryDto>(category);

            return new Response<DeleteSubCategoryDto>(_mapper.Map<DeleteSubCategoryDto>(category), "Category remove successful");
        }
    }
}
