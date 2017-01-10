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
    /// Converts the Defuse Attempt input to a count of the number of correct colors in the attempt
    /// </summary>
    /// <seealso cref="System.Windows.Data.IMultiValueConverter" />
    public class DefuseAttemptToCorrectColoursCount : IMultiValueConverter
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

            int numCorrectColors = 0;
            BombSquad.Enumerations.InputEnum alreadyTabulated = Enumerations.InputEnum.Unset;
            for(int i = 0; i < solution.Count; i++)
            {
                foreach(BombSquad.Enumerations.InputEnum input in attempt)
                {
                    if (input == solution[i] && ((input & alreadyTabulated) == Enumerations.InputEnum.Unset))
                    {
                        alreadyTabulated |= input;
                        numCorrectColors++;
                        break;
                    }
                }
            }

            return (numCorrectColors > 0 ? numCorrectColors.ToString() : "0");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
