using AutoMapper;
using NetCoreWebApp.Application.Mappings;
using NetCoreWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebApp.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductRequest : IMapFrom<UpdateProductCommand>
    {
        [Required]
        public Guid Id { get; set; }
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

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateProductCommand, UpdateProductRequest>();
        }
    }
}
