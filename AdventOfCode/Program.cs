using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    /// <summary>
    /// Main program.
    /// </summary>
    public class Program
    {
        #region Constructors

        /// <summary>
        /// Main.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] pArgs)
        {
            Submarine lSubmarine = new Submarine();
            int lResult = Puzzle2_1();
            Console.WriteLine(lResult);
            Console.ReadLine();
        }

        #endregion

        #region Methods

        static int Puzzle1_1(Submarine pSubmarine)
        {
            return pSubmarine.Sonar.GetMeasurementsLargerThanPrevious(DataProvider.GetSonarRawMeasurements());
        }

        static int Puzzle1_2(Submarine pSubmarine)
        {
            return pSubmarine.Sonar.GetMeasurementsLargerThanPrevious(DataProvider.GetSonar3Measurements());
        }

        // DEPRECATED
        static int Puzzle2_1()
        {
            Tuple<int, int> lResult = DataProvider.GetPositionFromMovementInputs();
            return lResult.Item1 * lResult.Item2;
        }

        static int Puzzle2_2()
        {
            return Puzzle2_1();
        }

        #endregion Methods
    }
}
