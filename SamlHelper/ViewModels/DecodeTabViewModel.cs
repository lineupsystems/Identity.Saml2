using System;
using System.Reactive;
using System.Web;
using System.Xml.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using ITfoxtec.Identity.Saml2.Http.Compression;
using ReactiveUI;

namespace SamlHelper.ViewModels;

public partial class DecodeTabViewModel : TabViewModel
{
    [ObservableProperty]
    public string _samlToDecode;

    [ObservableProperty]
    public string _decodedSamlXml;
    
    public ReactiveCommand<Unit, Unit> DecodeSamlCmd { get; }

    public DecodeTabViewModel()
    {
        Title = "Decode";
        DecodeSamlCmd = ReactiveCommand.Create(DecodeSaml);
    }

    private void DecodeSaml()
    {
        try
        {
            var urlDecodedSaml = HttpUtility.UrlDecode(SamlToDecode);
            var samlXml = SamlCompressionHelper.DecompressResponse(urlDecodedSaml);
            XDocument doc = XDocument.Parse(samlXml);
            DecodedSamlXml = doc.ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            
        }
    }
}