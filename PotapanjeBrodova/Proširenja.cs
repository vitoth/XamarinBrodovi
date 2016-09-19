// "Proširenja.cs" u projektu "PotapanjeBrodova"
using System;
using System.Collections.Generic;
using System.Linq;

namespace PotapanjeBrodova
{
    public static class Proširenja
    {
        public static IEnumerable<Polje> Sortiraj(this IEnumerable<Polje> polja)
        {
            if (polja.Count() <= 1)
                return polja;
            Polje prvo = polja.First();
            bool uspravnoPoravnata = polja.Skip(1).All(p => p.Stupac == prvo.Stupac);
            bool vodoravnoPoravnata = polja.Skip(1).All(p => p.Redak == prvo.Redak);
            if (vodoravnoPoravnata == uspravnoPoravnata)
                throw new ArgumentException();
            return polja.OrderBy(p => p.Redak + p.Stupac);
        }
    }
}
