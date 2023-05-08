using ElectronicCad.MVVM.Properties.Abstractions;
using System.ComponentModel;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Transformation;

/// <summary>
/// Transformation custom section
/// </summary>
public class TransformationCustomSection : ICustomSection
{
    private readonly ITransformationProxy transformationProxy;

    /// <summary>
    /// Transformation model.
    /// </summary>
    public TransformationModel TransformationModel { get; private set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public TransformationCustomSection(ITransformationProxy transformationProxy)
    {
        this.transformationProxy = transformationProxy;
        this.transformationProxy.Updated += HandleProxyUpdate;

        TransformationModel = new();

        UpdateFromProxy();
    }

    private void HandleProxyUpdate(object? sender, EventArgs args)
    {
        UpdateFromProxy();
    }

    private void HandleTransformationModelChanged(object? sender, PropertyChangedEventArgs args)
    {
        UpdateProxy();
    }

    private void UpdateFromProxy()
    {
        TransformationModel.PropertyChanged -= HandleTransformationModelChanged;

        TransformationModel.X = transformationProxy.X;
        TransformationModel.Y = transformationProxy.Y;
        TransformationModel.Width = transformationProxy.Width;
        TransformationModel.Height = transformationProxy.Height;

        TransformationModel.PropertyChanged += HandleTransformationModelChanged;
    }

    private void UpdateProxy()
    {
        transformationProxy.X = TransformationModel.X;
        transformationProxy.Y = TransformationModel.Y;
        transformationProxy.Width = TransformationModel.Width;
        transformationProxy.Height = TransformationModel.Height;

        transformationProxy.UpdateEntity();
    }
}
