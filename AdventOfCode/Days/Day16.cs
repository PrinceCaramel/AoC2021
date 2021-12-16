using AdventOfCode.DataModel;
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
    public class Day16 : ADay
    {
        #region Fields

        /// <summary>
        /// Stores the last packet.
        /// </summary>
        private Packet mLastPacket;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the path of the input.
        /// </summary>
        protected override string Path
        {
            get
            {
                return DataProvider.DAY16INPUTS_PATH;
            }
        }

        /// <summary>
        /// Gets the part1 function.
        /// </summary>
        protected override Func<IEnumerable<string>, string> Part1Function
        {
            get
            {
                return pInput => this.ComputePart1(pInput);
            }
        }

        /// <summary>
        /// Gets the part2 function.
        /// </summary>
        protected override Func<IEnumerable<string>, string> Part2Function
        {
            get
            {
                return pInput => this.ComputePart2(pInput);
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Day16"/> class.
        /// </summary>
        public Day16()
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Computes part 1.
        /// </summary>
        /// <param name="pInput"></param>
        /// <returns></returns>
        private string ComputePart1(IEnumerable<string> pInput)
        {
            this.InitializeData(pInput);
            return string.Format("\n{0}\nValue:{1}",this.mLastPacket.ToString(), this.mLastPacket.VersionSum.ToString());
        }

        /// <summary>
        /// Computes part 2.
        /// </summary>
        /// <param name="pInput"></param>
        /// <returns></returns>
        private string ComputePart2(IEnumerable<string> pInput)
        {
            this.InitializeData(pInput);
            return "\nValue: "+this.mLastPacket.Value.ToString();
        }

        /// <summary>
        /// Initializes the data.
        /// </summary>
        /// <param name="pInput"></param>
        private void InitializeData(IEnumerable<string> pInput)
        {
            foreach (string lLine in pInput)
            {
                string lBinary = this.ConvertToBinary(lLine);
                Packet lPacket = new Packet(lBinary);
                this.mLastPacket = lPacket;
            }
        }

        /// <summary>
        /// Converts an input into a binary code.
        /// </summary>
        /// <param name="pLine"></param>
        /// <returns></returns>
        private string ConvertToBinary(string pLine)
        {
            StringBuilder lStringBuilder = new StringBuilder();
            pLine.ForEach<char>(pChar => lStringBuilder.Append(Convert.ToString(Convert.ToInt32(pChar.ToString(), 16), 2).PadLeft(4, '0')));
            return lStringBuilder.ToString();
        }

        #endregion
    }
}
