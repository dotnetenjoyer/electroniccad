using AutoMapper;
using ElectronicCad.Domain.Geometry.LayoutGrids;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.DiagramLayoutGrid.Models;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.DiagramLayoutGrid;

/// <summary>
/// Configurates layout grid mapper.
/// </summary>
public class LayoutGridMappingProfile : Profile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public LayoutGridMappingProfile()
    {
        CreateMap<LayoutGrid, LayoutGridModel>()
            .Include<ColumnLayoutGrid, ColumnLayoutGridModel>()
            .Include<RowLayoutGrid, RowLayoutGridModel>()
            .ReverseMap();

        CreateMap<ColumnLayoutGrid, ColumnLayoutGridModel>()
            .ReverseMap();
        CreateMap<RowLayoutGrid, RowLayoutGridModel>()
            .ReverseMap();
    }
}