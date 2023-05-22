using System;
using System.Windows.Media;

namespace ElectronicCad.Desktop.UI.Utils;

/// <summary>
/// Color in HSV format.
/// </summary>
public class HSVColor
{
    /// <summary>
    /// Hue value.
    /// </summary>
    public float Hue { get; private set; }

    /// <summary>
    /// Saturation value.
    /// </summary>
    public float Saturation { get; private set; }

    /// <summary>
    /// Value.
    /// </summary>
    public float Value { get; private set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="hue">Hue.</param>
    /// <param name="saturation">Satruration.</param>
    /// <param name="value">Value.</param>
    public HSVColor(float hue, float saturation, float value)
    {
        Hue = hue;
        Saturation = saturation;
        Value = value;
    }

    /// <summary>
    /// Creates HSV color from RGB color.
    /// </summary>
    /// <param name="color">RGB color.</param>
    /// <returns>HSV color.</returns>
    public static HSVColor FromColor(Color color)
    {
        int max = Math.Max(color.R, Math.Max(color.G, color.B));
        int min = Math.Min(color.R, Math.Min(color.G, color.B));

        var hsl = HSLColor.FromRgbColor(color);
        var saturation = (max == 0) ? 0 : 1f - (1f * min / max);
        var value = max / 255f;

        return new HSVColor(hsl.Hue, saturation, value);
    }
}