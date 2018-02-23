using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace FrOG
{
    public static class Parsing
    {
        public static double[] ParseDoubles(IEnumerable<string> strs)
        {
            return strs.Select(ParseDouble).ToArray();
        }

        public static decimal[] ParseDecimals(IEnumerable<string> strs)
        {
            return strs.Select(ParseDecimal).ToArray();
        }

        public static double ParseDouble(string str)
        {
            double val;
            var isDouble = double.TryParse(str, NumberStyles.Integer | NumberStyles.AllowDecimalPoint | NumberStyles.Float, CultureInfo.InvariantCulture, out val);
            if (isDouble) return val;

            MessageBox.Show(String.Format("Wrong parameter type(double or int) for parse to double: {0})", str), "FrOG Parse Error");
            return double.NaN;
        }

        private static decimal ParseDecimal(string str)
        {
            return Convert.ToDecimal(ParseDouble(str));
        }
    }
}
