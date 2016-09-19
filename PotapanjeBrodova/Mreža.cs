// "Mreža.cs" u projektu "PotapanjeBrodova"
using System;
using System.Collections.Generic;

namespace PotapanjeBrodova
{
    using DvaDPolja = RječnikSDvaKljuča<int, int, Polje>;

    public class Mreža
    {
        public Mreža(int redaka, int stupaca)
        {
            Stupaca = stupaca;
            Redaka = redaka;
            for (int s = 0; s < Stupaca; ++s)
            {
                for (int r = 0; r < Redaka; ++r)
                    polja[s, r] = new Polje(s, r);
            }
        }

        public IEnumerable<Polje> RaspoloživaPolja
        {
            get
            {
                return polja.Vrijednosti;
            }
        }

        public void UkloniPolje(int stupac, int redak)
        {
            if (polja.Ukloni(stupac, redak))
                return;
            if (redak < 0 || redak >= Redaka)
                throw new ArgumentOutOfRangeException(string.Format("Redak {0} je izvan dozvoljenog rapona vrijednosti", redak));
            if (stupac < 0 || stupac >= Stupaca)
                throw new ArgumentOutOfRangeException(string.Format("Stupac {0} je izvan dozvoljenog rapona vrijednosti", stupac));
        }

        public void UkloniPolje(Polje polje)
        {
            UkloniPolje(polje.Stupac, polje.Redak);
        }

        private DvaDPolja polja = new DvaDPolja();

        public readonly int Stupaca;
        public readonly int Redaka;
    }
}
