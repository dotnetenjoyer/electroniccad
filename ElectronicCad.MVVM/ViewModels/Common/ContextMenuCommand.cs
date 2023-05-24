using System.Windows.Input;

namespace ElectronicCad.MVVM.ViewModels.Common;

/// <summary>
/// Context menu command.
/// </summary>
public class ContextMenuCommand : ICommand
{
    private readonly ICommand command;

    /// <summary>
    /// Command name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Command icon path.
    /// </summary>
    public string IconPath { get; }

    /// <inhertidoc />
    public event EventHandler? CanExecuteChanged;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="name">Command name.</param>
    /// <param name="iconPath">Command icon path.</param>
    /// <param name="command">Command to execute.</param>
    public ContextMenuCommand(string name, string iconPath, ICommand command) : this(name, command)
    {
        IconPath = iconPath;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="name">Command name.</param>
    /// <param name="command">Command to execute.</param>
    public ContextMenuCommand(string name, ICommand command)
    {
        Name = name;

        this.command = command;
        this.command.CanExecuteChanged += HandleInnerCommandCanExecuteChange;
    }

    private void HandleInnerCommandCanExecuteChange(object? sender, EventArgs eventArgs)
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <inhertidoc />
    public bool CanExecute(object? parameter)
    {
        return command.CanExecute(parameter);
    }

    /// <inhertidoc />
    public void Execute(object? parameter)
    {
        command.Execute(parameter);
    }
}