namespace ElectronicCad.MVVM.Common.Attributes;

/// <summary>
/// Associates view model and view.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class ViewModelAssociatingAttribute : Attribute
{
    /// <summary>
    /// View model type.
    /// </summary>
    public Type ViewModelType { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public ViewModelAssociatingAttribute(Type viewModelType)
    {
        ViewModelType = viewModelType;
    }
}