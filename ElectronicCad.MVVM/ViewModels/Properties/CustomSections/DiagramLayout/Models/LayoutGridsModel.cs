using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.DiagramLayout.Models;

/// <summary>
/// Layout grids view model.
/// </summary>
public class LayoutsModel : ObservableObject
{
    /// <summary>
    /// Collection of layouts on the diagram.
    /// </summary>
    public ObservableCollection<LayoutModel> Layouts 
    { 
        get => layouts; 
        set
        {
            if (layouts != null)
            {
                foreach (var layout in layouts)
                {
                    layout.PropertyChanged -= HandleLayoutUpdate;
                }
            }

            SetProperty(ref layouts, value);

            foreach (var layout in layouts)
            {
                layout.PropertyChanged += HandleLayoutUpdate;
            }
        } 
    }

    private ObservableCollection<LayoutModel> layouts;

    /// <summary>
    /// The event fires when layout added.
    /// </summary>
    public event EventHandler<LayoutModel> LayoutAdded;

    /// <summary>
    /// The event fires when layout updated.
    /// </summary>
    public event EventHandler<LayoutModel> LayoutGridUpdated;

    /// <summary>
    /// The event fires when layout removed.
    /// </summary>
    public event EventHandler<LayoutModel> LayoutRemoved;

    /// <summary>
    /// Command to add a new layout.
    /// </summary>
    public RelayCommand AddLayoutCommand { get; }

    /// <summary>
    /// Command to remove a layout.
    /// </summary>
    public RelayCommand<LayoutModel> RemoveLayoutCommand { get; }

    /// <summary>
    /// Selected layout type.
    /// </summary>
    public LayoutType SelectedLayoutType 
    { 
        get => selectedLayoutType; 
        set => SetProperty(ref selectedLayoutType, value); 
    }

    private LayoutType selectedLayoutType = LayoutType.Column;

    /// <summary>
    /// Available layoutr types to select.
    /// </summary>
    public IEnumerable<LayoutType> LayoutTypes { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public LayoutsModel()
    {
        LayoutTypes = Enum.GetValues<LayoutType>();
        AddLayoutCommand = new RelayCommand(AddLayout);
        RemoveLayoutCommand = new RelayCommand<LayoutModel>(RemoveLayout);
    }

    private void AddLayout()
    {
        var layoutGrid = CreateLayout();
        layoutGrid.PropertyChanged += HandleLayoutUpdate;
        Layouts.Add(layoutGrid);
        LayoutAdded?.Invoke(this, layoutGrid);
    }

    private LayoutModel CreateLayout()
    {
        LayoutModel layout;

        if (SelectedLayoutType == LayoutType.Column)
        {
            layout = CreateLayoutColumn();
        }
        else if (SelectedLayoutType == LayoutType.Row)
        {
            layout = CreateLayoutRow();
        }
        else
        {
            layout = CreateLayoutGrid();
        }

        return layout;
    }

    private LayoutColumnModel CreateLayoutColumn()
    {
        var layoutColumn = new LayoutColumnModel()
        {
            Count = 5,
            Width = 40,
            Gutter = 100
        };

        return layoutColumn;
    }

    private LayoutRowModel CreateLayoutRow()
    {
        var layoutRow = new LayoutRowModel()
        {
            Count = 5,
            Height = 40,
            Gutter = 100
        };

        return layoutRow;
    }

    private LayoutGridModel CreateLayoutGrid()
    {
        var layoutGrid = new LayoutGridModel
        {
            Size = 20
        };

        return layoutGrid;
    }

    private void RemoveLayout(LayoutModel? layout)
    {
        if (layout != null)
        {
            layout.PropertyChanged -= HandleLayoutUpdate;
            Layouts.Remove(layout);
            LayoutRemoved?.Invoke(this, layout);
        }
    }

    private void HandleLayoutUpdate(object? sender, PropertyChangedEventArgs args)
    {
        LayoutGridUpdated?.Invoke(this, (LayoutModel)sender);
    }
}
