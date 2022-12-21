using System.Windows.Input;

namespace NullSoft.Diagramming.Behaviour;

/// <summary>
/// Behaviour of diagram.
/// </summary>
public interface IDiagramBehaviour
{
    /// <summary>
    /// Start behaviour button.
    /// </summary>
    MouseButton StartButton { get; }
    
    /// <summary>
    /// Cancel behaviour button.
    /// </summary>
    MouseButton CancelButton { get; }

    /// <summary>
    /// Method to start behaviour
    /// </summary>
    void Start();

    /// Method to cancel behaviour
    void Cancel();
}