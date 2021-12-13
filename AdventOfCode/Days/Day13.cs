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
    public class Day13 : ADay
    {
        #region Fields

        /// <summary>
        /// Stores the points coordinates.
        /// </summary>
        List<Tuple<int, int>> mPointCoordinates;

        /// <summary>
        /// Stores the fold instructions.
        /// </summary>
        List<Tuple<string, int>> mFoldInstructions;

        /// <summary>
        /// Stores the const of fold instruction.
        /// </summary>
        private const string FOLD_ALONG = "fold along ";

        /// <summary>
        /// Stores the x value.
        /// </summary>
        private const string X = "x";

        /// <summary>
        /// Stores the y value.
        /// </summary>
        private const string Y = "y";

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the path of the input.
        /// </summary>
        protected override string Path
        {
            get
            {
                return DataProvider.DAY13INPUTS_PATH;
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
        /// Initializes a new instance of the <see cref="Day13"/> class.
        /// </summary>
        public Day13()
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
            this.mFoldInstructions = new List<Tuple<string, int>>();
            this.mPointCoordinates = new List<Tuple<int, int>>();
            foreach (string lLine in pInput)
            {
                if (!string.IsNullOrEmpty(lLine))
                {
                    string[] lSplit;
                    if (lLine.StartsWith(Day13.FOLD_ALONG))
                    {
                        lSplit = lLine.Replace(Day13.FOLD_ALONG, "").Split('=');
                        this.mFoldInstructions.Add(new Tuple<string, int>(lSplit[0], int.Parse(lSplit[1])));
                    }
                    else
                    {
                        lSplit = lLine.Split(',');
                        this.mPointCoordinates.Add(new Tuple<int,int> (int.Parse(lSplit[0]), int.Parse(lSplit[1])));
                    }
                }
            }
        }

        /// <summary>
        /// Computes part 1.
        /// </summary>
        /// <param name="pInput"></param>
        /// <returns></returns>
        private string ComputePart1(IEnumerable<string> pInput)
        {
            this.InitializeData(pInput);
            Tuple<string, int> lInstruction = this.mFoldInstructions.Pop();
            this.Fold(lInstruction);
            return this.mPointCoordinates.Count().ToString();
        }

        /// <summary>
        /// Computes part 2.
        /// </summary>
        /// <param name="pInput"></param>
        /// <returns></returns>
        private string ComputePart2(IEnumerable<string> pInput)
        {
            this.InitializeData(pInput);
            while (this.mFoldInstructions.Any())
            {
                Tuple<string, int> lInstruction = this.mFoldInstructions.Pop();
                this.Fold(lInstruction);
            }
            return this.Display();
        }

        /// <summary>
        /// Fold according to an instruction.
        /// </summary>
        /// <param name="pFoldInstruction"></param>
        private void Fold(Tuple<string, int> pFoldInstruction)
        {
            List<Tuple<int, int>> lInvolvedPoints;
            lInvolvedPoints = this.GetPointsFromFoldInstruction(pFoldInstruction);
            this.mPointCoordinates.RemoveAll(pPoint => lInvolvedPoints.Contains(pPoint));
            foreach (Tuple<int, int> lPoint in lInvolvedPoints)
            {
                int lX = pFoldInstruction.Item1.Equals(Day13.Y) ? lPoint.Item1 : (2 * pFoldInstruction.Item2 - lPoint.Item1);
                int lY = pFoldInstruction.Item1.Equals(Day13.Y) ? (2 * pFoldInstruction.Item2 - lPoint.Item2) : lPoint.Item2;
                this.mPointCoordinates.AddTuple(new Tuple<int, int>(lX, lY));
            }
        }

        /// <summary>
        /// Gets the points to be folded.
        /// </summary>
        /// <param name="pFoldInstruction"></param>
        /// <returns></returns>
        private List<Tuple<int,int>> GetPointsFromFoldInstruction(Tuple<string, int> pFoldInstruction)
        {
            return this.mPointCoordinates.Where(pCoord => (pFoldInstruction.Item1.Equals(Day13.X) ? pCoord.Item1 > pFoldInstruction.Item2 : pCoord.Item2 > pFoldInstruction.Item2)).ToList();
        }

        /// <summary>
        /// Gives the string to display.
        /// </summary>
        /// <returns></returns>
        private string Display()
        {
            int lXMax = this.mPointCoordinates.Aggregate(0, (pAcc, pNext) => pAcc = Math.Max(pNext.Item1, pAcc), pAcc => pAcc) + 1;
            int lYMax = this.mPointCoordinates.Aggregate(0, (pAcc, pNext) => pAcc = Math.Max(pNext.Item2, pAcc), pAcc => pAcc) + 1;
            List<string> lDisplay = new List<string>();
            StringBuilder lStrBuil = new StringBuilder();
            lStrBuil.Append('.', lXMax);
            for (int lY = 0; lY < lYMax; lY++)
            {
                lDisplay.Add(lStrBuil.ToString());
            }
            this.mPointCoordinates.ForEach(pTuple => lDisplay[pTuple.Item2] = lDisplay[pTuple.Item2].Remove(pTuple.Item1, 1).Insert(pTuple.Item1, "#"));
            StringBuilder lResult = new StringBuilder();
            lDisplay.ForEach(pLine => lResult.AppendLine(pLine));
            return lResult.ToString();
        }

        #endregion
    }
}
