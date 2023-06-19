namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Typography;

/// <summary>
/// Typography custom section.
/// </summary>
public class TypographyCustomSection : BaseCustomSection<ITypographyPropertiesProxy, TypographyModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public TypographyCustomSection(ITypographyPropertiesProxy typographyProxy, IServiceProvider serviceProvider) 
        : base(typographyProxy, serviceProvider)
    {
        UpdateFromProxy();
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
        Model.LineHeight = Proxy.LineHeight;
        Model.LetterSpacing = Proxy.LetterSpacing;
        Model.Align = Proxy.Align;
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
        Proxy.LineHeight = Model.LineHeight;
        Proxy.LetterSpacing = Model.LetterSpacing;
        Proxy.Align = Model.Align;
    }
}
