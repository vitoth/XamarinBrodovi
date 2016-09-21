using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using PotapanjeBrodova;

namespace PrikazFlote //to je taj namsepace koji treba navest u xamlu
{
    public partial class MojaStranica : ContentPage
    //partial jer ix xamla autogenerira ostatak klase xaml.g.cs, objašnjenjo u dkoumentaciji xamarin
    {
        public MojaStranica()
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

        void OnSložiNovuFlotu(object sender, object EventArgs) // u xamlu mi je ovaj event u kontroli <button pod atribut Cliked, i ime eventa OnSložiNovuFlotu
                                                               //u xamlu sa ctrl space vidim ponuđene evente i ostlao, i svugje
        {
            Brodograditelj bg = new Brodograditelj();
            int[] brodovi = new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };
            Flota flota = bg.SložiFlotu(10, 10, brodovi);
            mreža.PrikažiFlotu(flota);
        }

    }
}
