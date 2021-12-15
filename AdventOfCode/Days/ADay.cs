using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    /// <summary>
    /// Abstract class for a day.
    /// </summary>
    public abstract class ADay
    {
        #region Properties

        /// <summary>
        /// A flag indicating whether we want to time the process or not.
        /// </summary>
        protected virtual bool ShouldTimeStamp
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the path of the input.
        /// </summary>
        protected abstract string Path
        {
            get;
        }

        /// <summary>
        /// Gets the part1 function.
        /// </summary>
        protected abstract Func<IEnumerable<string>, string> Part1Function
        {
            get;
        }

        /// <summary>
        /// Gets the part2 function.
        /// </summary>
        protected abstract Func<IEnumerable<string>, string> Part2Function
        {
            get;
        }

        /// <summary>
        /// Gets the input.
        /// </summary>
        private IEnumerable<string> Input
        {
            get
            {
                return File.ReadLines(Utils.GetFullPath(this.Path));
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ADay"/> class.
        /// </summary>
        protected ADay()
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Solves part 1 puzzle.
        /// </summary>
        /// <returns></returns>
        public string SolvePart1()
        {
            string lResult;
            Stopwatch lStopWatch = new Stopwatch();
            if (this.ShouldTimeStamp)
            {
                lStopWatch.Start();
                lResult = this.Part1Function(this.Input);
                lStopWatch.Stop();
            }
            else
            {
                lResult = this.Part1Function(this.Input);
            }
            return string.Format("PART1 : {0} {1}", lResult, this.ShouldTimeStamp ? string.Format("TS :{0}", lStopWatch.ElapsedMilliseconds.ToString()) : string.Empty);
        }

        /// <summary>
        /// Solve part 2 puzzle.
        /// </summary>
        /// <returns></returns>
        public string SolvePart2()
        {
            string lResult;
            Stopwatch lStopWatch = new Stopwatch();
            if (this.ShouldTimeStamp)
            {
                lStopWatch.Start();
                lResult = this.Part2Function(this.Input);
                lStopWatch.Stop();
            }
            else
            {
                lResult = this.Part2Function(this.Input);
            }
            return string.Format("PART2 : {0} {1}", lResult, this.ShouldTimeStamp ? string.Format("TS :{0}", lStopWatch.ElapsedMilliseconds.ToString()) : string.Empty); ;
        }

        #endregion Methods

    }
}
