using ElectronicCad.Domain.Geometry.Typography;

namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Text geometry
/// </summary>
public class Text : ContentGeometry
{
    /// <summary>
    /// Text content.
    /// </summary>
    public string? Content 
    { 
        get => content;
        set
        {
            ValidateModification();
            SetProperty(ref content, value);
        }
    }

    private string? content = "Текст";

    /// <summary>
    /// Font size.
    /// </summary>
    public double FontSize
    {
        get => fontSize;
        set
        {
            ValidateModification();
            SetProperty(ref fontSize, value);
        }
    }

    private double fontSize = 12;

    /// <summary>
    /// Font weight.
    /// </summary>
    public FontWeight FontWeight
    {
        get => fontWeight;
        set
        {
            ValidateModification();
            SetProperty(ref fontWeight, value);
        }
    }

    private FontWeight fontWeight = FontWeight.Regular;

    /// <summary>
    /// Font family.
    /// </summary>
    public string FontFamily 
    {
        get => fontFamily;
        set
        {
            ValidateModification();
            SetProperty(ref fontFamily, value);
        }
    }

    private string fontFamily = "Times New Roman";

    /// <summary>
    /// Constructor.
    /// </summary>
    public Text(Point center, double width, double height) : base(center, width, height)
    {
        Name = "Текст";
        fillColor = Theme.Foreground;
    }

    /// <summary>
    /// Cloning constructor.
    /// </summary>
    /// <param name="cloneFrom">Clone from.</param>
    public Text(Text cloneFrom) : base(cloneFrom)
    {
        content = cloneFrom.Content;
        fontSize = cloneFrom.fontSize;
        fontWeight = cloneFrom.FontWeight;
        fontFamily = cloneFrom.FontFamily;
    }
}