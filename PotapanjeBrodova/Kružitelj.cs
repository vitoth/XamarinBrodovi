using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PotapanjeBrodova
{
    using ListePolja = List<IEnumerable<Polje>>;

    public class Kružitelj : IPucač
    {
        public Kružitelj(Mreža mreža, int duljinaBroda, Polje pogođenoPolje)
        {
            this.mreža = mreža;
            this.duljinaBroda = duljinaBroda;
            this.tražilica = new TražilicaNizovaPolja(mreža);
            pogođenaPolja.Add(pogođenoPolje);
        }

        public Polje UputiPucanj()
        {
            Debug.Assert(zadnjeGađano == null);

            var kandidati = DajKandidate();
            int izbor = slučajni.Next(kandidati.Count());
            zadnjeGađano = kandidati.ElementAt(izbor);
            mreža.UkloniPolje(zadnjeGađano);
            return zadnjeGađano;
        }

        public void EvidentirajRezultat(RezultatGađanja rezultat)
        {
            if (rezultat != RezultatGađanja.Promašaj)
                pogođenaPolja.Add(zadnjeGađano);
            zadnjeGađano = null;
        }

        public IEnumerable<Polje> PogođenaPolja
        {
            get
            {
                return pogođenaPolja;
            }
        }

        public IEnumerable<Polje> DajKandidate()
        {
            ListePolja nizoviPolja = new ListePolja();
            foreach (Smjer smjer in Enum.GetValues(typeof(Smjer)))
            {
                var nizPolja = tražilica.DajPoljaUNastavku(pogođenaPolja.First(), smjer);
                if (nizPolja.Count() >= 0)
                    nizoviPolja.Add(nizPolja);
            }
            // traži najdulji niz
            int najdulji = nizoviPolja.Max(niz => niz.Count());
            // odabire samo nizove koji su duljine veće od duljine broda - 1
            // ili su najveće duljine
            int granica = Math.Min(najdulji, duljinaBroda - 1);
            var probraniNizovi = nizoviPolja.Where(niz => niz.Count() >= granica);
            // vraća prve članove nizova, tj. polja uz prvo pogođeno
            return probraniNizovi.Select(niz => niz.First());
        }

        private readonly Mreža mreža;
        private readonly int duljinaBroda;
        private Polje zadnjeGađano;
        private readonly List<Polje> pogođenaPolja = new List<Polje>();
        private readonly TražilicaNizovaPolja tražilica;
        private readonly Random slučajni = new Random();
    }
}
