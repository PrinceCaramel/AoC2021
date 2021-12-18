using AdventOfCode.DataModel;
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
    public class Day18 : ADay
    {
        #region Fields

        /// <summary>
        /// Stores the snailnumbers.
        /// </summary>
        private List<SnailPair> mSnailNumbers;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the path of the input.
        /// </summary>
        protected override string Path
        {
            get
            {
                return DataProvider.DAY18INPUTS_PATH;
            }
        }

        /// <summary>
        /// Gets the part1 function.
        /// </summary>
        protected override Func<IEnumerable<string>, string> Part1Function
        {
            get
            {
                return pInput => this.ComputePart1(pInput);
            }
        }

        /// <summary>
        /// Gets the part2 function.
        /// </summary>
        protected override Func<IEnumerable<string>, string> Part2Function
        {
            get
            {
                return pInput => this.ComputePart2(pInput);
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Day18"/> class.
        /// </summary>
        public Day18()
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Computes the part 1.
        /// </summary>
        /// <param name="pInput"></param>
        /// <returns></returns>
        private string ComputePart1(IEnumerable<string> pInput)
        {
            this.InitializeData(pInput);
            SnailPair lNumber = this.mSnailNumbers.Pop<SnailPair>();
            SnailPair lNumber2 = this.mSnailNumbers.Pop<SnailPair>();
            SnailPair lResult = this.AddTwoSnailPairs(lNumber, lNumber2);
            while (this.mSnailNumbers.Any())
            {
                lResult = this.AddTwoSnailPairs(lResult, this.mSnailNumbers.Pop<SnailPair>());
            }
            return lResult.Magnitude.ToString();
        }

        /// <summary>
        /// Adds two snailnumber.
        /// </summary>
        /// <param name="pLeft"></param>
        /// <param name="pRight"></param>
        /// <returns></returns>
        private SnailPair AddTwoSnailPairs(SnailPair pLeft, SnailPair pRight)
        {
            SnailPair lResult = new SnailPair();
            lResult.SetLeft(pLeft);
            lResult.SetRight(pRight);
            lResult.Reduce();
            return lResult;
        }

        /// <summary>
        /// Computes the part 2.
        /// </summary>
        /// <param name="pInput"></param>
        /// <returns></returns>
        private string ComputePart2(IEnumerable<string> pInput)
        {
            int lMax = 0;
            for (int lIndexA = 0; lIndexA < pInput.Count(); lIndexA++)
            {
                for (int lIndexB = 0; lIndexB < pInput.Count(); lIndexB++)
                {
                    if (lIndexB != lIndexA)
                    {
                        SnailPair lLeft = this.ComputeSnailNumber(pInput.ElementAt(lIndexA));
                        SnailPair lRight = this.ComputeSnailNumber(pInput.ElementAt(lIndexB));
                        SnailPair lResult = this.AddTwoSnailPairs(lLeft, lRight);
                        lMax = Math.Max(lMax, lResult.Magnitude);
                    }
                }
            }
            return lMax.ToString();
        }

        /// <summary>
        /// Initializes the data.
        /// </summary>
        /// <param name="pInput"></param>
        private void InitializeData(IEnumerable<string> pInput)
        {
            this.mSnailNumbers = new List<SnailPair>();
            foreach (string lLine in pInput)
            {
                SnailPair lSnailPair = this.ComputeSnailNumber(lLine);
                this.mSnailNumbers.Add(lSnailPair);
            }
        }

        /// <summary>
        /// Computes a snailnumber.
        /// </summary>
        /// <param name="pInput"></param>
        /// <returns></returns>
        private SnailPair ComputeSnailNumber(string pInput)
        {
            SnailPair lCurrent = new SnailPair();
            for (int lIndex = 1; lIndex < pInput.Count(); lIndex++)
            {
                int lValue;
                char lChar = pInput[lIndex];
                if (int.TryParse(lChar.ToString(), out lValue))
                {
                    if (lCurrent.Left != null)
                    {
                        lCurrent.SetRight(new SnailNumber(lValue));
                    }
                    else
                    {
                        lCurrent.SetLeft(new SnailNumber(lValue));
                    }
                }

                else if (lChar.Equals('['))
                {
                    SnailPair lNewPair = new SnailPair();
                    if (lCurrent.Left != null)
                    {
                        lCurrent.SetRight(lNewPair);
                    }
                    else
                    {
                        lCurrent.SetLeft(lNewPair);
                    }
                    lCurrent = lNewPair;
                }
                else if (lChar.Equals(']'))
                {
                    lCurrent = lCurrent.Parent as SnailPair ?? lCurrent;
                }

            }
            return lCurrent.Root as SnailPair;
        }

        #endregion
    }
}
