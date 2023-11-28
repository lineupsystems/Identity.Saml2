#if !NETFULL
using Microsoft.IdentityModel.Tokens;
#endif
using System;
using System.Xml.Linq;

namespace ITfoxtec.Identity.Saml2.Schemas
{
    public static class Saml2Constants
    {
        /// <summary>
        /// SAML 2.0 request / response max length.
        /// </summary>
#if !NETFULL
        public const int RequestResponseMaxLength = TokenValidationParameters.DefaultMaximumTokenSizeInBytes;
#else
        public const int RequestResponseMaxLength = 1024 * 250;        
#endif

        /// <summary>
        /// SAML 2.0 Authentication Type.
        /// </summary>
        public const string AuthenticationScheme = "saml2";

        /// <summary>
        /// SAML Version Number.
        /// </summary>
        public const string VersionNumber = "2.0";

        /// <summary>
        /// All SAML time values have the type xs:dateTime, which is built in to the W3C XML Schema Datatypes specification[Schema2], and MUST be expressed in UTC form, 
        /// with no time zone component.
        /// SAML system entities SHOULD NOT rely on time resolution finer than milliseconds.Implementations MUST NOT generate time instants that specify leap seconds.
        /// </summary>
        public const string DateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";

        /// <summary>
        /// Saml2 Bearer token.
        /// </summary>
        public static readonly Uri Saml2BearerToken = new Uri("urn:oasis:names:tc:SAML:2.0:cm:bearer");

        /// <summary>
        /// The XML namespace of the SAML2 Assertion.
        /// </summary>
        public static readonly Uri AssertionNamespace = new Uri("urn:oasis:names:tc:SAML:2.0:assertion");
        /// <summary>
        /// The XML namespace of the SAML2 Assertion.
        /// </summary>
        public static readonly XNamespace AssertionNamespaceX = XNamespace.Get(AssertionNamespace.OriginalString);
        /// <summary>
        /// The XML Namespace Name of the SAML2 Assertion.
        /// </summary>
        public static readonly XName AssertionNamespaceNameX = XNamespace.Xmlns + "saml";

        /// <summary>
        /// The XML namespace of the SAML2 Protocol.
        /// </summary>
        public static readonly Uri ProtocolNamespace = new Uri("urn:oasis:names:tc:SAML:2.0:protocol");
        /// <summary>
        /// The XML namespace of the SAML2 Protocol.
        /// </summary>
        public static readonly XNamespace ProtocolNamespaceX = XNamespace.Get(ProtocolNamespace.OriginalString);
        /// <summary>
        /// The XML Namespace Name of the SAML2 Protocol.
        /// </summary>
        public static readonly XName ProtocolNamespaceNameX = XNamespace.Xmlns + "samlp";

        /// <summary>
        /// The XML namespace of the SAML2 SOAP Envelope.
        /// </summary>
        public static readonly Uri SoapEnvironmentNamespace = new Uri("http://schemas.xmlsoap.org/soap/envelope/");
        /// <summary>
        /// The XML namespace of the SAML2 SOAP Envelope.
        /// </summary>
        public static readonly XNamespace SoapEnvironmentNamespaceX = XNamespace.Get(SoapEnvironmentNamespace.OriginalString);
        /// <summary>
        /// The XML namespace Name of the SAML2 SOAP Envelope.
        /// </summary>
        public static readonly XName SoapEnvironmentNamespaceNameX = XNamespace.Xmlns + "SOAP-ENV";

        public static class Message
        {
            public const string SamlResponse = "SAMLResponse";

            public const string SamlRequest = "SAMLRequest";

            public const string SamlArt = "SAMLart";

            public const string RelayState = "RelayState";

            public const string Assertion = "Assertion";

            public const string EncryptedAssertion = "EncryptedAssertion";

            public const string Protocol = "Protocol";

            public const string AuthnRequest = "AuthnRequest";

            public const string AuthnResponse = "Response";

            public const string LogoutRequest = "LogoutRequest";

            public const string LogoutResponse = "LogoutResponse";

            public const string ArtifactResolve = "ArtifactResolve";

            public const string ArtifactResponse = "ArtifactResponse";

            public const string Artifact = "Artifact";

            public const string Id = "ID";

            public const string Version = "Version";

            public const string IssueInstant = "IssueInstant";

            public const string Consent = "Consent";

            public const string Destination = "Destination";

            public const string Signature = "Signature";

            public const string SigAlg = "SigAlg";

            public const string Issuer = "Issuer";

            public const string Status = "Status";

            public const string StatusCode = "StatusCode";

            public const string StatusMessage = "StatusMessage";

            public const string Value = "Value";

            public const string AssertionConsumerServiceIndex = "AssertionConsumerServiceIndex";

            public const string AssertionConsumerServiceURL = "AssertionConsumerServiceURL";

            public const string AttributeConsumingServiceIndex = "AttributeConsumingServiceIndex";

            public const string ProtocolBinding = "ProtocolBinding";

            public const string RequestedAuthnContext = "RequestedAuthnContext";

            public const string Comparison = "Comparison";

            public const string AuthnContextClassRef = "AuthnContextClassRef";

            public const string ForceAuthn = "ForceAuthn";

            public const string IsPassive = "IsPassive";

            public const string NameId = "NameID";

            public const string SessionIndex = "SessionIndex";

            public const string Format = "Format";

            public const string NotOnOrAfter = "NotOnOrAfter";

            public const string NotBefore = "NotBefore";

            public const string Reason = "Reason";
            
            public const string NameIdPolicy = "NameIDPolicy";

            public const string AllowCreate = "AllowCreate";

            public const string SpNameQualifier = "SPNameQualifier";
            
            public const string Extensions = "Extensions";

            public const string InResponseTo = "InResponseTo";

            public const string Conditions = "Conditions";

            public const string AudienceRestriction = "AudienceRestriction";

            public const string Audience = "Audience";

            public const string Subject = "Subject";

            public const string SubjectConfirmation = "SubjectConfirmation";

            public const string SubjectConfirmationData = "SubjectConfirmationData";

            public const string OneTimeUse = "OneTimeUse";

            public const string ProxyRestriction = "ProxyRestriction";

            public const string Count = "Count";

            public const string Envelope = "Envelope";

            public const string Body = "Body";
        }
    }
}
