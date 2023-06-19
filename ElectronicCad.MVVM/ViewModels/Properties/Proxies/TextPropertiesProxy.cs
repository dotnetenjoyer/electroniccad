using ElectronicCad.Domain.Geometry;
using ElectronicCad.Domain.Geometry.Typography;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Typography;

namespace ElectronicCad.MVVM.ViewModels.Properties.Proxies;

/// <summary>
/// Text properties proxy
/// </summary>
public class TextPropertiesProxy : GeometryObjectPropertiesProxy<Text>, ITypographyPropertiesProxy
{
    /// <inheritdoc />
    public string? Text { get; set; }

    /// <inheritdoc />
    public double FontSize { get; set; }

    /// <inheritdoc />
    public FontWeight FontWeight { get; set; }

    /// <inheritdoc />
    public string FontFamily { get; set; }

    /// <inheritdoc />
    public Color ForegroundColor { get; set; }

    /// <inheritdoc />
    public double LineHeight { get; set; }

    /// <inheritdoc />
    public double LetterSpacing { get; set; }

    /// <inheritdoc />
    public TextAlign Align { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="text">Text object.</param>
    public TextPropertiesProxy(Text text) : base(text)
    {
    }

    /// <inheritdoc />
    public override void UpdateFromSource()
    {
        base.UpdateFromSource();
     
        Text = Source.Content;
        FontSize = Source.FontSize;
        FontWeight = Source.FontWeight;
        FontFamily = Source.FontFamily;
        ForegroundColor = Source.FillColor;
        LineHeight = Source.LineHeight;
        LetterSpacing = Source.LetterSpacing;
        Align = Source.Align;
    }

    /// <inheritdoc />
    protected override void UpdateSourceInternal()
    {
        base.UpdateSourceInternal();
        
        Source.Content = Text;
        Source.FontSize = FontSize;
        Source.FontWeight = FontWeight;
        Source.FontFamily = FontFamily;
        Source.FillColor = ForegroundColor;
        Source.LineHeight = LineHeight;
        Source.LetterSpacing = LetterSpacing;
        Source.Align = Align;
    }
}
