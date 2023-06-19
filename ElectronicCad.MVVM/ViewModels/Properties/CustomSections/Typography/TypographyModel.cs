using ElectronicCad.Domain.Geometry;
using ElectronicCad.Domain.Geometry.Typography;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Typography;

/// <summary>
/// Typography view model.
/// </summary>
public class TypographyModel : ObservableObject
{
    /// <summary>
    /// Text content.
    /// </summary>
    public string? Text
    {
        get => text;
        set
        {
            SetProperty(ref text, value);
        }
    }

    private string? text;

    /// <summary>
    /// Font size.
    /// </summary>
    public double FontSize
    {
        get => fontSize;
        set
        {
            SetProperty(ref fontSize, value);
        }
    }

    private double fontSize;

    /// <summary>
    /// Foreground color.
    /// </summary>
    public Color ForegroundColor 
    { 
        get => foregroundColor;
        set => SetProperty(ref foregroundColor, value); 
    }

    private Color foregroundColor;

    /// <summary>
    /// Font weight.
    /// </summary>
    public FontWeight FontWeight
    {
        get => fontWeight;
        set => SetProperty(ref fontWeight, value);
    }

    private FontWeight fontWeight;

    /// <summary>
    /// All available font weights.
    /// </summary>
    public IEnumerable<FontWeight> FontWeights { get; private set; }

    /// <summary>
    /// All available font weights.
    /// </summary>
    public IEnumerable<string> FontFamilies { get; private set; }

    /// <summary>
    /// Font family.
    /// </summary>
    public string FontFamily
    {
        get => fontFamily;
        set => SetProperty(ref fontFamily, value);
    }

    private string fontFamily;

    /// <summary>
    /// Text line height.
    /// </summary>
    public double LineHeight
    {
        get => lineHeight;
        set => SetProperty(ref lineHeight, value);
    }

    private double lineHeight;

    /// <summary>
    /// Text letter spacing.
    /// </summary>
    public double LetterSpacing
    {
        get => letterSpacing;
        set => SetProperty(ref letterSpacing, value);
    }

    private double letterSpacing;

    /// <summary>
    /// Text align.
    /// </summary>
    public TextAlign Align 
    { 
        get => align;
        set => SetProperty(ref align, value);
    }

    private TextAlign align;

    /// <summary>
    /// Available text aligments.
    /// </summary>
    public IEnumerable<TextAlign> Alignments { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public TypographyModel(IFontFamilyStorage fontFamilyStorage)
    {
        FontWeights = Enum.GetValues<FontWeight>();
        Alignments = Enum.GetValues<TextAlign>();
        FontFamilies = fontFamilyStorage.GetFontFamilyNames();
    }
}