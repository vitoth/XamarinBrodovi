// "IPucač.cs" u projektu "PotapanjeBrodova"
using System.Collections.Generic;

namespace PotapanjeBrodova
{
    public interface IPucač
    {
        Polje UputiPucanj();
        void EvidentirajRezultat(RezultatGađanja rezultat);
        IEnumerable<Polje> PogođenaPolja { get; }
    }
}
