using System.Windows.Input;

namespace NullSoft.Diagramming.Modes;

/// <summary>
/// Base diagram mode.
/// </summary>
public abstract class BaseDiagramMode : IDiagramMode
{
    private Cursor _originalCursor;
    
    /// <summary>
    /// Diagram control.
    /// </summary>
    protected Diagram Diagram { get; private set; }
    
    /// <inheritdoc/>
    public virtual Cursor Cursor => Cursors.Cross;

    /// <inheritdoc/>
    public virtual MouseButton PrimaryButton => MouseButton.Left;

    /// <inheritdoc/>
    public virtual MouseButton SecondaryButton => MouseButton.Right;
    
    /// <inheritdoc/>
    public void Initialize(Diagram diagram)
    {
        Diagram = diagram;
        
        Diagram.MouseDown += HandleDiagramMouseDown;
        Diagram.MouseUp += HandleDiagramMouseUp;
        Diagram.MouseMove += HandleDiagramMouseMove;
        _originalCursor = Diagram.Cursor;
        Diagram.Cursor = Cursor;
    }

    private void HandleDiagramMouseDown(object sender, MouseButtonEventArgs eventArgs)
    {
        if (eventArgs.ChangedButton == PrimaryButton)
        {
            ProcessPrimaryButtonDown(eventArgs);
        }
        else if(eventArgs.ChangedButton == SecondaryButton)
        {
            ProcessSecondaryButtonDown(eventArgs);
        }
    }
    
    private void HandleDiagramMouseUp(object sender, MouseButtonEventArgs eventArgs)
    {
        if (eventArgs.ChangedButton == PrimaryButton)
        {
            ProcessPrimaryButtonUp(eventArgs);
        }
        else if(eventArgs.ChangedButton == SecondaryButton)
        {
            ProcessSecondaryButtonUp(eventArgs);
        }
    }

    private void HandleDiagramMouseMove(object sender, MouseEventArgs eventArgs)
    {
        ProcessMouseMove(eventArgs);
    }
    
    /// <summary>
    /// Method that process primary button down.
    /// </summary>
    /// <param name="args">Mouse button event args.</param>
    protected virtual void ProcessPrimaryButtonDown(MouseButtonEventArgs args)
    {
    }

    /// <summary>
    /// Method that process primary button up.
    /// </summary>
    /// <param name="args">Mouse button event args.</param>
    protected virtual void ProcessPrimaryButtonUp(MouseButtonEventArgs args)
    {
    }

    /// <summary>
    /// Method that process secondary button down.
    /// </summary>
    /// <param name="args">Mouse button event args.</param>
    protected virtual void ProcessSecondaryButtonDown(MouseButtonEventArgs args)
    {
        Cancel();
    }
    
    /// <summary>
    /// Method that process secondary button up.
    /// </summary>
    /// <param name="args">Mouse button event args.</param>
    protected virtual void ProcessSecondaryButtonUp(MouseButtonEventArgs args)
    {
    }

    protected virtual void ProcessMouseMove(MouseEventArgs args)
    {
    }
    
    /// <summary>
    /// Method that cancel mode interaction with diagram.
    /// </summary>
    protected virtual void Cancel()
    {
    }

    /// <inheritdoc/>
    public void Finalize()
    {
        Diagram.MouseDown -= HandleDiagramMouseDown;
        Diagram.MouseUp -= HandleDiagramMouseUp;
        Diagram.MouseMove -= HandleDiagramMouseMove;
        Diagram.Cursor = _originalCursor;
    }
}