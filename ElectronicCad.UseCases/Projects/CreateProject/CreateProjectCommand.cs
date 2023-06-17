using MediatR;

namespace ElectronicCad.UseCases.Projects.CreateProject;

/// <summary>
/// Command to create new project.
/// </summary>
public class CreateProjectCommand : IRequest
{
    /// <summary>
    /// Project name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Project file name.
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// Project description.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Name of customer.
    /// </summary>
    public string Customer { get; set; }

    /// <summary>
    /// Contact of customer.
    /// </summary>
    public string CustomerContact { get; set; }
}