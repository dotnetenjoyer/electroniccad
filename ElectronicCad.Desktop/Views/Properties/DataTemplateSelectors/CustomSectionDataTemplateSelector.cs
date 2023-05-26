using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.DiagramLayoutGrid;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.SizeSection;
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
    public DataTemplate? TransformationSectionTemplate { get; set; }

    /// <summary>
    /// Template for the shape section.
    /// </summary>
    public DataTemplate? ShapeSectionTemplate { get; set; }

    /// <summary>
    /// Template for the shape section.
    /// </summary>
    public DataTemplate? TypographySectionTemplate { get; set; }

    /// <summary>
    /// Template for the layout grid section.
    /// </summary>
    public DataTemplate? LayoutGridSectionTemplate { get; set; }

    /// <summary>
    /// Template for diagram size section.
    /// </summary>
    public DataTemplate? DiagramSizeSectionTemplate { get; set; }

    /// <inheritdoc />
    public override DataTemplate? SelectTemplate(object item, DependencyObject container)
    {
        return item switch
        {
            TransformationCustomSection => TransformationSectionTemplate,
            ShapeCustomSection => ShapeSectionTemplate,
            TypographyCustomSection => TypographySectionTemplate,
            LayoutGridCustomSection => LayoutGridSectionTemplate,
            SizeCustomSection => DiagramSizeSectionTemplate,
            _ => throw new NotSupportedException($"{item.GetType()} is not supported.")
        };
    }
}