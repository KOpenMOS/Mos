using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OHT_SampleClient
{
    internal class Program
    {
        // https://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
        private static readonly HttpClient s_client = BuildHttpClient(new Uri("http://localhost:5000/"));

        private static async Task Main(string[] args)
        {
            TestSender testSender = new TestSender(s_client);
            await testSender.SendSensorDataAsync("OHT_001", "Sensor1", "DataSensor1");
            await testSender.SendSensorDataEventAsync("OHT_001", "InputEvent", "Sensor1Event");
            await testSender.SendVideoAsync("OHT_001", "Video1", "DataVideo1");
            await testSender.SendVideoEventAsync("OHT_001", "InputEvent", "Video1Event");
        }

        private static HttpClient BuildHttpClient(Uri baseAddress)
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler
            {
                // https://github.com/dotnet/runtime/issues/1844 참고
                MaxConnectionsPerServer = Environment.ProcessorCount * 2
            };

            HttpClient httpClient = new HttpClient(httpClientHandler)
            {
                BaseAddress = baseAddress
            };
            return httpClient;
        }
    }
}


