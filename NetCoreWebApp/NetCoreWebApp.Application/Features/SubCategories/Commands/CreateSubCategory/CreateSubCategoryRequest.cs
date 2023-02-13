using AutoMapper;
using NetCoreWebApp.Application.Mappings;
using NetCoreWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebApp.Application.Features.SubCategories.Commands.CreateSubCategory
{
    public class CreateSubCategoryRequest : IMapFrom<CreateSubCategoryCommand>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateSubCategoryCommand, SubCategory>();
        }
    
    }
}
