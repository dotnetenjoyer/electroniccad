﻿using ElectronicCad.MVVM.Properties.Configuration;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Shape;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Transformation;
using ElectronicCad.MVVM.ViewModels.Properties.Proxies;

namespace ElectronicCad.MVVM.ViewModels.Properties.Profiles;

/// <summary>
/// Creates configuration for rectangle.
/// </summary>
internal class PolygoneObjectProfile : PropertyObjectProfile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PolygoneObjectProfile()
    {
        CreateConfiguration<PolygonPropertiesProxy>()
            .HasCustomSection<TransformationCustomSection>()
            .HasCustomSection<ShapeCustomSection>()
            .HasPrimitive(source => source.CornerRadius);
    }
}
