using System.Windows.Input;
using ElectronicCad.Diagramming.Nodes;
using SkiaSharp;
using SkiaSharp.Views.WPF;

namespace ElectronicCad.Diagramming.Modes;

public class NewLineMode : BaseDiagramMode
{
    private LineDiagramNode? _line;

    protected override void ProcessPrimaryButtonDown(MouseButtonEventArgs args)
    {
        var position = args.GetPosition(Diagram).ToSKPoint();
        
        if (_line == null)
        {
            _line = new LineDiagramNode
            {
                Bounds = new SKRect(position.X, position.Y, position.X, position.Y)
            };
        }
        else
        {
            _line.Bounds = new SKRect(_line.Bounds.Left, _line.Bounds.Top, position.X, position.Y);

            _line = new LineDiagramNode
            {
                Bounds = new SKRect(position.X, position.Y, position.X, position.Y)
            };
        }
        
        Diagram.ActiveLayer.AddNode(_line);
    }

    protected override void ProcessMouseMove(MouseEventArgs args)
    {
        var position = args.GetPosition(Diagram).ToSKPoint();

        if (_line != null)
        {
            _line.Bounds = new SKRect(_line.Bounds.Left, _line.Bounds.Top, position.X, position.Y);
            Diagram.RedrawDiagram();
        }
    }

    protected override void Cancel()
    {
        if (_line != null)
        {
            Diagram.ActiveLayer.RemoveNode(_line);
            Diagram.RedrawDiagram();
        }
    }
}