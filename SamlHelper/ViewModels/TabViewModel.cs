using CommunityToolkit.Mvvm.ComponentModel;

namespace SamlHelper.ViewModels;

public partial class TabViewModel : ViewModelBase
{
    [ObservableProperty]
    public string _title;
}