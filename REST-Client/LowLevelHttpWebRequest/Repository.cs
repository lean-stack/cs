using Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LowLevelHttpWebRequest
{
    public class Repository : ITeilnehmerRepository
    {
        public Repository(string baseUrl, string resource)
        {
            BaseUrl = baseUrl;
            Resource = resource;
        }

        public string BaseUrl { get; set; }
        public string Resource { get; set; }

        public IEnumerable<Teilnehmer> GetAll()
        {
            var request = WebRequest.Create(BaseUrl + Resource + "/teilnehmer.json");
            WebResponse response = request.GetResponse();

            Stream stream = response.GetResponseStream();
            string result = null;
            using (StreamReader sr = new StreamReader(stream, Encoding.Default))
            {
                result = sr.ReadToEnd();
            }

            response.Close();

            JObject obj = (JObject) JsonConvert.DeserializeObject(result);
            foreach (var token in obj.Children())
            {
                var tn = JsonConvert.DeserializeObject<Teilnehmer>(token.First.ToString());
                tn.Id = (token as JProperty).Name;
                yield return tn;
            }
        }

        public Teilnehmer Create(string vorname, string nachname, string firma)
        {
            var nr = getNextNr();
            var tn = new Teilnehmer(nr, vorname, nachname, firma);

            byte[] data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(tn));

            var request = WebRequest.Create(BaseUrl + Resource + "/teilnehmer.json");

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(data, 0, data.Length);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            string content = null;
            using (StreamReader sr = new StreamReader(responseStream, Encoding.Default))
            {
                content = sr.ReadToEnd();
            }

            response.Close();

            saveLastNr(nr);

            var result = JsonConvert.DeserializeObject<Key>(content);
            tn.Id = result.Name;

            return tn;
        }

        public bool Delete(Teilnehmer t)
        {
            throw new NotImplementedException();
        }

        public bool Update(Teilnehmer t)
        {
            throw new NotImplementedException();
        }

        // Helper
        private class Key
        {
            public string Name { get; set; }
        }

        private int getNextNr()
        {
            var request = WebRequest.Create(BaseUrl + Resource + "/maxId.json");

            WebResponse response = request.GetResponse();

            Stream stream = response.GetResponseStream();
            string maxId = null;
            using (StreamReader sr = new StreamReader(stream, Encoding.Default))
            {
                maxId = sr.ReadToEnd();
            }
            
            if (String.IsNullOrEmpty(maxId) || maxId.Equals("null"))
            {
                return 1;
            } 
            else
            {
                return int.Parse(maxId) + 1;
            }
        }

        private void saveLastNr(int nr)
        {
            var request = WebRequest.Create(BaseUrl + Resource + "/maxId.json");

            byte[] data = Encoding.ASCII.GetBytes(nr.ToString());

            request.Method = "PUT";
            request.ContentLength = data.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(data, 0, data.Length);
            }

            WebResponse response = request.GetResponse();
            
            Stream responseStream = response.GetResponseStream();
            string content = null;
            using (StreamReader sr = new StreamReader(responseStream, Encoding.Default))
            {
                content = sr.ReadToEnd();
            }

            response.Close();

            Console.WriteLine(content);
        }
    }
}
