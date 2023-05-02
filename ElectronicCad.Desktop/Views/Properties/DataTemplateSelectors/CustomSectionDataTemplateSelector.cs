using ElectronicCad.MVVM.Properties.Implementation.CustomSections.Colors;
using ElectronicCad.MVVM.Properties.Implementation.CustomSections.Transformation;
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
    /// Data template for colors custom section.
    /// </summary>
    public DataTemplate ColorsSectionDataTemplate { get; set; }

    /// <inheritdoc />
    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        if (item is TransformationCustomSection)
        {
            return TransformationSectionDataTemplate;
        }
        else if (item is ColorsCustomSection)
        {
            return ColorsSectionDataTemplate;
        }

        throw new NotSupportedException($"{item.GetType()} is not supported.");
    }
}