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
    public class Day17 : ADay
    {
        #region Fields

        /// <summary>
        /// Stores the target area.
        /// </summary>
        private TargetArea mTargetArea;

        /// <summary>
        /// Stores the shoots that goes inside the area.
        /// </summary>
        private List<Shoot> mGoodShoots = new List<Shoot>();

        /// <summary>
        /// Stores the maxY.
        /// </summary>
        private int mMaxY;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the path of the input.
        /// </summary>
        protected override string Path
        {
            get
            {
                return DataProvider.DAY17INPUTS_PATH;
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
        /// Initializes a new instance of the <see cref="Day17"/> class.
        /// </summary>
        public Day17()
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
            string lLine = pInput.First();
            lLine = lLine.Remove(0, 12);
            string[] lSplit = lLine.Split(',');
            string [] lXSplit = lSplit[0].Remove(0, 3).Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            string [] lYSplit = lSplit[1].Remove(0, 3).Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            this.mTargetArea = new TargetArea(int.Parse(lXSplit[0]), int.Parse(lXSplit[1]), int.Parse(lYSplit[0]), int.Parse(lYSplit[1]));
        }

        /// <summary>
        /// Computes the part 1.
        /// </summary>
        /// <param name="pInput"></param>
        /// <returns></returns>
        private string ComputePart1(IEnumerable<string> pInput)
        {
            this.InitializeData(pInput);
            this.RunPart1();
            this.mMaxY = this.mGoodShoots.Aggregate(0, (pAcc, pNext) => pAcc = Math.Max(pAcc, pNext.InitialYSpeed), pAcc => pAcc);
            return this.MaxHeightFromGoodShots().ToString();
        }

        /// <summary>
        /// Computes the part 1.
        /// </summary>
        /// <param name="pInput"></param>
        /// <returns></returns>
        private string ComputePart2(IEnumerable<string> pInput)
        {
            this.mGoodShoots.Clear();
            this.RunPart2();
            return this.mGoodShoots.Count().ToString();
        }

        /// <summary>
        /// Returns the max height from good shots.
        /// </summary>
        /// <returns></returns>
        private int MaxHeightFromGoodShots()
        {
            return this.mGoodShoots.Aggregate(0, (pAcc, pNext) => pAcc = Math.Max(pAcc, pNext.MaxY), pAcc => pAcc);
        }

        /// <summary>
        /// Runs part 1.
        /// </summary>
        private void RunPart1()
        {
            int lInitialXSpeed = 1;
            int lInitialYSpeed = 1;
            bool lMaxYReached = false;
            while (!lMaxYReached)
            {
                bool lXTooFar = false;
                lInitialXSpeed = 1;
                while (!lXTooFar)
                {
                    Shoot lShoot = new Shoot(lInitialXSpeed, lInitialYSpeed);
                    lShoot.Run(this.mTargetArea);
                    if (lShoot.IsInsideArea(this.mTargetArea))
                    {
                        this.mGoodShoots.Add(lShoot);
                    }
                    lInitialXSpeed++;
                    lMaxYReached = this.IsTooFarOnY(lShoot);
                    lXTooFar = this.IsTooFarOnX(lShoot) || lMaxYReached;
                }
                lInitialYSpeed++;
            }
        }

        /// <summary>
        /// Runs part 2.
        /// </summary>
        private void RunPart2()
        {
            for (int lX = 0; lX < this.mTargetArea.MaxX + 1; lX++)
            {
                for (int lY = this.mMaxY + 1; lY > this.mTargetArea.MinY - 1; lY--)
                {
                    Shoot lShoot = new Shoot(lX, lY);
                    lShoot.Run(this.mTargetArea);
                    if (lShoot.IsInsideArea(this.mTargetArea))
                    {
                        this.mGoodShoots.Add(lShoot);
                    }
                }
            }
        }

        /// <summary>
        /// Returns true if the shoot is too far on x.
        /// </summary>
        /// <param name="pShoot"></param>
        /// <returns></returns>
        private bool IsTooFarOnX(Shoot pShoot)
        {
            return pShoot.CurrentX > this.mTargetArea.MaxX && pShoot.CurrentY >= this.mTargetArea.MinY;
        }

        /// <summary>
        /// Returns true if the shoot is too far on Y.
        /// </summary>
        /// <param name="pShoot"></param>
        /// <returns></returns>
        private bool IsTooFarOnY(Shoot pShoot)
        {
            return Math.Abs(pShoot.YSpeedWhenFirstTimeUnder0) > Math.Abs(this.mTargetArea.MinY); 
        }

        #endregion
    }
}
