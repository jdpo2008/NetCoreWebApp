using AutoMapper;
using MediatR;
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

namespace NetCoreWebApp.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<Response<CreateProductDto>>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [RegularExpression("^\\d{0,8}(\\.\\d{1,2})?$")]
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        [Required]
        public Guid MarcaId { get; set; }
        public string ImageUrl { get; set; }
        
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response<CreateProductDto>>
    {
        private readonly IProductRepositoryAsync _ProductRepository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductRepositoryAsync ProductRepository, IMapper mapper)
        {
            _ProductRepository = ProductRepository;
            _mapper = mapper;
        }
        public async Task<Response<CreateProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var response = await _ProductRepository.AddAsync((_mapper.Map<Product>(request)));

            return new Response<CreateProductDto>(_mapper.Map<CreateProductDto>(response), "Product created successfull");
        }
    }
}
