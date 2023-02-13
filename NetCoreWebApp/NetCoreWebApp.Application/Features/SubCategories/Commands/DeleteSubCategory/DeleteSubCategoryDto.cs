using AutoMapper;
using NetCoreWebApp.Application.Mappings;
using NetCoreWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebApp.Application.Features.SubCategories.Commands.DeleteSubCategory
{
    public class DeleteSubCategoryDto : IMapFrom<SubCategory>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SubCategory, DeleteSubCategoryDto>();
        }
    }
}
