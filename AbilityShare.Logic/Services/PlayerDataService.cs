using RestSharp;
using Newtonsoft.Json;
using System.Threading;
using AbilityShare.Logic.Models;


namespace AbilityShare.Logic.Services;
public class PlayerDataService
{
    public string status = "";

    public async Task<PlayerDataModel?> GetPlayerData()
    {
        var options = new RestClientOptions("https://127.0.0.1:2999/liveclientdata/activeplayer/")
        {
            RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
        };
        var client = new RestClient(options);
        var request = new RestRequest();
        var response = await client.ExecuteAsync<PlayerDataModel>(request);

        PlayerDataModel? model = null;
        if (response.Content != null)
            model = JsonConvert.DeserializeObject<PlayerDataModel>(response.Content);

        return model;
    }
}
