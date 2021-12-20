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
    public class Day20 : ADay
    {
        #region Fields

        /// <summary>
        /// Stores the decode algorithm.
        /// </summary>
        private List<int> mDecodeAlgorithm = new List<int>();

        /// <summary>
        /// Stores the pixel to value map.
        /// </summary>
        private Dictionary<Coord, int> mPixelToValue = new Dictionary<Coord, int>();

        /// <summary>
        /// Stores the topleft.
        /// </summary>
        private Coord mTopLeft;

        /// <summary>
        /// Stores the botoom right.
        /// </summary>
        private Coord mBottomRight;

        /// <summary>
        /// Stores the step count.
        /// </summary>
        private int mStepCount = 0;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the path of the input.
        /// </summary>
        protected override string Path
        {
            get
            {
                return DataProvider.DAY20INPUTS_PATH;
            }
        }

        /// <summary>
        /// Gets the part1 function.
        /// </summary>
        protected override Func<IEnumerable<string>, string> Part1Function
        {
            get
            {
                return pInput => this.Compute(pInput, 2);
            }
        }

        /// <summary>
        /// Gets the part2 function.
        /// </summary>
        protected override Func<IEnumerable<string>, string> Part2Function
        {
            get
            {
                return pInput => this.Compute(pInput, 50);
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Day20"/> class.
        /// </summary>
        public Day20()
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Computes the result.
        /// </summary>
        /// <param name="pInput"></param>
        /// <param name="pLoop"></param>
        /// <returns></returns>
        private string Compute(IEnumerable<string> pInput, int pLoop)
        {
            this.InitializeData(pInput);
            for (int lCount = 0; lCount < pLoop; lCount++)
            {
                this.Step();
            }
            this.DisplayCurrentImage();
            return this.mPixelToValue.Values.Where(pVal => pVal == 1).Count().ToString();
        }

        /// <summary>
        /// Initializes the data.
        /// </summary>
        /// <param name="pInput"></param>
        private void InitializeData(IEnumerable<string> pInput)
        {
            this.mDecodeAlgorithm.Clear();
            this.mPixelToValue.Clear();
            List<string> lInput = pInput.ToList();
            string lDecodeAlgorithm = lInput.Pop<string>();
            for (int lIndex = 0; lIndex < lDecodeAlgorithm.Length; lIndex++)
            {
                if (lDecodeAlgorithm[lIndex].Equals('#'))
                {
                    this.mDecodeAlgorithm.Add(lIndex);
                }
            }
            lInput.Pop<string>();
            this.mTopLeft = new Coord(0, 0);
            this.mBottomRight = new Coord(lInput.First().Count() - 1, lInput.Count() - 1);
            int lLineCount = 0;
            while (lInput.Any())
            {
                string lLine = lInput.Pop<string>();
                for (int lLineIndex = 0; lLineIndex < lLine.Count(); lLineIndex++)
                {
                    Coord lCoordToAdd = new Coord(lLineIndex, lLineCount);
                    int lIsPixelOn = lLine[lLineIndex].Equals('#') ? 1 : 0;
                    this.mPixelToValue.Add(lCoordToAdd, lIsPixelOn);
                }
                lLineCount++;
            }
        }

        /// <summary>
        /// Runs a step.
        /// </summary>
        private void Step()
        {
            Dictionary<Coord, int> lNewImage = new Dictionary<Coord, int>();
            this.mStepCount++;
            this.mTopLeft = new Coord(this.mTopLeft.X - 1, this.mTopLeft.Y - 1);
            this.mBottomRight = new Coord(this.mBottomRight.X + 1, this.mBottomRight.Y + 1);

            for (int lY = mTopLeft.Y; lY <= this.mBottomRight.Y; lY++)
            {
                for (int lX = this.mTopLeft.X; lX <= this.mBottomRight.X; lX++)
                {
                    Coord lCoord = new Coord(lX, lY);
                    int lNewValue = this.mDecodeAlgorithm.Contains(this.GetPixelValue(lCoord)) ? 1 : 0;
                    lNewImage.Add(lCoord, lNewValue);
                }
            }
            this.mPixelToValue = lNewImage;
        }

        /// <summary>
        /// Gets pixel value.
        /// </summary>
        /// <param name="pCoord"></param>
        /// <returns></returns>
        private int GetPixelValue(Coord pCoord)
        {
            List<Coord> lNeighbors = pCoord.NeighborsAndSelf();
            StringBuilder lStrBuilder = new StringBuilder();
            foreach (Coord lCoord in lNeighbors)
            {
                int lValue;
                if (!this.mPixelToValue.TryGetValue(lCoord, out lValue))
                {
                    lValue = this.GetOutsideChar();
                }
                string lChar = lValue.ToString();
                lStrBuilder.Append(lChar);
            }
            return Convert.ToInt32(lStrBuilder.ToString(), 2);
        }

        /// <summary>
        /// Gets outside char.
        /// </summary>
        /// <returns></returns>
        private int GetOutsideChar()
        {
            int lResult = 0;
            if (this.mDecodeAlgorithm.First() == 0)
            {
                lResult = this.mStepCount % 2 == 0 ? 1 : 0;
            }
            return lResult;
        }

        /// <summary>
        /// Display current image.
        /// </summary>
        private void DisplayCurrentImage()
        {
            StringBuilder lStrBuilder = new StringBuilder();
            for (int lY = mTopLeft.Y; lY <= this.mBottomRight.Y; lY++)
            {
                for (int lX = this.mTopLeft.X; lX <= this.mBottomRight.X; lX++)
                {
                    char lChar = this.mPixelToValue[new Coord(lX, lY)] == 1 ? '#' : '.';
                    lStrBuilder.Append(lChar);
                }
                lStrBuilder.AppendLine();
            }
            Console.WriteLine(lStrBuilder.ToString());
        }

        #endregion
    }

    /// <summary>
    /// Struct for a coord.
    /// </summary>
    public struct Coord
    {
        /// <summary>
        /// X coordinate.
        /// </summary>
        public int X
        {
            get;
        }

        /// <summary>
        /// Y coordinate.
        /// </summary>
        public int Y
        {
            get;
        }

        /// <summary>
        /// Instantiate a new coord.
        /// </summary>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        public Coord(int pX, int pY)
        {
            this.X = pX;
            this.Y = pY;
        }

        /// <summary>
        /// Gets the neighbors and self.
        /// </summary>
        /// <returns></returns>
        public List<Coord> NeighborsAndSelf()
        {
            List<Coord> lResult = new List<Coord>();
            for (int lY = -1; lY < 2; lY++)
            {
                for (int lX = -1; lX < 2; lX++)
                {
                    lResult.Add(new Coord(this.X + lX, this.Y + lY));
                }
            }
            return lResult;
        }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="pCoord"></param>
        /// <returns></returns>
        public override bool Equals(object pCoord)
        {
            if (pCoord is Coord lCoord)
            {
                return this.X == lCoord.X && this.Y == lCoord.Y;
            }
            return false;
        }

        /// <summary>
        /// Tos tring.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("x:{0} y:{1}", this.X, this.Y);
        }
    }
}
