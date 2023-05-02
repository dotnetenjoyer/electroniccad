using ElectronicCad.MVVM.Properties.Abstractions;
using ElectronicCad.MVVM.Properties.Implementation.CustomSections.Colors;
using ElectronicCad.MVVM.Properties.Implementation.CustomSections.Transformation;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronicCad.MVVM.Properties.Implementation.CustomSections;

/// <summary>
/// Factory of custom section factories.
/// </summary>
public static class CustomSectionFactoriesFactory
{
    /// <summary>
    /// Creates custom section factory.
    /// </summary>
    /// <param name="customSectionType">Custom section type.</param>
    /// <returns>Custom section factory.</returns>
    public static ICustomSectionFactory CreateFactory(IServiceProvider serviceProvider, Type customSectionType)
    {
        if (customSectionType == typeof(TransformationCustomSection))
        {
            var factory = ActivatorUtilities.CreateInstance(serviceProvider, typeof(TransformationCustomSectionFactory));
            return (ICustomSectionFactory)factory;
        }
        else if(customSectionType == typeof(ColorsCustomSection))
        {
            var factory = ActivatorUtilities.CreateInstance(serviceProvider, typeof(ColorsCustomSectionFactory));
            return (ICustomSectionFactory)factory;
        }

        throw new NotSupportedException($"{customSectionType} is not supported.");
    }
}