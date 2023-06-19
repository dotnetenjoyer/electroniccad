using Microsoft.Extensions.DependencyInjection;
using ElectronicCad.MVVM.Properties.Abstractions;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.DiagramLayout;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.SizeSection;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Shape;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Transformation;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Typography;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Align;

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
        [typeof(LayoutCustomSection)] = typeof(LayoutCustomSectionFactory),
        [typeof(SizeCustomSection)] = typeof(SizeCustomSectionFactory), 
        [typeof(AlignCustomSection)] = typeof(AlignCustomSectionFactory), 
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