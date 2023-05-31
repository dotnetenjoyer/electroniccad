using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ElectronicCad.Desktop.UI.Components;

/// <summary>
/// Tree view extension allow multiselection.
/// </summary>
public sealed class MultiSelectTreeView : TreeView
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public MultiSelectTreeView()
    {
        AddHandler(TreeViewItem.MouseLeftButtonDownEvent,
            new MouseButtonEventHandler(OnTreeViewItemClicked), true);
    }

    private static void OnTreeViewItemClicked(object? sender, MouseButtonEventArgs args)
    {
        var treeView = sender as MultiSelectTreeView;
        var treeViewItem = FindTreeViewItem(args.OriginalSource as DependencyObject);

        if (treeViewItem != null && treeView != null)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                SelectMultipleItemsRandomly(treeView, treeViewItem);
            }
            else if (Keyboard.Modifiers == ModifierKeys.Shift)
            {
                SelectMultipleItemsContinuously(treeView, treeViewItem);
            }
            else
            {
                SelectSingleItem(treeView, treeViewItem);
            }
        }
    }

    private static TreeViewItem FindTreeViewItem(DependencyObject parent)
    {
        while (parent != null && !(parent is TreeViewItem))
        {
            parent = VisualTreeHelper.GetParent(parent);
        }

        return parent as TreeViewItem;
    }

    private static void SelectSingleItem(MultiSelectTreeView treeView, TreeViewItem treeViewItem)
    {
        DeselectItems(treeView);
        SetIsItemSelected(treeViewItem, true);
        treeView.StartItem = treeViewItem;
    }

    private static void DeselectItems(TreeView treeView)
    {
        var treeViewItems = GetAllTreeViewItems(treeView);
        DeselectItems(treeViewItems);
    }

    private static void DeselectItems(IEnumerable<TreeViewItem> items)
    {
        foreach (var item in items)
        {
            SetIsItemSelected(item, false);
        }
    }

    private static List<TreeViewItem> GetAllTreeViewItems(ItemsControl parent, List<TreeViewItem>? items = null)
    {
        if (items == null)
        {
            items = new();
        }

        for (int i = 0; i < parent.Items.Count; i++)
        {
            var item = parent.ItemContainerGenerator.ContainerFromIndex(i) as TreeViewItem;

            if (item == null)
            {
                continue;
            }

            items.Add(item);
            GetAllTreeViewItems(item, items);
        }

        return items;
    }

    /// <summary>
    /// Indicates if tree view item is selected.
    /// </summary>
    public static readonly DependencyProperty IsItemSelectedProperty =
        DependencyProperty.RegisterAttached(
            "IsItemSelected",
            typeof(bool),
            typeof(MultiSelectTreeView),
            new PropertyMetadata(false, OnIsItemSelectedPropertyChanged));

    /// <summary>
    /// Get tree view is item selected value.
    /// </summary>
    /// <param name="item">Tree view item.</param>
    /// <returns>Value.</returns>
    public static bool GetIsItemSelected(TreeViewItem item)
    {
        return (bool)item.GetValue(IsItemSelectedProperty);
    }

    /// <summary>
    /// Sets tree view is item selected property value.
    /// </summary>
    /// <param name="item">Tree view item.</param>
    /// <param name="value">New value.</param>
    public static void SetIsItemSelected(TreeViewItem item, bool value)
    {
        item.SetValue(IsItemSelectedProperty, value);
    }

    private static void OnIsItemSelectedPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
    {
        var treeViewItem = obj as TreeViewItem;
        var treeView = FindMultiSelectTree(treeViewItem!);

        if (treeViewItem != null && treeView != null)
        {
            var selectedItems = treeView.SelectedItems.ToList();
            if (selectedItems != null)
            {
                if (GetIsItemSelected(treeViewItem))
                {
                    selectedItems.Add(treeViewItem.Header);
                }
                else
                {
                    selectedItems.Remove(treeViewItem.Header);
                }
            }

            treeView.SelectedItems = selectedItems; 
        }
    }

    private static MultiSelectTreeView? FindMultiSelectTree(DependencyObject child)
    {
        while(child != null && !(child is MultiSelectTreeView))
        {
            child = VisualTreeHelper.GetParent(child);
        }

        return child as MultiSelectTreeView;
    }

    /// <inheritdoc cref="SelectedItemsProperty"/>
    public IEnumerable<object> SelectedItems
    {
        get => (IEnumerable<object>)GetValue(SelectedItemsProperty);
        set => SetValue(SelectedItemsProperty, value);
    }

    /// <summary>
    /// Collection of selected items.
    /// </summary>
    public static readonly DependencyProperty SelectedItemsProperty =
        DependencyProperty.Register(
            "SelectedItems",
            typeof(IEnumerable<object>),
            typeof(MultiSelectTreeView),
            new FrameworkPropertyMetadata(Array.Empty<object>(), HandleSelectedItemsChange));

    private static void HandleSelectedItemsChange(DependencyObject obj, DependencyPropertyChangedEventArgs args)
    {
        var treeView = obj as MultiSelectTreeView;

        var allTreeViewItems = GetAllTreeViewItems(treeView);
        foreach (var treeViewItem in allTreeViewItems)
        {
            var isSelected = treeView.SelectedItems.Contains(treeViewItem.Header);
            SetIsItemSelected(treeViewItem, isSelected);
        }
    }

    /// <inheritdoc cref="StartItemProperty"/>
    public TreeViewItem StartItem
    {
        get => (TreeViewItem)GetValue(StartItemProperty);
        set => SetValue(StartItemProperty, value);
    }

    /// <summary>
    /// Tree view item from selection is started.
    /// </summary>
    public static readonly DependencyProperty StartItemProperty =
       DependencyProperty.Register(
           nameof(StartItem),
           typeof(TreeViewItem),
           typeof(MultiSelectTreeView),
           new PropertyMetadata());

    private static void SelectMultipleItemsRandomly(MultiSelectTreeView treeView, TreeViewItem treeViewItem)
    {
        SetIsItemSelected(treeViewItem, !GetIsItemSelected(treeViewItem));
        
        if (treeView.StartItem == null)
        {
            if (GetIsItemSelected(treeViewItem))
            {
                treeView.StartItem = treeViewItem;
            }
        }
        else
        {
            if (treeView.SelectedItems.Count() == 0)
            {
                treeView.StartItem = null;
            }
        }
    }

    private static void SelectMultipleItemsContinuously(MultiSelectTreeView treeView, TreeViewItem treeViewItem)
    {
        if (treeView.StartItem != null)
        {
            if (treeView.StartItem == treeViewItem)
            {
                SelectSingleItem(treeView, treeViewItem);
                return;
            }

            var allTreeViewItems = GetAllTreeViewItems(treeView);
            DeselectItems(allTreeViewItems);

            var (start, end) = GetSelectedItemsRange(allTreeViewItems);

            var selectedItems = allTreeViewItems.GetRange(start, end - start + 1);
            foreach (var item in selectedItems)
            {
                SetIsItemSelected(item, true);
            }
        }

        (int start, int end) GetSelectedItemsRange(List<TreeViewItem> allTreeViewItems)
        {
            var start = allTreeViewItems.IndexOf(treeView.StartItem);
            var end = allTreeViewItems.IndexOf(treeViewItem);

            start = start == -1 ? end : start;
            end = end == -1 ? start : end;

            if (start == -1)
            {
                start = end = 0;
            }

            if (start > end)
            {
                var temp = end;
                end = start;
                start = temp;
            }

            return (start, end);
        }
    }
}

