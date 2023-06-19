namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Enumeration of text alignment.
/// </summary>
public enum TextAlign
{
    /// <summary>
    /// Use base direction of the text block.
    /// </summary>
    Auto,

    /// <summary>
    /// Left-aligns text to a x-coord of 0.
    /// </summary>
    Left,

    /// <summary>
    /// Center aligns text.
    /// </summary>
    Center,

    /// <summary>
    /// Right aligns text.
    /// </summary>
    Right,
}