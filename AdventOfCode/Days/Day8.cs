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
    public class Day8 : ADay
    {
        #region Fields

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the path of the input.
        /// </summary>
        protected override string Path
        {
            get
            {
                return DataProvider.DAY8INPUTS_PATH;
            }
        }

        /// <summary>
        /// Gets the part1 function.
        /// </summary>
        protected override Func<IEnumerable<string>, string> Part1Function
        {
            get
            {
                return pInput => this.GetPart1(pInput);
            }
        }

        /// <summary>
        /// Gets the part2 function.
        /// </summary>
        protected override Func<IEnumerable<string>, string> Part2Function
        {
            get
            {
                return pInput => this.GetPart2(pInput);
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Day8"/> class.
        /// </summary>
        public Day8()
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets the result of the first part.
        /// </summary>
        /// <param name="pLines"></param>
        /// <returns></returns>
        private string GetPart1(IEnumerable<string> pLines)
        {
            int lResult = 0;
            foreach (string lLine in pLines)
            {
                DigitSignalPattern lDSP = new DigitSignalPattern(lLine);
                lResult += lDSP.CountSumOf147And8InOutput();
            }
            return lResult.ToString();
        }

        /// <summary>
        /// Gets the result of the first part.
        /// </summary>
        /// <param name="pLines"></param>
        /// <returns></returns>
        private string GetPart2(IEnumerable<string> pLines)
        {
            int lResult = 0;
            foreach (string lLine in pLines)
            {
                DigitSignalPattern lDSP = new DigitSignalPattern(lLine);
                lResult += lDSP.GetDecodedOutput();
            }
            return lResult.ToString();
        }

        #endregion
    }
}
