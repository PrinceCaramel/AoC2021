using AdventOfCode.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    /// <summary>
    /// Class that defines the puzzles of the day.
    /// </summary>
    public class Day19 : ADay
    {
        #region Fields

        /// <summary>
        /// Stores the scanners.
        /// </summary>
        private List<Scanner> mScanners = new List<Scanner>();

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the path of the input.
        /// </summary>
        protected override string Path
        {
            get
            {
                return DataProvider.DAY19TESTINPUTS_PATH;
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
                return pInput => pInput.First();
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Day19"/> class.
        /// </summary>
        public Day19()
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Initializes the data.
        /// </summary>
        /// <param name="pInput"></param>
        private void InitializeData(IEnumerable<string> pInput)
        {
            this.mScanners.Clear();
            List<string> lInput = pInput.ToList();
            while (lInput.Any())
            {
                List<string> lScanner = new List<string>();
                string lLine = lInput.Pop<string>();
                while (!string.IsNullOrEmpty(lLine))
                {
                    lScanner.Add(lLine);
                    lLine = lInput.Pop<string>();
                }
                if (!string.IsNullOrEmpty(lLine))
                {
                    lScanner.Add(lLine);
                }
                this.mScanners.Add(new Scanner(lScanner));
            }
        }

        private string ComputePart1(IEnumerable<string> pInput)
        {
            this.InitializeData(pInput);
            Scanner lScanner = this.mScanners.First();
            Scanner lScanner2 = this.mScanners.Last();

            return lScanner.AreOverlapping(lScanner2).ToString();
        }

        #endregion
    }
}
