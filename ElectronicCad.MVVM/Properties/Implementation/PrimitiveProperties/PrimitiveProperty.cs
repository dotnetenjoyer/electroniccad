using System.Reflection;
using ElectronicCad.MVVM.Properties.Abstractions;

namespace ElectronicCad.MVVM.Properties.Implementation.PrimitiveProperties;

/// <summary>
/// Primitive property.
/// </summary>
public class PrimitiveProperty : IProperty
{
    /// <inheritdoc />
    public string Name { get; set; }

    /// <inheritdoc />
    public bool IsReadOnly { get; }

    /// <summary>
    /// Source property.
    /// </summary>
    public PropertyInfo SourceProperty { get; }
}
