using System;
using System.Windows.Media;

namespace ElectronicCad.Desktop.UI.Utils;

/// <summary>
/// Color in HSL format.
/// </summary>
public class HSLColor
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
    /// Luminosity value.
    /// </summary>
    public float Luminosity { get; private set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="hue">Hue.</param>
    /// <param name="saturation">Saturation.</param>
    /// <param name="luminosity">Luminosity.</param>
    public HSLColor(float hue, float saturation, float luminosity)
    {
        Hue = hue;
        Saturation = saturation;
        Luminosity = luminosity;
    }

    /// <summary>
    /// Converts RGB color to HSL color.
    /// </summary>
    /// <param name="color">RGB color.</param>
    /// <returns>HSL color.</returns>
    public static HSLColor FromRgbColor(Color color)
    {
        float r = (color.R / 255.0f);
        float g = (color.G / 255.0f);
        float b = (color.B / 255.0f);

        float min = Math.Min(Math.Min(r, g), b);
        float max = Math.Max(Math.Max(r, g), b);
        float delta = max - min;

        float hue;
        float saturation;
        float luminosity = (max + min) / 2;

        if (delta == 0)
        {
            hue = 0;
            saturation = 0.0f;
        }
        else
        {
            saturation = (luminosity <= 0.5) ? (delta / (max + min)) : (delta / (2 - max - min));
            
            if (r == max)
            {
                hue = ((g - b) / 6) / delta;
            }
            else if (g == max)
            {
                hue = (1.0f / 3) + ((b - r) / 6) / delta;
            }
            else
            {
                hue = (2.0f / 3) + ((r - g) / 6) / delta;
            }

            if (hue < 0)
                hue += 1;
            if (hue > 1)
                hue -= 1;

            hue = (int)(hue * 360);
        }

        return new HSLColor(hue, saturation, luminosity);
    }


    /// <summary>
    /// Creates color based on hsl structure.
    /// </summary>
    /// <returns>Color.</returns>
    public Color ToColor()
    {
        byte r, g, b;

        if (Saturation == 0)
        {
            r = g = b = (byte)(Luminosity * 255);
        }
        else
        {
            float v1, v2;
            float huePercent = Hue / 360;

            v2 = (Luminosity < 0.5)
                ? (Luminosity * (1 + Saturation))
                : ((Luminosity + Saturation) - (Luminosity * Saturation));

            v1 = 2 * Luminosity - v2;

            r = (byte)(255 * HueToRGB(v1, v2, huePercent + (1.0f / 3)));
            g = (byte)(255 * HueToRGB(v1, v2, huePercent));
            b = (byte)(255 * HueToRGB(v1, v2, huePercent - (1.0f / 3)));
        }

        return Color.FromRgb(r, g, b);
    }

    private static float HueToRGB(float v1, float v2, float vH)
    {
        if (vH < 0)
            vH += 1;

        if (vH > 1)
            vH -= 1;

        if ((6 * vH) < 1)
            return (v1 + (v2 - v1) * 6 * vH);

        if ((2 * vH) < 1)
            return v2;

        if ((3 * vH) < 2)
            return (v1 + (v2 - v1) * ((2.0f / 3) - vH) * 6);

        return v1;
    }
}
