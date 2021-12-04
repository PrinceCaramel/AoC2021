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

        #endregion Properties
        
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BingoBoard"/> class.
        /// </summary>
        public BingoBoard(List<string> pBingoBoard)
        {
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
                        this.BingoBoardAsList[lColumn][lRow] = -1;
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
            if (this.BingoBoardAsList.Any(pLine => pLine.All(pValue => pValue == -1)))
            {
                return true;
            }
            else
            {
                for (int lIndex = 0; lIndex < 5; lIndex++)
                {
                    bool lResult = (this.BingoBoardAsList[0][lIndex] + this.BingoBoardAsList[1][lIndex] + this.BingoBoardAsList[2][lIndex] + this.BingoBoardAsList[3][lIndex] + this.BingoBoardAsList[4][lIndex]) == -5;
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
                    int lValue = this.BingoBoardAsList[lColumnIndex][lRowIndex];
                    if (lValue != -1)
                    {
                        lAcc += lValue;
                    }
                }
            }
            return lAcc;
        }

        #endregion Methods
    }
}
