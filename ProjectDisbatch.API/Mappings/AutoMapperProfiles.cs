using AutoMapper;
using ProjectDisbatch.API.Models.Domain;
using ProjectDisbatch.API.Models.DTO;

namespace ProjectDisbatch.API.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            //You can use ReverseMap to create a bidirectional mapping between source and destination and destination and source
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<AddDepartmentRequestDto, Department>().ReverseMap();
            CreateMap<UpdateDepartmentRequestDto, Department>().ReverseMap();

            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<AddProjectRequestDto, Project>().ReverseMap();
            CreateMap<UpdateProjectRequestDto, Project>().ReverseMap();

            CreateMap<ProjectType, ProjectTypeDto>().ReverseMap();
        }
    }
}
