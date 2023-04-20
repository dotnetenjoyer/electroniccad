using ElectronicCad.MVVM.Properties.Abstractions;
using System.Reflection;

namespace ElectronicCad.MVVM.Properties.Implementation.PrimitiveProperties;

/// <summary>
/// Primitive property configuration.
/// </summary>
internal class PrimitivePropertyConfiguration : IPropertyConfiguration
{
    /// <inheritdoc />
    public string Name { get; set; }

    /// <inheritdoc />
    public string GroupName => throw new NotImplementedException();

    /// <inheritdoc />
    public PropertyInfo Source { get; set; }
}