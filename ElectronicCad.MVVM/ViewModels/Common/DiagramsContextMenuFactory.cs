using ElectronicCad.Domain.Geometry;
using Microsoft.Toolkit.Mvvm.Input;
using WorkspaceDiagram = ElectronicCad.Domain.Workspace.Diagram;

namespace ElectronicCad.MVVM.ViewModels.Common;

/// <summary>
/// Factory to create context menu for the specified diagrams objects.
/// </summary>
public class DiagramsContextMenuFactory
{
    /// <summary>
    /// Creates context menu for the specified objects.
    /// </summary>
    /// <param name="objects">Objects for which a context menu is created.</param>
    /// <returns>Collection of context menu commands.</returns>
    public IEnumerable<ContextMenuCommand> CreateContextMenu(IEnumerable<object> objects)
    {
        var commands = new List<ContextMenuCommand>();
        commands.AddRange(CreateGeometryObjectsCommands(objects));
        commands.AddRange(CreateDiagramsCommands(objects));
        return commands;
    }   

    private IEnumerable<ContextMenuCommand> CreateGeometryObjectsCommands(IEnumerable<object> objects)
    {
        var commands = new List<ContextMenuCommand>();

        var geometryObjects = objects
            .OfType<GeometryObject>()
            .ToList();
        
        if (geometryObjects.Count != objects.Count())
        {
            return commands;
        }

        commands.Add(new ContextMenuCommand("Clone", new RelayCommand(() => 
            CloneGeometryObject(geometryObjects))));

        commands.Add(new ContextMenuCommand("Remove", new RelayCommand(() => 
            RemoveGeometryObjects(geometryObjects))));

        commands.Add(new ContextMenuCommand("Group", new RelayCommand(() =>
        {
            var diagram = geometryObjects.First().Diagram;
            var geometry = diagram.ActiveLayer.Children.ToList();
            diagram.CreateGroup(geometry);
        })));

        return commands;

        void CloneGeometryObject(IEnumerable<GeometryObject> geometryObjects)
        {
            foreach (var geometryObject in geometryObjects)
            {
                geometryObject.Diagram?.DuplicateGeometry(geometryObject);
            }
        }

        void RemoveGeometryObjects(IEnumerable<GeometryObject> geometryObjects)
        {
            foreach (var diagramGeometryObjects in geometryObjects.GroupBy(x => x.Diagram))
            {
                diagramGeometryObjects.Key!.RemoveGeometry(diagramGeometryObjects);
            }
        }
    }

    private IEnumerable<ContextMenuCommand> CreateDiagramsCommands(IEnumerable<object> objects)
    {
        var commands = new List<ContextMenuCommand>();

        var diagrams = objects
            .OfType<WorkspaceDiagram>()
            .ToList();
        
        var diagram = diagrams.FirstOrDefault();
        if (diagram == null || diagrams.Count != objects.Count() || diagrams.Count != 1)
        {
            return commands;
        }

        commands.Add(new ContextMenuCommand("Add layer", new RelayCommand(() =>
        {
            diagram.GeometryDiagram.AddLayer("New");
        })));

        return commands;
    }
}