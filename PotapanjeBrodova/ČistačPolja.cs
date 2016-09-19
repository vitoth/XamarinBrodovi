// "ČistačPolja.cs" u projektu "PotapanjeBrodova"
using System;
using System.Collections.Generic;
using System.Linq;

namespace PotapanjeBrodova
{
    public class ČistačPolja
    {
        public ČistačPolja(Mreža mreža)
        {
            this.mreža = mreža;
        }

        public void Ukloni(IEnumerable<Polje> brodskaPolja)
        {
            brodskaPolja = brodskaPolja.Sortiraj();
            Polje prvo = brodskaPolja.First();
            int s1 = Math.Max(prvo.Stupac - 1, 0);
            int r1 = Math.Max(prvo.Redak - 1, 0);
            Polje zadnje = brodskaPolja.Last();
            int s2 = Math.Min(zadnje.Stupac + 2, mreža.Stupaca);
            int r2 = Math.Min(zadnje.Redak + 2, mreža.Redaka);
            for (int s = s1; s < s2; ++s)
            {
                for (int r = r1; r < r2; ++r)
                    mreža.UkloniPolje(s, r);
            }
        }

        private Mreža mreža;
    }
}
