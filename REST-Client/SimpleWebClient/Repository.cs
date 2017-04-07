using Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWebClient
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

        public IEnumerable<Teilnehmer> GetAll()
        {
            var client = new WebClient();
            var result = client.DownloadString(BaseUrl + Resource + "/teilnehmer.json");

            JObject obj = (JObject)JsonConvert.DeserializeObject(result);
            foreach (var token in obj.Children())
            {
                var tn = JsonConvert.DeserializeObject<Teilnehmer>(token.First.ToString());
                tn.Id = (token as JProperty).Name;
                yield return tn;
            }
        }

        public bool Delete(Teilnehmer t)
        {
            var client = new WebClient();
            client.UploadValues(BaseUrl + Resource + "/teilnehmer/" + t.Id + ".json","DELETE", new System.Collections.Specialized.NameValueCollection());

            return true;
        }

        public bool Update(Teilnehmer t)
        {
            var id = t.Id;

            t.Id = null;
            var payload = JsonConvert.SerializeObject(t);
            t.Id = id;

            var client = new WebClient();
            var result = client.UploadString(BaseUrl + Resource + "/teilnehmer/" + id + ".json", "PUT", payload);

            return true;
        }
    }
}
