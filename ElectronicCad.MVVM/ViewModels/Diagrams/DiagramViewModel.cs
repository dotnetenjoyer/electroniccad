using Microsoft.Toolkit.Mvvm.Input;
using MediatR;
using ElectronicCad.MVVM.Common;
using ElectronicCad.Infrastructure.Abstractions.Services.Projects.Diagrams;
using ElectronicCad.Domain.Workspace;
using ElectronicCad.UseCases.ProjectDiagrams.AddNewImage;

namespace ElectronicCad.MVVM.ViewModels.Diagrams;

/// <summary>
/// Diagram view model.
/// </summary>
public class DiagramViewModel : ViewModel
{
    private readonly IOpenDiagramProvider activeDiagramProvider;
    private readonly IMediator mediator;

    /// <summary>
    /// Active workspace diagram.
    /// </summary>
    public Diagram Diagram 
    { 
        get => diagram; 
        set => SetProperty(ref diagram, value);
    }

    private Diagram diagram;

    /// <summary>
    /// Command to add new image.
    /// </summary>
    public RelayCommand AddNewImageCommand 
    { 
        get => addNewImageCommand; 
        set => SetProperty(ref addNewImageCommand, value); 
    }

    private RelayCommand addNewImageCommand;

    /// <summary>
    /// Constructor.
    /// </summary>
    public DiagramViewModel(IOpenDiagramProvider activeDiagramProvider, IMediator mediator)
    {
        this.activeDiagramProvider = activeDiagramProvider;
        this.mediator = mediator;

        AddNewImageCommand = new RelayCommand(AddNewImage);
        
        Diagram = activeDiagramProvider.Diagram;
        activeDiagramProvider.OpenDiagramChanged += HandleActiveDiagramChanges;
    }

    private void HandleActiveDiagramChanges(object? sender, EventArgs e)
    {
        Diagram = activeDiagramProvider.Diagram;
    }

    private async void AddNewImage()
    {
        await mediator.Send(new AddNewImageCommand(), CancellationToken.None);
    }
}