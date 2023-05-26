using MediatR;
using ElectronicCad.Domain.Geometry;
using ElectronicCad.Infrastructure.Abstractions.Interfaces;
using ElectronicCad.Infrastructure.Abstractions.Services;
using ElectronicCad.Infrastructure.Abstractions.Services.Projects.Diagrams;
using Bitmap = System.Drawing.Bitmap;
using System.Reflection.Metadata.Ecma335;

namespace ElectronicCad.UseCases.ProjectDiagrams.AddNewImage;

/// <summary>
/// Handler for <see cref="AddNewImageCommand"/>.
/// </summary>
public class AddNewImageCommandHandler : IRequestHandler<AddNewImageCommand>
{
    private readonly IOpenDiagramProvider activeDiagramProvider;
    private readonly IFilePicker filePicker;
    private readonly IApplicationLocalStorage applicationLocalStorage;

    /// <summary>
    /// Constructor.
    /// </summary>
    public AddNewImageCommandHandler(IOpenDiagramProvider activeDiagramProvider, IFilePicker filePicker,
        IApplicationLocalStorage applicationLocalStorage)
    {
        this.activeDiagramProvider = activeDiagramProvider;
        this.filePicker = filePicker;
        this.applicationLocalStorage = applicationLocalStorage;
    }

    /// <inheritdoc />
    public async Task<Unit> Handle(AddNewImageCommand request, CancellationToken cancellationToken)
    {
        var diagram = activeDiagramProvider.Diagram.GeometryDiagram;

        var imagePath = PickImage();
        var imageSize = GetImageSize(imagePath);
        var imageCenterPoint = new Point(diagram.Size.Width / 2, diagram.Size.Height / 2);
        var image = new Image(imageCenterPoint, imageSize.Width, imageSize.Height, imagePath);
        diagram.AddGeometry(image);

        return Unit.Value;    
    }

    private string PickImage()
    {
        var pathToImage = filePicker.PickFile();
        var pathToImageInLocalStorage = applicationLocalStorage.SaveFile(pathToImage);
        return pathToImageInLocalStorage;
    }

    private (int Width, int Height) GetImageSize(string pathToImage)
    {
        var bitmap = new Bitmap(pathToImage);
        return (bitmap.Width, bitmap.Height);
    }
}
