using ITfoxtec.Identity.Saml2.Schemas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.Xml;
#if NETFULL
using System.IdentityModel.Configuration;
#else
using Microsoft.IdentityModel.Tokens;
#endif

namespace ITfoxtec.Identity.Saml2
{
    /// <summary>
    /// SAML2 component configuration
    /// </summary>
    public class Saml2Configuration
    {
        private string _issuer;

	    /// <summary>
	    /// Identifies the entity that generated the response message. (For more information on this element, see Section 2.2.5.)
	    /// </summary>
	    public string Issuer
	    {
		    get => _issuer;
		    set
		    {
			    _issuer = value;
			    AllowedAudienceUris.Add(value);
		    }
	    }

	    /// <summary>
	    /// SingleSignOnDestination
	    /// </summary>
	    public Uri SSOUrl { get; set; }

        /// <summary>
        /// SingleLogoutDestination
        /// </summary>
        public Uri SLOUrl { get; set; }

        /// <summary>
        /// Endpoint used for handling SAML artifact resolution requests. Artifacts are a means of communication in SAML that allows transferring large SAML messages or responses efficiently.
        /// <remarks>
        /// When a SAML message, such as an authentication request or response, exceeds a certain size, it may become impractical to send it as part of the original SAML message. In such cases, instead of sending the entire message within the SAML request or response, a shorter "artifact" is sent in its place. This artifact is a reference, usually in the form of an identifier, that the recipient can use to retrieve the complete message.
        /// </remarks>
        /// </summary>
        public Saml2IndexedEndpoint ArtifactResolutionService { get; set; }

        /// <summary>
        /// Gets or sets whether the SAML library should validate SAML artifacts received from the <see cref="ArtifactResolutionService"/>
        /// </summary>
        [DefaultValue(true)]
        public bool ValidateArtifact { get; set; } = true;

        /// <summary>
        /// Signature Algorithm used to sign the SAML xml document.
        /// </summary>
        [DefaultValue(Saml2SecurityAlgorithms.RsaSha256Signature)]
        public string SignatureAlgorithm { get; set; } = Saml2SecurityAlgorithms.RsaSha256Signature;
        
        /// <summary>
        /// Specifies the canonicalization method applied to XML documents before signing. Canonicalization is the process of converting an XML document to a standardized format, ensuring that semantically equivalent XML structures have the same canonical form.
        /// <remarks>
        /// In the context of SAML, canonicalization is critical to ensure that the signed XML documents (such as SAML assertions or requests) maintain their integrity even if there are different XML representations that still have the same meaning.
        /// </remarks>
        /// </summary>
        [DefaultValue(SignedXml.XmlDsigExcC14NTransformUrl)]
        public string XmlCanonicalizationMethod { get; set; } = SignedXml.XmlDsigExcC14NTransformUrl;        

        /// <summary>
        /// Certificate used to sign an XmlDocument with an XML signature.
        /// <remarks>
        /// In SAML (Security Assertion Markup Language), digital signatures play a critical role in ensuring the authenticity and integrity of the exchanged messages. When an entity, such as an Identity Provider (IdP) or a Service Provider (SP), wants to send a SAML message, it signs the message using its private key. The recipient can then verify the signature using the corresponding public key, which is embedded in the entity's X509 certificate.
        /// </remarks>
        /// </summary>
        public X509Certificate2? SigningCertificate { get; set; }
        
        /// <summary>
        /// Certificate used for decrypting encrypted SAML messages, such as encrypted SAML assertions.
        /// <remarks>
        ///	In SAML (Security Assertion Markup Language), encryption is used to protect sensitive information within SAML assertions and messages. When an entity (e.g., an Identity Provider - IdP) wants to send a SAML assertion that contains sensitive data, it can encrypt the assertion using the public key of the recipient (e.g., a Service Provider - SP). The recipient (SP) can then decrypt the encrypted assertion using its corresponding private key, which is associated with its X509 certificate.
        /// </remarks>
        /// </summary>
        public X509Certificate2 DecryptionCertificate { get; set; }
        
        /// <summary>
        /// Certificate used for encrypting SAML messages, such as SAML assertions.
        /// </summary>
        public X509Certificate2 EncryptionCertificate { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="string"/> that represents a valid issuer that will be used to check against the token's issuer.
        /// </summary>
        public string AllowedIssuer { get; set; }
        
        /// <summary>
        /// Collection of X509 certificates used for validating signatures on SAML messages, such as SAML assertions and SAML requests
        /// <remarks>
        /// In SAML, digital signatures are used to ensure the authenticity and integrity of the exchanged messages. The signatures are created using the private key of the signing entity (e.g., Identity Provider or Service Provider) and can be verified using the corresponding public key found in the signing entity's X509 certificate.
        /// </remarks>
        /// </summary>
        public List<X509Certificate2> SignatureValidationCertificates { get; protected set; } = new List<X509Certificate2>();
        
        /// <summary>
        /// Determines the certificate validation mode used when validating X.509 certificates in the context of SAML implementations
        /// <remarks>Certificate validation is the process of verifying the authenticity and trustworthiness of X.509 certificates used by the entities involved in the SAML communication.</remarks>
        /// </summary>
        [DefaultValue(X509CertificateValidationMode.ChainTrust)]
        public X509CertificateValidationMode CertificateValidationMode { get; set; } = X509CertificateValidationMode.ChainTrust;
        
        /// <summary>
        /// Specifies the mode used for checking the revocation status of X.509 certificates during certificate validation in the context of SAML.
        /// <remarks>
        /// Certificate revocation status validation is an important aspect of certificate validation to ensure that certificates used in the SAML communication have not been revoked by the issuing Certificate Authority (CA) before their expiration date.
        /// </remarks>
        /// </summary>
        [DefaultValue(X509RevocationMode.Online)]
        public X509RevocationMode RevocationMode { get; set; } = X509RevocationMode.Online;
        
        public X509CertificateValidator CustomCertificateValidator { get; set; }

        public ITokenReplayCache TokenReplayCache { get; set; }
	    
        /// <summary>
        /// Gets or sets a boolean to control if the original token should be saved after the security token is validated.
        /// </summary>
	    [DefaultValue(false)]
        public bool SaveBootstrapContext { get; set; } = false;

        /// <summary>
        /// By default no replayed validation is performed. Validation requires that <see cref="TokenReplayCache"/> has been set.
        /// </summary>
        [DefaultValue(false)]
        public bool DetectReplayedTokens { get; set; } = false;

        /// <summary>
        /// Gets or sets a boolean to control if the audience will be validated during token validation.
        /// </summary>
        [DefaultValue(true)]
        public bool AudienceRestricted { get; set; } = true;
        
        /// <summary>
        /// Collection of valid and trusted Audience URIs for SAML assertions.
        /// <remarks>
        /// In SAML, the Audience Restriction is a security mechanism that restricts the SAML assertion to be used only by a specific audience or relying party (Service Provider - SP). The Audience URI (sometimes referred to as "Audience Restriction" or "Audience" in SAML specifications) is a unique identifier that represents the intended recipient of the SAML assertion. It typically corresponds to the Service Provider's entity identifier or the URL of the Service Provider's endpoint.
        /// </remarks>
        /// </summary>
        public List<string> AllowedAudienceUris { get; protected set; } = new List<string>();
        
        /// <summary>
        /// Gets or sets a value indicating whether SAML tokens must have at least one AudienceRestriction.
        /// The default is <c>true</c>.
        /// </summary>
        [DefaultValue(true)]
        public bool RequireAudience { get; set; } = true;

        /// <summary>
        /// Sign and validate signed authn requests.
        /// <remarks>
        /// When set to <c>true</c>, the SAML library will sign the authentication requests generated by the SP before sending them to the IdP. The IdP can then verify the signature using the SP's public key, thus ensuring that the request has not been tampered with and originated from the trusted SP.
        /// </remarks>
        /// </summary>
        [DefaultValue(false)]
        public bool SignAuthnRequest { get; set; } = false;

        /// <summary>
        /// Sign type for the authn responses created by the library.
        /// </summary>
        [DefaultValue(Saml2AuthnResponseSignTypes.SignResponse)]
        public Saml2AuthnResponseSignTypes AuthnResponseSignType { get; set; } = Saml2AuthnResponseSignTypes.SignResponse;
    }
}
