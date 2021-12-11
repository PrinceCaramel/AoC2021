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
    public class Day11 : ADay
    {
        #region Fields

        /// <summary>
        /// Max row/
        /// </summary>
        private int mMaxRow = 9;

        /// <summary>
        /// Max col
        /// </summary>
        private int mMaxCol = 9;

        /// <summary>
        /// Stores the octopuses.
        /// </summary>
        private List<List<int>> mOctopuses;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the path of the input.
        /// </summary>
        protected override string Path
        {
            get
            {
                return DataProvider.DAY11INPUTS_PATH;
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
        /// Initializes a new instance of the <see cref="Day11"/> class.
        /// </summary>
        public Day11()
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Initializes the data.
        /// </summary>
        /// <param name="pInput"></param>
        private void InitializesData(IEnumerable<string> pInput)
        {
            this.mOctopuses = new List<List<int>>();
            foreach (string lLine in pInput)
            {
                this.mOctopuses.Add(lLine.Select(pChar => int.Parse(pChar.ToString())).ToList());
            }
        }

        /// <summary>
        /// Computes part 1
        /// </summary>
        /// <param name="pInput"></param>
        /// <returns></returns>
        private string ComputePart1(IEnumerable<string> pInput)
        {
            this.InitializesData(pInput);
            int lResult = 0;
            this.mOctopuses.MapPrint();
            for (int lCount = 0; lCount < 100; lCount++)
            {
                this.Step();
                lResult += this.CountZero();
            }
            this.mOctopuses.MapPrint();
            return lResult.ToString();
        }

        /// <summary>
        /// Computes part 2
        /// </summary>
        /// <param name="pInput"></param>
        /// <returns></returns>
        private string ComputePart2(IEnumerable<string> pInput)
        {
            this.InitializesData(pInput);
            int lResult = 0;
            while (this.CountZero() != (this.mMaxCol+1)*(this.mMaxRow+1))
            {
                this.Step();
                lResult++;
            }
            return lResult.ToString();
        }

        /// <summary>
        /// Run a step.
        /// </summary>
        /// <returns></returns>
        private void Step()
        {
            this.IncreaseEverybodyLevel(1);
            List<Tuple<int, int>> lZeros = this.GetZeroCoordinates();
            List<Tuple<int, int>> lAlreadyTreatedZeros = new List<Tuple<int, int>>();
            while (lZeros.Any())
            {
                Tuple<int, int> lPop = lZeros.Pop();
                lAlreadyTreatedZeros.AddTuple(lPop);
                List<Tuple<int, int>> lNeighbors = Utils.GetNeighbors(lPop, this.mMaxCol, this.mMaxRow, true);
                this.IncreaseGivenOctopusesValue(lNeighbors, 1);
                lZeros = this.GetZeroCoordinates();
                foreach (Tuple<int, int> lTreated in lAlreadyTreatedZeros)
                {
                    lZeros.RemoveTuple(lTreated);
                }
            }
        }

        /// <summary>
        /// Increases the value of the given octopuses.
        /// </summary>
        /// <param name="pCoordinates"></param>
        /// <param name="pValue"></param>
        private void IncreaseGivenOctopusesValue(List<Tuple<int,int>> pCoordinates, int pValue)
        {
            foreach (Tuple<int,int> lCoordinate in pCoordinates)
            {
                int lValue = this.mOctopuses.GetValueFromTuple(lCoordinate);
                if (lValue >= (10 - pValue))
                {
                    lValue = 0;
                }
                else
                {
                    if (lValue != 0)
                    {
                        lValue = lValue + pValue;
                    }
                }
                this.mOctopuses.SetValueByCoordinates(lCoordinate, lValue);
            }
        }

        /// <summary>
        /// Increases every octopus by 1 level.
        /// </summary>
        /// <param name="pValue"></param>
        private void IncreaseEverybodyLevel(int pValue)
        {
            for (int lRow = 0; lRow <= this.mMaxRow; lRow++)
            {
                for (int lCol = 0; lCol <= this.mMaxCol; lCol++)
                {
                    int lValue = this.mOctopuses[lCol][lRow];
                    this.mOctopuses[lCol][lRow] = (lValue + pValue) % 10;
                }
            }
        }

        /// <summary>
        /// Counts the zero in the map.
        /// </summary>
        /// <returns></returns>
        private int CountZero()
        {
            int lResult = 0;
            foreach (List<int> lRow in this.mOctopuses)
            {
                lResult += lRow.Where(pVal => pVal == 0).Count();
            }
            return lResult;
        }

        /// <summary>
        /// Returns the list of coordinates of the zero.
        /// </summary>
        /// <returns></returns>
        private List<Tuple<int, int>> GetZeroCoordinates()
        {
            List<Tuple<int, int>> lResult = new List<Tuple<int, int>>();
            for (int lRow = 0; lRow <= this.mMaxRow; lRow++)
            {
                for (int lCol = 0; lCol <= this.mMaxCol; lCol++)
                {
                    Tuple<int, int> lCoordinates = new Tuple<int, int>(lCol, lRow);
                    if (this.mOctopuses.GetValueFromTuple(lCoordinates) == 0)
                    {
                        lResult.Add(lCoordinates);
                    }
                }
            }
            return lResult;
        }

        #endregion
    }
}
