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

    /// <summary>
    /// Project description.
    /// </summary>
    public string Description { get; set; }

    /// Name of customer.
    /// </summary>
    public string Customer { get; set; }

    /// <summary>
    /// Contact of customer.
    /// </summary>
    public string CustomerContact { get; set; }
}

