using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Microsoft.eShopWeb.ApplicationCore.Extensions;

public class VaultConfigurationProvider : ConfigurationProvider
{
    private readonly string uri;
    private readonly string token;
    private readonly string @namespace;

    public VaultConfigurationProvider()
    {
        uri = Environment.GetEnvironmentVariable("VAULT_URL");
        token = Environment.GetEnvironmentVariable("VAULT_TOKEN");
        @namespace = Environment.GetEnvironmentVariable("VAULT_NAMESPACE");
    }

    public override void Load()
    {
        if (String.IsNullOrWhiteSpace(uri) || String.IsNullOrWhiteSpace(token) ||
            String.IsNullOrWhiteSpace(@namespace))
        {
            return;
        }

        using var clientHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
        };
        using var client = new HttpClient(clientHandler);

        client.DefaultRequestHeaders.Add("X-Vault-Token", token);
        client.DefaultRequestHeaders.Add("X-Vault-Namespace", @namespace);

        var response = client.GetStringAsync(uri).GetAwaiter().GetResult();

        var secrets = JsonConvert.DeserializeObject<VaultSecretsResponse>(response);

        foreach (var (key, value) in secrets.Data.Data)
        {
            Set(key, value);
        }
    }
}
