using AutoMapper;
using ElectronicCad.Domain.Geometry;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.SizeSection.Models;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.SizeSection;

/// <summary>
/// Configurates mapper.
/// </summary>
public class MapperProfile : Profile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public MapperProfile()
    {
        CreateMap<Size, SizeModel>()
            .ReverseMap();
    }
}