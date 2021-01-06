using System;
using System.Globalization;
using System.Windows.Data;

namespace MyInsurance.BusinessLogic.Converters
{
    /// <summary>
    /// Klasa <c>BoolToVisibilityConverter</c> zawiera instrukcję konwersji z bool do Visibility.
    /// </summary>
    /// <remarks>
    /// Implementuje interfejs IValueConverter
    /// </remarks>
    class GenderBoolToStringConverter : IValueConverter
    {
        /// <summary>
        /// Konversja z bool na string
        /// </summary>
        /// <param name="value">Obiekt konwertowany (bool)</param>
        /// <param name="targetType">Typ docelowy (string)</param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>zwraca przekonwertowany obiekt</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool gender = (bool)value;
            if (gender)
            {
                return "Mężczyzna";
            }
            else
            {
                return "Kobieta";
            }
        }

        /// <summary>
        /// Konversja ze string na bool
        /// </summary>
        /// <param name="value">Obiekt konwertowany (string)</param>
        /// <param name="targetType">Typ docelowy (bool)</param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>zwraca przekonwertowany obiekt</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string gender = value.ToString();
            if (gender.ToLower() == "mężczyzna" || gender.ToLower() == "mezczyzna" || gender.ToLower() == "męzczyzna" || gender.ToLower() == "meżczyzna")
            {
                return true;
            }
            else if (gender.ToLower() == "kobieta")
            {
                return false;
            }
            return false;
        }
    }
}
