using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using BaSyx.Models.Core.AssetAdministrationShell.Generics;
using BaSyx.Models.Core.AssetAdministrationShell.Implementations.SubmodelElementTypes;
using BaSyx.Models.Core.Common;
using BaSyx.Models.Extensions;
using BaSyx.Utils.ResultHandling;

using MOS.AAS.Client.Http;

namespace OHT_SampleClient
{
    public class TestSender
    {
        private readonly HttpClient _httpClient;

        public TestSender(HttpClient httpClient) => _httpClient = httpClient;

        /// <summary>
        /// 센서 데이터 전송
        /// </summary>
        /// <param name="aasId">장비 AAS Id</param>
        /// <param name="sensorDataSubmodelId">센서 Id</param>
        /// <param name="sensorDataElementId">센서 Data Element Id</param>
        /// <returns></returns>
        public async Task SendSensorDataAsync(string aasId, string sensorDataSubmodelId, string sensorDataElementId)
        {
            // 임시생성 데이터
            IEnumerable<object[]> rawDatas = BuildSensorRawDatas();

            /*
             * Submodel-SubmodelElement(Container)-SubmodelCollection-List<Property>
             * 
             * Submodel : submodel
             * SubmodelElement(Container) : submodel.SubmodelElements
             * SubmodelCollection : 1개 row 데이터 (collection형태)
             * List<Property> : 여러 데이터  (time, VibrationAx,  VibrationAy, VibrationAz, ...)
             */

            // sensor data 생성
            var sensorDataElement = new SubmodelElementCollection()
            {
                IdShort = sensorDataElementId,
                Value = new ElementContainer<ISubmodelElement>(
                    rawDatas.Select((rawData, index) =>
                        new SubmodelElementCollection()
                        {
                            IdShort = index.ToString(),
                            Value = new ElementContainer<ISubmodelElement>(new ISubmodelElement[]
                            {
                                new Property<DateTime> { IdShort = "Time", Value = (DateTime)rawData[0] },
                                new Property<float> { IdShort = "Ax", Value = (float)rawData[1] },
                                new Property<float> { IdShort = "Ay", Value = (float)rawData[2] },
                                new Property<float> { IdShort = "Az", Value = (float)rawData[3] },
                            })
                        }))
            };

            SubmodelHttpClient client = new(_httpClient);

            // 센서 Data 저장하는 Submodel endpoint 구성
            client.SetSubmodelIdShot(aasId, sensorDataSubmodelId);

            // UPDATE DATA
            await client.UpdateSubmodelElementAsync(sensorDataElementId, sensorDataElement);
        }

        /// <summary>
        /// 센서 데이터 Event 발생
        /// </summary>
        /// <param name="aasId">장비 AAS Id</param>
        /// <param name="eventsSubmodelId">장비 Event 모아놓은 Submodel Id</param>
        /// <param name="sensorDataEventId">센서 Event Id</param>
        /// <returns></returns>
        public async Task SendSensorDataEventAsync(string aasId, string eventsSubmodelId, string sensorDataEventId)
        {
            SubmodelHttpClient client = new(_httpClient);

            // 센서 Event Submodel endpoint 설정
            client.SetSubmodelIdShot(aasId, eventsSubmodelId);

            // 센서 data 갱신 EVENT FIRE
            IResult result = await client.EventFireAsync(sensorDataEventId);

            Console.WriteLine($"{result.Success} {result.Messages}");
        }

        /// <summary>
        /// 센서 데이터 가져오기
        /// </summary>
        /// <param name="aasId">장비 AAS Id</param>
        /// <param name="sensorDataSubmodelId">센서 Id</param>
        /// <param name="sensorDataElementId">센서 Data Element Id</param>
        /// <returns></returns>
        public async Task GetSensorDataAsync(string aasId, string sensorDataSubmodelId, string sensorDataElementId)
        {
            SubmodelHttpClient client = new(_httpClient);

            // 센서 Data 가져오는 Submodel endpoint 구성
            client.SetSubmodelIdShot(aasId, sensorDataSubmodelId);

            IResult<ISubmodelElement> result = await client.RetrieveSubmodelElementAsync(sensorDataElementId);
            SubmodelElementCollection submodelElementCollection = result.GetEntity<SubmodelElementCollection>();
            IElementContainer<ISubmodelElement> sensorElementCollection = submodelElementCollection.Value;

            foreach (SubmodelElementCollection sensorData in sensorElementCollection.Cast<SubmodelElementCollection>())
            {
                IEnumerable<string> propertyStrings = sensorData.Value.Cast<Property>().Select(p => $"{p.IdShort}:{p.Value}");
                Console.WriteLine(string.Join(',', propertyStrings));
            }
        }

        /// <summary>
        /// 영상 데이터 전송
        /// </summary>
        /// <param name="aasId">장비 AAS Id</param>
        /// <param name="videoDataSubmodelId">비디오 데이터 Id</param>
        /// <param name="videoDataElementlId">비디오 Data Element Id</param>
        /// <returns></returns>
        public async Task SendVideoAsync(string aasId, string videoDataSubmodelId, string videoDataElementlId)
        {
            string mp4FileUri = "https://sec.ch9.ms/ch9/d3a3/c6523df8-4b00-4943-b1d9-19d60b51d3a3/CTCDataScienceMod4V3_mid.mp4";

            SubmodelElementCollection videoDataElement = new()
            {
                IdShort = videoDataElementlId,
                Value = new ElementContainer<ISubmodelElement>(new ISubmodelElement[]
                {
                    new SubmodelElementCollection()
                    {
                        IdShort = videoDataElementlId,
                        Value = new ElementContainer<ISubmodelElement>(new ISubmodelElement[]
                        {
                            new Property<DateTime> { IdShort = "Time", Value = DateTime.UtcNow },
                            new File { IdShort = "OnVideo1", MimeType = "video/mp4", Value = mp4FileUri },
                            new File { IdShort = "OnVideo2", MimeType = "video/mp4", Value = mp4FileUri },
                        })
                    },
                })
            };

            SubmodelHttpClient client = new(_httpClient);

            // 영상 데이터 저장하는 Submodel endpoint 구성
            client.SetSubmodelIdShot(aasId, videoDataSubmodelId);

            // UPDATE DATA
            await client.UpdateSubmodelElementAsync(videoDataElementlId, videoDataElement);
        }

        /// <summary>
        /// 영상 데이터 Event 발생
        /// </summary>
        /// <param name="aasId">장비 AAS Id</param>
        /// <param name="eventsSubmodelId">장비 Event 모아놓은 Submodel Id</param>
        /// <param name="videoDataEvetId">비디오 Event Id</param>
        /// <returns></returns>
        public async Task SendVideoEventAsync(string aasId, string eventsSubmodelId, string videoDataEvetId)
        {
            SubmodelHttpClient client = new(_httpClient);

            // 영상 Event Submodel endpoint 설정
            client.SetSubmodelIdShot(aasId, eventsSubmodelId);

            // 영상 데이터 갱신 EVENT FIRE
            IResult result = await client.EventFireAsync(videoDataEvetId);
            Console.WriteLine($"{result.Success} {result.Messages}");
        }

        /// <summary>
        /// 영상 데이터 가져오기
        /// </summary>
        /// <param name="aasId">장비 AAS Id</param>
        /// <param name="videoDataSubmodelId">비디오 데이터 Id</param>
        /// <param name="videoDataElementlId">비디오 Data Element Id</param>
        /// <returns></returns>
        public async Task GetVideoAsync(string aasId, string videoDataSubmodelId, string videoDataElementlId)
        {
            SubmodelHttpClient client = new(_httpClient);

            // 영상 데이터 가져오는 Submodel endpoint 구성
            client.SetSubmodelIdShot(aasId, videoDataSubmodelId);

            IResult<ISubmodelElement> result = await client.RetrieveSubmodelElementAsync(videoDataElementlId);
            SubmodelElementCollection submodelElementCollection = result.GetEntity<SubmodelElementCollection>();
            IElementContainer<ISubmodelElement> sensorElementCollection = submodelElementCollection.Value;

            foreach (SubmodelElementCollection sensorData in sensorElementCollection.Cast<SubmodelElementCollection>())
            {
                var property = sensorData.Value[0] as Property;
                Console.WriteLine($"{property.IdShort} : {property.Value}");

                var file = sensorData.Value[1] as File;
                Console.WriteLine($"{file.IdShort} : {file.MimeType} {file.Value}");
            }
        }

        private static IEnumerable<object[]> BuildSensorRawDatas() =>
            Enumerable.Range(0, 3).Select(idx => new object[] { DateTime.UtcNow, 0.01f, 0.02f, 0.03f });
    }
}
