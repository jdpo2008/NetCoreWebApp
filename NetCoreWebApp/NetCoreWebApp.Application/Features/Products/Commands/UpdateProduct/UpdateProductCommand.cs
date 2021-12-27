using AutoMapper;
using MediatR;
using NetCoreWebApp.Application.Exceptions;
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

namespace NetCoreWebApp.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<Response<UpdateProductDto>>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        [Required]
        public Guid MarcaId { get; set; }
        public string ImageUrl { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response<UpdateProductDto>>
    {
        private readonly IProductRepositoryAsync _ProductRepository;
        private readonly IMapper _mapper;
        public UpdateProductCommandHandler(IProductRepositoryAsync ProductRepository, IMapper mapper)
        {
            _ProductRepository = ProductRepository;
            _mapper = mapper;
        }
        public async Task<Response<UpdateProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateProductRequest>(request);
            var product = await _ProductRepository.GetByIdAsync(command.Id);

            if (product == null)
            {
                throw new ApiException($"Product Not Found.");
            }
            else
            {
                product.Name = command.Name;
                product.Description = command.Description;
                product.Price = command.Price;
                product.Stock = command.Stock;
                product.ImageUrl = command.ImageUrl;

                await _ProductRepository.UpdateAsync(product);

                return new Response<UpdateProductDto>(_mapper.Map<UpdateProductDto>(product), "Product Update Successufull");
            }
        }
    }

}
