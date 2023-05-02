﻿using ElectronicCad.MVVM.Properties.Configuration;
using ElectronicCad.MVVM.ViewModels.Properties.Proxies;

namespace ElectronicCad.MVVM.ViewModels.Properties.Profiles;

/// <summary>
/// Creates configuration for line.
/// </summary>
internal class LineObjectProfile : PropertyObjectProfile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public LineObjectProfile()
    {
        CreateConfiguration<LinePropertyProxy>()
            .HasPrimitive(source => source.X)
            .HasPrimitive(source => source.Y)
            .HasPrimitive(source => source.Width)
            .HasPrimitive(source => source.Height)
            .HasPrimitive(source => source.StrokeColor);
    }
}
