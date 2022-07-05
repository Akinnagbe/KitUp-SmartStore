using Newtonsoft.Json;
using SmartStore.Core.Domain.DellyMan;
using SmartStore.Core.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Services.DellyMan
{
  public  class DellyManService: IDellyManService
    {
        public DellyManService()
        {
            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }
        public async Task<List<City>> GetCitiesAsync(string stateId)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["DellyMan:BaseUrl"]);
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ConfigurationManager.AppSettings["DellyMan:ApiKey"]);

                httpClient.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));                          
              
                string endpoint = string.Format("/api/v3.0/Cities?StateID={0}", stateId);
                var request =await httpClient.GetAsync(endpoint);
                var content =await request.Content.ReadAsStringAsync();
                Logger.InfoFormat("DellyMan Response for Get Cities is {0}", content);

                var cities = JsonConvert.DeserializeObject<List<City>>(content);

                return cities;
            }
        }

        public async Task<List<State>> GetStatesAsync()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["DellyMan:BaseUrl"]);
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ConfigurationManager.AppSettings["DellyMan:ApiKey"]);

                httpClient.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));

             
                var request = await httpClient.GetAsync("/api/v3.0/States");
                var content = await request.Content.ReadAsStringAsync();
                Logger.InfoFormat("DellyMan Response for Get States is {0}", content);

                var states = JsonConvert.DeserializeObject<List<State>>(content);

                return states;
            }
        }
    }
}
