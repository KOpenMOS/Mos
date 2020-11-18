using System;
using System.Net.Http;

using OHT_SampleClient;

// https://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/ 참고
HttpClient s_client = BuildHttpClient(new Uri("http://dev.kopenmos.com/"));

TestSender testSender = new(s_client);
await testSender.SendSensorDataAsync("OHTM002", "Vibration", "DataVibration");
await testSender.SendSensorDataEventAsync("OHTM002", "Vibration", "EventVibration");
await testSender.GetSensorDataAsync("OHTM002", "Vibration", "DataVibration");

await testSender.SendVideoAsync("OHTM002", "OnVideo", "DataOnVideo");
await testSender.SendVideoEventAsync("OHTM002", "OnVideo", "EventOnVideo");
await testSender.GetVideoAsync("OHTM002", "OnVideo", "DataOnVideo");

static HttpClient BuildHttpClient(Uri baseAddress)
{
    HttpClientHandler httpClientHandler = new()
    {
        // https://github.com/dotnet/runtime/issues/1844 참고
        MaxConnectionsPerServer = Environment.ProcessorCount * 2,
        AutomaticDecompression = System.Net.DecompressionMethods.Brotli,
    };

    HttpClient httpClient = new(httpClientHandler)
    {
        BaseAddress = baseAddress
    };
    return httpClient;
}


