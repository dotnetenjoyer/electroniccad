using System.Windows.Input;

namespace NullSoft.Diagramming.Modes;

/// <summary>
/// The diagram mode contains logic to interact with diagram.
/// </summary>
public interface IDiagramMode
{
    /// <summary>
    /// Mode cursor.
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
    /// Method that invokes to initialize mode.
    /// </summary>
    /// <param name="diagram">Diagram control.</param>
    void Initialize(Diagram diagram);
    
    /// <summary>
    ///M Method that invokes to finalize mode.
    /// </summary>
    void Finalize();
}