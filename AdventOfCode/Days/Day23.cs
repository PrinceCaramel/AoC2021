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
    public class Day23 : ADay
    {
        #region Fields

        private State mInitialState;
        private Dictionary<State, int> mMapToMin;
        private int mBest;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the path of the input.
        /// </summary>
        protected override string Path
        {
            get
            {
                return DataProvider.DAY23TESTINPUTS_PATH;
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
                return pInput => pInput.First();
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Day23"/> class.
        /// </summary>
        public Day23()
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
            this.mMapToMin = new Dictionary<State, int>();
            this.mBest = int.MaxValue;
            List<string> lInput = pInput.ToList();
            lInput.Pop<string>();
            lInput.Pop<string>();
            lInput.Remove(lInput.Last());
            string lRoomA = string.Empty;
            string lRoomB = string.Empty;
            string lRoomC = string.Empty;
            string lRoomD = string.Empty;
            foreach (string lLine in lInput)
            {
                string[] lSplit = lLine.Split(new char[] { '#', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                lRoomA += lSplit[0];
                lRoomB += lSplit[1];
                lRoomC += lSplit[2];
                lRoomD += lSplit[3];
            }
            this.mInitialState = new State(lRoomA, lRoomB, lRoomC, lRoomD, "XXXXXXXXXXX");
        }

        private string ComputePart1(IEnumerable<string> pInput)
        {
            this.InitializeData(pInput);

            this.CheckNextState(this.mInitialState, 0);

            
            return string.Empty;
        }

        private void CheckNextState(State pState, int pValue)
        {
            List<StateScore> lNextStates = pState.GetPossibleNextStates(pValue);
            IEnumerable<StateScore> lWinningStates = lNextStates.Where(pSS => pSS.State.IsWinning);
            foreach (StateScore lWinningState in lWinningStates)
            {
                this.mBest = Math.Min(this.mBest, lWinningState.Score);
            }
            lNextStates.RemoveAll(pSV => pSV.State.IsWinning || pSV.Score > 30000 || ((this.mMapToMin.ContainsKey(pSV.State) && this.mMapToMin[pSV.State] <= pSV.Score)));


            foreach (StateScore lNextState in lNextStates)
            {
                int lValue;
                if (this.mMapToMin.TryGetValue(lNextState.State, out lValue))
                {
                    this.mMapToMin[lNextState.State] = Math.Min(lNextState.Score, lValue);
                }
                else
                {
                    this.mMapToMin.Add(lNextState.State, lNextState.Score);
                }
            }
            lNextStates = lNextStates.OrderBy(pSS => pSS.Score).ToList();
            if (lNextStates.Any())
            {
                foreach (StateScore lNextState in lNextStates)
                {
                    this.CheckNextState(lNextState.State, lNextState.Score);
                }
            }
            //else
            //{
            //    Console.WriteLine("pipi");
            //}

        }


        #endregion
    }

    public struct StateScore
    {
        public State State { get; }
        public int Score { get; }
        public StateScore(State pState, int pScore)
        {
            this.State = pState;
            this.Score = pScore;
        }
        public override string ToString()
        {
            return string.Format("{0}    Val:{1}", this.State.ToString(), this.Score);
        }
    }

    public struct State
    {
        public string RoomA { get; }
        public string RoomB { get; }
        public string RoomC { get; }
        public string RoomD { get; }
        public string H { get; }

        public bool IsWinning { get { return RoomA.Equals("AA") && RoomB.Equals("BB") && RoomC.Equals("CC") && RoomD.Equals("DD"); } }

        public State(string pRoomA, string pRoomB, string pRoomC, string pRoomD, string pH)
        {
            this.RoomA = pRoomA;
            this.RoomB = pRoomB;
            this.RoomC = pRoomC;
            this.RoomD = pRoomD;
            this.H = pH;
        }

        private List<int> PossibleSpotForH
        {
            get { return new List<int>() { 0, 1, 3, 5, 7, 9, 10 }; }
        }

        private static int Length = 2;

        public List<StateScore> GetPossibleNextStates(int pValue)
        {
            List<StateScore> lResult = new List<StateScore>();

            lResult.AddRange(this.GetPossibleStatesForRoom("A", pValue));
            lResult.AddRange(this.GetPossibleStatesForRoom("B", pValue));
            lResult.AddRange(this.GetPossibleStatesForRoom("C", pValue));
            lResult.AddRange(this.GetPossibleStatesForRoom("D", pValue));
            lResult.AddRange(this.GetPossibleStatesForHallway(pValue));

            lResult = lResult.Distinct().ToList();

            List<StateScore> lResult2 = lResult.ToList();
            foreach (StateScore lR in lResult2)
            {
                IEnumerable<StateScore> lToRemove = lResult.Where(pSS => pSS.State.Equals(lR.State) && pSS.Score > lR.Score);
                lResult.RemoveAll(pC => lToRemove.Contains(pC));

            }


            return lResult;
        }

        private List<StateScore> GetPossibleStatesForHallway(int pValue)
        {
            List<StateScore> lResult = new List<StateScore>();

            for (int lIndex = 0; lIndex <= 10; lIndex++)
            {
                char lAmphipod = this.H[lIndex];
                if (!lAmphipod.Equals('X'))
                {
                    // GO RIGHT
                    int lMoveCounter = 0;
                    for (int lMoveIndex = lIndex + 1; lMoveIndex <= 10 ; lMoveIndex++)
                    {
                        lMoveCounter++;
                        if (this.PossibleSpotForH.Contains(lMoveIndex))
                        {
                            if (this.H[lMoveIndex].Equals('X'))
                            {
                                lResult.Add(new StateScore(new State(this.RoomA, this.RoomB, this.RoomC, this.RoomD, this.H.Remove(lIndex, 1).Insert(lIndex, "X").Remove(lMoveIndex,1).Insert(lMoveIndex, lAmphipod.ToString())), pValue + lMoveCounter * GetAmphipodCost(lAmphipod)));
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            if (this.CanGoInRoom(lAmphipod, lMoveIndex))
                            {
                                int lRoomLength = lMoveIndex == 2 ? this.RoomA.Length : (lMoveIndex == 4 ? this.RoomB.Length : (lMoveIndex == 6 ? this.RoomC.Length : this.RoomD.Length));

                                string lH = this.H.Remove(lIndex, 1).Insert(lIndex, "X");
                                string lRoomA = lMoveIndex == 2 ? this.RoomA.Insert(0, lAmphipod.ToString()) : this.RoomA;
                                string lRoomB = lMoveIndex == 4 ? this.RoomB.Insert(0, lAmphipod.ToString()) : this.RoomB;
                                string lRoomC = lMoveIndex == 6 ? this.RoomC.Insert(0, lAmphipod.ToString()) : this.RoomC;
                                string lRoomD = lMoveIndex == 8 ? this.RoomD.Insert(0, lAmphipod.ToString()) : this.RoomD;

                                lResult.Add(new StateScore(new State(lRoomA, lRoomB, lRoomC, lRoomD, lH), pValue + (lMoveCounter + Length - lRoomLength) * GetAmphipodCost(lAmphipod)));
                            }
                        }
                    }


                    // GO LEFT
                    lMoveCounter = 0;
                    for (int lMoveIndex = lIndex - 1; lMoveIndex >= 0; lMoveIndex--)
                    {
                        lMoveCounter++;
                        if (this.PossibleSpotForH.Contains(lMoveIndex))
                        {
                            if (this.H[lMoveIndex].Equals('X'))
                            {
                                lResult.Add(new StateScore(new State(this.RoomA, this.RoomB, this.RoomC, this.RoomD, this.H.Remove(lIndex, 1).Insert(lIndex, "X").Remove(lMoveIndex, 1).Insert(lMoveIndex, lAmphipod.ToString())), pValue + lMoveCounter * GetAmphipodCost(lAmphipod)));
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            if (this.CanGoInRoom(lAmphipod, lMoveIndex))
                            {
                                int lRoomLength = lMoveIndex == 2 ? this.RoomA.Length : (lMoveIndex == 4 ? this.RoomB.Length : (lMoveIndex == 6 ? this.RoomC.Length : this.RoomD.Length));

                                string lH = this.H.Remove(lIndex, 1).Insert(lIndex, "X");
                                string lRoomA = lMoveIndex == 2 ? this.RoomA.Insert(0, lAmphipod.ToString()) : this.RoomA;
                                string lRoomB = lMoveIndex == 4 ? this.RoomB.Insert(0, lAmphipod.ToString()) : this.RoomB;
                                string lRoomC = lMoveIndex == 6 ? this.RoomC.Insert(0, lAmphipod.ToString()) : this.RoomC;
                                string lRoomD = lMoveIndex == 8 ? this.RoomD.Insert(0, lAmphipod.ToString()) : this.RoomD;

                                lResult.Add(new StateScore(new State(lRoomA, lRoomB, lRoomC, lRoomD, lH), pValue + (lMoveCounter + Length - lRoomLength) * GetAmphipodCost(lAmphipod)));
                            }
                        }
                    }

                }
            }
            return lResult;
        }

        /// <summary>
        /// Can amphipod go in room under given index.
        /// </summary>
        /// <param name="pAmphipod"></param>
        /// <param name="pIndex"></param>
        /// <returns></returns>
        private bool CanGoInRoom(char pAmphipod, int pIndex)
        {
            if (pAmphipod.Equals('A'))
            {
                if (pIndex == 2)
                {
                    return this.RoomA.Length < Length && this.RoomA.All(pChar => pChar.Equals('A'));
                }
                return false;
            }
            if (pAmphipod.Equals('B'))
            {
                if (pIndex == 4)
                {
                    return this.RoomB.Length < Length && this.RoomB.All(pChar => pChar.Equals('B'));
                }
                return false;
            }
            if (pAmphipod.Equals('C'))
            {
                if (pIndex == 6)
                {
                    return this.RoomC.Length < Length && this.RoomC.All(pChar => pChar.Equals('C'));
                }
                return false;
            }
            if (pAmphipod.Equals('D'))
            {
                if (pIndex == 8)
                {
                    return this.RoomD.Length < Length && this.RoomD.All(pChar => pChar.Equals('D'));
                }
                return false;
            }
            return false;
        }

        /// <summary>
        /// Gets the possible future states for a move from the room.
        /// </summary>
        /// <param name="pRoom"></param>
        /// <returns></returns>
        private List<StateScore> GetPossibleStatesForRoom(string pRoom, int pValue)
        {
            List<StateScore> lResult = new List<StateScore>();

            string lRoooom = GetRoom(pRoom);
            if (string.IsNullOrEmpty(lRoooom))
            {
                return lResult;
            }
            char lTopAmphipod = lRoooom.FirstOrDefault();
            int lStartIndex = 2;
            if (pRoom.Equals("B"))
            {
                lStartIndex = 4;
            }
            else if (pRoom.Equals("C"))
            {
                lStartIndex = 6;
            }
            else if (pRoom.Equals("D"))
            {
                lStartIndex = 8;
            }
            List<KeyValuePair<string, int>> lPossibleH = new List<KeyValuePair<string, int>>();
            // Can go to left.
            if (this.H[lStartIndex - 1].Equals('X'))
            {
                int lMoveCounter = 1 + (GetRoom(pRoom).Length - Length);
                for (int lIndex = lStartIndex - 1; lIndex >= 0; lIndex--)
                {
                    lMoveCounter++;
                    if (this.PossibleSpotForH.Contains(lIndex))
                    {
                        if (this.H[lIndex].Equals('X'))
                        {
                            lPossibleH.Add(new KeyValuePair<string, int>(this.H.Remove(lIndex, 1).Insert(lIndex, lTopAmphipod.ToString()), lMoveCounter * GetAmphipodCost(lTopAmphipod)));
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            // Can go to right.
            if (this.H[lStartIndex + 1].Equals('X'))
            {
                int lMoveCounter = 1 + (GetRoom(pRoom).Length - Length);
                for (int lIndex = lStartIndex + 1; lIndex <= 10; lIndex++)
                {
                    lMoveCounter++;
                    if (this.PossibleSpotForH.Contains(lIndex))
                    {
                        if (this.H[lIndex].Equals('X'))
                        {
                            lPossibleH.Add(new KeyValuePair<string, int>(this.H.Remove(lIndex, 1).Insert(lIndex, lTopAmphipod.ToString()), lMoveCounter * GetAmphipodCost(lTopAmphipod)));
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            foreach (KeyValuePair<string, int> lHtoCost in lPossibleH)
            {
                string lH = lHtoCost.Key;
                string lRoomA = pRoom.Equals("A") ? this.RoomA.Remove(0, 1) : this.RoomA;
                string lRoomB = pRoom.Equals("B") ? this.RoomB.Remove(0, 1) : this.RoomB;
                string lRoomC = pRoom.Equals("C") ? this.RoomC.Remove(0, 1) : this.RoomC;
                string lRoomD = pRoom.Equals("D") ? this.RoomD.Remove(0, 1) : this.RoomD;
                lResult.Add(new StateScore(new State(lRoomA, lRoomB, lRoomC, lRoomD, lH), pValue + lHtoCost.Value));
            }

            return lResult;
        }



        /// <summary>
        /// Gets the amphipod cost.
        /// </summary>
        /// <param name="pAmphipod"></param>
        /// <returns></returns>
        private int GetAmphipodCost(char pAmphipod)
        {
            if (pAmphipod.Equals('A'))
            {
                return 1;
            }
            if (pAmphipod.Equals('B'))
            {
                return 10;
            }
            if (pAmphipod.Equals('C'))
            {
                return 100;
            }
            if (pAmphipod.Equals('D'))
            {
                return 1000;
            }
            return 0;
        }

        /// <summary>
        /// Gets the room according to an amphipod.
        /// </summary>
        /// <param name="pAmphipod"></param>
        /// <returns></returns>
        private string GetRoom(string pAmphipod)
        {
            if (pAmphipod.Equals("A"))
            {
                return this.RoomA;
            }
            if (pAmphipod.Equals("B"))
            {
                return this.RoomB;
            }
            if (pAmphipod.Equals("C"))
            {
                return this.RoomC;
            }
            if (pAmphipod.Equals("D"))
            {
                return this.RoomD;
            }
            return string.Empty;
        }

        public override string ToString()
        {
            StringBuilder lStrBuilder = new StringBuilder();
            lStrBuilder.AppendLine(this.H);
            lStrBuilder.AppendLine(string.Format("ROOM-A: {0}", this.RoomA));
            lStrBuilder.AppendLine(string.Format("ROOM-B: {0}", this.RoomB));
            lStrBuilder.AppendLine(string.Format("ROOM-C: {0}", this.RoomC));
            lStrBuilder.AppendLine(string.Format("ROOM-D: {0}", this.RoomD));
            return lStrBuilder.ToString();
        }
    }

}
