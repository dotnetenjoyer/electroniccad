namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Shape;

/// <summary>
/// Factory to shape custom section.
/// </summary>
public class ShapeCustomSectionFactory : BaseCustomSectionFactory<ShapeCustomSection , IShapeProxy>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public ShapeCustomSectionFactory(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        
    }
}
