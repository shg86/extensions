using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class BooleanExtensions
    {
        /// <summary>
        /// Converts the value of this instance to its equivalent string representation (either "Yes" or "No").
        /// </summary>
        /// <param name="boolean"></param>
        /// <returns>string</returns>
        public static string ToYesNoString(this Boolean boolean)
        {
            return boolean ? "Yes" : "No";
        }

        /// <summary>
        /// Converts the value in number format {1 , 0}.
        /// </summary>
        /// <param name="boolean"></param>
        /// <returns>int</returns>
        /// <example>
        /// 	<code>
        /// 		int result= default(bool).ToBinaryTypeNumber()
        /// 	</code>
        /// </example>
        public static int ToBinaryTypeNumber(this Boolean boolean)
        {
            return boolean ? 1 : 0;
        }
    }
}
