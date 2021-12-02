using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    public abstract class ADay
    {
        #region Properties

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
            return this.Part1Function(this.Input);
        }

        /// <summary>
        /// Solve part 2 puzzle.
        /// </summary>
        /// <returns></returns>
        public string SolvePart2()
        {
            return this.Part2Function(this.Input);
        }

        #endregion Methods

    }
}
