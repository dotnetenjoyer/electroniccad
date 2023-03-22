using AutoMapper;
using ElectronicCad.MVVM.ViewModels.Project.Models;
using ElectronicCad.UseCases.Projects.CreateProject;
using ElectronicCad.UseCases.Projects.UpdateProject;

namespace ElectronicCad.MVVM.ViewModels.Project
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
