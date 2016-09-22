// "Brod.cs" u projektu "PotapanjeBrodova"
using System;
using System.Collections.Generic;
using System.Linq;

namespace PotapanjeBrodova
{
    

    public class Brod : IGađani
    {

        public class PotopljeniBrodEventArgs : EventArgs
        {
            public PotopljeniBrodEventArgs(List<Polje> poljaPotopljenogBroda)
            {
                PoljaPotopljenogBroda = poljaPotopljenogBroda;
            }
            public List<Polje> PoljaPotopljenogBroda;
        }

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
            {
                OnProtivničkiBrodJePotopljen(); //event zovemo
                return RezultatGađanja.Potonuće;
            }
            return RezultatGađanja.Pogodak;
        }

        public delegate void ProtivničkiBrodJePotopljenEventHandler(object sender, PotopljeniBrodEventArgs e);

        public event ProtivničkiBrodJePotopljenEventHandler ProtivničkiBrodJePotopljen;

        protected virtual void OnProtivničkiBrodJePotopljen()
        {
            PotopljeniBrodEventArgs args = new PotopljeniBrodEventArgs(pogođenaPolja.ToList());
            ProtivničkiBrodJePotopljen?.Invoke(this, args);
        }

        public readonly IEnumerable<Polje> Polja;

        private HashSet<Polje> pogođenaPolja = new HashSet<Polje>();
    }

  
}
