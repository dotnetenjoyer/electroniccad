﻿using ElectronicCad.MVVM.Properties.Configuration;
using ElectronicCad.MVVM.ViewModels.Properties.Proxies;

namespace ElectronicCad.MVVM.ViewModels.Properties.Profiles;

/// <summary>
/// Creates properties configuration for geometry group.
/// </summary>
internal class GeometryGroupProfile : PropertyObjectProfile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public GeometryGroupProfile()
    {
        CreateConfiguration<GeometryGroupPropertiesProxy>();
    }
}