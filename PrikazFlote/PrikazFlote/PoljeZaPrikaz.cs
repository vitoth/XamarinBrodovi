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
        public RezultatGađanja rez;
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
                this.OnProtivničkoPoljeJeGađano();  //zovemo event i pretpostavljamo da će obavijestiti pretplatnike
            };
            this.GestureRecognizers.Add(tapGestureRecognizer);
        } // napravit ovdje public event negdje dolje unutar klase, koji će jednostavno poslat informaciju koji je stupac redk kliknut
        //i moram imati


        public delegate void ProtivničkoPoljeJeGađanoEventHandler(object sender, GađanoPoljeEventArgs e); //1. definiramo delegat

        public event ProtivničkoPoljeJeGađanoEventHandler ProtivničkoPoljeJeGađano; //2.definiramo event na temelju delegatra

        protected virtual void OnProtivničkoPoljeJeGađano() //3. raising event ,event publisher methods should be protected, virtual and void, započinje s On  i onda ime eventa ProtivničkoPoljeJeGađano
        { //odgovornost ove metode je obavijestiti pretplatnike
            GađanoPoljeEventArgs args = new GađanoPoljeEventArgs(new Polje(Stupac, Redak));
            ProtivničkoPoljeJeGađano?.Invoke(this, args);
            // ovo gore je nova konvencija za ovo dolje, provjera je ugrađena i sve
            ObojiPolje(args.rez);
            //if (PoljeGađano != null)  ako imamo nekog ko je pretpalćen na događaj  onda dižemo event
            //    PoljeGađano(this, new GađanoPoljeEventArgs(new Polje(Stupac, Redak))); tu je to dizanje4 eventa
            //šaljemo sendera (this) drugi argument je informacija o polju , eventargs svoj smo morali napravit izveden iz običnog
            //koji je izvor eventa, tj ko publisha event, to je trenutna klasa, tj this,
        }


        public void ObojiPolje(RezultatGađanja rez)
        {
            
            switch (rez)
            {
                case RezultatGađanja.Pogodak:
                    this.Pogodi();
                    break;
                case RezultatGađanja.Promašaj:
                    this.Promaši();
                    break;
                case RezultatGađanja.Potonuće:
                    this.Potopi();
                    break;
            }
        }

        public void Pogodi()
        {
            BackgroundColor = bojaPogotka;
        }
        public void Promaši()
        {
            BackgroundColor = bojaPromašaja;
        }
        public void Potopi()
        {
            BackgroundColor = bojaPotonuća;
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
        private static Xamarin.Forms.Color bojaPromašaja = Xamarin.Forms.Color.Gray;
        private static Xamarin.Forms.Color bojaPogotka = Xamarin.Forms.Color.Black;
        private static Xamarin.Forms.Color bojaPotonuća= Xamarin.Forms.Color.Red;
    }
}
