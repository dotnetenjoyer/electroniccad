namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.DiagramLayout;

/// <summary>
/// Factory to layout custom section.
/// </summary>
public class LayoutCustomSectionFactory : BaseCustomSectionFactory<LayoutCustomSection, ILayoutProxy>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public LayoutCustomSectionFactory(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}