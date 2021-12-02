using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    /// <summary>
    /// Class that defines the puzzles of the day.
    /// </summary>
    public class Day1 : ADay
    {
        #region Properties

        /// <summary>
        /// Gets the path of the input.
        /// </summary>
        protected override string Path
        {
            get
            {
                return DataProvider.MEASUREMENTS_PATH;
            }
        }

        /// <summary>
        /// Gets the part1 function.
        /// </summary>
        protected override Func<IEnumerable<string>, string> Part1Function
        {
            get
            {
                Submarine lSubmarine = new Submarine();
                return pInput => lSubmarine.Sonar.GetMeasurementsLargerThanPrevious(pInput.Select(pLine => int.Parse(pLine))).ToString();
            }
        }

        /// <summary>
        /// Gets the part2 function.
        /// </summary>
        protected override Func<IEnumerable<string>, string> Part2Function
        {
            get
            {
                Submarine lSubmarine = new Submarine();
                return pInput => lSubmarine.Sonar.GetMeasurementsLargerThanPrevious(Day1.GetSonar3Measurements(pInput)).ToString();
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Day1"/> class.
        /// </summary>
        public Day1()
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets the sonar measurements, split in a sum of 3 input, as an int array.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<int> GetSonar3Measurements(IEnumerable<string> pMeasures)
        {
            List<int> lResult = new List<int>();
            int lInputCount = pMeasures.Count();

            int l0 = int.Parse(pMeasures.ElementAt(0));
            int l1 = int.Parse(pMeasures.ElementAt(1));
            int l2 = int.Parse(pMeasures.ElementAt(2));
            lResult.Add(l0 + l1 + l2);
            for (int lIndex = 1; lIndex < lInputCount - 2; lIndex++)
            {
                l0 = l1;
                l1 = l2;
                l2 = int.Parse(pMeasures.ElementAt(lIndex + 2));
                int lSum = l0 + l1 + l2;
                lResult.Add(lSum);
            }
            return lResult;
        }

        #endregion Methods
    }
}
