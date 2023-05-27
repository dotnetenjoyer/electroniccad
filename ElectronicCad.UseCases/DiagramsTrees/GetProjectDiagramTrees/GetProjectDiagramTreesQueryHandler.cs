using MediatR;
using ElectronicCad.UseCases.DiagramsTrees.Dtos;
using ElectronicCad.Domain.Workspace;
using ElectronicCad.Infrastructure.Abstractions.Services.Projects;
using System.Runtime.CompilerServices;
using ElectronicCad.Domain.Geometry;
using System.Xml.Linq;
using WorkspaceDiagram = ElectronicCad.Domain.Workspace.Diagram;

namespace ElectronicCad.UseCases.DiagramsTrees.GetDiagramsTree;

/// <summary>
/// Handler for <see cref="GetProjectDiagramTreesQuery"/>
/// </summary>
public class GetProjectDiagramTreesQueryHandler : IRequestHandler<GetProjectDiagramTreesQuery, DiagramTrees>
{
    private readonly IOpenProjectProvider projectProvider;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetProjectDiagramTreesQueryHandler(IOpenProjectProvider projectProvider)
    {
        this.projectProvider = projectProvider;
    }

    /// <inheritdoc />
    public async Task<DiagramTrees> Handle(GetProjectDiagramTreesQuery request, CancellationToken cancellationToken)
    {
        var project = projectProvider.GetOpenProject();
        var projectDiagrams = project.Diagrams.Where(d => d is ProjectDiagram);

        var diagramNodes = projectDiagrams
            .Select(diagram => ComposeDiagramNode(diagram));

        var diagramTree = new DiagramTrees(diagramNodes);
        return diagramTree;
    }

    private DiagramTreeNode ComposeDiagramNode(WorkspaceDiagram diagram)
    {
        var childNodes = diagram.GeometryDiagram.Layers
            .Select(layer => ComposeLayerNode(layer));

        return new WorkspaceDiagramDiagramTreeNode(diagram, childNodes);
    }

    private LayerDiagramTreeNode ComposeLayerNode(Layer layer)
    {
        var childNodes = layer.Children
            .Select(obj => ComposeGeometryObjectNode(obj));
    
        return new LayerDiagramTreeNode(layer, childNodes);
    }

    private GeometryDiagramTreeNode ComposeGeometryObjectNode(GeometryObject geometryObject)
    {
        GeometryDiagramTreeNode geometryNode;

        if (geometryObject is IGeometryContainer container)
        {
            var childNodes = container.Children
                .Select(x => ComposeGeometryObjectNode(x))
                .ToList();

            geometryNode = new(geometryObject, childNodes);
        }
        else
        {
            geometryNode = new(geometryObject);
        }

        return geometryNode;
    }
}
