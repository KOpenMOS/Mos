using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using BaSyx.Models.Core.AssetAdministrationShell.Generics;
using BaSyx.Models.Core.AssetAdministrationShell.Implementations.SubmodelElementTypes;
using BaSyx.Models.Core.Common;

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
            var rawDatas = BuildSensorRawDatas(aasId, new[]
            {
                "Time",
                "Ax",
                "Ay",
                "Az",
            }, 30).ToArray();

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
                                new Property<DateTime> { IdShort = "Time", Value = (DateTime)rawData["Time"] },
                                new Property<int> { IdShort = "Ax", Value = (int)rawData["Ax"] },
                                new Property<int> { IdShort = "Ay", Value = (int)rawData["Ay"] },
                                new Property<int> { IdShort = "Az", Value = (int)rawData["Az"] },
                            })
                        }))
            };

            var client = new SubmodelHttpClient(_httpClient);

            // 센서 Data 저장하는 Submodel endpoint 구성
            client.SetSubmodelIdShot(aasId, sensorDataSubmodelId);

            // UPDATE DATA
            await client.UpdateSubmodelElement(sensorDataElementId, sensorDataElement);
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
            var client = new SubmodelHttpClient(_httpClient);

            // 장비 Event Submodel endpoint 설정
            client.SetSubmodelIdShot(aasId, eventsSubmodelId);

            // 센서 data 갱신 EVENT FIRE
            await client.EventFireAsync(sensorDataEventId);
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
            var client = new SubmodelHttpClient(_httpClient);

            // 센서 Data 가져오는 Submodel endpoint 구성
            client.SetSubmodelIdShot(aasId, sensorDataSubmodelId);

            var result = await client.RetrieveSubmodelElement(sensorDataElementId);

            var sensorElementCollection = (ElementContainer<SubmodelElementCollection>)result.GetEntity<SubmodelElementCollection>().Value;

            foreach (var sensorData in sensorElementCollection)
            {
                var propertyStrings = sensorData.Value.Cast<Property>().Select(p => $"{p.IdShort}:{p.Value}");
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
            var mp4FileUri = "https://sec.ch9.ms/ch9/d3a3/c6523df8-4b00-4943-b1d9-19d60b51d3a3/CTCDataScienceMod4V3_mid.mp4";

            var videoDataElement = new SubmodelElementCollection()
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
                            new File { IdShort = "VideoFile", MimeType = "video/mp4", Value = mp4FileUri },
                        })
                    },
                })
            };

            var client = new SubmodelHttpClient(_httpClient);

            // 비디오 로그 저장하는 Submodel endpoint 구성
            client.SetSubmodelIdShot(aasId, videoDataSubmodelId);

            // UPDATE DATA
            await client.UpdateSubmodelElement(videoDataElementlId, videoDataElement);
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
            var client = new SubmodelHttpClient(_httpClient);

            // 장비 Event Submodel endpoint 설정
            client.SetSubmodelIdShot(aasId, eventsSubmodelId);

            // 비디오 data 갱신 EVENT FIRE
            await client.EventFireAsync(videoDataEvetId);
        }

        private static ICollection<Dictionary<string, object>> BuildSensorRawDatas(string aasId, string[] valueNames, int cnt)
        {
            Func<DateTime> defaultDt = () => DateTime.Now;
            var random = new Random((int)defaultDt().ToBinary());
            var datas = Enumerable.Range(1, cnt).Select(idx =>
            {
                var dataDic = new Dictionary<string, object>();
                foreach (var valueName in valueNames)
                {
                    object data;
                    if (valueName.ToLower().Contains("name"))
                    {
                        data = "equip1";
                    }
                    else if (valueName.ToLower().Contains("time"))
                    {
                        data = DateTime.UtcNow;
                    }
                    else if (valueName.ToLower().Contains("move") && new[] { "x", "y", "z" }.Any(o => valueName.ToLower().Contains(o)))
                    {
                        var onData = random.Next(100, 700);
                        var underData = random.Next(1, 99);
                        float ranData = onData + (float)(underData * 0.01);
                        data = ranData.ToString();
                    }
                    else
                    {
                        var onData = random.Next(3000, 7000);
                        //var underData = random.Next(1, 999999);
                        var underData = 0;
                        data = onData + (float)(underData * 0.000001);
                    }
                    dataDic.Add(valueName, data);
                }

                return dataDic;
            }).ToArray();
            return datas;
        }
    }
}
