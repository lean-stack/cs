using Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ModernHttpClient
{
    class Repository : ITeilnehmerRepository
    {
        public Repository(string baseUrl, string resource)
        {
            BaseUrl = baseUrl;
            Resource = resource;
        }

        public string BaseUrl { get; set; }
        public string Resource { get; set; }

        public Teilnehmer Create(string vorname, string nachname, string firma)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Teilnehmer t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Teilnehmer> GetAll()
        {
            string result = null;
            using( var client = new HttpClient())
            {
                var response = client.GetAsync(BaseUrl + Resource + "/teilnehmer.json").Result;
                result = response.Content.ReadAsStringAsync().Result;

            }

            JObject obj = (JObject)JsonConvert.DeserializeObject(result);
            foreach (var token in obj.Children())
            {
                var tn = JsonConvert.DeserializeObject<Teilnehmer>(token.First.ToString());
                tn.Id = (token as JProperty).Name;
                yield return tn;
            }
        }

        public bool Update(Teilnehmer t)
        {
            throw new NotImplementedException();
        }
    }
}
