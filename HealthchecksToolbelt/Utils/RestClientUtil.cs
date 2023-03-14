using System.Net.Http;
using System.Threading.Tasks;

namespace HealthchecksToolbelt.Utils;

public class RestClientUtil
{
    private static readonly HttpClient _HttpClient = new HttpClient();

    public RestClientUtil()
    {
    }

    public async Task<HttpResponseMessage> ProcessRestApiCallAsync(HttpMethod method, string requestUri, object requestPayload = null)
    {
        var stringTask = _HttpClient.GetAsync(requestUri);

        var msg = await stringTask;

        return msg;
    }

    private void DoPostRequest() { }
}