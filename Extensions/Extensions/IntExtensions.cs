using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions
{
    public static class IntExtensions
    {
        /// <summary>
        /// The numbers percentage
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="percent">The percent.</param>
        /// <returns>The result</returns>
        public static decimal PercentageOf(this int number, int percent)
        {
            return (decimal)(number * percent / 100);
        }

        /// <summary>
        /// Checks if integer is a prime.
        /// </summary>
        public static bool IsPrime(this int number)
        {
            if ((number % 2) == 0)
            {
                return number == 2;
            }
            int sqrt = (int)Math.Sqrt(number);
            for (int t = 3; t <= sqrt; t = t + 2)
            {
                if (number % t == 0)
                {
                    return false;
                }
            }
            return number != 1;
        }

        public static int Squared(this int intToBeSquared)
        {
            return intToBeSquared * intToBeSquared;
        }

        /// <summary>
        /// Returns the integer as long.
        /// </summary>
        public static long AsLong(this int i)
        {
            return i;
        }

        /// <summary>
        /// 	Determines whether the value is even
        /// </summary>
        /// <param name = "value">The Value</param>
        /// <returns>true or false</returns>
        public static bool IsEven(this int value)
        {
            return value.AsLong().IsEven();
        }

        /// <summary>
        /// 	Determines whether the value is odd
        /// </summary>
        /// <param name = "value">The Value</param>
        /// <returns>true or false</returns>
        public static bool IsOdd(this int value)
        {
            return value.AsLong().IsOdd();
        }

        /// <summary>Checks whether the value is in range</summary>
        /// <param name="value">The Value</param>
        /// <param name="minValue">The minimum value</param>
        /// <param name="maxValue">The maximum value</param>
        public static bool InRange(this int value, int minValue, int maxValue)
        {
            return value.AsLong().InRange(minValue, maxValue);
        }

        /// <summary>Checks whether the value is in range or returns the default value</summary>
        /// <param name="value">The Value</param>
        /// <param name="minValue">The minimum value</param>
        /// <param name="maxValue">The maximum value</param>
        /// <param name="defaultValue">The default value</param>
        public static int InRange(this int value, int minValue, int maxValue, int defaultValue)
        {
            return (int)value.AsLong().InRange(minValue, maxValue, defaultValue);
        }

        #region Byte size calculation

        /// <summary>
        /// Kilobytes
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int KB(this int value)
        {
            return value * 1024;
        }

        /// <summary>
        /// Megabytes
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int MB(this int value)
        {
            return value.KB() * 1024;
        }

        /// <summary>
        /// Gigabytes
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GB(this int value)
        {
            return value.MB() * 1024;
        }

        /// <summary>
        /// Terabytes
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long TB(this int value)
        {
            return (long)value.GB() * (long)1024;
        }

        #endregion
    }
}
