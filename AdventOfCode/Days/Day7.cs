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
    public class Day7 : ADay
    {
        #region Fields

        /// <summary>
        /// Stores the crab positions.
        /// </summary>
        List<int> mInitialCrabPositions = new List<int>();

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the path of the input.
        /// </summary>
        protected override string Path
        {
            get
            {
                return DataProvider.DAY7INPUTS_PATH;
            }
        }

        /// <summary>
        /// Gets the part1 function.
        /// </summary>
        protected override Func<IEnumerable<string>, string> Part1Function
        {
            get
            {
                return pInput => this.Compute(pInput, (pV, pW) => Math.Abs(pV - pW));
            }
        }

        /// <summary>
        /// Gets the part2 function.
        /// </summary>
        protected override Func<IEnumerable<string>, string> Part2Function
        {
            get
            {
                return pInput => this.Compute(pInput, (pV, pW) => (Math.Abs(pV - pW) + 1) * Math.Abs(pV - pW) / 2);
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Day7"/> class.
        /// </summary>
        public Day7()
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Computes the result according to the input and a distance method.
        /// </summary>
        /// <param name="pInput"></param>
        /// <param name="pDistanceMethod"></param>
        /// <returns></returns>
        private string Compute(IEnumerable<string> pInput, Func<int,int,int> pDistanceMethod)
        {
            this.InitializeData(pInput);
            return this.ComputeMinimumSumWithDistanceFunction(pDistanceMethod).ToString();
        }

        /// <summary>
        /// Initializes the data.
        /// </summary>
        /// <param name="pInput"></param>
        private void InitializeData(IEnumerable<string> pInput)
        {
            this.mInitialCrabPositions = pInput.First().Split(',').Select(pValue => int.Parse(pValue)).ToList();
        }

        /// <summary>
        /// Computes the result according to a distance method.
        /// </summary>
        /// <param name="pInput"></param>
        /// <param name="pDistanceMethod"></param>
        /// <returns></returns>
        private int ComputeMinimumSumWithDistanceFunction(Func<int,int,int> pDistanceFunction)
        {
            int lMin = this.mInitialCrabPositions.Min();
            int lMax = this.mInitialCrabPositions.Max();
            int lMinSum = int.MaxValue;
            for (int lIndex = lMin; lIndex <= lMax; lIndex++)
            {
                lMinSum = this.mInitialCrabPositions.Aggregate(0, (pAcc, pNext) => pAcc += pDistanceFunction(pNext, lIndex), pAcc => Math.Min(pAcc, lMinSum));
            }

            return lMinSum;
        }

        #endregion
    }
}
