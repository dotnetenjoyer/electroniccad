using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.DiagramLayoutGrid.Models;

/// <summary>
/// Layout grids view model.
/// </summary>
public class LayoutGridsModel : ObservableObject
{
    /// <summary>
    /// Collection of layout grids on the diagram.
    /// </summary>
    public ObservableCollection<LayoutGridModel> LayoutGrids 
    { 
        get => layoutGrids; 
        set
        {
            if (layoutGrids != null)
            {
                foreach (var layoutGrid in layoutGrids)
                {
                    layoutGrid.PropertyChanged -= HandleLayoutGridUpdate;
                }
            }

            SetProperty(ref layoutGrids, value);

            foreach (var layoutGrid in layoutGrids)
            {
                layoutGrid.PropertyChanged += HandleLayoutGridUpdate;
            }
        } 
    }

    private ObservableCollection<LayoutGridModel> layoutGrids;

    /// <summary>
    /// The event fires when layout grid added.
    /// </summary>
    public event EventHandler<LayoutGridModel> LayoutGridAdded;

    /// <summary>
    /// The event fires when layout grid updated.
    /// </summary>
    public event EventHandler<LayoutGridModel> LayoutGridUpdated;

    /// <summary>
    /// The event fires when layout grid removed.
    /// </summary>
    public event EventHandler<LayoutGridModel> LayoutGridRemoved;

    /// <summary>
    /// Command to add a new layout grid.
    /// </summary>
    public RelayCommand AddLayoutGridCommand { get; }

    /// <summary>
    /// Command to remove a layout grid.
    /// </summary>
    public RelayCommand<LayoutGridModel> RemoveLayoutGridCommand { get; }

    /// <summary>
    /// Selected layout grid type.
    /// </summary>
    public LayoutGridType SelectedLayoutGridType 
    { 
        get => selectedLayoutGridType; 
        set => SetProperty(ref selectedLayoutGridType, value); 
    }

    private LayoutGridType selectedLayoutGridType = LayoutGridType.Column;

    /// <summary>
    /// Available layoutr grid types to select.
    /// </summary>
    public IEnumerable<LayoutGridType> LayoutGridTypes { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public LayoutGridsModel()
    {
        LayoutGridTypes = Enum.GetValues<LayoutGridType>();
        AddLayoutGridCommand = new RelayCommand(AddLayoutGrid);
        RemoveLayoutGridCommand = new RelayCommand<LayoutGridModel>(RemoveLayoutGrid);
    }

    private void AddLayoutGrid()
    {
        var layoutGrid = CreateLayoutGrid();
        layoutGrid.PropertyChanged += HandleLayoutGridUpdate;
        LayoutGrids.Add(layoutGrid);
        LayoutGridAdded?.Invoke(this, layoutGrid);
    }

    private LayoutGridModel CreateLayoutGrid()
    {
        LayoutGridModel layoutGrid;

        if (SelectedLayoutGridType == LayoutGridType.Column)
        {
            layoutGrid = CreateColumnLayoutGrid();
        }
        else if (SelectedLayoutGridType == LayoutGridType.Row)
        {
            layoutGrid = CreateRowLayoutGrid();
        }
        else
        {
            layoutGrid = CreateGridLayoutGrid();
        }

        return layoutGrid;
    }

    private ColumnLayoutGridModel CreateColumnLayoutGrid()
    {
        var columnLayoutGrid = new ColumnLayoutGridModel()
        {
            Count = 5,
            Width = 40,
        };

        return columnLayoutGrid;
    }

    private RowLayoutGridModel CreateRowLayoutGrid()
    {
        var rowLayoutGrid = new RowLayoutGridModel()
        {
            Count = 5,
            Height = 40
        };

        return rowLayoutGrid;
    }

    private GridLayoutGridModel CreateGridLayoutGrid()
    {
        var gridLayout = new GridLayoutGridModel
        {
            Size = 20
        };

        return gridLayout;
    }
    private void RemoveLayoutGrid(LayoutGridModel? layoutGrid)
    {
        if (layoutGrid != null)
        {
            layoutGrid.PropertyChanged -= HandleLayoutGridUpdate;
            LayoutGrids.Remove(layoutGrid);
            LayoutGridRemoved?.Invoke(this, layoutGrid);
        }
    }

    private void HandleLayoutGridUpdate(object? sender, PropertyChangedEventArgs args)
    {
        LayoutGridUpdated?.Invoke(this, (LayoutGridModel)sender);
    }
}
