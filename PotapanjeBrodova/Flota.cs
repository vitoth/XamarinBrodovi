// "Flota.cs" u projektu "PotapanjeBrodova"
using System;
using System.Collections.Generic;
using System.Linq;

namespace PotapanjeBrodova
{
    public class Flota : IGađani
    {
        public class PotopljeniBrodEventArgs : EventArgs
        {
            public PotopljeniBrodEventArgs(List<Polje> poljaPotopljenogBroda)
            {
                PoljaPotopljenogBroda = poljaPotopljenogBroda;
            }
            public List<Polje> PoljaPotopljenogBroda;
        }

        public void DodajBrod(IEnumerable<Polje> polja)
        {
            Brod brod = new Brod(polja);
            brodovi.Add(brod);
            brod.ProtivničkiBrodJePotopljen += OnProtivničkiBrodJePotopljen;

        }

        private void OnProtivničkiBrodJePotopljen(object sender, Brod.PotopljeniBrodEventArgs e)
        {
           
            PotopljeniBrodEventArgs args = new PotopljeniBrodEventArgs(e.PoljaPotopljenogBroda);
            ProtivničkiBrodJePotopljen?.Invoke(this, args);
        }

        public RezultatGađanja Gađaj(Polje p)
        {
            foreach (Brod brod in brodovi)
            {
                RezultatGađanja rez = brod.Gađaj(p);
                if (rez != RezultatGađanja.Promašaj)
                    return rez;
            }
            return RezultatGađanja.Promašaj;
        }

        public IEnumerable<Brod> Brodovi
        {
            get { return brodovi; }
        }



        public delegate void ProtivničkiBrodJePotopljenEventHandler(object sender, PotopljeniBrodEventArgs e);

        public event ProtivničkiBrodJePotopljenEventHandler ProtivničkiBrodJePotopljen;

        private List<Brod> brodovi = new List<Brod>();
    }
}
