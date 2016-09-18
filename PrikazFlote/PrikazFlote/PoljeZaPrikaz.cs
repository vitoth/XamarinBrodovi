using System;

namespace PrikazFlote
{
    public class PoljeZaPrikaz : Xamarin.Forms.Frame
    {
        public PoljeZaPrikaz(int stupac, int redak)
        {
            Stupac = stupac;
            Redak = redak;
            BackgroundColor = Xamarin.Forms.Color.Blue;
        }

        public void SmjestiBrod()
        {
            brodsko = true;
            BackgroundColor = Xamarin.Forms.Color.Maroon;
        }

        public void Gađaj()
        {
            pogođeno = true;
            Content = new Xamarin.Forms.Label { Text = "x" };
        }

        public readonly int Redak;
        public readonly int Stupac;

        private bool brodsko = false;
        private bool pogođeno = false;
    }
}
