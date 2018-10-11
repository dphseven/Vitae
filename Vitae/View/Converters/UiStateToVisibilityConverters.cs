namespace Vitae.View
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using ViewModel;

    /// <summary>
    /// Converter used with editable controls... Textboxes, comboboxes, etc.
    /// </summary>
    [ValueConversion(typeof(UIState), typeof(Visibility))]
    public class EditStateToVisibilityConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            UIState state = (UIState)value;
            if (state == UIState.View) return Visibility.Collapsed;
            else return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) 
        {
            return null;
        }
    }

    /// <summary>
    /// Converter used with read-only controls... Labels, textblocks, etc.
    /// </summary>
    [ValueConversion(typeof(UIState), typeof(Visibility))]
    public class ViewStateToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            UIState state = (UIState)value;
            if (state == UIState.View) return Visibility.Visible;
            else return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) 
        {
            return null;
        }
    }
}