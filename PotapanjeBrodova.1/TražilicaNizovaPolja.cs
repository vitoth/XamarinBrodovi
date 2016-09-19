// "IzbornikPoljaZaBrod.cs" u projektu "PotapanjeBrodova"
using System;
using System.Collections.Generic;
using System.Linq;

namespace PotapanjeBrodova
{
    using DovoljanOdmak = Func<Polje, Polje, bool>;
    using JesuLiUNizu = Func<Polje, Polje, bool>;
    using ListePolja = List<IEnumerable<Polje>>;
    // skraćeni sinonimi
    using NizoviPolja = IEnumerable<IEnumerable<Polje>>;

    public enum Smjer
    {
        Gore,
        Desno,
        Dolje,
        Lijevo
    }

    public class TražilicaNizovaPolja
    {
        public TražilicaNizovaPolja(Mreža mreža)
        {
            this.mreža = mreža;
        }

        public NizoviPolja DajNizovePolja(int duljina)
        {
            return DajNizove(duljina, JeLiDesno, DovoljnoOdmaknutoOdNajdesnijeg)
                .Concat(DajNizove(duljina, JeLiDolje, DovoljnoOdmaknutoOdNajdonjeg));
        }

        public IEnumerable<Polje> DajPoljaUNastavku(Polje polje, Smjer smjer)
        {
            switch (smjer)
            {
                case Smjer.Gore:
                    return PoljaUNastavku(polje, JeLiIznad);
                case Smjer.Desno:
                    return PoljaUNastavku(polje, JeLiDesno);
                case Smjer.Dolje:
                    return PoljaUNastavku(polje, JeLiDolje);
                case Smjer.Lijevo:
                    return PoljaUNastavku(polje, JeLiLijevo);
            }
            throw new ArgumentOutOfRangeException("Nepodržani smjer.");
        }

        private NizoviPolja DajNizove(int duljina, JesuLiUNizu jeLiUNizu, DovoljanOdmak dovoljnoOdmaknuto)
        {
            int najdesnijiStupac = mreža.RaspoloživaPolja.Max(p => p.Stupac);
            int najdonjiRedak = mreža.RaspoloživaPolja.Max(p => p.Redak);
            Polje granica = new Polje(najdesnijiStupac - duljina + 1, najdonjiRedak - duljina + 1);
            ListePolja liste = new ListePolja();
            foreach (Polje početno in mreža.RaspoloživaPolja)
            {
                // dodatni uvjet kojim izbjegavamo jalova pretraživanja:
                if (dovoljnoOdmaknuto(početno, granica))
                {
                    List<Polje> poljaUNizu = PoljaUNizu(početno, duljina, jeLiUNizu);
                    if (poljaUNizu.Count() == duljina)
                        liste.Add(poljaUNizu);
                }
            }
            return liste;
        }

        private List<Polje> PoljaUNizu(Polje polje, int najvišePolja, JesuLiUNizu jeLiUNizu)
        {
            List<Polje> polja = new List<Polje> { polje };
            while (--najvišePolja > 0)
            {
                polje = mreža.RaspoloživaPolja.FirstOrDefault(p => jeLiUNizu(p, polje));
                if (polje == null)
                    break;
                polja.Add(polje);
            }
            return polja;
        }

        private List<Polje> PoljaUNastavku(Polje polje, JesuLiUNizu jeLiUNizu)
        {
            List<Polje> polja = new List<Polje>();
            while ((polje = mreža.RaspoloživaPolja.FirstOrDefault(p => jeLiUNizu(p, polje))) != null)
                polja.Add(polje);
            return polja;
        }

        bool JeLiDesno(Polje polje, Polje prethodno)
        {
            return polje.Redak == prethodno.Redak && polje.Stupac == prethodno.Stupac + 1;
        }

        bool JeLiDolje(Polje polje, Polje prethodno)
        {
            return polje.Stupac == prethodno.Stupac && polje.Redak == prethodno.Redak + 1;
        }

        bool JeLiLijevo(Polje polje, Polje prethodno)
        {
            return polje.Redak == prethodno.Redak && polje.Stupac == prethodno.Stupac - 1;
        }

        bool JeLiIznad(Polje polje, Polje prethodno)
        {
            return polje.Stupac == prethodno.Stupac && polje.Redak == prethodno.Redak - 1;
        }

        private bool DovoljnoOdmaknutoOdNajdesnijeg(Polje polje, Polje granica)
        {
            return polje.Stupac <= granica.Stupac;
        }

        private bool DovoljnoOdmaknutoOdNajdonjeg(Polje polje, Polje granica)
        {
            return polje.Redak <= granica.Redak;
        }

        private Mreža mreža;
    }
}
