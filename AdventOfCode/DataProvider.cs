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
        public const string MEASUREMENTS_PATH = @"..\..\Data\Measurements.txt";

        /// <summary>
        /// Stores the Movement inputs file path.
        /// </summary>
        public const string MOVEMENTINPUTS_PATH = @"..\..\Data\MovementInputs.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY3INPUTS_PATH = @"..\..\Data\Day3Inputs.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string BINGOTESTINPUTS_PATH = @"..\..\Data\BingoTest.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string BINGOINPUTS_PATH = @"..\..\Data\BingoDay4.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY5TESTINPUTS_PATH = @"..\..\Data\Day5TestInput.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY5INPUTS_PATH = @"..\..\Data\Day5Input.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY6TESTINPUTS_PATH = @"..\..\Data\Day6InputTest.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY6INPUTS_PATH = @"..\..\Data\Day6Input.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY7TESTINPUTS_PATH = @"..\..\Data\Day7TestInput.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY7INPUTS_PATH = @"..\..\Data\Day7Input.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY8TESTINPUTS_PATH = @"..\..\Data\Day8TestInput.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY8INPUTS_PATH = @"..\..\Data\Day8Input.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY9TESTINPUTS_PATH = @"..\..\Data\Day9TestInput.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY9INPUTS_PATH = @"..\..\Data\Day9Input.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY10TESTINPUTS_PATH = @"..\..\Data\Day10TestInput.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY10INPUTS_PATH = @"..\..\Data\Day10Input.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY11TESTINPUTS_PATH = @"..\..\Data\Day11TestInput.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY11TEST2INPUTS_PATH = @"..\..\Data\Day11Test2Input.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY11INPUTS_PATH = @"..\..\Data\Day11Input.txt";

        #endregion
    }
}
