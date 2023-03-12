using System;
using System.Windows.Controls;
using ElectronicCad.MVVM.Common;

namespace ElectronicCad.Desktop.Views;

/// <summary>
/// Base navigate page.
/// </summary>
public class BaseNavigatedPage : Page
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public BaseNavigatedPage()
    {
        Loaded += HandleLoaded;
    }

    private async void HandleLoaded(object sender, EventArgs args)
    {
        var viewModel = DataContext as ViewModel;

        if (viewModel == null)
        {
            return;
        }

        try
        {
            viewModel.IsBusy = true;
            await viewModel.LoadAsync();
        }
        catch (Exception exception)
        {
            throw;
        }
        finally
        {
            viewModel.IsBusy = false;
        }
    }
}