using AutoMapper;
using ElectronicCad.MVVM.ViewModels.Projects.Models;
using ElectronicCad.UseCases.Projects.CreateProject;
using ElectronicCad.UseCases.Projects.UpdateProject;

namespace ElectronicCad.MVVM.ViewModels.Projects
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProjectPropertiesModel, CreateProjectCommand>();
            CreateMap<ProjectPropertiesModel, UpdateProjectCommand>();
        }
    }
}
