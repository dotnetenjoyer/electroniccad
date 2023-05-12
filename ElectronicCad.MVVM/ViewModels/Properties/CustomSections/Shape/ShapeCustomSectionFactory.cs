using ElectronicCad.MVVM.Properties.Abstractions;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Shape;

/// <summary>
/// Creates shape custom sections.
/// </summary>
public class ShapeCustomSectionFactory : ICustomSectionFactory
{
    /// <inheritdoc />
    public bool CanCreate(IProxy proxy)
    {
        if (proxy is IShapeProxy)
        {
            return true;
        }

        return false;

    }

    /// <inheritdoc />
    public ICustomSection Create(IProxy proxy)
    {
        if (!CanCreate(proxy))
        {
            throw new InvalidOperationException($"Cannot create {nameof(ShapeCustomSection)}.");
        }

        return new ShapeCustomSection(proxy as IShapeProxy);
    }
}
