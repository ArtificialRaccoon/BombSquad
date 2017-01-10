using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BombSquad.DataConverters
{
    /// <summary>
    /// Converts the current games configuration value for the maximum number of code inputs into a visibility enumeration.   
    /// </summary>
    /// <seealso cref="System.Windows.Data.IValueConverter" />
    public class ConfigurationToVisibilityConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentNullException("Input Parameter cannot be null.");
            if (value.GetType() != typeof(List<Enumerations.InputEnum>))
               throw new ArgumentException("Input Parameter was not of the correct type.");
            if (parameter == null)
                throw new ArgumentNullException("Conversion Parameter cannot be null.");

            System.Windows.Visibility returnValue = System.Windows.Visibility.Hidden;
            List<Enumerations.InputEnum> defuseCode = (List<Enumerations.InputEnum>)value;
            int currentPosition = 999;
            int.TryParse((string)parameter, out currentPosition);

            if (defuseCode.Count >= currentPosition)
                returnValue = System.Windows.Visibility.Visible;

            return returnValue;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return string.Empty;
        }
    }
}
