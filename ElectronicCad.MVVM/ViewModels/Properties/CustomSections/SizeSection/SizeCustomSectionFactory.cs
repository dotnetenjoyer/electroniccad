namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.SizeSection;

/// <summary>
/// Factory of size custom section.
/// </summary>
public class SizeCustomSectionFactory : BaseCustomSectionFactory<SizeCustomSection, ISizeProxy>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="serviceProvider">Service provider.</param>
    public SizeCustomSectionFactory(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
} 