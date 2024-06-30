using OAuth20.Common.Models.Credential.Interfaces;

namespace OAuth20.Common.Models.Credential;

public class FacebookCredential : IOAuth20Credential, IRedirectUri
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string RedirectUri { get; set; }
}