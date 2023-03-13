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

    private readonly Stack<ViewState> _navigationStates;

    /// <summary>
    /// Constructor.
    /// </summary>
    public FrameNavigation(Frame frame)
    {
        _frame = frame;
        _navigationStates = new();
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
    
    /// <summary>
    /// Open new page.
    /// </summary>
    /// <param name="viewModel">The view model that associated with page.</param>
    public void Open(ViewModel viewModel)
    {
        var pageType = _viewModelToPageAssociations[viewModel.GetType()];
        var page = (Page)Activator.CreateInstance(pageType)!;
        page.DataContext = viewModel;
        _navigationStates.Push(new ViewState(page, viewModel));
        NavigateCurrentState();
    }

    /// <summary>
    /// Close current state.
    /// </summary>
    public void Close()
    {
        _navigationStates.Pop();
        NavigateCurrentState();
    }

    private void NavigateCurrentState()
    {
        if (_navigationStates.Count == 0)
        {
            _frame.Visibility = Visibility.Collapsed;
            return;
        } 
        
        var currentState = _navigationStates.Peek();
        _frame.Visibility = Visibility.Visible;
        _frame.Navigate(currentState.Page);
    }
}