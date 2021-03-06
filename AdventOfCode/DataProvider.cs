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

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY12TESTINPUTS_PATH = @"..\..\Data\Day12TestInput.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY12INPUTS_PATH = @"..\..\Data\Day12Input.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY13TESTINPUTS_PATH = @"..\..\Data\Day13TestInput.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY13INPUTS_PATH = @"..\..\Data\Day13Input.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY14TESTINPUTS_PATH = @"..\..\Data\Day14TestInput.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY14INPUTS_PATH = @"..\..\Data\Day14Input.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY15TESTINPUTS_PATH = @"..\..\Data\Day15TestInput.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY15INPUTS_PATH = @"..\..\Data\Day15Input.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY16TESTINPUTS_PATH = @"..\..\Data\Day16TestInput.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY16INPUTS_PATH = @"..\..\Data\Day16Input.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY17TESTINPUTS_PATH = @"..\..\Data\Day17TestInput.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY17INPUTS_PATH = @"..\..\Data\Day17Input.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY18TESTINPUTS_PATH = @"..\..\Data\Day18TestInput.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY18INPUTS_PATH = @"..\..\Data\Day18Input.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY19TESTINPUTS_PATH = @"..\..\Data\Day19TestInput.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY19INPUTS_PATH = @"..\..\Data\Day19Input.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY20TESTINPUTS_PATH = @"..\..\Data\Day20TestInput.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY20INPUTS_PATH = @"..\..\Data\Day20Input.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY21TESTINPUTS_PATH = @"..\..\Data\Day21TestInput.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY21INPUTS_PATH = @"..\..\Data\Day21Input.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY22TESTINPUTS_PATH = @"..\..\Data\Day22TestInput.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY22INPUTS_PATH = @"..\..\Data\Day22Input.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY23TESTINPUTS_PATH = @"..\..\Data\Day23TestInput.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY23INPUTS_PATH = @"..\..\Data\Day23Input.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY24TESTINPUTS_PATH = @"..\..\Data\Day24TestInput.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY24INPUTS_PATH = @"..\..\Data\Day24Input.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY25TESTINPUTS_PATH = @"..\..\Data\Day25TestInput.txt";

        /// <summary>
        /// Stores the Day3 inputs file path.
        /// </summary>
        public const string DAY25INPUTS_PATH = @"..\..\Data\Day25Input.txt";

        #endregion
    }
}
