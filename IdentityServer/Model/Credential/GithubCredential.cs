using IdentityServer.Models.Credential.Interfaces;

namespace IdentityServer.Models.Credential;

public class GithubCredential : IOAuth20Credential, IRedirectUri
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string RedirectUri { get; set; }
}