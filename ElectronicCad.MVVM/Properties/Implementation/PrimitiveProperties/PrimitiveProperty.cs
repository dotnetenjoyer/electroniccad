using System.Reflection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using ElectronicCad.MVVM.Properties.Abstractions;

namespace ElectronicCad.MVVM.Properties.Implementation.PrimitiveProperties;

/// <summary>
/// Primitive property.
/// </summary>
public class PrimitiveProperty<TValue> : ObservableObject, IProperty
{
    /// <inheritdoc />
    public string Name { get; set; }

    /// <inheritdoc />
    public bool IsReadOnly { get; }

    /// <summary>
    /// Value of property.
    /// </summary>
    public TValue Value
    {
        get => propertyValue;
        set => SetProperty(ref propertyValue, value);
    }

    private TValue propertyValue;

    public TValue OriginValue { get; private set; }

    /// <summary>
    /// Source property.
    /// </summary>
    public PropertyInfo SourceProperty { get; }

    /// <summary>
    /// Source object.
    /// </summary>
    public object SourceObject { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="sourceObject">Source object.</param>
    /// <param name="sourceProperty">Source property.</param>
    public PrimitiveProperty(object sourceObject, PropertyInfo sourceProperty)
    {
        SourceObject = sourceObject;
        SourceProperty = sourceProperty;

        UpdateFromSourceObject();
    }

    void UpdateFromSourceObject()
    {
        var value = SourceProperty.GetValue(SourceObject);
        Value = (TValue)value;
    }

    void UpdateSourceObject()
    {

    }
}
