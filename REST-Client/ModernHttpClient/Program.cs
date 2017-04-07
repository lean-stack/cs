using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernHttpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseUrl = "https://training-1344d.firebaseio.com/";
            string resource = "schulung";

            var rep = new Repository(baseUrl, resource);

            var liste = rep.GetAll();
            foreach (var item in liste)
            {
                Console.WriteLine(item.Nr + ": " + item.Vorname + $" ({item.Id})");
            }
        }
    }
}
