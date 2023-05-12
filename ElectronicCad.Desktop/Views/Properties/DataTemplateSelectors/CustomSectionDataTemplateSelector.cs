using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Shape;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Transformation;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ElectronicCad.Desktop.Views.Properties.DataTemplateSelectors;

/// <summary>
/// Helps to determite which template should be used for a custom section.
/// </summary>
internal class CustomSectionDataTemplateSelector : DataTemplateSelector
{
    /// <summary>
    /// Data template for transformation custom section.
    /// </summary>
    public DataTemplate TransformationSectionDataTemplate { get; set; }

    /// <summary>
    /// Data template for shape custom section.
    /// </summary>
    public DataTemplate ShapeSectionDataTemplate { get; set; }

    /// <inheritdoc />
    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        if (item is TransformationCustomSection)
        {
            return TransformationSectionDataTemplate;
        }
        else if (item is ShapeCustomSection)
        {
            return ShapeSectionDataTemplate;
        }

        throw new NotSupportedException($"{item.GetType()} is not supported.");
    }
}