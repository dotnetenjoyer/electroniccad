using ElectronicCad.Domain.Geometry;
using ElectronicCad.MVVM.Properties.Abstractions;

namespace ElectronicCad.MVVM.ViewModels.Properties.Proxies;

/// <summary>
/// Factory to create proxy object based on source object.
/// </summary>
public static class ProxyFactory
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static IProxy Create(object sourceObject)
        => sourceObject switch
        {
            Line line => new LinePropertyProxy(line),
            _ => throw new InvalidOperationException()
        };
}