using System;
using System.Threading.Tasks;
using ElectronicCad.MVVM.Common;
using ElectronicCad.MVVM.ServiceAbstractions.Navigation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ElectronicCad.Desktop.Infrastructure.Navigation;

internal class DialogService : ObservableObject, IDialogService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly FrameNavigation _frameNavigation;
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public DialogService(IServiceProvider serviceProvider, FrameNavigation frameNavigation)
    {
        _serviceProvider = serviceProvider;
        _frameNavigation = frameNavigation;
    }

    /// <inhertdoc/>
    public Task OpenAsync<TViewModel>(params object[] parameters) where TViewModel : ViewModel
    {
        var viewModel = ActivatorUtilities.CreateInstance<TViewModel>(_serviceProvider, parameters);
        _frameNavigation.Open(viewModel);
        return Task.CompletedTask;
    }

    /// <inhertdoc/>
    public void Close()
    {
        _frameNavigation.Close();
    }
}