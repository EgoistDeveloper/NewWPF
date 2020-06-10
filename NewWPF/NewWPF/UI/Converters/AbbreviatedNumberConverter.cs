using System;
using System.Globalization;
using System.Windows.Data;

namespace NewWPF.UI.Converters
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class AbbreviatedNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "0";

            return KiloFormat((long)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Converts long numbers to readable string
        /// Soruce <see cref="https://stackoverflow.com/a/2412387/6940144"/>
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private string KiloFormat(long num)
        {
            if (num >= 100000000)
                return (num / 1000000).ToString("#,0M");

            if (num >= 10000000)
                return (num / 1000000).ToString("0.#") + "M";

            if (num >= 100000)
                return (num / 1000).ToString("#,0K");

            if (num >= 10000)
                return (num / 1000).ToString("0.#") + "K";

            return num.ToString("#,0");
        }
    }
}