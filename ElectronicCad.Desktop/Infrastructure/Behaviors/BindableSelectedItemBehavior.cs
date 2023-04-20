using ElectronicCad.UseCases.DiagramsTrees.Dtos;
using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;

namespace ElectronicCad.Desktop.Infrastructure.Behaviors;

/// <summary>
/// Allow binds to treeview selected item.
/// </summary>
internal class BindableSelectedItemBehavior : Behavior<TreeView>
{
    #region SelectedItem Property

    public object SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    private static readonly DependencyProperty SelectedItemProperty = DependencyProperty
        .Register(nameof(SelectedItem), 
            typeof(object), 
            typeof(BindableSelectedItemBehavior),
            new UIPropertyMetadata(OnSelectedItemChanged));

    private static void OnSelectedItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs eventArgs)
    {
        var diagramTreeNode = eventArgs.NewValue as DiagramTreeNode;
        if (diagramTreeNode != null)
        {
            diagramTreeNode.IsSelected = true;
        }
    }

    #endregion

    /// <inheritdoc />
    protected override void OnAttached()
    {
        base.OnAttached();

        this.AssociatedObject.SelectedItemChanged += OnTreeViewSelectedItemChanged;
    }

    /// <inheritdoc />
    protected override void OnDetaching()
    {
        base.OnDetaching();

        if (this.AssociatedObject != null)
        {
            this.AssociatedObject.SelectedItemChanged -= OnTreeViewSelectedItemChanged;
        }
    }

    private void OnTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> eventArgs)
    {
        this.SelectedItem = eventArgs.NewValue;
    }
}