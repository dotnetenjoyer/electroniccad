using MediatR;
using ElectronicCad.UseCases.DiagramsTrees.Dtos;

namespace ElectronicCad.UseCases.DiagramsTrees.GetDiagramsTree;

/// <summary>
/// Returns current project diagram trees.
/// </summary>
public class GetProjectDiagramTreesQuery : IRequest<DiagramTrees>
{
}