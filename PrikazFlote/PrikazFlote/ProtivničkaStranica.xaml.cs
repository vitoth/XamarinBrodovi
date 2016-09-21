using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PrikazFlote
{
    public partial class ProtivničkaStranica : ContentPage
    {
        public ProtivničkaStranica()
        {
            InitializeComponent();
        }

        void OnMrežaZaPrikazSizeChanged(object sender, EventArgs args)
        {
            ContentView prikaz = (ContentView)sender;
            double visina = prikaz.Height;
            double širina = prikaz.Width;
            double veličina = Math.Min(visina, širina);
            double lijeviRub = (širina - veličina) / 2;
            double gornjiRub = (visina - veličina) / 2;
            prikaz.Padding = new Thickness(lijeviRub, gornjiRub);
        }

    }
}
