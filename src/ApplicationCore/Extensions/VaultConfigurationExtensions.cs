using Microsoft.Extensions.Configuration;


namespace Microsoft.eShopWeb.ApplicationCore.Extensions;
public static class VaultConfigurationExtensions
{
    public static void AddVaultConfiguration(this IConfigurationBuilder configuration)
    {
        configuration.Add(new VaultConfigurationSource());
    }
}
