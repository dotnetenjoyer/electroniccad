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
        transformationProxy.Updated += (object sedner, EventArgs args) =>
        {
            UpdateFromProxy();
        };

        TransformationModel = new();
        TransformationModel.PropertyChanged += (object sender, PropertyChangedEventArgs args) =>
        {
            Console.WriteLine();
        };

        
        UpdateFromProxy();
    }

    public void UpdateFromProxy()
    {
        TransformationModel.X = transformationProxy.X;
        TransformationModel.Y = transformationProxy.Y;
        TransformationModel.Width = transformationProxy.Width;
        TransformationModel.Height = transformationProxy.Height;
    }

    //public void UpdateProxy()
    //{
    //    proxy.X = TransformationModel.X;
    //    proxy.Y = TransformationModel.Y;
    //    proxy.Width = TransformationModel.Width;
    //    proxy.Height = TransformationModel.Height;
    //}
}
