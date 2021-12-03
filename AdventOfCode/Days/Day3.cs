using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    /// <summary>
    /// Class that defines the puzzles of the day.
    /// </summary>
    public class Day3 : ADay
    {
        #region Properties

        /// <summary>
        /// Gets the path of the input.
        /// </summary>
        protected override string Path
        {
            get
            {
                return DataProvider.DAY3INPUTS_PATH;
            }
        }

        /// <summary>
        /// Gets the part1 function.
        /// </summary>
        protected override Func<IEnumerable<string>, string> Part1Function
        {
            get
            {
                return pInput => Utils.Multiply(this.GetGammaAndEpsilon(pInput)).ToString();
            }
        }

        /// <summary>
        /// Gets the part2 function.
        /// </summary>
        protected override Func<IEnumerable<string>, string> Part2Function
        {
            get
            {
                return pInput => pInput.ToString();
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Day3"/> class.
        /// </summary>
        public Day3()
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets the gamma and the epsilon as a tuple.
        /// </summary>
        /// <param name="pInput"></param>
        /// <returns></returns>
        private Tuple<int, int> GetGammaAndEpsilon(IEnumerable<string> pInput)
        {
            int lLength = pInput.First().Length;
            int[] lCache = new int[lLength];
            foreach (string lLine in pInput)
            {
                this.SplitBinaryStringAndAddToArray(lLine, ref lCache);
            }
            return new Tuple<int, int>(this.GetGamma(lCache), this.GetEpsilon(lCache));
        }

        /// <summary>
        /// Gets the gamma according to an array of int.
        /// </summary>
        /// <param name="pInput"></param>
        /// <returns></returns>
        private int GetGamma(int[] pInput)
        {
            int[] lArrayOf0And1 = this.GetMax0Or1FromArray(pInput, pInput.Count() / 2);
            int lResultInt = Utils.ConvertArrayOf0And1IntoInteger(lArrayOf0And1);
            return lResultInt;
        }

        /// <summary>
        /// Gets the epsilon according to an array of int.
        /// </summary>
        /// <param name="pInput"></param>
        /// <returns></returns>
        private int GetEpsilon(int[] pInput)
        {
            int[] lArrayOf0And1 = this.GetMax0Or1FromArray(pInput, pInput.Count() / 2, false);
            int lResultInt = Utils.ConvertArrayOf0And1IntoInteger(lArrayOf0And1);
            return lResultInt;
        }

        /// <summary>
        /// Split a line and add the result to the given array.
        /// </summary>
        /// <param name="pLine"></param>
        /// <param name="pArray"></param>
        private void SplitBinaryStringAndAddToArray(string pLine, ref int[] pArray)
        {
            for (int lIndex = 0; lIndex < pLine.Length; lIndex++)
            {
                pArray[lIndex] += int.Parse(pLine[lIndex].ToString());
            }
        }

        /// <summary>
        /// Turns an array of int, into a array of 1 or 0 according to a given threshold.
        /// </summary>
        /// <param name="pArray"></param>
        /// <param name="pThreshold"></param>
        /// <returns></returns>
        private int[] GetMax0Or1FromArray(int[] pArray, int pThreshold, bool pMax = true)
        {
            int lLength = pArray.Length;
            Func<int, int, bool> lFunction;
            if (pMax)
            {
                lFunction = (pRight, pLeft) => pRight < pLeft; 
            }
            else
            {
                lFunction = (pRight, pLeft) => pRight > pLeft;
            }
            for (int lIndex = 0; lIndex < lLength; lIndex++)
            {
                pArray[lIndex] = lFunction(pArray[lIndex], pThreshold) ? 0 : 1;
            }
            return pArray;
        }

        #endregion
    }
}
