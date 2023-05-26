using ElectronicCad.MVVM.Properties.Abstractions;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Transformation;

/// <summary>
/// Factory to create transformation custom section.
/// </summary>
public class TransformationCustomSectionFactory : BaseCustomSectionFactory<TransformationCustomSection, ITransformationProxy> 
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public TransformationCustomSectionFactory(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}
