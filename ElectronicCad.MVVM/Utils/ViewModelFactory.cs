using ElectronicCad.MVVM.Common;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronicCad.MVVM.Utils;

/// <summary>
/// View model factory.
/// </summary>
public class ViewModelFactory
{
    private readonly IServiceProvider serviceProvider;

    /// <summary>
    /// Constructor.
    /// </summary>
    public ViewModelFactory(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Creates view model instance.
    /// </summary>
    /// <typeparam name="TViewModel">View model type.</typeparam>
    /// <param name="parameters">Parameters.</param>
    /// <returns></returns>
    public TViewModel Create<TViewModel>(params object[] parameters) where TViewModel : ViewModel
    {
        return ActivatorUtilities.CreateInstance<TViewModel>(serviceProvider, parameters);
    }
}
