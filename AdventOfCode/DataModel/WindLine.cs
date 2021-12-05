using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.DataModel
{
    /// <summary>
    /// Describes the grid points.
    /// </summary>
    public class GridPoint
    {
        #region Properties

        /// <summary>
        /// Gets the X
        /// </summary>
        public int X
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the Y
        /// </summary>
        public int Y
        {
            get;
            private set;
        }

        /// <summary>
        /// This point as a string.
        /// </summary>
        public string AsString
        {
            get;
            private set;
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GridPoint"/> class.
        /// </summary>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        public GridPoint(int pX, int pY)
        {
            this.X = pX;
            this.Y = pY;
            this.AsString = string.Format("{0},{1}", this.X, this.Y);
        }

        #endregion Constructors
    }

    /// <summary>
    /// Describes a line.
    /// </summary>
    public class WindLine
    {
        #region Properties

        /// <summary>
        /// Gets the beginning point.
        /// </summary>
        public GridPoint Begin
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the end point.
        /// </summary>
        public GridPoint End
        {
            get;
            private set;
        }

        /// <summary>
        /// The points involved in the line.
        /// </summary>
        public List<GridPoint> Points
        {
            get;
            private set;
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WinfLine"/> class.
        /// </summary>
        public WindLine(string pLine)
        {
            this.InitializesLine(pLine);
            this.InitializesInvolvedPoints();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Indicates if this line is vertical or horizontal.
        /// </summary>
        /// <returns></returns>
        public bool IsVerticalOrHorizontal()
        {
            return this.Begin.X == this.End.X || this.Begin.Y == this.End.Y;
        }

        /// <summary>
        /// Initializes the line.
        /// </summary>
        /// <param name="pLine"></param>
        private void InitializesLine(string pLine)
        {
            string[] lSplit = pLine.Replace(" -> ", ",").Split(',');
            this.Begin = new GridPoint(int.Parse(lSplit[0]), int.Parse(lSplit[1]));
            this.End = new GridPoint(int.Parse(lSplit[2]), int.Parse(lSplit[3]));
        }

        /// <summary>
        /// Initializes the involved points.
        /// </summary>
        private void InitializesInvolvedPoints()
        {
            this.Points = new List<GridPoint>();
            int lX = this.Begin.X;
            int lY = this.Begin.Y;
            Func<int, int> lXMethod = this.GetIncrementDecrementMethod(this.Begin.X, this.End.X);
            Func<int, int> lYMethod = this.GetIncrementDecrementMethod(this.Begin.Y, this.End.Y);
            while (lX != this.End.X || lY != this.End.Y)
            {
                this.Points.Add(new GridPoint(lX, lY));
                lX = lXMethod(lX);
                lY = lYMethod(lY);
            }
            this.Points.Add(this.End);
        }

        /// <summary>
        /// Gets the increment or decrement method according to 2 values.
        /// </summary>
        /// <param name="pBeginning"></param>
        /// <param name="pEnd"></param>
        /// <returns></returns>
        private Func<int, int> GetIncrementDecrementMethod(int pBeginning, int pEnd)
        {
            if (pBeginning == pEnd)
            {
                return pX => pX;
            }
            else
            {
                if (pBeginning > pEnd)
                {
                    return pX => pX - 1;
                }
                else
                {
                    return pX => pX + 1;
                }
            }
        }

        #endregion Methods
    }
}
