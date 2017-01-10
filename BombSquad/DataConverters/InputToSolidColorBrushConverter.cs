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
    /// Converts an InputEnum to a SolidColorBrush
    /// </summary>
    /// <seealso cref="System.Windows.Data.IValueConverter" />
    public class InputToSolidColorBrushConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentNullException("Input Parameter cannot be null.");
            if (value.GetType() != typeof(BombSquad.Enumerations.InputEnum))
                throw new ArgumentException("Input Parameter was not of the correct type.");

            SolidColorBrush returnValue;
            switch((BombSquad.Enumerations.InputEnum)value)
            {
                case Enumerations.InputEnum.Blue:
                    returnValue = new SolidColorBrush(Colors.Blue);
                    break;
                case Enumerations.InputEnum.Red:
                    returnValue = new SolidColorBrush(Colors.Red);
                    break;
                case Enumerations.InputEnum.Yellow:
                    returnValue = new SolidColorBrush(Colors.Gold);
                    break;
                case Enumerations.InputEnum.Green:
                    returnValue = new SolidColorBrush(Colors.Green);
                    break;
                case Enumerations.InputEnum.Purple:
                    returnValue = new SolidColorBrush(Colors.Purple);
                    break;
                default:
                    returnValue = new SolidColorBrush(Colors.Transparent);
                    break;
            }
            return returnValue;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return string.Empty;
        }
    }
}
