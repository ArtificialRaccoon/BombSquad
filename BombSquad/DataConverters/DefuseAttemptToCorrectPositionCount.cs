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
    /// Converts the Defuse Attempt input to a count of the number of colors in the correct position
    /// </summary>
    /// <seealso cref="System.Windows.Data.IMultiValueConverter" />
    public class DefuseAttemptToCorrectPositionCount : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values == null || values[0] == null || values[1] == null)
                throw new ArgumentNullException("Input Parameter cannot be null.");
            if (values[0].GetType() != typeof(List<BombSquad.Enumerations.InputEnum>))
                throw new ArgumentException("Input Parameter was not of the correct type.");
            if (values[1].GetType() != typeof(List<BombSquad.Enumerations.InputEnum>))
                throw new ArgumentException("Input Parameter was not of the correct type.");

            List<BombSquad.Enumerations.InputEnum> attempt = (List<BombSquad.Enumerations.InputEnum>)values[0];
            List<BombSquad.Enumerations.InputEnum> solution = (List<BombSquad.Enumerations.InputEnum>)values[1];

            //While uncompleted, do not display anything
            if (attempt[attempt.Count-1] == Enumerations.InputEnum.Unset)
                return string.Empty;

            int numCorrectPositions = 0;
            for(int i = 0; i < solution.Count; i++)
            {
                if (attempt[i] == solution[i])
                    numCorrectPositions++;
            }

            return (numCorrectPositions > 0 ? numCorrectPositions.ToString() : "0");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
