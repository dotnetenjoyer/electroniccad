using ElectronicCad.MVVM.Properties.Abstractions;

namespace ElectronicCad.MVVM.Properties;

/// <summary>
/// Contains properties of object.
/// </summary>
public class PropertyObject
{
    public IEnumerable<IProperty> Properties { get; set; }

    ///// <summary>
    ///// Properties groups.
    ///// </summary>
    //public IEnumerable<PropertyGroup> Groups { get; set; }
}

/// <summary>
/// Group of properties.
/// </summary>
//public class PropertyGroup
//{
//    public string GroupName { get; }

//    public IEnumerable<IProperty> Properties { get; set; }
//}