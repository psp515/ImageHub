using System.Text;
using System.Text.Json;

namespace ImageHub.Api.Tests;

internal class TestsCommon
{
    public static HttpContent Serialize<T>(T content)
    {
        var json = JsonSerializer.Serialize(content);
        var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
        return stringContent;
    }

    public static async Task<T> Deserialize<T>(HttpResponseMessage response)
    {
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseObject = JsonSerializer.Deserialize<T>(responseContent);
        return responseObject!;
    }
}
