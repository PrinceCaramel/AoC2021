using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    /// <summary>
    /// Toolbox
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Stores the forward constant.
        /// </summary>
        public static string FORWARD = "forward";

        /// <summary>
        /// Stores the up constant.
        /// </summary>
        public static string UP = "up";

        /// <summary>
        /// Stores the down constant.
        /// </summary>
        public static string DOWN = "down";

        /// <summary>
        /// Stores the a constant.
        /// </summary>
        public static char A = 'a';

        /// <summary>
        /// Stores the b constant.
        /// </summary>
        public static char B = 'b';

        /// <summary>
        /// Stores the c constant.
        /// </summary>
        public static char C = 'c';

        /// <summary>
        /// Stores the d constant.
        /// </summary>
        public static char D = 'd';

        /// <summary>
        /// Stores the e constant.
        /// </summary>
        public static char E = 'e';

        /// <summary>
        /// Stores the f constant.
        /// </summary>
        public static char F = 'f';

        /// <summary>
        /// Stores the g constant.
        /// </summary>
        public static char G = 'g';

        /// <summary>
        /// ForEach Linq extension.
        /// </summary>
        public static void ForEach<T>(this IEnumerable<T> pSource, Action<T> pAction)
        {
            foreach (T lElement in pSource)
            {
                pAction(lElement);
            }
        }

        /// <summary>
        /// Multiplies the two items of a tuple.
        /// </summary>
        /// <param name="pTuple"></param>
        /// <returns></returns>
        public static int Multiply(Tuple<int, int> pTuple)
        {
            return pTuple.Item1 * pTuple.Item2;
        }

        /// <summary>
        /// Gets the path of the given file.
        /// </summary>
        /// <param name="pRelativePath">The relative path</param>
        /// <returns></returns>
        public static string GetFullPath(string pRelativePath)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), pRelativePath);
        }

        /// <summary>
        /// Convert an array of 0 and 1 into an int.
        /// </summary>
        /// <param name="p0And1Array"></param>
        /// <returns></returns>
        public static int ConvertArrayOf0And1IntoInteger(int[] p0And1Array)
        {
            return Convert.ToInt32(string.Join("", p0And1Array), 2);
        }

        /// <summary>
        /// Pop method for a list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pList"></param>
        /// <returns></returns>
        public static T Pop<T>(this List<T> pList)
        {
            T lFirst = pList.FirstOrDefault();
            if (lFirst != null)
            {
                pList.RemoveAt(0);
            }
            return lFirst;
        }
    }
}
