using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_1_dll
{
    public static class Utils
    {
        /// <summary>
        /// Переводит из двоичной в десятичную систему
        /// </summary>
        /// <param name="sBinary">Строка с числом в двоичном представлении</param>
        /// <returns>Десятичное число</returns>
        public static int ToDec(string sBinary)
        {
            if (string.IsNullOrWhiteSpace(sBinary)) return 0;
            return sBinary.Last() - 48 + 2 * ToDec(sBinary.Substring(0, sBinary.Length - 1));
        }

        /// <summary>
        /// Переводит из десятичной в двоичную систему
        /// </summary>
        /// <param name="dec">Десятичное число</param>
        /// <returns>Строка с числом в двоичном представлении</returns>
        public static string ToBin(int dec)
        {
            if (dec == 0) return "";
            return ToBin(dec >> 1) + (dec & 1).ToString();
        }
    }
}
