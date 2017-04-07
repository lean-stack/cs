using Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LowLevelHttpWebRequest
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseUrl = "https://training-1344d.firebaseio.com/";
            string resource = "schulung";

            var rep = new Repository(baseUrl, resource);

            rep.Create("Cosma-Shiva", "Hagen", "Schauspiel");

            foreach (var item in rep.GetAll())
            {
                Console.WriteLine(item.Vorname);
            } 
        }
    }
}
