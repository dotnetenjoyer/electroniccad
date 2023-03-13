using ElectronicCad.Desktop.Views.Common;
using ElectronicCad.MVVM.Common.Attributes;
using ElectronicCad.MVVM.ViewModels.Project;

namespace ElectronicCad.Desktop.Views.Project;

[ViewModelAssociating(typeof(ProjectPropertiesViewModel))]
public partial class ProjectProperties : BaseNavigatedPage
{
    public ProjectProperties()
    {
        InitializeComponent();
    }
}