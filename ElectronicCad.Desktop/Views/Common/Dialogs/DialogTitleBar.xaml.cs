using System.Windows;
using System.Windows.Controls;
using Microsoft.Toolkit.Mvvm.Input;

namespace ElectronicCad.Desktop.Views.Common.Dialogs;

/// <summary>
/// Dialog title bar control.
/// </summary>
internal partial class DialogTitleBar : UserControl
{
    /// <summary>
    /// Title.
    /// </summary>
    public string Title
    {
        get => (string)GetValue(TitleProperty); 
        set => SetValue(TitleProperty, value);
    }
    
    /// <summary>
    /// Title dependecy property
    /// </summary>
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
        nameof(Title),
        typeof(string),
        typeof(DialogTitleBar), 
        new PropertyMetadata());

    /// <summary>
    /// Close dialog command.
    /// </summary>
    public RelayCommand CloseCommand
    {
        get => (RelayCommand) GetValue(CloseCommandProperty); 
        set => SetValue(CloseCommandProperty, value);
    }

    /// <summary>
    /// Close dialog command dependency property.
    /// </summary>
    public static readonly DependencyProperty CloseCommandProperty = DependencyProperty.Register(
        nameof(CloseCommand),
        typeof(RelayCommand),
        typeof(DialogTitleBar),
        new PropertyMetadata());
    
    public DialogTitleBar()
    {
        InitializeComponent();
    }
}