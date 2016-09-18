using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

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

            BackgroundColor = Color.Navy;

            SizeChanged += (sender, args) =>
            {
                double širinaPolja = (Width - Stupaca * RazmakIzmeđuPolja) / Stupaca;
                double visinaPolja = (Height - Redaka * RazmakIzmeđuPolja) / Redaka;

                foreach (PoljeZaPrikaz polje in polja)
                {
                    Rectangle okvir = new Rectangle(polje.Stupac * širinaPolja + RazmakIzmeđuPolja, polje.Redak * visinaPolja + RazmakIzmeđuPolja, širinaPolja - RazmakIzmeđuPolja, visinaPolja - RazmakIzmeđuPolja);
                    SetLayoutBounds(polje, okvir);
                }
            };

        }

        private const int Stupaca = 10;
        private const int Redaka = 10;
        private const int RazmakIzmeđuPolja = 1;

        PoljeZaPrikaz[,] polja;
    }
}
