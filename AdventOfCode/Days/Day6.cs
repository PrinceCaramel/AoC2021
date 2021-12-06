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
    public class Day6 : ADay
    {
        #region Fields

        /// <summary>
        /// Initializes the numbers of days until it reproduces.
        /// </summary>
        private int mReproductionDuration = 0;

        /// <summary>
        /// Initializes the numbers of days when born.
        /// </summary>
        private int mNewbornFishDuration = 0;


        /// <summary>
        /// Stores the population by day.
        /// </summary>
        private UInt64[] mPopulationByDays;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the path of the input.
        /// </summary>
        protected override string Path
        {
            get
            {
                return DataProvider.DAY6INPUTS_PATH;
            }
        }

        /// <summary>
        /// Gets the part1 function.
        /// </summary>
        protected override Func<IEnumerable<string>, string> Part1Function
        {
            get
            {
                return pInput => this.Compute(pInput, 80, 6, 8);
            }
        }

        /// <summary>
        /// Gets the part2 function.
        /// </summary>
        protected override Func<IEnumerable<string>, string> Part2Function
        {
            get
            {
                return pInput => this.Compute(pInput, 256, 6, 8);
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Day6"/> class.
        /// </summary>
        public Day6()
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Initializes the data.
        /// </summary>
        /// <param name="pInput"></param>
        /// <param name="pReproductionDuration"></param>
        /// <param name="pNewbornDuration"></param>
        private void InitializesData(string pInput, int pReproductionDuration, int pNewbornDuration)
        {
            this.mNewbornFishDuration = pNewbornDuration;
            this.mReproductionDuration = pReproductionDuration;
            this.mPopulationByDays = new UInt64[this.mNewbornFishDuration + 1];
            IEnumerable<int> lInitialFishPopulation = pInput.Split(',').Select(pValue => int.Parse(pValue)).ToList();
            foreach (int lFish in lInitialFishPopulation)
            {
                this.mPopulationByDays[lFish] = this.mPopulationByDays[lFish] + 1;
            }
        }

        /// <summary>
        /// Method used to simulation one day.
        /// </summary>
        private void Step()
        {
            UInt64[] lResult = new UInt64[this.mNewbornFishDuration + 1];
            for (int lIndex = 0; lIndex <= this.mNewbornFishDuration; lIndex++)
            {
                lResult[lIndex] = this.mPopulationByDays[(lIndex + 1) % (this.mNewbornFishDuration + 1)];
                if (lIndex == 6)
                {
                    lResult[lIndex] = lResult[lIndex] + this.mPopulationByDays[0];
                }
            }
            this.mPopulationByDays = lResult;
        }

        /// <summary>
        /// Computes the result.
        /// </summary>
        /// <param name="pInput"></param>
        /// <param name="pDays"></param>
        /// <param name="pReproductionDuration"></param>
        /// <param name="pNewbornDuration"></param>
        /// <returns></returns>
        private string Compute(IEnumerable<string> pInput, int pDays, int pReproductionDuration, int pNewbornDuration)
        {
            this.InitializesData(pInput.First(), pReproductionDuration, pNewbornDuration);
            for (int lCount = 0; lCount < pDays; lCount++)
            {
                this.Step();
            }
            return this.CountPopulation().ToString();
        }

        /// <summary>
        /// Method used to count the fish population.
        /// </summary>
        /// <returns></returns>
        private UInt64 CountPopulation()
        {
            //return this.mLanternfishPopulation.Count();
            UInt64 lResult = 0;
            this.mPopulationByDays.ForEach<UInt64>(pValue => lResult += pValue);
            return lResult;
        }

        #endregion
    }
}
