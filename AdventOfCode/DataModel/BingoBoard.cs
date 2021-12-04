using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.DataModel
{
    /// <summary>
    /// Describes a bingo board.
    /// </summary>
    public class BingoBoard
    {
        #region Properties

        /// <summary>
        /// Bingo board represented as a list of list of int
        /// </summary>
        public List<List<int>> BingoBoardAsList
        {
            get;
            private set;
        }

        /// <summary>
        /// Bingo board represented as a list of list of bool
        /// </summary>
        public List<List<bool>> BingoBoardCheck
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the id of the board.
        /// </summary>
        public int Id
        { get; private set; }

        #endregion Properties
        
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BingoBoard"/> class.
        /// </summary>
        public BingoBoard(List<string> pBingoBoard, int pId)
        {
            this.Id = pId;
            this.BingoBoardAsList = new List<List<int>>();
            this.InitializesBingoBoard(pBingoBoard);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Initializes the bingo board with given data.
        /// </summary>
        /// <param name="pBingoBoard"></param>
        private void InitializesBingoBoard(List<string> pBingoBoard)
        {
            this.BingoBoardCheck = new List<List<bool>>();
            this.BingoBoardCheck.Add(new List<bool> { false, false, false, false, false });
            this.BingoBoardCheck.Add(new List<bool> { false, false, false, false, false });
            this.BingoBoardCheck.Add(new List<bool> { false, false, false, false, false });
            this.BingoBoardCheck.Add(new List<bool> { false, false, false, false, false });
            this.BingoBoardCheck.Add(new List<bool> { false, false, false, false, false });
            pBingoBoard.ForEach(pLine => this.BingoBoardAsList.Add(this.SplitLine(pLine)));
        }

        /// <summary>
        /// Split a line into a list of tuple(int, bool).
        /// </summary>
        /// <param name="pLine"></param>
        /// <returns></returns>
        private List<int> SplitLine(string pLine)
        {
            List<int> lResult = new List<int>();

            string[] lSplits = pLine.Split(' ');
            foreach (string lSplit in lSplits)
            {
                int lValue;
                if (int.TryParse(lSplit, out lValue))
                {
                    lResult.Add(lValue);
                }
            }
            return lResult;
        }

        /// <summary>
        /// Update bingoboard.
        /// </summary>
        /// <param name="pValue"></param>
        public void UpdateBingoBoard(int pValue)
        {
            int lRow = 0;
            int lColumn = 0;
            foreach (List<int> lLine in this.BingoBoardAsList)
            {
                lRow = 0;
                foreach (int lValue in lLine)
                {
                    if (lValue == pValue)
                    {
                        this.BingoBoardCheck[lColumn][lRow] = true;
                        break;
                    }
                    lRow++;
                }
                lColumn++;
            }
        }

        /// <summary>
        /// Indicates if the board is a winning board.
        /// </summary>
        /// <returns></returns>
        public bool IsWinning()
        {
            if (this.BingoBoardCheck.Any(pLine => pLine.All(pValue => pValue)))
            {
                return true;
            }
            else
            {
                for (int lIndex = 0; lIndex < 5; lIndex++)
                {
                    bool lResult = this.BingoBoardCheck[0][lIndex] && this.BingoBoardCheck[1][lIndex] && this.BingoBoardCheck[2][lIndex] && this.BingoBoardCheck[3][lIndex] && this.BingoBoardCheck[4][lIndex];
                    if (lResult)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Gets the sum of unmarked number.
        /// </summary>
        /// <returns></returns>
        public int GetSumOfUnmarkedNumbers()
        {
            int lAcc = 0;
            for (int lColumnIndex = 0; lColumnIndex < 5; lColumnIndex++)
            {
                for (int lRowIndex = 0; lRowIndex < 5; lRowIndex++)
                {
                    if (!this.BingoBoardCheck[lColumnIndex][lRowIndex])
                    {
                        lAcc += this.BingoBoardAsList[lColumnIndex][lRowIndex];
                    }
                }
            }
            return lAcc;
        }

        #endregion Methods
    }
}
