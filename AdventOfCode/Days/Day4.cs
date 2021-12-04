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
    public class Day4 : ADay
    {
        #region Fields

        /// <summary>
        /// Stores the list of numbers that can be drawn.
        /// </summary>
        private List<int> mNumbers = new List<int>();

        /// <summary>
        /// Stores the list of bingo boards.
        /// </summary>
        private List<BingoBoard> mBingoBoards = new List<BingoBoard>();

        /// <summary>
        /// Stores the winning value.
        /// </summary>
        private int mWinningValue = -1;

        /// <summary>
        /// Stores the winning board.
        /// </summary>
        private BingoBoard mWinningBoard = null;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the path of the input.
        /// </summary>
        protected override string Path
        {
            get
            {
                return DataProvider.BINGOINPUTS_PATH;
            }
        }

        /// <summary>
        /// Gets the part1 function.
        /// </summary>
        protected override Func<IEnumerable<string>, string> Part1Function
        {
            get
            {
                return pInput => this.Compute(pInput, RunPart1).ToString();
            }
        }

        /// <summary>
        /// Gets the part2 function.
        /// </summary>
        protected override Func<IEnumerable<string>, string> Part2Function
        {
            get
            {
                return pInput => this.Compute(pInput, RunPart2).ToString();
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Day4"/> class.
        /// </summary>
        public Day4()
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Computes the puzzle.
        /// </summary>
        /// <param name="pInput"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        private int Compute(IEnumerable<string> pInput, Action pMethod)
        {
            this.InitializesData(pInput.ToList());
            pMethod();
            return this.mWinningBoard.GetSumOfUnmarkedNumbers() * this.mWinningValue;
        }

        /// <summary>
        /// Runs the computation.
        /// </summary>
        private void RunPart1()
        {
            bool lHasSomeoneWon = false;
            List<int> lNumbers = this.mNumbers.ToList();
            while (!lHasSomeoneWon)
            {
                int lDrawnValue = lNumbers.Pop<int>();
                foreach (BingoBoard lBingoBoard in this.mBingoBoards)
                {
                    lBingoBoard.UpdateBingoBoard(lDrawnValue);
                    lHasSomeoneWon = lBingoBoard.IsWinning();
                    if (lHasSomeoneWon)
                    {
                        this.mWinningBoard = lBingoBoard;
                        this.mWinningValue = lDrawnValue;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Runs the computation.
        /// </summary>
        private void RunPart2()
        {
            foreach(int lDraw in this.mNumbers)
            {
                List<BingoBoard> lBoards = this.mBingoBoards.ToList();
                foreach (BingoBoard lBingoBoard in lBoards)
                {
                    lBingoBoard.UpdateBingoBoard(lDraw);
                    if (lBingoBoard.IsWinning())
                    {
                        this.mWinningBoard = lBingoBoard;
                        this.mWinningValue = lDraw;
                        this.mBingoBoards.Remove(this.mWinningBoard);
                    }
                }
            }
        }

        /// <summary>
        /// Initiliazes the boards.
        /// </summary>
        /// <param name="pInput"></param>
        private void InitializesData(List<string> pInput)
        {
            int lIndex = 0;
            int lNumberOfLines = pInput.Count();
            while (lIndex < lNumberOfLines)
            {
                if (lIndex == 0)
                {
                    this.InitializesDrawnNumbers(pInput.FirstOrDefault());
                    lIndex += 2;
                }
                else
                {
                    List<string> lLines = new List<string>() { pInput[lIndex], pInput[lIndex+1], pInput[lIndex+2], pInput[lIndex+3], pInput[lIndex+4] };
                    this.mBingoBoards.Add(new BingoBoard(lLines));
                    lIndex += 6;
                }
            }
        }

        /// <summary>
        /// Initializes the drawn numbers.
        /// </summary>
        /// <param name="pFirstLine"></param>
        private void InitializesDrawnNumbers(string pFirstLine)
        {
            this.mNumbers = pFirstLine.Split(',').Select(pVal => int.Parse(pVal)).ToList();
        }

        #endregion
    }
}
