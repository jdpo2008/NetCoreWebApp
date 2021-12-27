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

namespace NetCoreWebApp.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<Response<DeleteProductDto>>
    {
        [Required]
        public Guid Id { get; set; }

    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Response<DeleteProductDto>>
    {
        private readonly IProductRepositoryAsync _productRepository;
        private readonly IMapper _mapper;
        public DeleteProductCommandHandler(IProductRepositoryAsync productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;

        }
        public async Task<Response<DeleteProductDto>> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(command.Id);
            if (product == null) throw new ApiException($"Product Not Found.");
            await _productRepository.RemoveAsync(product);

            var response = _mapper.Map<DeleteProductDto>(product);

            return new Response<DeleteProductDto>(_mapper.Map<DeleteProductDto>(product), "Product remove successful");
        }
    }
}
