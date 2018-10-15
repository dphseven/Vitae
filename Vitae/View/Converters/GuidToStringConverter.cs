namespace Vitae.View
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    [ValueConversion(typeof(Guid), typeof(string))]
    public class GuidToStringConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Guid g = (Guid)value;
            if (g == Guid.Empty) return string.Empty;
            else return g.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) 
        {
            string s = (string)value;
            if (Guid.TryParse(s, out Guid g)) return g;
            else return Guid.Empty;
        }
    }
}