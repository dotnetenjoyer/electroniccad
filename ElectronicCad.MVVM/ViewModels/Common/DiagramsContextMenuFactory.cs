using ElectronicCad.Domain.Geometry;
using Microsoft.Toolkit.Mvvm.Input;
using System.Reflection.Metadata.Ecma335;

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

        var geometryObjects = objects.OfType<GeometryObject>();
        if (geometryObjects.Count() == objects.Count())
        {
            commands.Add(new ContextMenuCommand("Clone", new RelayCommand(() => CloneGeometryObject(geometryObjects))));
            commands.Add(new ContextMenuCommand("Remove", new RelayCommand(() => RemoveGeometryObjects(geometryObjects))));
        }
        
        return commands;
    }
    
    private void CloneGeometryObject(IEnumerable<GeometryObject> geometryObjects)
    {
        foreach (var geometryObject in geometryObjects) 
        {
            geometryObject.Diagram?.DuplicateGeometry(geometryObject);
        }
    }

    private void RemoveGeometryObjects(IEnumerable<GeometryObject> geometryObjects)
    {
        geometryObjects = geometryObjects.Where(x => x.Diagram != null);
        foreach(var diagramGeometryObjects in geometryObjects.GroupBy(x => x.Diagram))
        {
            diagramGeometryObjects.Key!.RemoveGeometry(diagramGeometryObjects);
        }
    }
}