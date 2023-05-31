using System.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MediatR;
using ElectronicCad.Domain.Geometry;
using ElectronicCad.Infrastructure.Abstractions.Services;
using ElectronicCad.Infrastructure.Abstractions.Services.Projects.Diagrams;
using ElectronicCad.UseCases.ProjectDiagrams.AddNewImage;
using ElectronicCad.MVVM.Common;
using WorkbookDiagram = ElectronicCad.Domain.Workspace.Diagram;

namespace ElectronicCad.MVVM.ViewModels.Diagrams;

/// <summary>
/// Diagram view model.
/// </summary>
public class DiagramViewModel : ViewModel
{
    private readonly IOpenDiagramProvider activeDiagramProvider;
    private readonly IMediator mediator;
    private readonly ISelectionService selectionService;

    private bool isSyncsWithSelectionService;

    /// <summary>
    /// Active workspace diagram.
    /// </summary>
    public WorkbookDiagram Diagram 
    { 
        get => diagram; 
        set => SetProperty(ref diagram, value);
    }

    private WorkbookDiagram diagram;

    /// <summary>
    /// Selected diagram geometry objects.
    /// </summary>
    public IEnumerable<GeometryObject> SelectedGeometry
    {
        get => selectedGeometry;
        set => SetProperty(ref selectedGeometry, value);
    }

    private IEnumerable<GeometryObject> selectedGeometry;

    /// <summary>
    /// Command to add new image.
    /// </summary>
    public RelayCommand AddNewImageCommand { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public DiagramViewModel(IOpenDiagramProvider activeDiagramProvider, IMediator mediator, 
        ISelectionService selectionService)
    {
        this.mediator = mediator;

        this.activeDiagramProvider = activeDiagramProvider;
        this.activeDiagramProvider.OpenDiagramChanged += HandleActiveDiagramChanges;
        Diagram = activeDiagramProvider.Diagram;

        this.selectionService = selectionService;
        this.selectionService.SelectionChanged += HandleSelectionServiceSelectedItemsChange; 

        AddNewImageCommand = new RelayCommand(AddNewImage);

        PropertyChanged += HandlePropertyChange;
    }

    private void HandleActiveDiagramChanges(object? sender, EventArgs eventArgs)
    {
        Diagram = activeDiagramProvider.Diagram;
    }

    private void HandleSelectionServiceSelectedItemsChange(object? sender, EventArgs eventArgs)
    {
        isSyncsWithSelectionService = true;

        SelectedGeometry = selectionService.SelectedObjects
            .OfType<GeometryObject>()
            .ToList();
        
        isSyncsWithSelectionService = false;
    }

    private void HandlePropertyChange(object? sender, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == nameof(SelectedGeometry) && !isSyncsWithSelectionService)
        {
            selectionService.Select(SelectedGeometry.ToArray());
        }
    }

    private async void AddNewImage()
    {
        await mediator.Send(new AddNewImageCommand(), CancellationToken.None);
    }
}