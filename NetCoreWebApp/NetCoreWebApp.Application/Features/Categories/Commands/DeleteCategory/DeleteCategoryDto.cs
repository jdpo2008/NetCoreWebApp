using AutoMapper;
using NetCoreWebApp.Application.Mappings;
using NetCoreWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebApp.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryDto : IMapFrom<Category>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, DeleteCategoryDto>();
        }
    }
}
