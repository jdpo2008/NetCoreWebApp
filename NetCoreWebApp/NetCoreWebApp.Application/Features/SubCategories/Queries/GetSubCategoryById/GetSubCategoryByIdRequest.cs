using AutoMapper;
using NetCoreWebApp.Application.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebApp.Application.Features.SubCategories.Queries.GetSubCategoryById
{
    public class GetSubCategoryByIdRequest : IMapFrom<GetSubCategoryByIdQuery>
    {
        public Guid Id { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetSubCategoryByIdQuery, GetSubCategoryByIdRequest>();
        }
    }
}
