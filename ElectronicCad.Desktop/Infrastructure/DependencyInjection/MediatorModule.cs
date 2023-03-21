using ElectronicCad.UseCases.Projects.CreateNewProject;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronicCad.Desktop.Infrastructure.DependencyInjection;

/// <summary>
/// Mediator module.
/// </summary>
internal static class MediatorModule
{
    /// <summary>
    /// Mediator registration.
    /// </summary>
    public static void Register(IServiceCollection services)
    {
        services.AddMediatR(typeof(CreateNewProjectCommand));
    }
}