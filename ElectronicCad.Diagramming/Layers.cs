using System;
using System.Collections.Generic;
using System.Linq;

namespace ElectronicCad.Diagramming;

internal class Layers
{
    private readonly List<Layer> _layers = new();

    public Layer AddNew()
    {
        int maxLayerIndex = _layers.Max(_ => _.Index);
        var layer = new Layer(maxLayerIndex + 1);
        _layers.Add(layer);
        return layer;
    }
    
    public Layer this[int index]
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }
}