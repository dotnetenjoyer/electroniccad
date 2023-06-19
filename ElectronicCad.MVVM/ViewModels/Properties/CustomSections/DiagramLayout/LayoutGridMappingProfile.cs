using AutoMapper;
using ElectronicCad.Domain.Geometry.Layouts;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.DiagramLayout.Models;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.DiagramLayout;

/// <summary>
/// Configurates layout mapper.
/// </summary>
public class LayoutMappingProfile : Profile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public LayoutMappingProfile()
    {
        CreateMap<Layout, LayoutModel>()
            .Include<LayoutColumn, LayoutColumnModel>()
            .Include<LayoutRow, LayoutRowModel>()
            .Include<LayoutGrid, LayoutGridModel>()
            .ReverseMap();

        CreateMap<LayoutColumn, LayoutColumnModel>()
            .ReverseMap();

        CreateMap<LayoutRow, LayoutRowModel>()
            .ReverseMap();

        CreateMap<LayoutGrid, LayoutGridModel>()
            .ReverseMap();
    }
}