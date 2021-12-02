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
    public class Day2 : ADay
    {
        #region Properties

        /// <summary>
        /// Gets the path of the input.
        /// </summary>
        protected override string Path
        {
            get
            {
                return DataProvider.MOVEMENTINPUTS_PATH;
            }
        }

        /// <summary>
        /// Gets the part1 function.
        /// </summary>
        protected override Func<IEnumerable<string>, string> Part1Function
        {
            get
            {

                return pInput => Utils.Multiply(Day2.GetPositionFromMovementInputs(pInput)).ToString();
            }
        }

        /// <summary>
        /// Gets the part2 function.
        /// </summary>
        protected override Func<IEnumerable<string>, string> Part2Function
        {
            get
            {
                return pInput => Utils.Multiply(Day2.GetPositionFromMovementInputs2(pInput)).ToString();
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Day2"/> class.
        /// </summary>
        public Day2()
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets the position from the movement inputs.
        /// </summary>
        /// <returns></returns>
        protected static Tuple<int, int> GetPositionFromMovementInputs2(IEnumerable<string> pInputs)
        {
            int lHorizontal = 0, lDepth = 0, lAim = 0;
            pInputs.ForEach<string>(pLine => Day2.HandleMovementInput2(pLine, ref lHorizontal, ref lDepth, ref lAim));
            return new Tuple<int, int>(lHorizontal, lDepth);
        }

        /// <summary>
        /// Gets the position from the movement inputs.
        /// </summary>
        /// <returns></returns>
        protected static Tuple<int, int> GetPositionFromMovementInputs(IEnumerable<string> pInputs)
        {
            int lHorizontal = 0, lDepth = 0;
            pInputs.ForEach<string>(pLine => Day2.HandleMovementInput(pLine, ref lHorizontal, ref lDepth));
            return new Tuple<int, int>(lHorizontal, lDepth);
        }

        /// <summary>
        /// Handles the movement inputs.
        /// </summary>
        /// <param name="pLine"></param>
        /// <param name="pHorizontal"></param>
        /// <param name="pDepth"></param>
        /// <param name="pAim"></param>
        protected static void HandleMovementInput2(string pLine, ref int pHorizontal, ref int pDepth, ref int pAim)
        {
            string[] lLineSplit = pLine.Split(' ');
            string lAction = lLineSplit[0];
            int lValue = int.Parse(lLineSplit[1]);
            if (lAction.Equals(Utils.FORWARD))
            {
                pHorizontal += lValue;
                pDepth += pAim * lValue;
            }
            else if (lAction.Equals(Utils.UP))
            {
                pAim -= lValue;
            }
            else if (lAction.Equals(Utils.DOWN))
            {
                pAim += lValue;
            }
        }

        /// <summary>
        /// Handles the movement inputs.
        /// </summary>
        /// <param name="pLine"></param>
        /// <param name="pHorizontal"></param>
        /// <param name="pDepth"></param>
        protected static void HandleMovementInput(string pLine, ref int pHorizontal, ref int pDepth)
        {
            string[] lLineSplit = pLine.Split(' ');
            string lAction = lLineSplit[0];
            int lValue = int.Parse(lLineSplit[1]);
            if (lAction.Equals(Utils.FORWARD))
            {
                pHorizontal += lValue;
            }
            else if (lAction.Equals(Utils.UP))
            {
                pDepth -= lValue;
            }
            else if (lAction.Equals(Utils.DOWN))
            {
                pDepth += lValue;
            }
        }

        #endregion
    }
}
