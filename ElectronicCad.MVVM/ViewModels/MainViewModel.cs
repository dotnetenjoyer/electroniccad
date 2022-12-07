using ElectronicCad.MVVM.ViewModels.Base;

namespace ElectronicCad.MVVM.ViewModels;

public class MainViewModel : BaseViewModel
{
    public decimal Value
    {
        get => _value;
        set
        {
            _value = value;
            OnPropertyChanged();
        }
    }
    
    private decimal _value;
}