using Microsoft.Toolkit.Mvvm.ComponentModel;
using ElectronicCad.MVVM.Properties.Abstractions;
using System.ComponentModel;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Shape;

/// <summary>
/// Geometry shape custom section.
/// </summary>
public class ShapeCustomSection : ObservableObject, ICustomSection
{
    private readonly IShapeProxy proxy;

    /// <summary>
    /// Shape model.
    /// </summary>
    public ShapeModel ShapeModel 
    { 
        get => shapeModel;
        set => SetProperty(ref shapeModel, value);
    }

    private ShapeModel shapeModel;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="proxy">Shape proxy.</param>
    public ShapeCustomSection(IShapeProxy proxy)
    {
        this.proxy = proxy;
        this.proxy.Updated += HandleProxyUpdated;

        ShapeModel = new();
        UpdateFromProxy();
    }

    private void HandleProxyUpdated(object? sender, EventArgs e)
    {
        UpdateFromProxy();
    }

    private void UpdateFromProxy()
    {
        ShapeModel.PropertyChanged -= HandleShapeModelPropertyChanges;
        
        ShapeModel.FillColor = proxy.FillColor;
        ShapeModel.StrokeColor = proxy.StrokeColor; 
        ShapeModel.StrokeWidth = proxy.StrokeWidth; 
        
        ShapeModel.PropertyChanged += HandleShapeModelPropertyChanges;
    }

    private void HandleShapeModelPropertyChanges(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        UpdateProxy();
    }

    private void UpdateProxy()
    {
        proxy.FillColor = ShapeModel.FillColor;
        proxy.StrokeColor = ShapeModel.StrokeColor;
        proxy.StrokeWidth = ShapeModel.StrokeWidth;
        proxy.UpdateEntity();
    }
}