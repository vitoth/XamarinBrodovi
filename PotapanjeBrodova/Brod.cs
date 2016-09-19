// "Brod.cs" u projektu "PotapanjeBrodova"
using System;
using System.Collections.Generic;
using System.Linq;

namespace PotapanjeBrodova
{
    public class Brod : IGađani
    {
        public Brod(IEnumerable<Polje> polja)
        {
            Polja = polja;
        }

        public RezultatGađanja Gađaj(Polje p)
        {
            if (!Polja.Contains(p))
                return RezultatGađanja.Promašaj;
            pogođenaPolja.Add(p);
            if (pogođenaPolja.Count() == Polja.Count())
                return RezultatGađanja.Potonuće;
            return RezultatGađanja.Pogodak;
        }

        public readonly IEnumerable<Polje> Polja;

        private HashSet<Polje> pogođenaPolja = new HashSet<Polje>();
    }
}
