namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Typography;

/// <summary>
/// Typography custom section.
/// </summary>
public class TypographyCustomSection : BaseCustomSection<ITypographyProxy, TypographyModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public TypographyCustomSection(ITypographyProxy typographyProxy, IServiceProvider serviceProvider) 
        : base(typographyProxy, serviceProvider)
    {
    }

    /// <inheritdoc />
    protected override void UpdateFromProxyInternal()
    {
        base.UpdateFromProxyInternal();

        Model.Text = Proxy.Text;
        Model.FontSize = Proxy.FontSize;
        Model.FontWeight = Proxy.FontWeight;
        Model.FontFamily = Proxy.FontFamily;
        Model.ForegroundColor = Proxy.ForegroundColor;
    }

    /// <inheritdoc />
    protected override void UpdateProxyInternal()
    {
        base.UpdateProxyInternal();

        Proxy.Text = Model.Text;
        Proxy.FontSize = Model.FontSize;
        Proxy.FontWeight = Model.FontWeight;
        Proxy.FontFamily = Model.FontFamily;
        Proxy.ForegroundColor = Model.ForegroundColor;
    }
}
