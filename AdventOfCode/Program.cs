using AdventOfCode.Days;
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
            ADay lDay = new Day23(); 
            Console.WriteLine(lDay.SolvePart1());
            Console.WriteLine(lDay.SolvePart2());
            Console.ReadLine();
        }

        #endregion
    }
}
