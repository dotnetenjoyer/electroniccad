using ElectronicCad.MVVM.Properties.Abstractions;

namespace ElectronicCad.MVVM.Properties.Implementation.CustomSections.Colors;

/// <summary>
/// Creates colors custom sections.
/// </summary>
public class ColorsCustomSectionFactory : ICustomSectionFactory
{
    /// <inheritdoc />
    public bool CanCreate(IProxy proxy)
    {
        if (proxy is object)
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
            throw new InvalidOperationException($"Cannot create {nameof(ColorsCustomSection)}.");
        }

        return new ColorsCustomSection();
    }
}
