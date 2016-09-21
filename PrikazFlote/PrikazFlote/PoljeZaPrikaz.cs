using System;
using Xamarin.Forms;
using PotapanjeBrodova;

namespace PrikazFlote
{
    public class GađanoPoljeEventArgs : EventArgs
    {
        public GađanoPoljeEventArgs(Polje polje)
        {
            Polje = polje;
        }

        public readonly Polje Polje;
    }
    public class PoljeZaPrikaz : Xamarin.Forms.Image
    {
        public PoljeZaPrikaz(int stupac, int redak)
        {
            Stupac = stupac;
            Redak = redak;
            Isprazni();

            var tapGestureRecognizer = new TapGestureRecognizer();  //pokušaj dodavanja promjene boje na dodir
            tapGestureRecognizer.Tapped += (s, e) => {  //lambda
                if (this.BackgroundColor == bojaBrodskogPolja)
                    this.BackgroundColor = bojaPraznogPolja;
                else
                    this.BackgroundColor = bojaBrodskogPolja;
                this.OnGađanje();
            };
            this.GestureRecognizers.Add(tapGestureRecognizer);
        } // napravit ovdje public event negdje dolje unutar klase, koji će jednostavno poslat informaciju koji je stupac redk kliknut
        //i moram imati


        public delegate void GađanjePolja(object sender, GađanoPoljeEventArgs e);

        public event GađanjePolja PoljeGađano;

        protected virtual void OnGađanje()
        {
            PoljeGađano?.Invoke(this, new GađanoPoljeEventArgs(new Polje(Stupac, Redak)));
            // ovo gore je nova konvencija za ovo dolje, provjera je ugrađena i sve

            //if (PoljeGađano != null)  ako imamo nekog ko je pretpalćen na događaj  onda dižemo event
            //    PoljeGađano(this, new GađanoPoljeEventArgs(new Polje(Stupac, Redak))); tu je to dizanje4 eventa
            //šaljemo sendera (this) drugi argument je informacija o polju , eventargs svoj smo morali napravit izveden iz običnog
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
        private static Xamarin.Forms.Color bojaBrodskogPolja = Xamarin.Forms.Color.Yellow;
    }
}
