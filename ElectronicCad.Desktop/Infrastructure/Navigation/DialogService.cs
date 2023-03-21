using System.Threading.Tasks;
using ElectronicCad.MVVM.Common;
using ElectronicCad.MVVM.ServiceAbstractions.Navigation;
using ElectronicCad.MVVM.Utils;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ElectronicCad.Desktop.Infrastructure.Navigation;

internal class DialogService : ObservableObject, IDialogService
{
    private readonly ViewModelFactory _viewModelFactory;
    private readonly FrameNavigation _frameNavigation;
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public DialogService(ViewModelFactory viewModelFactory, FrameNavigation frameNavigation)
    {
        _viewModelFactory = viewModelFactory;
        _frameNavigation = frameNavigation;
    }

    /// <inhertdoc/>
    public Task OpenAsync<TViewModel>(params object[] parameters) where TViewModel : ViewModel
    {
        var viewModel = _viewModelFactory.Create<TViewModel>(parameters);
        _frameNavigation.Open(viewModel);
        return Task.CompletedTask;
    }

    /// <inhertdoc/>
    public void Close()
    {
        _frameNavigation.Close();
    }
}