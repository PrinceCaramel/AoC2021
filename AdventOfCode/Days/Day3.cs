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
                return pInput => Utils.Multiply(this.GetO2AndCO2AsTuple(pInput)).ToString();
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
            List<int> lIndexes = Enumerable.Range(0, lLength).ToList();
            foreach (string lLine in pInput)
            {
                this.SplitBinaryStringAndAddToArray(lLine, lIndexes, ref lCache);
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
            int[] lArrayOf0And1 = pInput.Select(pInt => pInt < 0 ? 0 : 1).ToArray();
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
            int[] lArrayOf0And1 = pInput.Select(pInt => pInt > 0 ? 0 : 1).ToArray();
            int lResultInt = Utils.ConvertArrayOf0And1IntoInteger(lArrayOf0And1);
            return lResultInt;
        }

        /// <summary>
        /// Split a line and add the result to the given array. 
        /// 2x -1 this way, we add either -1 or 1 
        /// </summary>
        /// <param name="pLine">The line to split</param>
        /// <param name="pArray">The array</param>
        private void SplitBinaryStringAndAddToArray(string pLine, List<int> pIndexes, ref int[] pArray)
        {
            foreach (int lIndex in pIndexes)
            {
                pArray[lIndex] += 2 * int.Parse(pLine[lIndex].ToString()) - 1;
            }
        }

        /// <summary>
        /// Gets the O2 and CO2 as a tuple.
        /// </summary>
        /// <param name="pInput"></param>
        /// <returns></returns>
        private Tuple<int, int> GetO2AndCO2AsTuple(IEnumerable<string> pInput)
        {
            return new Tuple<int, int>(this.GetO2Support(pInput), this.GetCO2Support(pInput));
        }

        /// <summary>
        /// Gets the O2 support.
        /// </summary>
        /// <param name="pInput"></param>
        /// <returns></returns>
        private int GetO2Support(IEnumerable<string> pInput)
        {
            int lLineLength = pInput.First().Length;
            int lResult = Convert.ToInt32(this.Recursive(pInput, lLineLength, 0, pVal => pVal < 0 ? 0 : 1), 2);
            return lResult;
        }

        /// <summary>
        /// Gets the CO2 support.
        /// </summary>
        /// <param name="pInput"></param>
        /// <returns></returns>
        private int GetCO2Support(IEnumerable<string> pInput)
        {
            int lLineLength = pInput.First().Length;
            int lResult = Convert.ToInt32(this.Recursive(pInput, lLineLength, 0, pVal => pVal >= 0 ? 0 : 1), 2);
            return lResult;
        }

        /// <summary>
        /// Recursive method to get the last value according to a bitcriteria.
        /// </summary>
        /// <param name="pInput"></param>
        /// <param name="pLineLength"></param>
        /// <param name="pAcc"></param>
        /// <param name="pBitCriteriaFunction"></param>
        /// <returns></returns>
        private string Recursive(IEnumerable<string> pInput, int pLineLength, int pAcc, Func<int, int> pBitCriteriaFunction)
        {
            if (pInput.Count() <= 1)
            {
                return pInput.FirstOrDefault();
            }

            int[] lCache = new int[pLineLength];
            List<int> lIndexes = new List<int> { pAcc };
            foreach (string lLine in pInput)
            {
                this.SplitBinaryStringAndAddToArray(lLine, lIndexes, ref lCache);
            }
            int lBitCriteria = pBitCriteriaFunction(lCache[pAcc]);

            IEnumerable<string> lNewArray = pInput.Where(pLine => int.Parse(pLine[pAcc].ToString()) == lBitCriteria).ToArray();
            return this.Recursive(lNewArray, pLineLength, pAcc + 1, pBitCriteriaFunction);
        }

        #endregion
    }
}
