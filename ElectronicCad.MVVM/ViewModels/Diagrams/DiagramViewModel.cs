﻿using Microsoft.Toolkit.Mvvm.Input;
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

    private IEnumerable<GeometryObject> selectedGeometry = Array.Empty<GeometryObject>();

    /// <summary>
    /// Command to handle selected diagram geometry changes.
    /// </summary>
    public RelayCommand HandleGeometrySelectionCommand { get; }

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
        this.selectionService.SelectionChanged += HandleSelectionChange; 

        AddNewImageCommand = new RelayCommand(AddNewImage);
        HandleGeometrySelectionCommand = new RelayCommand(HandleGeometrySelection);
    }

    private void HandleActiveDiagramChanges(object? sender, EventArgs eventArgs)
    {
        Diagram = activeDiagramProvider.Diagram;
    }

    private void HandleSelectionChange(object? sender, EventArgs eventArgs)
    {
        SelectedGeometry = selectionService.SelectedObjects
            .OfType<GeometryObject>()
            .ToList();
    }

    private async void AddNewImage()
    {
        await mediator.Send(new AddNewImageCommand(), CancellationToken.None);
    }

    private void HandleGeometrySelection()
    {
        selectionService.Select(SelectedGeometry.ToArray());
    }
}