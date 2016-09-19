// "Brodograditelj.cs" u projektu "PotapanjeBrodova"
using System;
using System.Collections.Generic;
using System.Linq;

namespace PotapanjeBrodova
{
    using NizoviPolja = IEnumerable<IEnumerable<Polje>>;

    public class Brodograditelj
    {
        public Flota SložiFlotu(int redaka, int stupaca,
                                IEnumerable<int> duljineBrodova)
        {
            duljineBrodova = duljineBrodova.OrderByDescending(d => d);
            const int brojPokušaja = 5;
            for (int i = 0; i < brojPokušaja; ++i)
            {
                Mreža mreža = new Mreža(redaka, stupaca);
                Flota flota = SložiBrodove(mreža, duljineBrodova);
                if (flota != null)
                    return flota;
            }
            return null;
        }

        private Flota SložiBrodove(Mreža mreža,
                                   IEnumerable<int> duljineBrodova)
        {
            Flota flota = new Flota();
            TražilicaNizovaPolja tražilica = new TražilicaNizovaPolja(mreža);
            ČistačPolja čistač = new ČistačPolja(mreža);
            foreach (int duljina in duljineBrodova)
            {
                var polja = mreža.RaspoloživaPolja;
                // raspoloživih polja manje nego duljina – ništa od flote!
                if (polja.Count() < duljina)
                    return null;
                var kandidati = tražilica.DajNizovePolja(duljina);
                // nema više nizova polja – ništa od flote!
                if (kandidati.Count() == 0)
                    return null;
                var izbor = Izaberi(kandidati);
                flota.DodajBrod(izbor);
                čistač.Ukloni(izbor);
            }
            return flota;
        }

        private IEnumerable<Polje> Izaberi(NizoviPolja kandidati)
        {
            int indeks = slučajni.Next(kandidati.Count());
            return kandidati.ElementAt(indeks);
        }

        private Random slučajni = new Random();
    }
}
