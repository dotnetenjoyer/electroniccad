using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.DiagramLayoutGrid;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Shape;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Transformation;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Typography;
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
    /// Template for the transformation section.
    /// </summary>
    public DataTemplate TransformationSectionTemplate { get; set; }

    /// <summary>
    /// Template for the shape section.
    /// </summary>
    public DataTemplate ShapeSectionTemplate { get; set; }

    /// <summary>
    /// Template for the shape section.
    /// </summary>
    public DataTemplate TypographySectionTemplate { get; set; }

    /// <summary>
    /// Template for the layout grid section.
    /// </summary>
    public DataTemplate LayoutGridSectionTemplate { get; set; }

    /// <inheritdoc />
    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        if (item is TransformationCustomSection)
        {
            return TransformationSectionTemplate;
        }
        else if (item is ShapeCustomSection)
        {
            return ShapeSectionTemplate;
        }
        else if (item is TypographyCustomSection)
        {
            return TypographySectionTemplate;
        }
        else if (item is LayoutGridCustomSection)
        {
            return LayoutGridSectionTemplate;
        }
        else
        {
            throw new NotSupportedException($"{item.GetType()} is not supported.");
        }
    }
}