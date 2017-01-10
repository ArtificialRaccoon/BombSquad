using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombSquad.Enumerations
{
    /// <summary>
    /// Enumeration for each of the possible input values in the defuse code for a bomb
    /// </summary>
    [Flags]
    public enum InputEnum {
        Unset = 0, Blue = 1, Red = 2, Yellow = 4, Green = 8, Purple = 16
    };
}
