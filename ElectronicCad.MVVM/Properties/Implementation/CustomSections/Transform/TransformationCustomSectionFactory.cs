using ElectronicCad.MVVM.Properties.Abstractions;

namespace ElectronicCad.MVVM.Properties.Implementation.CustomSections.Transformation;

public class TransformationCustomSectionFactory : ICustomSectionFactory
{
    /// <inheritdoc />
    public bool CanCreate(IProxy proxy)
    {
        if (proxy is ITransformationProxy)
        {
            return true;
        }

        return false;
    }
    
    /// <inheritdoc />
    public ICustomSection Create(IProxy proxy)
    {
        if (!CanCreate(proxy))
        {
            throw new InvalidOperationException($"Cannot create {nameof(TransformationCustomSection)}.");
        }

        return new TransformationCustomSection();
    }
}
