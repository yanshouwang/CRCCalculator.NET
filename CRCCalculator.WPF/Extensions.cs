using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace CRCCalculator.WPF
{
    static class Extensions
    {
        public static string LoopInsert(this string str, int interval, string value)
        {
            for (int i = interval; i < str.Length; i += interval + 1)
                str = str.Insert(i, value);
            return str;
        }
    }
}
