using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using CRCCalculator.WPF.Controls;

namespace CRCCalculator.WPF.Converters
{
    class Text2ArrayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = (string)value;
            if (string.IsNullOrEmpty(text))
                return null;
            var entry = (Entry)parameter;
            switch (entry.Mode)
            {
                case EntryMode.ASCII:
                    {
                        var data = Encoding.ASCII.GetBytes(text);
                        return data;
                    }
                case EntryMode.HEX:
                    {
                        var values = text.Split(" ");
                        var data = new byte[values.Length];
                        for (int i = 0; i < values.Length; i++)
                        {
                            data[i] = System.Convert.ToByte(values[i], 16);
                        }
                        return data;
                    }
                default:
                    {
                        return null;
                    }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
