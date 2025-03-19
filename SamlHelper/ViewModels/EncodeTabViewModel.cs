using System;
using System.Reactive;
using System.Web;
using System.Xml.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using ITfoxtec.Identity.Saml2.Http.Compression;
using ReactiveUI;

namespace SamlHelper.ViewModels;

public partial class EncodeTabViewModel : TabViewModel
{
    [ObservableProperty]
    public string _samlToEncode;

    [ObservableProperty]
    public string _compressedSamlXml;
    
    [ObservableProperty]
    public string _urlEncodedSamlXml;
    
    public ReactiveCommand<Unit, Unit> EncodeSamlCmd { get; }

    public EncodeTabViewModel()
    {
        Title = "Encode";
        EncodeSamlCmd = ReactiveCommand.Create(DecodeSaml);
    }

    private void DecodeSaml()
    {
        try
        {
            CompressedSamlXml = "";
            UrlEncodedSamlXml = "";
            
            CompressedSamlXml = SamlCompressionHelper.CompressRequest(SamlToEncode);
            UrlEncodedSamlXml = HttpUtility.UrlEncode(CompressedSamlXml);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            
        }
    }
}