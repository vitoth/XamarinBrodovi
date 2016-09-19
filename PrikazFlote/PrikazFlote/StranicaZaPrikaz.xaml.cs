using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using PotapanjeBrodova;

namespace PrikazFlote
{
    public partial class StranicaZaPrikaz : ContentPage
    {
        public StranicaZaPrikaz()
        {
            InitializeComponent();
        }

        void OnMrežaZaPrikazSizeChanged(object sender, EventArgs args)
        {
            ContentView prikaz = (ContentView)sender;
            double visina = prikaz.Height;
            double rub = (visina - mreža.Width - tipkaSložiFlotu.Height) / 3;
            prikaz.Padding = new Thickness(0, rub);
        }

        void OnSložiNovuFlotu(object sender, object EventArgs)
        {
            Brodograditelj bg = new Brodograditelj();
            int[] brodovi = new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };
            Flota flota = bg.SložiFlotu(10, 10, brodovi);
            mreža.PrikažiFlotu(flota);
        }

    }
}
