using System;
using System.Globalization;
using System.Windows.Data;
using Wonder.Core.Security.Cryptography;

namespace CRCCalculator.WPF.Converters
{
    class CRC2ExpressionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            var crc = (CRC)value;
            var polyStr = System.Convert.ToString(crc.Poly, 2);
            var str = $"x{crc.Width}";
            for (int i = 0; i < polyStr.Length; i++)
            {
                if (polyStr[i] == '0')
                    continue;
                var j = polyStr.Length - 1 - i;
                if (j == 0)
                {
                    str += " + 1";
                }
                else if (j == 1)
                {
                    str += $" + x";
                }
                else
                {
                    str += $" + x{j}";
                }
            }
            return str;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
