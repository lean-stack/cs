using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class Teilnehmer
    {
        public string Id { get; set; }

        public int Nr { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Firma { get; set; }

        public Teilnehmer()
        {

        }

        public Teilnehmer(int nr, string firstname, string lastname, string company)
        {
            Nr = nr;
            Vorname = firstname;
            Nachname = lastname;
            Firma = company;
        }
   }
}
