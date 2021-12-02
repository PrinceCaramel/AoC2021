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
    }
}
