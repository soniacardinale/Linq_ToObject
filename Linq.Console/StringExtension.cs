﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Linq.ConsoleApp
{
    public static class StringExtensions
    {
        // Estendo string
        public static double ToDouble(this string value)
        {
            double.TryParse(
                value,
                out double convertedValue
            );

            return convertedValue;
        }

        public static string WithPrefix(
            this string value,
            string prefix
        )
        {
            // String Interpolation
            return $"{prefix}-{value}";  //$ -> string interpolation
            // OPPURE
            //return prefix + "-" + value;
            // OPPURE
            //return string.Format(
            //    "{0}-{1}",
            //    prefix,
            //    value
            //);
        }

        // Estendo double
        //public static string ToMyString(this double value)
        //{
        //    return "";
        //}
    }
}
