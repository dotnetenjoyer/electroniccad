namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.DiagramLayoutGrid;

/// <summary>
/// Factory to layout grid custom section.
/// </summary>
public class LayoutGridCustomSectionFactory : BaseCustomSectionFactory<LayoutGridCustomSection, ILayoutGridProxy>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public LayoutGridCustomSectionFactory(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}