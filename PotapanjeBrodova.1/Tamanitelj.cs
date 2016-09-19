using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotapanjeBrodova
{
    using ListePolja = List<IEnumerable<Polje>>;

    public class Tamanitelj : IPucač
    {
        public Tamanitelj(Mreža mreža, int duljinaBroda, IEnumerable<Polje> pogođenaPolja)
        {
            this.mreža = mreža;
            this.duljinaBroda = duljinaBroda;
            this.tražilica = new TražilicaNizovaPolja(mreža);
            this.pogođenaPolja = new List<Polje>(pogođenaPolja);
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
            {
                pogođenaPolja.Add(zadnjeGađano);
                pogođenaPolja.Sortiraj();
            }
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
            var smjerovi = DajSmjerove();

            var nizPolja = tražilica.DajPoljaUNastavku(pogođenaPolja.First(), smjerovi.First());
            if (nizPolja.Count() >= 0)
                nizoviPolja.Add(nizPolja);
            nizPolja = tražilica.DajPoljaUNastavku(pogođenaPolja.Last(), smjerovi.Last());
            if (nizPolja.Count() >= 0)
                nizoviPolja.Add(nizPolja);

            // traži najdulji niz
            int najdulji = nizoviPolja.Max(niz => niz.Count());
            // odabire samo nizove koji su duljine veće od duljine broda - 1
            // ili su najveće duljine
            int granica = Math.Min(najdulji, duljinaBroda - pogođenaPolja.Count);
            var probraniNizovi = nizoviPolja.Where(niz => niz.Count() >= granica);
            // vraća prve članove nizova, tj. polja uz prvo pogođeno
            return probraniNizovi.Select(niz => niz.First());
        }

        private IEnumerable<Smjer> DajSmjerove()
        {
            if (pogođenaPolja.First().Redak == pogođenaPolja.Last().Redak)
                return new List<Smjer>{ Smjer.Lijevo, Smjer.Desno };
            return new List<Smjer> { Smjer.Gore, Smjer.Dolje };
        }

        private readonly Mreža mreža;
        private readonly int duljinaBroda;
        private Polje zadnjeGađano;
        private readonly List<Polje> pogođenaPolja;
        private readonly TražilicaNizovaPolja tražilica;
        private readonly Random slučajni = new Random();
    }
}
