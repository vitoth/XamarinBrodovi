// 'NasumičniIzbornikPolja.cs' u projektu 'PotapanjeBrodova'
using System;
using System.Collections.Generic;
using System.Linq;

namespace PotapanjeBrodova
{
    using NizoviPolja = IEnumerable<IEnumerable<Polje>>;

    class NasumičniIzbornikPolja
    {
        public IEnumerable<Polje> Izaberi(NizoviPolja kandidati)
        {
            int indeks = slučajni.Next(kandidati.Count());
            return kandidati.ElementAt(indeks);
        }

        private Random slučajni = new Random();
    }
}
