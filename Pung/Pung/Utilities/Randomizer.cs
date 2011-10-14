using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pung.Utilities
{
    static class Randomizer
    {
        // Do NOT re-create the randomNumberMaker in close successions. Error stated below.
        private static Random randomNumberMaker = new Random();

        public static int CreateRandom(int min, int max)
        {
            /* The default seed value is derived from the system clock and
             * has finite resolution. As a result, different Random objects
             * that are created in close succession by a call to the default
             * constructor will have identical default seed values and, therefore,
             * will produce identical sets of random numbers.
             * 
             * Good info at : 
             * http://msdn.microsoft.com/en-us/library/h343ddh9.aspx
             */
            int randomNumber = randomNumberMaker.Next(min, max);

            return randomNumber;
        }

    }
}
