using System.Windows.Media;

namespace ElectronicCad.Desktop.Styles.Utils;

/// <summary>
/// Contains utils methods for colors.
/// </summary>
public class ColorUtils
{
    /// <summary>
    /// Creates color based on hsl structure.
    /// </summary>
    /// <returns>Color.</returns>
    public static Color FromHSL(float hue, float saturation, float lightness)
    {
        byte r, g, b;

        if (saturation == 0)
        {
            r = g = b = (byte)(lightness * 255);
        }
        else
        {
            float v1, v2;
            float huePercent = hue / 360;

            v2 = (lightness < 0.5) 
                ? (lightness * (1 + saturation)) 
                : ((lightness + saturation) - (lightness * saturation));

            v1 = 2 * lightness - v2;

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