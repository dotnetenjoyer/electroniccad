using SkiaSharp;

namespace ElectronicCad.Diagramming.Drawing;

/// <summary>
/// Describe mouse state.
/// </summary>
internal class MouseParameters
{
    /// <summary>
    /// Mouse position in global canvas coordinates.
    /// </summary>
    public SKPoint Position { get; init; }

    /// <summary>
    /// Left button state.
    /// </summary>

    public MouseButtonState LeftButton { get; init; }

    /// <summary>
    /// Rigth button state.
    /// </summary>
    public MouseButtonState RightButton { get; init; }
}

/// <summary>
/// Describe mouse state with position delta.
/// </summary>
internal class MovingMouseParameters : MouseParameters
{
    /// <summary>
    /// Delta between the current position and the previous position.
    /// </summary>
    public SKPoint Delta { get; set; }
}

/// <summary>
/// Mouse button states.
/// </summary>
internal enum MouseButtonState
{
    Released,
    Pressed
}