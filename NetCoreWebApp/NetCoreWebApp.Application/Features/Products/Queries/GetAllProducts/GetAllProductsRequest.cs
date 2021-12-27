using AutoMapper;
using NetCoreWebApp.Application.Mappings;
using NetCoreWebApp.Application.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebApp.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsRequest : RequestParameter, IMapFrom<GetAllProductsQuery>
    {

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetAllProductsQuery, GetAllProductsRequest>();
        }
    }
}
