namespace ElectronicCad.Domain.Workspace.Commands;

/// <summary>
/// Command to create a project.
/// </summary>
public class CreateProjectCommand
{
    /// <summary>
    /// Project name.
    /// </summary>
    public string Name { get; set; }
}

