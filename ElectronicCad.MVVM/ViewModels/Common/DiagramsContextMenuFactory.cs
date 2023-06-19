using ElectronicCad.Domain.Geometry;
using ElectronicCad.Domain.Geometry.Extensions;
using ElectronicCad.Domain.Geometry.Utils;
using ElectronicCad.Infrastructure.Abstractions.Services;
using Microsoft.Toolkit.Mvvm.Input;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.ObjectiveC;
using System.Security.Cryptography.X509Certificates;
using WorkspaceDiagram = ElectronicCad.Domain.Workspace.Diagram;

namespace ElectronicCad.MVVM.ViewModels.Common;

/// <summary>
/// Factory to create context menu for the specified diagrams objects.
/// </summary>
public class DiagramsContextMenuFactory
{
    private readonly ISelectionService selectionService;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="selectionService">Selection service.</param>
    public DiagramsContextMenuFactory(ISelectionService selectionService)
    {
        this.selectionService = selectionService;
    }

    /// <summary>
    /// Creates context menu for the specified objects.
    /// </summary>
    /// <param name="objects">Objects for which a context menu is created.</param>
    /// <returns>Collection of context menu commands.</returns>
    public IEnumerable<ContextMenuCommand> CreateContextMenu(IEnumerable<object> objects)
    {
        var commands = new List<ContextMenuCommand>();
        commands.AddRange(CreateGeometryObjectsCommands(objects));
        commands.AddRange(CreateLayerCommands(objects));
        commands.AddRange(CreateDiagramsCommands(objects));
        return commands;
    }   

    private IEnumerable<ContextMenuCommand> CreateGeometryObjectsCommands(IEnumerable<object> objects)
    {
        var commands = new List<ContextMenuCommand>();

        var geometryObjects = objects
            .OfType<GeometryObject>()
            .ToList();
        
        if (geometryObjects == null || !geometryObjects.Any() || geometryObjects.Count != objects.Count())
        {
            return commands;
        }

        if(geometryObjects.Count == 1)
        {
            var geometryObject = geometryObjects.First();

            commands.Add(new ContextMenuCommand("Поднять", new RelayCommand(Foo, CanFoo)));
            commands.Add(new ContextMenuCommand("Поднять вверх", new RelayCommand(Foo, CanFoo)));
            commands.Add(new ContextMenuCommand("Опустить", new RelayCommand(Foo, CanFoo)));
            commands.Add(new ContextMenuCommand("Опустить вниз", new RelayCommand(Foo, CanFoo)));
        }


        commands.Add(new ContextMenuCommand("Сгруппировать",
            new RelayCommand(GroupGeometry, CanGroupGeometry)));

        commands.Add(new ContextMenuCommand("Клонировать", new RelayCommand(() => 
            CloneGeometry(geometryObjects!))));


        var visibleGeometryObjects = geometryObjects
            .Where(x => x.IsVisible)
            .ToList();
        
        if (visibleGeometryObjects.Any())
        {
            commands.Add(new ContextMenuCommand("Скрыть", new RelayCommand(() =>
                HideGeometryObjects(visibleGeometryObjects!))));
        }

        var invisibleGeometryObjects = geometryObjects
            .Except(visibleGeometryObjects)
            .ToList();

        if (invisibleGeometryObjects.Any())
        {
            commands.Add(new ContextMenuCommand("Показать", new RelayCommand(() =>
                ShowGeometryObjects(invisibleGeometryObjects!))));
        }

        var lockedGeometryObjects = geometryObjects
            .Where(x => x.IsLock)
            .ToList();

        if (lockedGeometryObjects.Any())
        {
            commands.Add(new ContextMenuCommand("Разблокировать", new RelayCommand(() =>
                UnlockGeometryObjects(lockedGeometryObjects!))));
        }

        var unlockedGeometryObjects = geometryObjects
            .Except(lockedGeometryObjects)
            .ToList();
        
        if (unlockedGeometryObjects.Any())
        {
            commands.Add(new ContextMenuCommand("Заблокировать", new RelayCommand(() =>
                LockGeometryObjects(unlockedGeometryObjects!))));
        }

        commands.Add(new ContextMenuCommand("Удалить", new RelayCommand(() => 
            RemoveGeometry(geometryObjects!))));

        return commands;

        void Foo()
        {

        }

        bool CanFoo()
        {
            return true;
        }

        void CloneGeometry(IEnumerable<GeometryObject> geometryObjects)
        {
            geometryObjects = GeometryUtils.FilterNestingGeometry(geometryObjects);
            foreach (var geometryObject in geometryObjects)
            {
                geometryObject.Diagram?.DuplicateGeometry(geometryObject);
            }
        }

        void RemoveGeometry(IEnumerable<GeometryObject> geometryObjects)
        {
            foreach (var diagramGeometryObjects in geometryObjects.GroupBy(x => x.Diagram))
            {
                diagramGeometryObjects.Key!.RemoveGeometry(diagramGeometryObjects);
            }

            var removedSelectedItems = selectionService.SelectedObjects
                .Where(selected => geometryObjects.Contains(selected));

            if (removedSelectedItems.Any())
            {
                var newSelectedItems = selectionService.SelectedObjects.Except(removedSelectedItems);
                selectionService.Select(newSelectedItems);
            }

        }
    
        void GroupGeometry()
        {
            var diagram = geometryObjects!.First().Diagram;
            if (diagram == null)
            {
                return;
            }

            diagram.CreateGroup(geometryObjects!);
        }

        bool CanGroupGeometry()
        {
            var diagram = geometryObjects!.First().Diagram;
            if (diagram == null)
            {
                return false;
            }
            
            var validationResult = diagram!.CanCreateGroup(geometryObjects!);
            return validationResult.IsSuccessed;
        }

        void ShowGeometryObjects(IEnumerable<GeometryObject> geometryObjects)
        {
            ExcecuteActionOnTheDiagramObjects(geometryObjects, (diagram, geometryObjects)
                => diagram.ShowGeometryObjects(geometryObjects));
            
            ReselectSelected();
        }

        void HideGeometryObjects(IEnumerable<GeometryObject> geometryObjects)
        {
            ExcecuteActionOnTheDiagramObjects(geometryObjects, (diagram, geometryObjects)
                => diagram.HideGeometryObjects(geometryObjects));

            ReselectSelected();
        }

        void LockGeometryObjects(IEnumerable<GeometryObject> geometryObjects)
        {
            ExcecuteActionOnTheDiagramObjects(geometryObjects, (diagram, geometryObjects)
                => diagram.LockGeometryObjects(geometryObjects));

            ReselectSelected();
        }

        void UnlockGeometryObjects(IEnumerable<GeometryObject> geometryObjects)
        {
            ExcecuteActionOnTheDiagramObjects(geometryObjects, (diagram, geometryObjects) 
                => diagram.UnlockGeometryObjects(geometryObjects));
        
            ReselectSelected();
        }

        void ExcecuteActionOnTheDiagramObjects(IEnumerable<GeometryObject> geometryObjects, Action<Diagram, IEnumerable<GeometryObject>> action)
        {
            var geometryObjectsByDiagrams = geometryObjects
                .Where(x => x.Diagram != null)
                .GroupBy(x => x.Diagram);

            foreach (var diagramGeometryObjects in geometryObjectsByDiagrams)
            {
                using var modificationScope = diagramGeometryObjects.Key.StartModificationScope();
                action.Invoke(diagramGeometryObjects.Key, diagramGeometryObjects);
            }
        }

        void ReselectSelected()
        {
            selectionService.Select(selectionService.SelectedObjects.ToList());
        }
    }

    private IEnumerable<ContextMenuCommand> CreateLayerCommands(IEnumerable<object> objects)
    {
        var commands = new List<ContextMenuCommand>();

        var layers = objects
            .OfType<Layer>()
            .ToList();

        if (layers == null || !layers.Any() || layers.Count() != objects.Count())
        {
            return commands;
        }

        if (layers.Count == 1)
        {
            var layer = layers.First();

            commands.Add(new ContextMenuCommand("Поднять", new RelayCommand(Foo, CanFoo)));
            commands.Add(new ContextMenuCommand("Поднять вверх", new RelayCommand(Foo, CanFoo)));
            commands.Add(new ContextMenuCommand("Опустить", new RelayCommand(Foo, CanFoo)));
            commands.Add(new ContextMenuCommand("Опустить вниз", new RelayCommand(Foo, CanFoo)));
            
            commands.Add(new ContextMenuCommand("Активировать", new RelayCommand(()
                => ActivateLayer(layer), () => CanActivateLayer(layer))));
        }

        commands.Add(new ContextMenuCommand("Удалить", new RelayCommand(() => RemoveLayer(layers), () => CanRemoveLayer(layers))));

        return commands;

        void Foo()
        {

        }

        bool CanFoo()
        {
            return true;
        }
        
        void ActivateLayer(Layer layer)
        {
            layer.Diagram.ActivateLayer(layer);
        }

        bool CanActivateLayer(Layer layer)
        {
            return !layer.IsActive;
        }
    
        void RemoveLayer(IEnumerable<Layer> layers)
        {
            foreach (var layer in layers)
            {
               layer.Diagram.RemoveLayer(layer);
            }
        }

        bool CanRemoveLayer(IEnumerable<Layer> layers)
        {
            return true;
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

        commands.Add(new ContextMenuCommand("Добавить слой", new RelayCommand(() =>
        {
            diagram.GeometryDiagram.AddLayer("Слой");
        })));

        return commands;
    }
}