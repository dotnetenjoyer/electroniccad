using ElectronicCad.MVVM.Properties.Abstractions;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Typography;

/// <summary>
/// Typography custom section factory.
/// </summary>
public class TypographyCustomSectionFactory : ICustomSectionFactory
{
    private readonly IServiceProvider serviceProvider;

    /// <summary>
    /// Constructor.
    /// </summary>
    public TypographyCustomSectionFactory(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    public bool CanCreate(IProxy proxy)
    {
        if (proxy is ITypographyProxy)
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
            throw new InvalidOperationException($"Cannot create {nameof(TypographyCustomSectionFactory)}.");
        }

        return new TypographyCustomSection(proxy as ITypographyProxy, serviceProvider);
    }
}
