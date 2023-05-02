using System.Reflection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using ElectronicCad.MVVM.Properties.Abstractions;

namespace ElectronicCad.MVVM.Properties.Implementation.PrimitiveProperties;

/// <summary>
/// Primitive property.
/// </summary>
public class PrimitiveProperty<TValue> : ObservableObject, IProperty
{
    private readonly PropertyInfo sourceProperty;
    private readonly IProxy sourceObject;

    /// <inheritdoc />
    public string Name { get; init; }

    /// <inheritdoc />
    public string GroupName { get; init; } = "Properties";

    /// <inheritdoc />
    public bool IsReadOnly { get; init; }

    /// <summary>
    /// Value of property.
    /// </summary>
    public TValue Value
    {
        get => propertyValue;
        set
        {
            SetProperty(ref propertyValue, value);
            UpdateSource();
        }
    }

    private TValue propertyValue;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="sourceObject">Source object.</param>
    /// <param name="sourceProperty">Source property.</param>
    public PrimitiveProperty(IProxy sourceObject, PropertyInfo sourceProperty, string name)
    {
        this.sourceObject = sourceObject;
        this.sourceProperty = sourceProperty;
        Name = name;

        //TODO: optimize initialization;
        UpdateFromSource();
        sourceObject.Updated += HandleSourceChanges;
    }

    private void HandleSourceChanges(object? sender, EventArgs eventArgs)
    {
        UpdateFromSource();
    }

    private void UpdateFromSource()
    {
        var propertyValue = sourceProperty.GetValue(sourceObject);
        
        if (propertyValue is TValue value)
        {
            Value = value;
        }
    }

    private void UpdateSource()
    {
        sourceProperty.SetValue(sourceObject, Value);
        sourceObject.UpdateEntity();
    }
}
