using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ITeilnehmerRepository
    {
        IEnumerable<Teilnehmer> GetAll();

        Teilnehmer Create(string vorname, string nachname, string firma);
        bool Update(Teilnehmer t);
        bool Delete(Teilnehmer t);
    }
}
