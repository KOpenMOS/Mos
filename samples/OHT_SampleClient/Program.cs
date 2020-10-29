using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OHT_SampleClient
{
    internal class Program
    {
        // https://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/ 참고
        private static readonly HttpClient s_client = BuildHttpClient(new Uri("http://dev.kopenmos.com/"));

        private static async Task Main(string[] args)
        {
            TestSender testSender = new TestSender(s_client);
            await testSender.SendSensorDataAsync("OHTM002", "Vibration", "DataVibration");
            await testSender.SendSensorDataEventAsync("OHTM002", "Vibration", "EventVibration");
            await testSender.GetSensorDataAsync("OHTM002", "Vibration", "DataVibration");

            await testSender.SendVideoAsync("OHTM002", "OnVideo1", "DataOnVideo1");
            await testSender.SendVideoEventAsync("OHTM002", "OnVideo1", "EventOnVideo1");
            await testSender.GetVideoAsync("OHTM002", "OnVideo1", "DataOnVideo1");
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


