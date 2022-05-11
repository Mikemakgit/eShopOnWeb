using System.Collections.Generic;
using Newtonsoft.Json;

namespace Microsoft.eShopWeb.ApplicationCore.Extensions;
public class VaultSecretsResponse
{
    [JsonProperty("data")]
    public DataObject Data { get; set; }

    public class DataObject
    {
        [JsonProperty("data")]
        public Dictionary<string, string> Data { get; set; }
    }
}
