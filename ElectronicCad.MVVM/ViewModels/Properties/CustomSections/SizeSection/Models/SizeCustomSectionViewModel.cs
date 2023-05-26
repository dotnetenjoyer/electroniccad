using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using ElectronicCad.Infrastructure.Abstractions.Services.Projects;
using ElectronicCad.Infrastructure.Abstractions.Models.Projects;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.SizeSection.Models;

/// <summary>
/// Size custom section view model.
/// </summary>
public class SizeCustomSectionViewModel : ObservableObject
{
    private readonly ISizePrototypesStorage prototypesStorage;
    private readonly SizePrototype customPrototype;
    
    /// <summary>
    /// Available size prototypes to select.
    /// </summary>
    public IEnumerable<SizePrototype> Prototypes
    {
        get => prototypes;
        set => SetProperty(ref prototypes, value);
    }

    private IEnumerable<SizePrototype> prototypes;

    /// <summary>
    /// Selected size prototype.
    /// </summary>
    public SizePrototype SelectedPrototype
    {
        get => selectedPrototype;
        set => SetProperty(ref selectedPrototype, value);
    }

    private SizePrototype selectedPrototype;

    /// <summary>
    /// Size
    /// </summary>
    public SizeModel Size 
    {
        get => size;
        set
        {
            if (size != null)
            {
                size.PropertyChanged -= HandleSizeChange;
            }

            SetProperty(ref size, value);

            size.PropertyChanged += HandleSizeChange;
        } 
    }
    
    private SizeModel size;

    private void HandleSizeChange(object? sender,PropertyChangedEventArgs e)
    {
        OnPropertyChanged(nameof(Size));
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public SizeCustomSectionViewModel(ISizePrototypesStorage prototypesStorage)
    {
        this.prototypesStorage = prototypesStorage;

        customPrototype = new SizePrototype("Custom", 0, 0);
        var prototypes = new List<SizePrototype>();
        prototypes.Add(customPrototype);
        prototypes.AddRange(prototypesStorage.GetPrototypes());
        Prototypes = prototypes;
    
        PropertyChanged += HandlePropertyChange;
    }

    private void HandlePropertyChange(object? sender, PropertyChangedEventArgs eventArgs)
    {
        PropertyChanged -= HandlePropertyChange;

        switch (eventArgs.PropertyName)
        {
            case nameof(SelectedPrototype):
                AplyPrototype();
                break;
            case nameof(Size):
                UpdateSelectedPrototype();
                break;
        }

        PropertyChanged += HandlePropertyChange;
    }

    private void AplyPrototype()
    {
        Size = new SizeModel(SelectedPrototype.Size.Width, SelectedPrototype.Size.Height);
    }

    private void UpdateSelectedPrototype()
    {
        var selectedPrototype = Prototypes
            .FirstOrDefault(prototype => prototype.Size.Width == size.Width
                && prototype.Size.Height == size.Height);
        
        SelectedPrototype = selectedPrototype ?? customPrototype;
    }
}
