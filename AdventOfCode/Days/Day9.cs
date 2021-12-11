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
    public class Day9 : ADay
    {
        #region Fields
        /// <summary>
        /// Stores a flag that indicates if the data has already been initialized.
        /// </summary>
        private bool mHasInitialized = false;

        /// <summary>
        /// Stores the heightmap.
        /// </summary>
        private List<List<int>> mHeightMap;

        /// <summary>
        /// Stores the lowpoints.
        /// </summary>
        private List<Tuple<int, int>> mLowPoints;

        /// <summary>
        /// Stores the max row value.
        /// </summary>
        private int mMaxRow = int.MaxValue;

        /// <summary>
        /// Stores the max column value.
        /// </summary>
        private int mMaxCol = int.MaxValue;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the path of the input.
        /// </summary>
        protected override string Path
        {
            get
            {
                return DataProvider.DAY9INPUTS_PATH;
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
        /// Initializes a new instance of the <see cref="Day9"/> class.
        /// </summary>
        public Day9()
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
            if (!this.mHasInitialized)
            {
                this.mHeightMap = new List<List<int>>();
                foreach (string lLine in pInput)
                {
                    this.mHeightMap.Add(lLine.Select(pChar => int.Parse(pChar.ToString())).ToList());
                }
                this.mLowPoints = new List<Tuple<int, int>>();
                this.mMaxRow = this.mHeightMap.Count() - 1;
                this.mMaxCol = this.mHeightMap.First().Count() - 1;
                this.ComputeLowPoints();
                this.mHasInitialized = true;
            }
        }

        /// <summary>
        /// Computes the part 1.
        /// </summary>
        /// <param name="pInput"></param>
        /// <returns></returns>
        private string ComputePart1(IEnumerable<string> pInput)
        {
            this.InitializeData(pInput);
            return this.ComputeRiskLowPoints().ToString();
        }

        /// <summary>
        /// Computes the part 2.
        /// </summary>
        /// <param name="pInput"></param>
        /// <returns></returns>
        private string ComputePart2(IEnumerable<string> pInput)
        {
            this.InitializeData(pInput);
            List<int> lSizes = this.mLowPoints.Select(pLowPoint => this.ComputeBassin(pLowPoint).Count()).ToList();
            lSizes.Sort();
            lSizes.Reverse();
            return (lSizes[0] * lSizes[1] * lSizes[2]).ToString();
        }

        /// <summary>
        /// Computes the risk of low points.
        /// </summary>
        /// <returns></returns>
        private int ComputeRiskLowPoints()
        {
            return this.mLowPoints.Aggregate(0, (pAcc, pNext) => pAcc += this.mHeightMap.GetValueFromTuple(pNext) + 1, pAcc => pAcc);
        }

        /// <summary>
        /// Computes the low points.
        /// </summary>
        private void ComputeLowPoints()
        {
            for (int lRow = 0; lRow <= this.mMaxRow; lRow++)
            {
                for (int lColumn = 0; lColumn <= this.mMaxCol; lColumn++)
                {
                    Tuple<int, int> lCoordinates = new Tuple<int, int>(lRow, lColumn);
                    List<Tuple<int, int>> lNeighbors = Utils.GetNeighbors(lCoordinates, this.mMaxCol, this.mMaxRow);
                    int lValue = this.mHeightMap.GetValueFromTuple(lCoordinates);
                    bool lShouldAdd = lNeighbors.All(pNeighbor => lValue < this.mHeightMap.GetValueFromTuple(pNeighbor));
                    if (lShouldAdd)
                    {
                        this.mLowPoints.Add(lCoordinates);
                    }
                }
            }
        }

        /// <summary>
        /// Computes the bassin from a low point.
        /// </summary>
        /// <param name="pLowPoint"></param>
        /// <returns></returns>
        private List<Tuple<int, int>> ComputeBassin(Tuple<int, int> pLowPoint)
        {
            List<Tuple<int, int>> lResult = new List<Tuple<int, int>>();
            List<Tuple<int, int>> lAddedTuples = new List<Tuple<int, int>>() { pLowPoint };
            int lMaxValue = 9;

            while (lAddedTuples.Any())
            {
                lAddedTuples.ForEach(pTuple => lResult.AddTuple(pTuple));
                List<Tuple<int, int>> lNewNeighbors = new List<Tuple<int, int>>();
                foreach (Tuple<int, int> lCoordinates in lAddedTuples)
                {
                    Utils.GetNeighbors(lCoordinates, this.mMaxCol, this.mMaxRow).ForEach(pTuple => lNewNeighbors.AddTuple(pTuple));
                }
                lNewNeighbors.RemoveAll(pTuple => lResult.Contains(pTuple) || this.mHeightMap.GetValueFromTuple(pTuple) == lMaxValue);
                lAddedTuples.Clear();
                lAddedTuples.AddRange(lNewNeighbors);
            }
            return lResult;
        }

        #endregion
    }
}
