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
    public class Day21 : ADay
    {
        #region Fields

        private int mPlayer1Position;
        private int mPlayer2Position;
        private int mPlayer1Score;
        private int mPlayer2Score;
        private int mLastDeterministicDiceValue;
        private int mDeterministicRolls;
        private Dictionary<PosScore, UInt64> mPlayer1CurrentRound;
        private Dictionary<PosScore, UInt64> mPlayer2CurrentRound;
        private List<DicePossibility> mDiceValueToPossibilities;
        private UInt64 mPlayer1UniverseVictories = 0;
        private UInt64 mPlayer2UniverseVictories = 0;

        #endregion Fieldss

        #region Properties

        /// <summary>
        /// Gets the path of the input.
        /// </summary>
        protected override string Path
        {
            get
            {
                return DataProvider.DAY21INPUTS_PATH;
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
        /// Initializes a new instance of the <see cref="Day21"/> class.
        /// </summary>
        public Day21()
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Initializes the data.
        /// </summary>
        /// <param name="pInput"></param>
        private void InitializeDataPart1(IEnumerable<string> pInput)
        {
            this.mPlayer1Position = int.Parse(pInput.ElementAt(0).Split(' ').Last());
            this.mPlayer2Position = int.Parse(pInput.ElementAt(1).Split(' ').Last());
            this.mPlayer1Score = 0;
            this.mPlayer2Score = 0;
            this.mLastDeterministicDiceValue = 0;
            this.mDeterministicRolls = 0;
        }

        /// <summary>
        /// Initializes the data.
        /// </summary>
        /// <param name="pInput"></param>
        private void InitializeDataPart2(IEnumerable<string> pInput)
        {
            this.mPlayer1Position = int.Parse(pInput.ElementAt(0).Split(' ').Last());
            this.mPlayer2Position = int.Parse(pInput.ElementAt(1).Split(' ').Last());
            this.mPlayer1CurrentRound = new Dictionary<PosScore, UInt64>();
            this.mPlayer2CurrentRound = new Dictionary<PosScore, UInt64>();
            this.mDiceValueToPossibilities = new List<DicePossibility>();
            this.mDiceValueToPossibilities.Add(new DicePossibility(3,1));
            this.mDiceValueToPossibilities.Add(new DicePossibility(4,3));
            this.mDiceValueToPossibilities.Add(new DicePossibility(5,6));
            this.mDiceValueToPossibilities.Add(new DicePossibility(6,7));
            this.mDiceValueToPossibilities.Add(new DicePossibility(7,6));
            this.mDiceValueToPossibilities.Add(new DicePossibility(8,3));
            this.mDiceValueToPossibilities.Add(new DicePossibility(9,1));
            this.mPlayer1CurrentRound.Add(new PosScore(this.mPlayer1Position, 0), 1);
            this.mPlayer2CurrentRound.Add(new PosScore(this.mPlayer2Position, 0), 1);
        }

        /// <summary>
        /// Plays a turn. true if player 1, false if player 2.
        /// </summary>
        /// <param name="pIsPlayer1"></param>
        private void PlayTurn(bool pIsPlayer1)
        {
            Dictionary<PosScore, UInt64> lPreviousTurn = pIsPlayer1 ? this.mPlayer1CurrentRound : this.mPlayer2CurrentRound;
            Dictionary<PosScore, UInt64> lFutureTurn = new Dictionary<PosScore, UInt64>();
            foreach (KeyValuePair<PosScore, UInt64> lKPosScore in lPreviousTurn)
            {
                foreach (DicePossibility lPossibility in this.mDiceValueToPossibilities)
                {
                    int lNewPosition = this.GetNewPosition(lKPosScore.Key.Position, lPossibility.Roll);
                    PosScore lPosScore = new PosScore(lNewPosition, lKPosScore.Key.Score + lNewPosition);
                    UInt64 lValue;
                    if (lFutureTurn.TryGetValue(lPosScore, out lValue))
                    {
                        lFutureTurn[lPosScore] = lValue + lKPosScore.Value * lPossibility.PossibilityCount;
                    }
                    else
                    {
                        lFutureTurn.Add(lPosScore, lKPosScore.Value * lPossibility.PossibilityCount);
                    }
                }
            }

            if (pIsPlayer1)
            {
                this.mPlayer1CurrentRound = lFutureTurn;
                List<PosScore> lWinningPossibilities = this.mPlayer1CurrentRound.Select(pKVP => pKVP.Key).Where(pPS => pPS.Score >= 21).ToList();
                UInt64 lWinningUniverses = lWinningPossibilities.Aggregate((UInt64)0, (pAcc, pNext) => pAcc += this.mPlayer1CurrentRound[pNext], pAcc => pAcc);
                this.mPlayer1UniverseVictories += this.CountUniverses(!pIsPlayer1) * lWinningUniverses;
                lWinningPossibilities.ForEach(pPosScore => this.mPlayer1CurrentRound.Remove(pPosScore));
            }
            else
            {
                this.mPlayer2CurrentRound = lFutureTurn;
                List<PosScore> lWinningPossibilities = this.mPlayer2CurrentRound.Select(pKVP => pKVP.Key).Where(pPS => pPS.Score >= 21).ToList();
                UInt64 lWinningUniverses = lWinningPossibilities.Aggregate((UInt64)0, (pAcc, pNext) => pAcc += this.mPlayer2CurrentRound[pNext], pAcc => pAcc);
                this.mPlayer2UniverseVictories += this.CountUniverses(!pIsPlayer1) * lWinningUniverses;
                lWinningPossibilities.ForEach(pPosScore => this.mPlayer2CurrentRound.Remove(pPosScore));
            }
        }

        /// <summary>
        /// Computes part 2
        /// </summary>
        /// <param name="pInput"></param>
        /// <returns></returns>
        private string ComputePart2(IEnumerable<string> pInput)
        {
            this.InitializeDataPart2(pInput);
            while (this.mPlayer1CurrentRound.Any() || this.mPlayer2CurrentRound.Any())
            {
                this.PlayTurn(true);
                this.PlayTurn(false);
            }
            return Math.Max(this.mPlayer1UniverseVictories, this.mPlayer2UniverseVictories).ToString();
        }

        /// <summary>
        /// Counts the universes in a current player turn.
        /// </summary>
        /// <param name="pPlayerTurn"></param>
        /// <returns></returns>
        private UInt64 CountUniverses(bool pIsPlayer1)
        {
            Dictionary<PosScore, UInt64> lPlayerTurn = pIsPlayer1 ? this.mPlayer1CurrentRound : this.mPlayer2CurrentRound;
            return lPlayerTurn.Values.Aggregate((UInt64)0, (pAcc, pNext) => pAcc += (UInt64)pNext, pAcc => pAcc);
        }

        /// <summary>
        /// Returns the new position out of an initial position and a roll value.
        /// </summary>
        /// <param name="pInitialPosition"></param>
        /// <param name="pRollValue"></param>
        /// <returns></returns>
        private int GetNewPosition(int pInitialPosition, int pRollValue)
        {
            int lNewPosition = (pInitialPosition + pRollValue) % 10;
            if (lNewPosition == 0)
            {
                lNewPosition = 10;
            }
            return lNewPosition;
        }

        #region Part1

        /// <summary>
        /// Computes part1.
        /// </summary>
        /// <param name="pInput"></param>
        /// <returns></returns>
        private string ComputePart1(IEnumerable<string> pInput)
        {
            this.InitializeDataPart1(pInput);
            bool lIsOver = false;
            while (!lIsOver)
            {
                lIsOver = this.StepPart1();
            }
            return ((this.mPlayer1Score >= 1000 ? this.mPlayer2Score : this.mPlayer1Score) * this.mDeterministicRolls).ToString();
        }

        /// <summary>
        /// Step.
        /// </summary>
        /// <returns></returns>
        private bool StepPart1()
        {
            bool lIsOver = false;
            this.mPlayer1Position = (this.mPlayer1Position + this.RollDetermministicDice(3)) % 10;
            this.mPlayer1Score += this.mPlayer1Position == 0 ? 10 : this.mPlayer1Position;
            lIsOver = this.mPlayer1Score >= 1000;
            if (!lIsOver)
            {
                this.mPlayer2Position = (this.mPlayer2Position + this.RollDetermministicDice(3)) % 10;
                this.mPlayer2Score += this.mPlayer2Position == 0 ? 10 : this.mPlayer2Position;
                lIsOver = this.mPlayer2Score >= 1000;
            }
            return lIsOver;
        }

        /// <summary>
        /// Rolls dice.
        /// </summary>
        /// <param name="pTime"></param>
        /// <returns></returns>
        private int RollDetermministicDice(int pTime)
        {
            int lSum = 0;
            for (int lRoll = 0; lRoll < pTime; lRoll++)
            {
                this.mLastDeterministicDiceValue++;
                if (this.mLastDeterministicDiceValue == 101)
                {
                    this.mLastDeterministicDiceValue = 1;
                }
                lSum += this.mLastDeterministicDiceValue;
            }
            this.mDeterministicRolls += pTime;
            return lSum;
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// The dice possibilities.
    /// </summary>
    public struct DicePossibility
    {
        /// <summary>
        /// roll
        /// </summary>
        public int Roll
        {
            get;
        }

        /// <summary>
        /// possibility count
        /// </summary>
        public UInt64 PossibilityCount
        {
            get;
        }

        /// <summary>
        /// Possibilities.
        /// </summary>
        /// <param name="pRoll"></param>
        /// <param name="pPossibilityCount"></param>
        public DicePossibility(int pRoll, UInt64 pPossibilityCount)
        {
            this.Roll = pRoll;
            this.PossibilityCount = pPossibilityCount;
        }

        /// <summary>
        /// To strnig.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("R:{0} PC:{1}", this.Roll, this.PossibilityCount);
        }
    }

    /// <summary>
    /// Struct for a coord.
    /// </summary>
    public struct PosScore
    {
        /// <summary>
        /// position on the board.
        /// </summary>
        public int Position
        {
            get;
        }

        /// <summary>
        /// Score
        /// </summary>
        public int Score
        {
            get;
        }

        /// <summary>
        /// Instantiate a new coord.
        /// </summary>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        public PosScore(int pPosition, int pScore)
        {
            this.Position = pPosition;
            this.Score = pScore;
        }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="pPosScore"></param>
        /// <returns></returns>
        public override bool Equals(object pPosScore)
        {
            if (pPosScore is PosScore lPosScore)
            {
                return this.Position == lPosScore.Position && this.Score == lPosScore.Score;
            }
            return false;
        }

        /// <summary>
        /// To strnig.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("P:{0} S:{1}", this.Position, this.Score);
        }
    }
}
