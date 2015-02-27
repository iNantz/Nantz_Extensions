using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nantz.Utils.Extensions
{
    public static class LuhnAlgorithm
    {
        #region Helpers

        /// <summary>
        /// Create a function that will check the value if its Odd or not
        /// </summary>
        static Func<int, bool> 
            mbIsOdd = i => 
                i % 2 == 0;

        /// <summary>
        /// Create a function that converts each charater in the list to integer
        /// We are using the ASCII codes to convert the char to integer
        /// '0' = 48
        /// ..
        /// '9' = 57
        /// </summary>
        static Func<char, int> 
                moCharToInt = c => 
                    c - '0';

        /// <summary>
        /// Create a function that will double each digit in the list and sum the digit
        /// input                      =  1 2 3 4  5  6
        /// double                     =  2 4 6 8 10 12
        /// sum each digit from double =  2 4 6 8  1  3
        /// </summary>
        static Func<int, int> 
            moDoubleEachDigit = d => 
                // double each digit and convert it to string
                (d * 2).ToString()
                       // convert each character to integer
                       .Select(moCharToInt)
                       // sum each digit from the double
                       .Sum();

        #endregion

        #region Extensions

        /// <summary>
        /// String Extension to Validate a number using 
        /// Luhn Algorithm http://en.wikipedia.org/wiki/Luhn_algorithm
        /// </summary>
        /// <param name="nbrToCheck">Number to Check</param>
        /// <returns>true if pass the Luhn Check otherwise false</returns>
        public static bool LuhnCheck(this string nbrToCheck)
        {
            long lNbr = 0;

            // check if input string is numeric 
            if (!long.TryParse(nbrToCheck, out lNbr))
                throw new ArgumentException("Must be a valid number", "nbrToCheck");

            var lCheckSum = nbrToCheck
                // convert each character to integer
                .Select(moCharToInt)
                // reverse it                                            
                .Reverse()   
                // if digit is even double it
                .Select((digit, index) => mbIsOdd(index) ? digit : moDoubleEachDigit(digit))
                // sum all the numbers
                .Sum();

            // If the total modulo 10 is equal to 0 (if the total ends in zero) then the number is valid according to the Luhn formula; else it is not valid.
            return lCheckSum % 10 == 0;
        }

        /// <summary>
        /// generic struct Extension to Validate a number using 
        /// Luhn Algorithm http://en.wikipedia.org/wiki/Luhn_algorithm
        /// </summary>
        /// <typeparam name="T">struc type</typeparam>
        /// <param name="nbr">number to check</param>
        /// <returns>true if pass the Luhn Check otherwise false</returns>
        public static bool LuhnCheck<T>(this T nbr) where T : struct
        {
            // convert the number to string and call the LuhnCheck
            return LuhnCheck(nbr.ToString());
        }
        #endregion
    }
}