using ElectronicCad.Desktop.Views.Common;
using ElectronicCad.MVVM.Common.Attributes;
using ElectronicCad.MVVM.ViewModels;

namespace ElectronicCad.Desktop.Views;

[ViewModelAssociating(typeof(WelcomeViewModel))]
public partial class WelcomeDialog : BaseNavigatedPage
{
    public WelcomeDialog()
    {
        InitializeComponent();
    }
}