using Microsoft.Extensions.DependencyInjection;
using ElectronicCad.MVVM.Properties.Abstractions;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.DiagramLayoutGrid;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.SizeSection;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Shape;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Transformation;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Typography;

namespace ElectronicCad.MVVM.Properties.Implementation.CustomSections;

/// <summary>
/// Factory to create custom section factories.
/// </summary>
public class CustomSectionFactoriesFactory : ICustomSectionFactoriesFactory
{
    private readonly static Dictionary<Type, Type> Factories = new()
    {
        [typeof(TransformationCustomSection)] = typeof(TransformationCustomSectionFactory),
        [typeof(ShapeCustomSection)] = typeof(ShapeCustomSectionFactory),
        [typeof(TypographyCustomSection)] = typeof(TypographyCustomSectionFactory),
        [typeof(LayoutGridCustomSection)] = typeof(LayoutGridCustomSectionFactory),
        [typeof(SizeCustomSection)] = typeof(SizeCustomSectionFactory), 
    };

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
        var factoryType = Factories[customSectionType];
        var factory = (ICustomSectionFactory) ActivatorUtilities.CreateInstance(serviceProvider, factoryType);
        return factory;            
    }
}