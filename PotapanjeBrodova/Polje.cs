// "Polje.cs" u projektu "PotapanjeBrodova"
using System;

namespace PotapanjeBrodova
{
    public class Polje : IEquatable<Polje>
    {
        public Polje(int stupac, int redak)
        {
            Stupac = stupac;
            Redak = redak;
        }

        // tipski sigurna metoda Equals:
        public bool Equals(Polje drugo)
        {
            if (drugo == null)
                return false;
            return Stupac == drugo.Stupac && Redak == drugo.Redak;
        }

        // tipski nesigurna metoda Equals:
        public override bool Equals(object drugi)
        {
            if (drugi == null)
                return false;
            if (GetType() != drugi.GetType())
                return false;
            // poziva tipski sigurnu izvedbu: 
            return Equals((Polje)drugi);
        }

        public override int GetHashCode()
        {
            return Stupac << 16 ^ Redak;
        }

        public readonly int Stupac;
        public readonly int Redak;
    }
}
