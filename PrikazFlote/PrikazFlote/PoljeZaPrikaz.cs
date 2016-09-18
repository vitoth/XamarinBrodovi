using System;

namespace PrikazFlote
{
    public class PoljeZaPrikaz : Xamarin.Forms.Image
    {
        public PoljeZaPrikaz(int stupac, int redak)
        {
            Stupac = stupac;
            Redak = redak;
            Isprazni();
        }

        public void SmjestiBrod()
        {
            brodsko = true;
            BackgroundColor = bojaBrodskogPolja;
        }

        public void Gađaj()
        {
            pogođeno = true;
            //Content = new Xamarin.Forms.Label { Text = "x" };
        }

        public void Isprazni()
        {
            BackgroundColor = bojaPraznogPolja;
            //Content = new Xamarin.Forms.Label { Text = "" };
        }

        public readonly int Redak;
        public readonly int Stupac;

        private bool brodsko = false;
        private bool pogođeno = false;

        private static Xamarin.Forms.Color bojaPraznogPolja = Xamarin.Forms.Color.Blue;
        private static Xamarin.Forms.Color bojaBrodskogPolja = Xamarin.Forms.Color.Maroon;
    }
}
