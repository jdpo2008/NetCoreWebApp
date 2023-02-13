using AutoMapper;
using NetCoreWebApp.Application.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebApp.Application.Features.SubCategories.Commands.UpdateSubCategory
{
    public class UpdateSubCategoryRequest : IMapFrom<UpdateSubCategoryCommand>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateSubCategoryCommand, UpdateSubCategoryRequest>();
        }
    }
}
