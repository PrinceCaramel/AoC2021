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

        /// <summary>
        /// Scanner coordinates.
        /// </summary>
        private Dictionary<int, Vector3> mScannerCoordinates = new Dictionary<int, Vector3>();

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the path of the input.
        /// </summary>
        protected override string Path
        {
            get
            {
                return DataProvider.DAY19INPUTS_PATH;
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
            Dictionary<int, List<int>> lOverlapping = new Dictionary<int, List<int>>();
            for (int lIndexI = 0; lIndexI < this.mScanners.Count(); lIndexI++)
            {
                lOverlapping.Add(lIndexI, new List<int>());
                for (int lIndexJ = lIndexI; lIndexJ < this.mScanners.Count(); lIndexJ++)
                {
                    if (lIndexI != lIndexJ)
                    {
                        if (this.mScanners[lIndexI].AreOverlapping(this.mScanners[lIndexJ]))
                        {
                            lOverlapping[lIndexI].Add(lIndexJ);
                        }
                    }
                }
            }

            List<Vector3> lBeaconsCoordinates = new List<Vector3>();
            List<int> lScannerIds = new List<int>();
            for (int lIndex = 0; lIndex < this.mScanners.Count(); lIndex++)
            {
                lScannerIds.Add(lIndex);
            }
            List<int> lAlreadyDone = new List<int>();
            lBeaconsCoordinates = lBeaconsCoordinates.Union<Vector3>(this.mScanners[0].Beacons).ToList();
            lAlreadyDone.Add(0);

            while (lAlreadyDone.Count() != this.mScanners.Count())
            {
                foreach (int lId in lScannerIds)
                {
                    if (lAlreadyDone.Contains(lId))
                    {
                        foreach (int lOverlapId in lOverlapping[lId])
                        {
                            if (!lAlreadyDone.Contains(lOverlapId))
                            {
                                lBeaconsCoordinates = lBeaconsCoordinates.Union<Vector3>(this.mScanners[lOverlapId].GetRotatedBeacons(lBeaconsCoordinates)).ToList();
                                lAlreadyDone.Add(lOverlapId);
                                this.mScannerCoordinates.Add(this.mScanners[lOverlapId].Id, this.mScanners[lOverlapId].RelativeVectorFromZero);
                            }
                        }
                    }
                    else
                    {
                        if (lAlreadyDone.Intersect<int>(lOverlapping[lId]).Any())
                        {
                            lBeaconsCoordinates = lBeaconsCoordinates.Union<Vector3>(this.mScanners[lId].GetRotatedBeacons(lBeaconsCoordinates)).ToList();
                            lAlreadyDone.Add(lId);
                            this.mScannerCoordinates.Add(this.mScanners[lId].Id, this.mScanners[lId].RelativeVectorFromZero);
                        }
                    }
                }
            }
            
            return lBeaconsCoordinates.Count().ToString();
        }

        /// <summary>
        /// Computes part 2
        /// </summary>
        /// <param name="pInput"></param>
        /// <returns></returns>
        private string ComputePart2(IEnumerable<string> pInput)
        {
            // Part 1 already computed the dictionary.
            int lResult = 0;
            for (int lIndexI = 0; lIndexI < this.mScannerCoordinates.Count(); lIndexI++)
            {
                for (int lIndexJ = lIndexI; lIndexJ < this.mScannerCoordinates.Count(); lIndexJ++)
                {
                    if (lIndexI != lIndexJ)
                    {
                        lResult = Math.Max(lResult, this.ManhattanDistance(this.mScannerCoordinates[lIndexI], this.mScannerCoordinates[lIndexJ]));
                    }
                }
            }
            return lResult.ToString() ;
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
