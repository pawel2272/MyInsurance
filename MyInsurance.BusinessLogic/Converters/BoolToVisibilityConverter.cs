using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MyInsurance.BusinessLogic.Converters
{
    /// <summary>
    /// Klasa <c>BoolToVisibilityConverter</c> zawiera instrukcję konwersji z bool do Visibility.
    /// </summary>
    /// <remarks>
    /// Implementuje interfejs IValueConverter
    /// </remarks>
    public class BoolToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Konversja z bool na Visibility
        /// </summary>
        /// <param name="value">Obiekt konwertowany (bool)</param>
        /// <param name="targetType">Typ docelowy (Visibility)</param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>zwraca przekonwertowany obiekt</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isVisible = (bool)value;
            if (isVisible)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Hidden;
            }
        }

        /// <summary>
        /// Konversja ze Visibility na bool
        /// </summary>
        /// <param name="value">Obiekt konwertowany (Visibility)</param>
        /// <param name="targetType">Typ docelowy (bool)</param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>zwraca przekonwertowany obiekt</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            if (visibility == Visibility.Visible)
                return true;
            else
                return false;
        }
    }
}
