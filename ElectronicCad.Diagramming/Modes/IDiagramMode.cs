using System.Windows.Input;

namespace ElectronicCad.Diagramming.Modes;

/// <summary>
/// The diagram mode contains logic to interact with diagram.
/// </summary>
public interface IDiagramMode
{
    /// <summary>
    /// Diagram mode cursor.
    /// </summary>
    Cursor Cursor { get; }
    
    /// <summary>
    /// Start mode button.
    /// </summary>
    MouseButton PrimaryButton { get; }
    
    /// <summary>
    /// Cancel mode button.
    /// </summary>
    MouseButton SecondaryButton { get; }

    /// <summary>
    /// The method to initialize diagram mode.
    /// </summary>
    /// <param name="diagram">Diagram control.</param>
    void Initialize(Diagram diagram);
    
    /// <summary>
    /// The method to finalize mode.
    /// </summary>
    void Finish();
}