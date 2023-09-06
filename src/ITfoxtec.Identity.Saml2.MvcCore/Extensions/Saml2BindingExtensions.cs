using Microsoft.AspNetCore.Mvc;

namespace ITfoxtec.Identity.Saml2.MvcCore
{
    /// <summary>
    /// Extension methods for Bindings
    /// </summary>
    public static class Saml2BindingExtensions
    {
        public static IActionResult ToActionResult(this Saml2Binding binding)
        {
            return binding switch
            {
                Saml2RedirectBinding redirectBinding => redirectBinding.ToActionResult(),
                Saml2PostBinding postBinding => postBinding.ToActionResult(),
                Saml2ArtifactBinding artifactBinding => artifactBinding.ToActionResult(),
                Saml2SoapEnvelope soapBinding => soapBinding.ToActionResult(),
                _ => throw new InvalidSaml2BindingException("Unknown Saml2Binding type!")
            };
        }
        
        /// <summary>
        /// To Redirect Action Result
        /// </summary>
        public static IActionResult ToActionResult(this Saml2RedirectBinding binding)
        {
            return new RedirectResult(binding.RedirectLocation.OriginalString);
        }

        /// <summary>
        /// To Post Action Result
        /// </summary>
        public static IActionResult ToActionResult(this Saml2PostBinding binding)
        {
            return new ContentResult
            {
                ContentType = "text/html",
                Content = binding.PostContent,
                StatusCode = 200
            };
        }

        /// <summary>
        /// To Artifact Action Result
        /// </summary>
        public static IActionResult ToActionResult(this Saml2ArtifactBinding binding)
        {
            return new RedirectResult(binding.RedirectLocation.OriginalString);
        }

        /// <summary>
        /// To SOAP Action Result
        /// </summary>
        public static IActionResult ToActionResult(this Saml2SoapEnvelope binding)
        {
            return new ContentResult
            {
                ContentType = "text/xml; charset=\"utf-8\"",                
                Content = binding.SoapResponseXml,
                StatusCode = 200
            };
        }

        /// <summary>
        /// To XML Action Result
        /// </summary>
        public static IActionResult ToActionResult(this Saml2Metadata metadata)
        {
            return new ContentResult
            {
                ContentType = "text/xml",
                Content = metadata.ToXml(),
                StatusCode = 200
            };
        }
    }
}
