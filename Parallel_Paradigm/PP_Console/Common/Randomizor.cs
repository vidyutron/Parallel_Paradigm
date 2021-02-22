using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_Console.Common
{
    public static class Randomizor
    {
        static readonly string _charset  ="abcdefghijklmnopqrstuvwxyz";
        static readonly string _numset   = "0123456789";
        static readonly string _mixedSet = "abcdefghijklmnopqrstuvwxyz0123456789";
        static Random _localRand = new Random();

        public static string AlphaNumericSet(this int len){
            string set = string.Empty;
            Enumerable.Range(0, len).ToList()
                .ForEach(x => set += _mixedSet[_localRand.Next(0, _mixedSet.Length - 1)]);
            return set;
        }

        public static string AlphaSet(this int len)
        {
            string set = string.Empty;
            Enumerable.Range(0, len).ToList()
                .ForEach(x => set += _charset[_localRand.Next(0, _charset.Length - 1)]);
            return set;
        }

        public static string NumericSet(this int len)
        {
            string set = string.Empty;
            Enumerable.Range(0, len).ToList()
                .ForEach(x => set += _numset[_localRand.Next(0, _numset.Length - 1)]);
            return set;
        }
    }
}
