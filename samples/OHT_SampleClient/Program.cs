using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OHT_SampleClient
{
    internal class Program
    {
        // https://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/ 참고
        private static readonly HttpClient s_client = BuildHttpClient(new Uri("http://localhost:5000/"));

        private static async Task Main(string[] args)
        {
            TestSender testSender = new TestSender(s_client);
            await testSender.SendSensorDataAsync("OTHM002", "Vibration", "DataVibration");
            await testSender.SendSensorDataEventAsync("OTHM002", "Vibration", "VibrationEvent");
            await testSender.SendVideoAsync("OTHM002", "OnVideo", "DataOnVideo");
            await testSender.SendVideoEventAsync("OTHM002", "OnVideo", "OnVideoEvent");
        }

        private static HttpClient BuildHttpClient(Uri baseAddress)
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler
            {
                // https://github.com/dotnet/runtime/issues/1844 참고
                MaxConnectionsPerServer = Environment.ProcessorCount * 2,
                AutomaticDecompression = System.Net.DecompressionMethods.Brotli,                
            };

            HttpClient httpClient = new HttpClient(httpClientHandler)
            {
                BaseAddress = baseAddress
            };
            return httpClient;
        }
    }
}


