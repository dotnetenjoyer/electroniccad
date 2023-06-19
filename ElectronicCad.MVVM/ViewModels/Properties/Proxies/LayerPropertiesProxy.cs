using ElectronicCad.Domain.Exceptions;
using ElectronicCad.Domain.Geometry;
using ElectronicCad.Domain.Geometry.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicCad.MVVM.ViewModels.Properties.Proxies;

/// <summary>
/// Layer properties proxy.
/// </summary>
public class LayerPropertiesProxy : NotificationPropertiesProxy<Layer>
{
    /// <summary>
    /// Layer name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="layer">Layer</param>
    public LayerPropertiesProxy(Layer layer) : base(layer)
    {
    }
    
    /// <inheritdoc />
    public override void UpdateFromSource()
    {
        Name = Source.Name;
    }

    public override void UpdateSource()
    {
        base.UpdateSource();
        Source.Rename(Name);
    }
}
