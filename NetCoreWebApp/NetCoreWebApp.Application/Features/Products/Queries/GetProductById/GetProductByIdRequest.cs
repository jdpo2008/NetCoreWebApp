using AutoMapper;
using NetCoreWebApp.Application.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebApp.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdRequest : IMapFrom<GetProductByIdQuery>
    {
        public Guid Id { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetProductByIdQuery, GetProductByIdRequest>();
        }
    }
}
