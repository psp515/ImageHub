using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace ImageHub.Api.Tests.Features;

public class ImagePackIntegrationTests : IClassFixture<ApiFactory>
{
    readonly HttpClient _client;

    public ImagePackIntegrationTests(ApiFactory application)
    {
        _client = application.CreateClient();
    }
}
