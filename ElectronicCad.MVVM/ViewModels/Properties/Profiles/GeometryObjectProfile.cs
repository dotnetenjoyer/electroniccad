using ElectronicCad.MVVM.Properties.Configuration;
using ElectronicCad.MVVM.ViewModels.Properties.Proxies;

namespace ElectronicCad.MVVM.ViewModels.Properties.Profiles;

internal class GeometryObjectProfile : PropertyObjectProfile
{
    public GeometryObjectProfile()
    {
        CreateConfiguration<GeometryObjectPropertyProxy>()
            .Primitives
                .HasPrimitive();
    }
}