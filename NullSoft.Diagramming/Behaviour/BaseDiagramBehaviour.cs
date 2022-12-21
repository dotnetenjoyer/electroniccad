using System.Diagnostics;
using System.Windows.Input;

namespace NullSoft.Diagramming.Behaviour;

/// <summary>
/// Base diagram behaviour.
/// </summary>
public abstract class BaseDiagramBehaviour : IDiagramBehaviour
{
    private readonly Diagram _diagram;

    /// <inheritdoc/> 
    public virtual MouseButton StartButton => MouseButton.Left;

    /// <inheritdoc/>
    public virtual MouseButton CancelButton => MouseButton.Right;
    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="diagram"></param>
    protected BaseDiagramBehaviour(Diagram diagram)
    {
        _diagram = diagram;
    }
    
    /// <inheritdoc/>
    public virtual void Start()
    {
        _diagram.MouseLeftButtonDown += (sender, args) =>
        {
            Debug.WriteLine("Click");
        };
    }

    /// <inheritdoc/>
    public virtual void Cancel()
    {
        throw new System.NotImplementedException();
    }
}