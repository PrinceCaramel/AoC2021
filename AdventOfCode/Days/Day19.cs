using AdventOfCode.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    // 13065 too low

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

        /// <summary>
        /// Scanner coordinates.
        /// </summary>
        private Dictionary<int, Vector3> mScannerCoordinates = new Dictionary<int, Vector3>();

        #endregion Fields

        #region Properties

        protected override bool ShouldTimeStamp => true;

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
                return pInput => this.ComputePart2(pInput);
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
            this.mScannerCoordinates.Add(0, new Vector3(0, 0, 0));
        }

        /// <summary>
        /// Computes the part 1.
        /// </summary>
        /// <param name="pInput"></param>
        /// <returns></returns>
        private string ComputePart1(IEnumerable<string> pInput)
        {
            this.InitializeData(pInput);
            List<Scanner> lScannerCopy = this.mScanners.ToList();
            Scanner lScanner00 = lScannerCopy.Pop<Scanner>();
            List<Vector3> lInitList = lScanner00.Beacons.ToList();
            while (lScannerCopy.Any())
            {
                Scanner lPop = lScannerCopy.Pop<Scanner>();
                if (lPop != null)
                {
                    lInitList = lPop.GetList(lInitList);
                }
                if (!lPop.HasOverlapped)
                {
                    lScannerCopy.Add(lPop);
                }
                else
                {
                    this.mScannerCoordinates.Add(lPop.Id, lPop.RelativeDistanceFromZero);
                }
            }
            return lInitList.Count().ToString();
        }

        private string ComputePart2(IEnumerable<string> pInput)
        {
            // Part 1 already computed the dictionary.
            return this.mScannerCoordinates.Aggregate(0, (pAcc, pNext) => pAcc = Math.Max(pAcc, this.mScannerCoordinates.Aggregate(0, (pAcc2, pNext2) => pAcc2 = Math.Max(pAcc, this.ManhattanDistance(pNext.Value, pNext2.Value)), pAcc2 => pAcc2)), pAcc => pAcc).ToString();
        }


        /// <summary>
        /// Returns the manhattan distance between two vectors.
        /// </summary>
        /// <param name="pVector1"></param>
        /// <param name="pVector2"></param>
        /// <returns></returns>
        private int ManhattanDistance(Vector3 pVector1, Vector3 pVector2)
        {
            return (int)Math.Abs(pVector1.X - pVector2.X) + (int)Math.Abs(pVector1.Y - pVector2.Y) + (int)Math.Abs(pVector1.Z - pVector2.Z);
        }

        #endregion
    }
}
