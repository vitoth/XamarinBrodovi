using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using PotapanjeBrodova;

namespace PrikazFlote
{
    public class MrežaZaPrikaz : AbsoluteLayout
    {
        public MrežaZaPrikaz()
        {
            polja = new PoljeZaPrikaz[Stupaca, Redaka];
            for (int s = 0; s < Stupaca; ++s)
            {
                for (int r = 0; r < Redaka; ++r)
                {
                    polja[s, r] = new PoljeZaPrikaz(s, r);
                    Children.Add(polja[s, r]);
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
        }

        public void PrikažiFlotu(Flota flota)
        {
            foreach (var polje in polja)
                polje.Isprazni();

            foreach (Brod brod in flota.Brodovi)
            {
                foreach (Polje polje in brod.Polja)
                {
                    int stupac = polje.Stupac;
                    int redak = polje.Redak;
                    polja[stupac, redak].SmjestiBrod();
                }
            }
        }

        private const int Stupaca = 10;
        private const int Redaka = 10;
        private const int RazmakIzmeđuPolja = 1;

        PoljeZaPrikaz[,] polja;
    }
}
