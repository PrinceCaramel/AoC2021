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
    public class Day14 : ADay
    {
        #region Fields

        /// <summary>
        /// Stores the pair map.
        /// </summary>
        private Dictionary<string, char> mPairMap;

        /// <summary>
        /// Stores the pair counter.
        /// </summary>
        private Dictionary<string, UInt64> mPairCounter;

        /// <summary>
        /// Stores the map from a pair to the constructed pairs after an iteration.
        /// </summary>
        private Dictionary<string, List<string>> mPairToConstructedPair;

        /// <summary>
        /// Stores the char counter.
        /// </summary>
        private Dictionary<char, UInt64> mCharCounter;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the path of the input.
        /// </summary>
        protected override string Path
        {
            get
            {
                return DataProvider.DAY14INPUTS_PATH;
            }
        }

        /// <summary>
        /// Gets the part1 function.
        /// </summary>
        protected override Func<IEnumerable<string>, string> Part1Function
        {
            get
            {
                return pInput => this.Compute(pInput, 10);
            }
        }

        /// <summary>
        /// Gets the part2 function.
        /// </summary>
        protected override Func<IEnumerable<string>, string> Part2Function
        {
            get
            {
                return pInput => this.Compute(pInput, 40);
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Day14"/> class.
        /// </summary>
        public Day14()
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Initializes the data.
        /// </summary>
        /// <param name="pInput"></param>
        private void InitializeData(IEnumerable<string> pInput)
        {
            string lTemplate = string.Empty;
            this.mPairMap = new Dictionary<string, char>();
            this.mPairCounter = new Dictionary<string, UInt64>();
            this.mPairToConstructedPair = new Dictionary<string, List<string>>();
            this.mCharCounter = new Dictionary<char, UInt64>();
            for (int lIndex = 0; lIndex < pInput.Count(); lIndex++)
            {
                if (lIndex == 0)
                {
                    lTemplate = pInput.ElementAt(lIndex);
                }
                else if (lIndex >= 2)
                {
                    string[] lSplit = pInput.ElementAt(lIndex).Replace(" -> ", ",").Split(',');
                    this.mPairMap.Add(lSplit[0], lSplit[1][0]);
                    this.mPairCounter.Add(lSplit[0], 0);
                    this.mPairToConstructedPair.Add(lSplit[0], new List<string>() { string.Join("", lSplit[0][0], lSplit[1]), string.Join("", lSplit[1], lSplit[0][1]) });
                }
            }
            this.IncrementCharCounter(lTemplate[0], 1);
            for (int lIndex = 1; lIndex < lTemplate.Count(); lIndex++)
            {
                this.mPairCounter[string.Join("", lTemplate[lIndex - 1], lTemplate[lIndex])] += 1;
                this.IncrementCharCounter(lTemplate[lIndex], 1);
            }
        }

        /// <summary>
        /// Runs a step.
        /// </summary>
        private void Step()
        {
            Dictionary<string, UInt64> lNewPairCount = this.mPairCounter.ToDictionary(pKvp => pKvp.Key, pKvp => (UInt64)0);
            foreach (KeyValuePair<string, UInt64> lKVP in this.mPairCounter)
            {
                this.mPairToConstructedPair[lKVP.Key].ForEach(pPair => lNewPairCount[pPair] += lKVP.Value);
                this.IncrementCharCounter(this.mPairMap[lKVP.Key], lKVP.Value);
            }
            this.mPairCounter = lNewPairCount;
        }

        /// <summary>
        /// Computes the substraction.
        /// </summary>
        /// <returns></returns>
        private UInt64 ComputeResult()
        {
            List<KeyValuePair<char, ulong>> lCharCounterAsList = this.mCharCounter.ToList();
            lCharCounterAsList.Sort((pKVP1, pKVP2) => pKVP1.Value.CompareTo(pKVP2.Value));
            return lCharCounterAsList.Last().Value - lCharCounterAsList.First().Value;
        }

        /// <summary>
        /// Computes the result according to a counter.
        /// </summary>
        /// <param name="pInput"></param>
        /// <param name="pCounter"></param>
        /// <returns></returns>
        private string Compute(IEnumerable<string> pInput, int pCounter)
        {
            this.InitializeData(pInput);
            for (int lCount = 0; lCount < pCounter; lCount++)
            {
                this.Step();
            }
            return this.ComputeResult().ToString();
        }

        /// <summary>
        /// Increments the char counter with the given value.
        /// </summary>
        /// <param name="pChar"></param>
        /// <param name="pValue"></param>
        private void IncrementCharCounter(char pChar, UInt64 pValue)
        {
            UInt64 lValue;
            if (this.mCharCounter.TryGetValue(pChar, out lValue))
            {
                this.mCharCounter[pChar] = lValue + pValue;
            }
            else
            {
                this.mCharCounter.Add(pChar, pValue);
            }
        }

        #endregion
    }
}
