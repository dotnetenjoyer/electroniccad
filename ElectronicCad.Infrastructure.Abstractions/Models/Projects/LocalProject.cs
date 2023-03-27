using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicCad.Infrastructure.Abstractions.Models.Projects;

/// <summary>
/// Local project.
/// </summary>
public class LocalProject
{
    /// <summary>
    /// Project id.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Project name.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Local project path.
    /// </summary>
    public string Path { get; init; }

    /// <summary>
    /// Last access time.
    /// </summary>
    public DateTime LastAccessTime { get; init; }
}
