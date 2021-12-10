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
    public class Day10 : ADay
    {
        #region Fields

        /// <summary>
        /// The current stack.
        /// </summary>
        private Stack<char> mCurrentStack = new Stack<char>();

        /// <summary>
        /// The opening char.
        /// </summary>
        private List<char> mOpeningChars = new List<char>() { '(', '<', '{', '[' };

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the path of the input.
        /// </summary>
        protected override string Path
        {
            get
            {
                return DataProvider.DAY10INPUTS_PATH;
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
        /// Initializes a new instance of the <see cref="Day10"/> class.
        /// </summary>
        public Day10()
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
            int lResult = 0;
            foreach (string lLine in pInput)
            {
                lResult += this.GetClosingValuePart1(this.ValidateLine(lLine));
            }
            return lResult.ToString();
        }

        /// <summary>
        /// Computes part 2.
        /// </summary>
        /// <param name="pInput"></param>
        /// <returns></returns>
        private string ComputePart2(IEnumerable<string> pInput)
        {
            List<UInt64> lValues = new List<UInt64>();
            foreach(string lLine in pInput)
            {
                if (this.GetClosingValuePart1(this.ValidateLine(lLine)) == 0)
                {
                    List<char> lClosingChar = new List<char>();
                    while (this.mCurrentStack.Any())
                    {
                        lClosingChar.Add(this.GetClosing(this.mCurrentStack.Pop()));
                    }
                    lValues.Add(lClosingChar.Aggregate((UInt64)0, (pAcc, pNext) => pAcc = 5 * pAcc + this.GetClosingValuePart2(pNext), pAcc => pAcc));
                }
            }
            lValues.Sort();
            return (lValues[lValues.Count()/2]).ToString();
        }

        /// <summary>
        /// Validate a line and returns the failure char if any, char.MaxValue otherwise.
        /// </summary>
        /// <param name="pLine"></param>
        /// <returns></returns>
        private char ValidateLine(string pLine)
        {
            char lResult = char.MaxValue;
            this.mCurrentStack.Clear();
            foreach (char lChar in pLine)
            {
                if (this.mOpeningChars.Contains(lChar))
                {
                    this.mCurrentStack.Push(lChar);
                }
                else if (this.GetClosing(this.mCurrentStack.Peek()).Equals(lChar))
                {
                    this.mCurrentStack.Pop();
                }
                else
                {
                    lResult = lChar;
                    break;
                }
            }
            return lResult;
        }

        /// <summary>
        /// Gets the closing char from an opening char.
        /// </summary>
        /// <param name="pOpenChar"></param>
        /// <returns></returns>
        private char GetClosing(char pOpenChar)
        {
            if (pOpenChar.Equals('('))
            {
                return ')';
            }
            if (pOpenChar.Equals('{'))
            {
                return '}';
            }
            if (pOpenChar.Equals('<'))
            {
                return '>';
            }
            if (pOpenChar.Equals('['))
            {
                return ']';
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the closing char from an opening char.
        /// </summary>
        /// <param name="pOpenChar"></param>
        /// <returns></returns>
        private int GetClosingValuePart1(char pOpenChar)
        {
            if (pOpenChar.Equals(')'))
            {
                return 3;
            }
            if (pOpenChar.Equals('}'))
            {
                return 1197;
            }
            if (pOpenChar.Equals('>'))
            {
                return 25137;
            }
            if (pOpenChar.Equals(']'))
            {
                return 57;
            }
            return 0;
        }

        /// <summary>
        /// Gets the closing char from an opening char.
        /// </summary>
        /// <param name="pOpenChar"></param>
        /// <returns></returns>
        private UInt64 GetClosingValuePart2(char pOpenChar)
        {
            if (pOpenChar.Equals(')'))
            {
                return 1;
            }
            if (pOpenChar.Equals('}'))
            {
                return 3;
            }
            if (pOpenChar.Equals('>'))
            {
                return 4;
            }
            if (pOpenChar.Equals(']'))
            {
                return 2;
            }
            return 0;
        }

        #endregion
    }
}
