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
            ContentView contentView = (ContentView)sender;
            double width = contentView.Width;
            double height = contentView.Height;
            double dimension = Math.Min(width, height);
            double horzPadding = (width - dimension) / 2;
            double vertPadding = (height - dimension) / 2;
            contentView.Padding = new Thickness(horzPadding, vertPadding);
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
