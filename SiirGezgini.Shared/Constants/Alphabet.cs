using System.Collections.Generic;
using System.Linq;

namespace SiirGezgini.Shared.Constants
{
    public static class Alphabet
    {
        private static string Alphabets => "A,B,C,Ç,D,E,F,G,Ğ,H,I,İ,J,K,L,M,N,O,Ö,P,R,S,Ş,T,U,Ü,V,Y,Z";

        public static List<char> TurkishAlphabets => Alphabets.Split(',').Select(char.Parse).ToList();
    }
}
