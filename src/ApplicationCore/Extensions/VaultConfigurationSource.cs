using Microsoft.Extensions.Configuration;


namespace Microsoft.eShopWeb.ApplicationCore.Extensions;
public class VaultConfigurationSource : IConfigurationSource
{
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new VaultConfigurationProvider();
    }
}
