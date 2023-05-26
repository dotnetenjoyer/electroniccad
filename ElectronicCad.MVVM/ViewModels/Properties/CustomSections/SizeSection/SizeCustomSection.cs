using AutoMapper;
using ElectronicCad.Domain.Geometry;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.SizeSection.Models;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.SizeSection;

/// <summary>
/// Size custom section.
/// </summary>
public class SizeCustomSection : BaseCustomSection<ISizeProxy, SizeCustomSectionViewModel>
{
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="proxy">Proxy.</param>
    /// <param name="serviceProvider">Service provider.</param>
    public SizeCustomSection(ISizeProxy proxy, IServiceProvider serviceProvider,
        IMapper mapper) : base(proxy, serviceProvider)
    {
        this.mapper = mapper;

        UpdateFromProxy();
    }

    /// <inheritdoc />
    protected override void UpdateFromProxyInternal()
    {
        base.UpdateFromProxyInternal();
        
        Model.Size = mapper.Map<SizeModel>(Proxy.Size);
    }

    /// <inheritdoc />
    protected override void UpdateProxyInternal()
    {
        base.UpdateProxyInternal();
     
        Proxy.Size = mapper.Map<Size>(Model.Size);
    }
}
