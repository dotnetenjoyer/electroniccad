namespace ElectronicCad.Domain.Geometry.Typography;

/// <summary>
/// Font family storage abstraction.
/// </summary>
public interface IFontFamilyStorage
{
    /// <summary>
    /// Returns available font family names.
    /// </summary>
    /// <returns>Available font family names.</returns>
    IEnumerable<string> GetFontFamilyNames();
}