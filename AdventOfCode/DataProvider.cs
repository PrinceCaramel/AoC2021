using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    /// <summary>
    /// Stores the data given by the game.
    /// </summary>
    public static class DataProvider
    {
        #region Fields

        /// <summary>
        /// Stores the measurements file path.
        /// </summary>
        private static string MEASUREMENTS_PATH = @"..\..\Measurements.txt";

        /// <summary>
        /// Stores the Movement inputs file path.
        /// </summary>
        private static string MOVEMENTINPUTS_PATH = @"..\..\MovementInputs.txt";

        #endregion

        #region Methods

        /// <summary>
        /// Gets the sonar measurements as an int array.
        /// </summary>
        /// <returns></returns>
        public static int[] GetSonarRawMeasurements()
        {
            return File.ReadLines(Utils.GetFullPath(DataProvider.MEASUREMENTS_PATH)).Select(pLine => int.Parse(pLine)).ToArray();
        }

        /// <summary>
        /// Gets the sonar measurements, split in a sum of 3 input, as an int array.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<int> GetSonar3Measurements()
        {
            List<int> lResult = new List<int>();
            int[] lRawMeasurements = GetSonarRawMeasurements();

            int lInputCount = lRawMeasurements.Count();

            for (int lIndex = 0; lIndex < lInputCount - 2; lIndex++)
            {
                int lSum = lRawMeasurements[lIndex] + lRawMeasurements[lIndex + 1] + lRawMeasurements[lIndex + 2];
                lResult.Add(lSum);
            }
            return lResult;
        }

        /// <summary>
        /// Gets the position from the movement inputs.
        /// </summary>
        /// <returns></returns>
        public static Tuple<int, int> GetPositionFromMovementInputs()
        {
            int lHorizontal = 0, lDepth = 0, lAim = 0;
            File.ReadLines(Utils.GetFullPath(DataProvider.MOVEMENTINPUTS_PATH)).ForEach<string>(pLine => HandleMovementInput(pLine, ref lHorizontal, ref lDepth, ref lAim));
            return new Tuple<int, int>(lHorizontal, lDepth);
        }

        /// <summary>
        /// Handles the movement inputs.
        /// </summary>
        /// <param name="pLine"></param>
        /// <param name="pHorizontal"></param>
        /// <param name="pDepth"></param>
        private static void HandleMovementInput(string pLine, ref int pHorizontal, ref int pDepth, ref int pAim)
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
        
        #endregion
    }
}
