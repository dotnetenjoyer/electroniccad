using ElectronicCad.MVVM.Properties.Abstractions;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.DiagramLayoutGrid;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Shape;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Transformation;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Typography;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronicCad.MVVM.Properties.Implementation.CustomSections;

/// <inheritdoc cref="ICustomSectionFactoriesFactory">
public class CustomSectionFactoriesFactory : ICustomSectionFactoriesFactory
{
    private readonly IServiceProvider serviceProvider;

    /// <summary>
    /// Constructor.
    /// </summary>
    public CustomSectionFactoriesFactory(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    public ICustomSectionFactory CreateFactory(Type customSectionType)
    {
        ICustomSectionFactory factory;

        if (customSectionType == typeof(TransformationCustomSection))
        {
            factory = (ICustomSectionFactory)ActivatorUtilities.CreateInstance(serviceProvider, typeof(TransformationCustomSectionFactory));
        }
        else if (customSectionType == typeof(ShapeCustomSection))
        {
            factory = (ICustomSectionFactory)ActivatorUtilities.CreateInstance(serviceProvider, typeof(ShapeCustomSectionFactory));
        }
        else if (customSectionType == typeof(TypographyCustomSection))
        {
            factory = (ICustomSectionFactory)ActivatorUtilities.CreateInstance(serviceProvider, typeof(TypographyCustomSectionFactory));
        }
        else if (customSectionType == typeof(LayoutGridCustomSection))
        {
            factory = (ICustomSectionFactory)ActivatorUtilities.CreateInstance(serviceProvider, typeof(LayoutGridCustomSectionFactory));
        }
        else
        {
            throw new NotSupportedException($"{customSectionType} is not supported.");
        }

        return factory;
    }
}