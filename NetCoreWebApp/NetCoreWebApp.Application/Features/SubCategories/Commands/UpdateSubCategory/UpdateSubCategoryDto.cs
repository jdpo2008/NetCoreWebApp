using AutoMapper;
using NetCoreWebApp.Application.Mappings;
using NetCoreWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebApp.Application.Features.SubCategories.Commands.UpdateSubCategory
{
    public class UpdateSubCategoryDto : IMapFrom<Category>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime UpdateAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SubCategory, UpdateSubCategoryDto>();
        }
    }
}
