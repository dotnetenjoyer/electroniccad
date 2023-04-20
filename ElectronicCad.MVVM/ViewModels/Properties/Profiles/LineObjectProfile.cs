using ElectronicCad.MVVM.Properties.Configuration;
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
            .HasPrimitive("X1")
            .HasPrimitive("Y1")
            .HasPrimitive("X2")
            .HasPrimitive("Y2");
    }
}