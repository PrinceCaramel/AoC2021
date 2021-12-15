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

        /// <summary>
        /// Adds a tuple to a list of tuple if it doesn't already exist.
        /// </summary>
        /// <param name="pCoordinates"></param>
        /// <param name="pElementToAdd"></param>
        public static void AddTuple(this List<Tuple<int, int>> pCoordinates, Tuple<int, int> pElementToAdd)
        {
            if (!pCoordinates.Any(pCoord => pCoord.Equals(pElementToAdd)))
            {
                pCoordinates.Add(pElementToAdd);
            }
        }

        /// <summary>
        /// Removes a tuple from a list if it contains it.
        /// </summary>
        /// <param name="pCoordinates"></param>
        /// <param name="pElementToRemove"></param>
        public static void RemoveTuple(this List<Tuple<int, int>> pCoordinates, Tuple<int, int> pElementToRemove)
        {
            Tuple<int, int> lToRemove = pCoordinates.FirstOrDefault(pCoord => pCoord.Equals(pElementToRemove));
            if (lToRemove != null)
            {
                pCoordinates.Remove(lToRemove);
            }
        }

        /// <summary>
        /// Checks if a tuple is contained in a list of tuple.
        /// </summary>
        /// <param name="pCoordinates"></param>
        /// <param name="pElement"></param>
        /// <returns></returns>
        public static bool ContainsTuple(this List<Tuple<int, int>> pCoordinates, Tuple<int, int> pElement)
        {
            return pCoordinates.Any(pTuple => pTuple.Equals(pElement));
        }

        /// <summary>
        /// Get the neighbors of a point.
        /// </summary>
        /// <param name="pPoint"></param>
        /// <returns></returns>
        public static List<string> GetNeighbors(string pPoint, int pMaxCol, int pMaxRow, bool pIncludeDiagonals = false)
        {
            string[] lSplit = pPoint.Split(',');
            List<Tuple<int, int>> lNeighbors = Utils.GetNeighbors(new Tuple<int,int>(int.Parse(lSplit[0]), int.Parse(lSplit[1])), pMaxCol, pMaxRow, pIncludeDiagonals);
            return lNeighbors.Select(pTuple => string.Format("{0},{1}", pTuple.Item1, pTuple.Item2)).ToList();
        }

        /// <summary>
        /// Get the neighbors of a point.
        /// </summary>
        /// <param name="pPoint"></param>
        /// <returns></returns>
        public static List<Tuple<int, int>> GetNeighbors(Tuple<int, int> pPoint, int pMaxCol, int pMaxRow, bool pIncludeDiagonals = false)
        {
            List<Tuple<int, int>> lResult = new List<Tuple<int, int>>();
            if (pPoint.Item1 - 1 >= 0)
            {
                lResult.Add(new Tuple<int, int>(pPoint.Item1 - 1, pPoint.Item2));
            }
            if (pPoint.Item2 - 1 >= 0)
            {
                lResult.Add(new Tuple<int, int>(pPoint.Item1, pPoint.Item2 - 1));
            }
            if (pPoint.Item2 + 1 <= pMaxCol)
            {
                lResult.Add(new Tuple<int, int>(pPoint.Item1, pPoint.Item2 + 1));
            }
            if (pPoint.Item1 + 1 <= pMaxRow)
            {
                lResult.Add(new Tuple<int, int>(pPoint.Item1 + 1, pPoint.Item2));
            }
            if (pIncludeDiagonals)
            {
                if (pPoint.Item1 - 1 >= 0 && pPoint.Item2 - 1 >= 0)
                {
                    lResult.Add(new Tuple<int, int>(pPoint.Item1 - 1, pPoint.Item2 - 1));
                }
                if (pPoint.Item1 - 1 >= 0 && pPoint.Item2 + 1 <= pMaxCol)
                {
                    lResult.Add(new Tuple<int, int>(pPoint.Item1 - 1, pPoint.Item2 + 1));
                }
                if (pPoint.Item1 + 1 <= pMaxRow && pPoint.Item2 - 1 >= 0)
                {
                    lResult.Add(new Tuple<int, int>(pPoint.Item1 + 1, pPoint.Item2 - 1));
                }
                if (pPoint.Item1 + 1 <= pMaxRow && pPoint.Item2 + 1 <= pMaxCol)
                {
                    lResult.Add(new Tuple<int, int>(pPoint.Item1 + 1, pPoint.Item2 + 1));
                }
            }
            return lResult;
        }

        /// <summary>
        /// Gets the value from coordinates.
        /// </summary>
        /// <param name="pCoordinates"></param>
        /// <returns></returns>
        public static int GetValueFromTuple(this List<List<int>> pMap, Tuple<int, int> pCoordinates)
        {
            return pMap[pCoordinates.Item2][pCoordinates.Item1];
        }

        /// <summary>
        /// Print a map.
        /// </summary>
        /// <param name="pMap"></param>
        /// <returns></returns>
        public static void MapPrint(this List<List<int>> pMap)
        {
            StringBuilder lStringBuilder = new StringBuilder();
            foreach(List<int> lRow in pMap)
            {
                lStringBuilder.AppendLine(string.Join(string.Empty, lRow));
            }
            Console.WriteLine(lStringBuilder.ToString());
        }

        /// <summary>
        /// Sets the value by coordinates.
        /// </summary>
        /// <param name="pMap"></param>
        /// <param name="pCoordinates"></param>
        /// <param name="pValue"></param>
        public static void SetValueByCoordinates(this List<List<int>> pMap, Tuple<int,int> pCoordinates, int pValue)
        {
            pMap[pCoordinates.Item2][pCoordinates.Item1] = pValue;
        }

        /// <summary>
        /// Prints a tuple.
        /// </summary>
        /// <param name="pTuple"></param>
        public static void TuplePrint(this Tuple<int,int> pTuple)
        {
            Console.WriteLine(string.Format("Tuple X:{0} | Y:{1}", pTuple.Item1, pTuple.Item2));
        }
    }
}
