using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reuseables
{
   static class Generators
    {

        public static string infoMsg = "";
        private static Random random = new Random();
        private static Random rand = new Random(1);
     
        public static  int RandomNumber()
        {
            int rnd_number = rand.Next(1, 6);
            return rnd_number;
        }
        public static int RandomNumber(int numberLength)
        {
            int rnd_number = rand.Next(1, numberLength);
            return rnd_number;
        }

        public static string Numbergen(int length)
        {

            string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());

        }
        public static string WordGen(int length)
        {

            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());

        }

    }

}
