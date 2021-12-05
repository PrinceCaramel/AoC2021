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
    public class Day5 : ADay
    {
        #region Fields

        /// <summary>
        /// Stores the dictionary where the key is the grid coordinates and the value the amount of time where there is an overlap.
        /// </summary>
        private Dictionary<string, int> mGridCoordinatesToOverlap;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the path of the input.
        /// </summary>
        protected override string Path
        {
            get
            {
                return DataProvider.DAY5INPUTS_PATH;
            }
        }

        /// <summary>
        /// Gets the part1 function.
        /// </summary>
        protected override Func<IEnumerable<string>, string> Part1Function
        {
            get
            {
                return pInput => this.Compute(pInput, true);
            }
        }

        /// <summary>
        /// Gets the part2 function.
        /// </summary>
        protected override Func<IEnumerable<string>, string> Part2Function
        {
            get
            {
                return pInput => this.Compute(pInput, false);
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Day5"/> class.
        /// </summary>
        public Day5()
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// computes the result according to the input and the flag indicating whether we should consider only vertical and horizontal lines or not.
        /// </summary>
        /// <param name="pLines"></param>
        /// <param name="pOnlyHorizontalAndVertical"></param>
        /// <returns></returns>
        private string Compute(IEnumerable<string> pLines, bool pOnlyHorizontalAndVertical)
        {
            this.InitializesGridCoordinatesWindLines(pLines, pOnlyHorizontalAndVertical);
            return this.GetOver2LinesOverlap().ToString();
        }

        /// <summary>
        /// Initializes data for the part 1.
        /// </summary>
        /// <param name="pLines"></param>
        private void InitializesGridCoordinatesWindLines(IEnumerable<string> pLines, bool pOnlyHorizontalOrVertical)
        {
            this.mGridCoordinatesToOverlap = new Dictionary<string, int>();
            foreach (string lLine in pLines)
            {
                WindLine lWindLine = new WindLine(lLine);
                if (lWindLine.IsVerticalOrHorizontal() || !pOnlyHorizontalOrVertical)
                {
                    foreach (GridPoint lPoint in lWindLine.Points)
                    {
                        int lValue = -1;
                        if (this.mGridCoordinatesToOverlap.TryGetValue(lPoint.AsString, out lValue))
                        {
                            this.mGridCoordinatesToOverlap[lPoint.AsString]++;
                        }
                        else
                        {
                            this.mGridCoordinatesToOverlap.Add(lPoint.AsString, 1);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Returns the amount of point where two lines overlap.
        /// </summary>
        /// <returns></returns>
        private int GetOver2LinesOverlap()
        {
            return this.mGridCoordinatesToOverlap.Where(pKVp => pKVp.Value >= 2).Count();
        }

        #endregion
    }
}
