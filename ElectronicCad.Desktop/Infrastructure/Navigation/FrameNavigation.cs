using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using ElectronicCad.Desktop.Views.Common;
using ElectronicCad.MVVM.Common;
using ElectronicCad.MVVM.Common.Attributes;

namespace ElectronicCad.Desktop.Infrastructure.Navigation;

/// <summary>
/// Navigating pages using a frame.
/// </summary>
internal class FrameNavigation
{
    private readonly Frame _frame;
    private readonly Dictionary<Type, Type> _viewModelToPageAssociations = new();

    /// <summary>
    /// Constructor.
    /// </summary>
    public FrameNavigation(Frame frame)
    {
        _frame = frame;
        InitializeViewModelToPageAssociations();
    }

    private void InitializeViewModelToPageAssociations()
    {
        var pages = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(_ => _.IsAssignableTo(typeof(BaseNavigatedPage)))
            .ToList();

        foreach (var page in pages)
        {
            var associatedAttribute = page.GetCustomAttribute<ViewModelAssociatingAttribute>();
            if (associatedAttribute is null)
            {
                continue;
            }
            
            _viewModelToPageAssociations[associatedAttribute.ViewModelType] = page;
        }
    }
    
    public void Open(ViewModel viewModel)
    {
        var page = _viewModelToPageAssociations[viewModel.GetType()];

        if (page == null)
        {
            return;
        }

        _frame.Visibility = Visibility.Visible;
        _frame.Navigate(Activator.CreateInstance(page));
    }
}