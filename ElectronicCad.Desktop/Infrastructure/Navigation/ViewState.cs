using System.Windows.Controls;
using ElectronicCad.MVVM.Common;

namespace ElectronicCad.Desktop.Infrastructure.Navigation;

/// <summary>
/// Navigate view state.
/// </summary>
public class ViewState
{
    /// <summary>
    /// Page associated with view model.
    /// </summary>
    public Page Page { get; }

    /// <summary>
    /// View model.
    /// </summary>
    public ViewModel ViewModel { get; }
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public ViewState(Page page, ViewModel viewModel)
    {
        Page = page;
        ViewModel = viewModel;
    }
}