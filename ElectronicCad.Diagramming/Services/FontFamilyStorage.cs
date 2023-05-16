using System.Collections.Generic;
using SkiaSharp;
using ElectronicCad.Domain.Geometry.Typography;

namespace ElectronicCad.Diagramming.Services;

/// <summary>
/// Implementation of font family storage.
/// </summary>
public class FontFamilyStorage : IFontFamilyStorage
{
    /// <inheritdoc />
    public IEnumerable<string> GetFontFamilyNames()
    {
        var fontManager = SKFontManager.Default;
        return fontManager.GetFontFamilies();
    }
}