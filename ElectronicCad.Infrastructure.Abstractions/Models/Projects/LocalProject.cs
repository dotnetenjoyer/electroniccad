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
    public Guid Id { get; set; }

    /// <summary>
    /// Project name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Local project path.
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// Last access time.
    /// </summary>
    public DateTime LastAccessTime { get; set; }
}
