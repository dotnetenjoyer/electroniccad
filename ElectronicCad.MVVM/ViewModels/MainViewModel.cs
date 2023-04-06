using ElectronicCad.Domain.Geometry;
using ElectronicCad.MVVM.Common;
using MediatR;
using System.Diagnostics;

namespace ElectronicCad.MVVM.ViewModels;

/// <summary>
/// Main window view model.
/// </summary>
public class MainViewModel : ViewModel
{
    /// <summary>
    /// Domain diagram.
    /// </summary>
    public Diagram Diagram 
    { 
        get => diagram; 
        set => SetProperty(ref diagram, value); 
    }

    private Diagram diagram;

    /// <summary>
    /// Constructor.
    /// </summary>
    public MainViewModel()
    {
        Diagram = new Diagram();
    }
}