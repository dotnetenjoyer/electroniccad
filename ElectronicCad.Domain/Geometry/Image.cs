namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Image diagram item.
/// </summary>
public class Image : ContentGeometry
{
    /// <inheritdoc />
    public override string Name { get; init; } = nameof(Image);

    /// <summary>
    /// Image to reference.
    /// </summary>
    public string Reference 
    { 
        get => reference;
        set 
        {
            ValidateModification();
            reference = value;
        } 
    }

    private string reference;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="center">Center position.</param>
    /// <param name="width">Width.</param>
    /// <param name="height">Height.</param>
    /// <param name="reference">Reference to image.</param>
    public Image(Point center, double width, double height, string reference) : base(center, width, height)
    {
        this.reference = reference;
    }

    /// <summary>
    /// Cloning constructor.
    /// </summary>
    /// <param name="cloneFrom">Clone from.</param>
    public Image(Image cloneFrom) : base(cloneFrom)
    {
        reference = cloneFrom.Reference;
    }
}
