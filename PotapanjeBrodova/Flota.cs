// "Flota.cs" u projektu "PotapanjeBrodova"
using System;
using System.Collections.Generic;

namespace PotapanjeBrodova
{
    public class Flota : IGađani
    {
        public void DodajBrod(IEnumerable<Polje> polja)
        {
            brodovi.Add(new Brod(polja));
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

        private List<Brod> brodovi = new List<Brod>();
    }
}
