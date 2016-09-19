using System.Collections.Generic;
using System.Linq;

namespace PotapanjeBrodova
{
    public class SelektivniNapipač : Napipač
    {
        public SelektivniNapipač(Mreža mreža, int duljinaBroda) : base(mreža, duljinaBroda)
        { }

        public override IEnumerable<Polje> DajKandidate()
        {
            var svaPolja = base.DajKandidate();
            // listu sortira u grupe po učestalosti pojave polja
            var sortiraneGrupe = svaPolja.GroupBy(polje => polje)
                .OrderByDescending(grupa => grupa.Count());
            // vraća samo polja koja se pojavljuju najčešće
            return sortiraneGrupe
                .TakeWhile(grupa => grupa.Count() == sortiraneGrupe.First().Count())
                .Select(grupa => grupa.Key);
        }
    }
}
