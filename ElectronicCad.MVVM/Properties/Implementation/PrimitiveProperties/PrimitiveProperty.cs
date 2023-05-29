using System.Reflection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using ElectronicCad.MVVM.Properties.Abstractions;
using System.Runtime.CompilerServices;

namespace ElectronicCad.MVVM.Properties.Implementation.PrimitiveProperties;

/// <summary>
/// Primitive property.
/// </summary>
public class PrimitiveProperty<TValue> : ObservableObject, IProperty
{
    private readonly PropertyInfo sourceProperty;
    private readonly IPropertiesProxy sourceObject;

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
    /// Value presentation format.
    /// </summary>
    public string ValueFormat 
    { 
        get => valueFormat; 
        set => SetProperty(ref valueFormat, value); 
    }

    private string valueFormat;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="sourceObject">Source object.</param>
    /// <param name="sourceProperty">Source property.</param>
    public PrimitiveProperty(IPropertiesProxy sourceObject, PropertyInfo sourceProperty, string name)
    {
        this.sourceObject = sourceObject;
        this.sourceProperty = sourceProperty;
        Name = name;

        UpdateFromSource();

        sourceObject.Updated += HandleSourceChanges;
    }

    private void UpdateFromSource()
    {
        var proxyPropertyValue = sourceProperty.GetValue(sourceObject);
        if (proxyPropertyValue is TValue value)
        {
            SetProperty(ref propertyValue, value, nameof(Value));
        }
    }

    private void UpdateSource()
    {
        sourceObject.Updated -= HandleSourceChanges;
        
        sourceProperty.SetValue(sourceObject, Value);
        sourceObject.UpdateSource();
 
        sourceObject.Updated += HandleSourceChanges;
    }

    private void HandleSourceChanges(object? sender, EventArgs eventArgs)
    {
        UpdateFromSource();
    }
}
