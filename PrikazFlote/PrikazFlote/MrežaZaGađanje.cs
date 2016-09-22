using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using PotapanjeBrodova;
using System.Collections;

namespace PrikazFlote
{
    public class MrežaZaGađanje : AbsoluteLayout
    {
        //mre
        public MrežaZaGađanje()
        {

            polja = new PoljeZaPrikaz[Stupaca, Redaka];
            for (int s = 0; s < Stupaca; ++s)
            {
                for (int r = 0; r < Redaka; ++r)
                {
                    var polje = new PoljeZaPrikaz(s, r);
                    polja[s, r] = polje;
                    Children.Add(polje);
                    polje.ProtivničkoPoljeJeGađano += OnProtivničkoPoljeJeGađano; //registriramo handler(desno) za event(lijevo)

                }
            }

            SizeChanged += (sender, args) =>
            {
                double širinaPolja = (Width - Stupaca * RazmakIzmeđuPolja) / Stupaca;
                double visinaPolja = (Height - Redaka * RazmakIzmeđuPolja) / Redaka;
                double veličina = Math.Min(širinaPolja, visinaPolja);

                foreach (PoljeZaPrikaz polje in polja)
                {
                    Rectangle okvir = new Rectangle(polje.Stupac * veličina + RazmakIzmeđuPolja, polje.Redak * veličina + RazmakIzmeđuPolja, veličina - RazmakIzmeđuPolja, veličina - RazmakIzmeđuPolja);
                    SetLayoutBounds(polje, okvir);
                }
            };

            SložiFlotu();
        }

        public void SložiFlotu()
        {
            Brodograditelj bg = new Brodograditelj();
            int[] brodovi = new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };
            flota = bg.SložiFlotu(10, 10, brodovi);
            flota.ProtivničkiBrodJePotopljen += OnProtivničkiBrodJePotopljen;
        }

        private void OnProtivničkiBrodJePotopljen(object sender, Flota.PotopljeniBrodEventArgs e)
        {
            if(e!=null && e.PoljaPotopljenogBroda != null)
            foreach (PoljeZaPrikaz polje in polja)
            {
                if (e.PoljaPotopljenogBroda.Contains(new Polje(polje.Stupac, polje.Redak)))
                    polje.ObojiPolje(RezultatGađanja.Potonuće);
            }
        }

        public void OnProtivničkoPoljeJeGađano(object sender, GađanoPoljeEventArgs e)
        {

            RezultatGađanja rez = flota.Gađaj(e.Polje);


            e.rez = rez;
        }


        private void OnProtivničkiBrodJePotopljen(object sender, Brod.PotopljeniBrodEventArgs e)
        {
           
        }

        private const int Stupaca = 10;
        private const int Redaka = 10;
        private const int RazmakIzmeđuPolja = 1;

        PoljeZaPrikaz[,] polja;

        List<Polje> poljaPotopljenogBroda;

        Flota flota;
    }
}
