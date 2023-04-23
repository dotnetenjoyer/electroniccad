using System;
using System.Windows;
using System.Windows.Controls;

namespace ElectronicCad.Desktop.Views.Properties.DataTemplateSelectors;

/// <summary>
/// Helps to determite which template should be used for a primitive property.
/// </summary>
internal class PrimitivePropertyDataTemplateSelector : DataTemplateSelector
{
    /// <summary>
    /// Data template for inputing values.
    /// </summary>
    public DataTemplate InputValueDataTemplate { get; set; }

    /// <summary>
    /// Data template for enum values.
    /// </summary>
    public DataTemplate EnumValueDataTemplate { get; set; }

    /// <summary>
    /// Data template for boolean values.
    /// </summary>
    public DataTemplate BooleanValueDataTemplate { get; set; }

    /// <inheritdoc />
    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        var valueType = GetTypeFromGeneric(item.GetType());

        if (valueType.IsEnum)
        {
            return EnumValueDataTemplate;
        }
        else if(valueType == typeof(bool))
        {
            return BooleanValueDataTemplate;
        }
        else
        {
            return InputValueDataTemplate;
        }
    }

    private Type GetTypeFromGeneric(Type type)
    {
        if (type.IsGenericType)
        {
            var genericArgument = type.GetGenericArguments()[0];
            type = GetTypeFromGeneric(genericArgument);
        }

        return type;
    }
}