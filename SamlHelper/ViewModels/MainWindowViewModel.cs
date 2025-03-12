using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Web;
using System.Windows.Input;
using System.Xml.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using ITfoxtec.Identity.Saml2.Http.Compression;
using ReactiveUI;

namespace SamlHelper.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<TabViewModel> Tabs { get; }

    public MainWindowViewModel()
    {
        Tabs = new ObservableCollection<TabViewModel>
        {
            new DecodeTabViewModel(),
            new EncodeTabViewModel()
        };
    }
}