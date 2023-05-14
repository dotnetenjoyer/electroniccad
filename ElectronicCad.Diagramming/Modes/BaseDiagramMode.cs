using System.Windows.Input;

namespace ElectronicCad.Diagramming.Modes;

/// <summary>
/// Base diagram mode.
/// </summary>
public abstract class BaseDiagramMode : IDiagramMode
{
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

    private Cursor previousCursor;

    /// <inheritdoc/>
    public virtual void Initialize(Diagram diagram)
    {
        Diagram = diagram;

        Diagram.MouseDown += HandleDiagramMouseDown;
        Diagram.MouseUp += HandleDiagramMouseUp;
        Diagram.MouseMove += HandleDiagramMouseMove;
        Diagram.Redraws += HandleDiagramRedraws;
        
        previousCursor = Diagram.Cursor;
        Diagram.Cursor = Cursor;
    }

    #region EventHandlers

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

    private void HandleDiagramRedraws(object? sender, SkiaDrawingContext context)
    {
        DrawGizmos(context);
    }

    #endregion

    /// <summary>
    /// The method to process primary button down.
    /// </summary>
    /// <param name="args">Mouse button event args.</param>
    protected virtual void ProcessPrimaryButtonDown(MouseButtonEventArgs args)
    {
    }

    /// <summary>
    /// The method to process primary button up.
    /// </summary>
    /// <param name="args">Mouse button event args.</param>
    protected virtual void ProcessPrimaryButtonUp(MouseButtonEventArgs args)
    {
    }

    /// <summary>
    /// The method to process secondary button down.
    /// </summary>
    /// <param name="args">Mouse button event args.</param>
    protected virtual void ProcessSecondaryButtonDown(MouseButtonEventArgs args)
    {
        Cancel();
    }
    
    /// <summary>
    /// The method to process secondary button up.
    /// </summary>
    /// <param name="args">Mouse button event args.</param>
    protected virtual void ProcessSecondaryButtonUp(MouseButtonEventArgs args)
    {
    }

    /// <summary>
    /// The method to process mouse move.
    /// </summary>
    /// <param name="args">Mouse button event args.</param>
    protected virtual void ProcessMouseMove(MouseEventArgs args)
    {
    }

    /// <summary>
    /// Draws diagram mode gizmos.
    /// </summary>
    /// <param name="context"></param>
    protected virtual void DrawGizmos(SkiaDrawingContext context)
    {
    }
    
    /// <summary>
    /// Method that cancel mode interaction with diagram.
    /// </summary>
    protected virtual void Cancel()
    {
    }

    /// <inheritdoc/>
    public virtual void Finish()
    {
        Cancel();

        Diagram.MouseDown -= HandleDiagramMouseDown;
        Diagram.MouseUp -= HandleDiagramMouseUp;
        Diagram.MouseMove -= HandleDiagramMouseMove;
        Diagram.Redraws -= HandleDiagramRedraws;

        Diagram.Cursor = previousCursor;
    }
}