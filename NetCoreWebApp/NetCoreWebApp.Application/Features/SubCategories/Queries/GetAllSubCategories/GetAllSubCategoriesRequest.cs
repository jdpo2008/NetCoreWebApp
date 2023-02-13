using AutoMapper;
using NetCoreWebApp.Application.Mappings;
using NetCoreWebApp.Application.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebApp.Application.Features.SubCategories.Queries.GetAllSubCategories
{
    public class GetAllSubCategoriesRequest : RequestParameter, IMapFrom<GetAllSubCategoriesQuery>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetAllSubCategoriesQuery, GetAllSubCategoriesRequest>();
        }
    }
}
