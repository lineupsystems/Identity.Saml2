using System;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using ITfoxtec.Identity.Saml2.Http.Compression;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;

namespace SamlHelper.ViewModels;

public partial class DecodeTabViewModel : TabViewModel
{
    [ObservableProperty]
    public string _samlToDecode;

    [ObservableProperty]
    public string _decodedSamlXml;
    
    [ObservableProperty]
    public bool _isQueryString;
    
    [ObservableProperty]
    public string _signature;
    
    [ObservableProperty]
    public string _signatureAlgorithm;
    
    public ReactiveCommand<Unit, Unit> DecodeSamlCmd { get; }

    public DecodeTabViewModel()
    {
        Title = "Decode";
        DecodeSamlCmd = ReactiveCommand.CreateFromTask(DecodeSaml);
    }

    private async Task DecodeSaml()
    {
        try
        {
            string samlXml = "";
            DecodedSamlXml = "";
            
            var queryParams = HttpUtility.ParseQueryString(SamlToDecode);
            
            if (queryParams.Count > 1)
            {
                IsQueryString = true;
                    
                var sAMLResponse = queryParams["SAMLResponse"] ?? queryParams["SAMLRequest"];
                Signature = queryParams["Signature"];
                SignatureAlgorithm = queryParams["SigAlg"];
                    
                samlXml = SamlCompressionHelper.DecompressResponse(sAMLResponse);
            }
            else if (IsUrlEncoded(SamlToDecode))
            {
                var urlDecodedSaml = HttpUtility.UrlDecode(SamlToDecode);
                samlXml = SamlCompressionHelper.DecompressResponse(urlDecodedSaml);
            }
            else
            {
                samlXml = SamlCompressionHelper.DecompressResponse(SamlToDecode);
            }
            
            XDocument doc = XDocument.Parse(samlXml);
            DecodedSamlXml = doc.ToString();
        }
        catch (Exception ex)
        {
            var box = MessageBoxManager.GetMessageBoxStandard("Error", ex.Message);
            await box.ShowAsync();
        }
    }
    
    public static bool IsUrlEncoded(string input)
    {
        if (string.IsNullOrEmpty(input))
            return false;

        string decoded = HttpUtility.UrlDecode(input);
        string reEncoded = HttpUtility.UrlEncode(decoded);

        return input.Equals(reEncoded, StringComparison.Ordinal);
    }
}