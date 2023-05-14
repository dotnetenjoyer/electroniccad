namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Text geometry
/// </summary>
public class Text : ContentGeometry
{
    /// <inheritdoc />
    public override string Name => nameof(Text);

    /// <summary>
    /// Text content.
    /// </summary>
    public string Content { get; private set; } = "Test";

    /// <summary>
    /// Constructor.
    /// </summary>
    public Text(Point center, double width, double height) : base(center, width, height)
    {
    }
}