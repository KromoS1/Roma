using AutoMapper;
using Todo.Entities;
using Todo.Web.Controllers;

namespace Todo.Web.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tasks, CreateTaskRequest>().ReverseMap();
            CreateMap<Tasks, UpdateTaskRequest>().ReverseMap();
        }
    }
}