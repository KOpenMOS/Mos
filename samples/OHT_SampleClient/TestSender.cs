using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BaSyx.Models.Core.AssetAdministrationShell.Implementations.SubmodelElementTypes;
using MOS.AAS.Client.Http;
using BaSyx.Models.Core.Common;
using BaSyx.Models.Core.AssetAdministrationShell.Generics;

namespace OHT_SampleClient
{
    public class TestSender
    {
        private string BaseAddress = $"http://localhost:5000/";

        private Timer _timer1 { get; set; } = null!;
        private Timer _timer2 { get; set; } = null!;
        private Timer _timer3 { get; set; } = null!;

        public TestSender()
        {
            _timer1 = new Timer(async o => await Do1One(o), null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(4));
            //_timer1 = new Timer(async o => await Do1(o), null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(4));
            //_timer2 = new Timer(async o => await Do2(o), null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(7));
            //_timer3 = new Timer(async o => await Do3(o), null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));
        }

        public async Task Do1One(object? state)
        {
            var aasId = "OHT_001";
            var submodelId = "Sensor1";
            var submodelElementId = $"Data{submodelId}";

            // 임시생성 데이터
            var datas = GetDatasDic(aasId, new[]
            {
                "time",
                "VibrationAx",
                "VibrationAy",
                "VibrationAz",
                "VibrationGx",
                "VibrationGy",
                "VibrationGz",
            }, 30).ToArray();

            /*
             * Submodel-SubmodelElement(Container)-SubmodelCollection-List<Property>
             * 
             * Submodel : submodel
             * SubmodelElement(Container) : submodel.SubmodelElements
             * SubmodelCollection : 1개 row 데이터 (collection형태)
             * List<Property> : 여러 데이터  (time, VibrationAx,  VibrationAy, VibrationAz, ...)
             */

            // 전달 데이터 구조
            var collection = new SubmodelElementCollection()
            {
                IdShort = submodelElementId,
                Value = new ElementContainer<ISubmodelElement>(new ISubmodelElement[]
                {
                    new SubmodelElementCollection()
                    {
                        IdShort = $"{0}",
                        Value = new ElementContainer<ISubmodelElement>(new ISubmodelElement[]
                        {
                            new Property{IdShort = "time", Value = datas[0]["time"] },
                            new Property{IdShort = "VibrationAx", Value = datas[0]["VibrationAx"] },
                            new Property{IdShort = "VibrationAy", Value = datas[0]["VibrationAy"] },
                            new Property{IdShort = "VibrationAz", Value = datas[0]["VibrationAz"] },
                            new Property{IdShort = "VibrationGx", Value = datas[0]["VibrationGx"] },
                            new Property{IdShort = "VibrationGy", Value = datas[0]["VibrationGy"] },
                            new Property{IdShort = "VibrationGz", Value = datas[0]["VibrationGz"] },
                        })
                    },
                    new SubmodelElementCollection()
                    {
                        IdShort = $"{1}",
                        Value = new ElementContainer<ISubmodelElement>(new ISubmodelElement[]
                        {
                            new Property{IdShort = "time", Value = datas[0]["time"] },
                            new Property{IdShort = "VibrationAx", Value = datas[1]["VibrationAx"] },
                            new Property{IdShort = "VibrationAy", Value = datas[1]["VibrationAy"] },
                            new Property{IdShort = "VibrationAz", Value = datas[1]["VibrationAz"] },
                            new Property{IdShort = "VibrationGx", Value = datas[1]["VibrationGx"] },
                            new Property{IdShort = "VibrationGy", Value = datas[1]["VibrationGy"] },
                            new Property{IdShort = "VibrationGz", Value = datas[1]["VibrationGz"] },
                        })
                    },
                    new SubmodelElementCollection()
                    {
                        IdShort = $"{2}",
                        Value = new ElementContainer<ISubmodelElement>(new ISubmodelElement[]
                        {
                            new Property{IdShort = "time", Value = datas[0]["time"] },
                            new Property{IdShort = "VibrationAx", Value = datas[2]["VibrationAx"] },
                            new Property{IdShort = "VibrationAy", Value = datas[2]["VibrationAy"] },
                            new Property{IdShort = "VibrationAz", Value = datas[2]["VibrationAz"] },
                            new Property{IdShort = "VibrationGx", Value = datas[2]["VibrationGx"] },
                            new Property{IdShort = "VibrationGy", Value = datas[2]["VibrationGy"] },
                            new Property{IdShort = "VibrationGz", Value = datas[2]["VibrationGz"] },
                        })
                    },
                    new SubmodelElementCollection()
                    {
                        IdShort = $"{3}",
                        Value = new ElementContainer<ISubmodelElement>(new ISubmodelElement[]
                        {
                            new Property{IdShort = "time", Value = datas[0]["time"] },
                            new Property{IdShort = "VibrationAx", Value = datas[3]["VibrationAx"] },
                            new Property{IdShort = "VibrationAy", Value = datas[3]["VibrationAy"] },
                            new Property{IdShort = "VibrationAz", Value = datas[3]["VibrationAz"] },
                            new Property{IdShort = "VibrationGx", Value = datas[3]["VibrationGx"] },
                            new Property{IdShort = "VibrationGy", Value = datas[3]["VibrationGy"] },
                            new Property{IdShort = "VibrationGz", Value = datas[3]["VibrationGz"] },
                        })
                    },
                    new SubmodelElementCollection()
                    {
                        IdShort = $"{4}",
                        Value = new ElementContainer<ISubmodelElement>(new ISubmodelElement[]
                        {
                            new Property{IdShort = "time", Value = datas[0]["time"] },
                            new Property{IdShort = "VibrationAx", Value = datas[4]["VibrationAx"] },
                            new Property{IdShort = "VibrationAy", Value = datas[4]["VibrationAy"] },
                            new Property{IdShort = "VibrationAz", Value = datas[4]["VibrationAz"] },
                            new Property{IdShort = "VibrationGx", Value = datas[4]["VibrationGx"] },
                            new Property{IdShort = "VibrationGy", Value = datas[4]["VibrationGy"] },
                            new Property{IdShort = "VibrationGz", Value = datas[4]["VibrationGz"] },
                        })
                    },
                })
            };

            var client = new SubmodelHttpClient(GetHttpClient());
            // set address
            client.SetSubmodelIdShot(aasId, submodelId);

            // UPDATE DATA
            await client.UpdateSubmodelElement(submodelElementId, collection);

            // EVENT FIRE
            var eventId = $"{submodelId}Event";
            var eventSubmodelId = "InputEvent";
            // set address
            client.SetSubmodelIdShot(aasId, eventSubmodelId);
            await client.EventFireAsync(eventId);
        }


        public async Task Do1(object? state)
        {
            var ohtTasks = Enumerable.Range(1, 3).Select(n => $"OHT_00{n}").Select(ohtId =>
            {
                var sensorIds = Enumerable.Range(1, 4).Select(n => $"Sensor{n}");
                var tasks = sensorIds.Select(async (sensorId, idx) =>
                {
                    var aasId = ohtId;
                    var submodelId = sensorId;
                    var submodelElementId = $"Data{sensorId}";

                    // 데이터
                    var datas = GetDatasDic(aasId, new[]
                    {
                        "time",
                        "VibrationAx",
                        "VibrationAy",
                        "VibrationAz",
                        "VibrationGx",
                        "VibrationGy",
                        "VibrationGz",
                    }, 30);
                    var properties = datas.Select((data, idx) => new Property { IdShort = idx.ToString(), Value = JsonConvert.SerializeObject(data) });

                    // 전달
                    var equip1_elementCollection = new SubmodelElementCollection()
                    {
                        IdShort = submodelElementId,
                    };
                    equip1_elementCollection.Value.AddRange(properties);


                    var client = new SubmodelHttpClient(GetHttpClient());
                    client.SetSubmodelIdShot(aasId, submodelId);

                    // update data
                    await client.UpdateSubmodelElement(submodelElementId, equip1_elementCollection);

                    //event fire
                    var eventId = $"Sensor{idx}Event";
                    var eventSubmodelId = "InputEvent";
                    client.SetSubmodelIdShot(aasId, eventSubmodelId);
                    await client.EventFireAsync(eventId);
                });

                return tasks;
            });
            var allTasks = ohtTasks.SelectMany(t => t);
            await Task.WhenAll(allTasks);
        }

        public async Task Do2(object? state)
        {
            var ohtTasks = Enumerable.Range(1, 3).Select(n => $"OHT_00{n}").Select(ohtId =>
            {
                var sensorIds = new[] { "Video1", };
                var tasks = sensorIds.Select(async (sensorId, idx) =>
                {
                    var aasId = ohtId;
                    var submodelId = sensorId;
                    var submodelElementId = $"Data{sensorId}";

                    // 데이터
                    var url = GetTempFileUrl();
                    var property = new File { IdShort = "0", Value = url };

                    // 전달
                    var equip1_elementCollection = new SubmodelElementCollection()
                    {
                        IdShort = submodelElementId,
                    };
                    equip1_elementCollection.Value.Add(property);

                    var client = new SubmodelHttpClient(GetHttpClient());
                    client.SetSubmodelIdShot(aasId, submodelId);

                    // update data
                    await client.UpdateSubmodelElement(submodelElementId, equip1_elementCollection);

                    //event fire
                    var eventId = $"Sensor{idx}Event";
                    var eventSubmodelId = "InputEvent";
                    client.SetSubmodelIdShot(aasId, eventSubmodelId);
                    await client.EventFireAsync(eventId);
                });
                return tasks;
            });

            var allTasks = ohtTasks.SelectMany(t => t);
            await Task.WhenAll(allTasks);
        }

        public async Task Do3(object? state)
        {
            var ohtTasks = Enumerable.Range(1, 2).Select(n => $"OTHER_{n:000}").Select(ohtId =>
            {
                var sensorIds = new[] { "Video1", };
                var tasks = sensorIds.Select(async (sensorId, idx) =>
                {
                    var aasId = ohtId;
                    var submodelId = sensorId;
                    var submodelElementId = $"Data{sensorId}";

                    // 데이터
                    var url = GetTempFileUrl();
                    var property = new File { IdShort = "0", Value = url };

                    // 전달
                    var equip1_elementCollection = new SubmodelElementCollection()
                    {
                        IdShort = submodelElementId,
                    };
                    equip1_elementCollection.Value.Add(property);

                    var client = new SubmodelHttpClient(GetHttpClient());
                    client.SetSubmodelIdShot(aasId, submodelId);

                    // update data
                    await client.UpdateSubmodelElement(submodelElementId, equip1_elementCollection);

                    //event fire
                    var eventId = $"Sensor{idx}Event";
                    var eventSubmodelId = "InputEvent";
                    client.SetSubmodelIdShot(aasId, eventSubmodelId);
                    await client.EventFireAsync(eventId);
                });
                return tasks;
            });

            var allTasks = ohtTasks.SelectMany(t => t);
            await Task.WhenAll(allTasks);
        }

        private HttpClient GetHttpClient(string url = "")
        {
            var baseAddress = string.IsNullOrWhiteSpace(url) ? this.BaseAddress : url;

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);
            return httpClient;
        }

        private string GetTempFileUrl()
        {
            // 임시 경로
            var videos = new[]
            {
                    @"https://sec.ch9.ms/ch9/d3a3/c6523df8-4b00-4943-b1d9-19d60b51d3a3/CTCDataScienceMod4V3_mid.mp4",
                    @"https://sec.ch9.ms/ch9/e1a7/474d8217-8ba7-4f45-b17b-e72b5046e1a7/ONDOTNETEventDrivenApplicationsWithKeda_high.mp4",
                    @"https://sec.ch9.ms/ch9/4859/4acfaded-0a03-4c62-af93-360d4d724859/IoTShow-HighSchoolProject_high.mp4",
                    @"https://sec.ch9.ms/ch9/bf96/f53cca7c-6774-44d8-91b7-43b10d1ebf96/DATAEXPOSEDAzureArcDataServicesAnywhere_high.mp4",
                    @"https://sec.ch9.ms/ch9/661b/9a6c6d1c-36cd-40ca-a8a6-c88aef8b661b/AKSBootcampPr%C3%A9M%C3%B3duloIntrodu%C3%A7%C3%A3odocursoeapresenta%C3%A7%C3%A3_high.mp4",
                    @"https://sec.ch9.ms/ch9/b883/1fb778f9-fe2f-4f47-832d-e56fd6bbb883/IoTShow-TwinThread-v2_high.mp4",
                    @"https://sec.ch9.ms/ch9/f9c6/2d834f6f-708a-4ea5-b909-94c0045af9c6/DEVOPSLABOctopusDeployandGitHubActions_high.mp4",
                    @"https://sec.ch9.ms/ch9/9a10/583c0106-77fc-48bc-9d63-04e57e299a10/BuildSQLDatabase_high.mp4",
                };
            var cnt = videos.Length;
            var remain = DateTime.Now.Ticks % cnt;
            return videos[remain];
        }

        private StringContent GetContent(string content)
        {
            return new StringContent(content, Encoding.UTF8, "application/json");
        }

        private ICollection<Dictionary<string, object>> GetDatasDic(string aasId, string[] valueNames, int cnt)
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
                        float ranData = onData + (float)(underData * 0.000001);
                        data = ranData.ToString();
                    }
                    dataDic.Add(valueName, data);
                }

                return dataDic;
            }).ToArray();
            return datas;
        }

    }
}
