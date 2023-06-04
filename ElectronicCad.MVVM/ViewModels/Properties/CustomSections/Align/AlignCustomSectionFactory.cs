namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Align;

/// <summary>
/// Align custom section factory.
/// </summary>
public class AlignCustomSectionFactory : BaseCustomSectionFactory<AlignCustomSection, IAlignPropertiesProxy>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public AlignCustomSectionFactory(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}