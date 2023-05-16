namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Image diagram item.
/// </summary>
public class Image : ContentGeometry
{
    /// <summary>
    /// Image to reference.
    /// </summary>
    public string Reference 
    { 
        get => refernce;
        set 
        {
            ValidateModification();
            refernce = value;
        } 
    }

    private string refernce = "C:\\Users\\null\\Downloads\\test.jpg";

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="center">Center position.</param>
    /// <param name="width">Width.</param>
    /// <param name="height">Height.</param>
    /// <param name="isTemporary">Indicates whether image is temporary.</param>
    public Image(Point center, double width, double height, bool isTemporary = false) : base(center, width, height, isTemporary)
    {
    }
}
