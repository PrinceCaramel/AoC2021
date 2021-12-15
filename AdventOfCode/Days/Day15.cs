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
    public class Day15 : ADay
    {
        #region Fields

        /// <summary>
        /// Stores the graph.
        /// </summary>
        private Dictionary<string, int> mGraphWithValue;
        private List<string> mNodes;
        private Dictionary<string, int> mDistance;
        private Dictionary<string, string> mPred = new Dictionary<string, string>();

        private int mMaxColIndex;
        private int mMaxRowIndex;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the path of the input.
        /// </summary>
        protected override string Path
        {
            get
            {
                return DataProvider.DAY15INPUTS_PATH;
            }
        }

        /// <summary>
        /// Gets the part1 function.
        /// </summary>
        protected override Func<IEnumerable<string>, string> Part1Function
        {
            get
            {
                return pInput => this.Run(pInput, 1);
            }
        }

        /// <summary>
        /// Gets the part2 function.
        /// </summary>
        protected override Func<IEnumerable<string>, string> Part2Function
        {
            get
            {
                return pInput => this.Run(pInput, 2);
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Day15"/> class.
        /// </summary>
        public Day15()
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
            this.mGraphWithValue = new Dictionary<string, int>();
            this.mDistance = new Dictionary<string, int>();
            this.mNodes = new List<string>();
            this.mMaxColIndex = pInput.First().Count() - 1;
            this.mMaxRowIndex = pInput.Count() - 1;
            this.mPred = new Dictionary<string, string>();
            for (int lY = 0; lY < pInput.Count(); lY++)
            {
                string lLine = pInput.ElementAt(lY);
                for (int lX = 0; lX < lLine.Count(); lX++)
                {
                    string lId = string.Format("{0},{1}", lX, lY);
                    this.mGraphWithValue.Add(lId, int.Parse(lLine[lX].ToString()));
                    this.mDistance.Add(lId, int.MaxValue);
                    this.mNodes.Add(lId);
                }
            }
            this.mDistance["0,0"] = 0;
        }

        /// <summary>
        /// Initializes the data.
        /// </summary>
        /// <param name="pInput"></param>
        private void InitializeDataPart2(IEnumerable<string> pInput, int pSize)
        {
            this.mGraphWithValue = new Dictionary<string, int>();
            this.mDistance = new Dictionary<string, int>();
            this.mNodes = new List<string>();
            int lMaxColInput = pInput.First().Count();
            int lMaxRowInput = pInput.Count();
            this.mMaxColIndex = (lMaxColInput * pSize) - 1;
            this.mMaxRowIndex =(lMaxRowInput * pSize) - 1;
            this.mPred = new Dictionary<string, string>();
            for (int lY = 0; lY < pInput.Count(); lY++)
            {
                string lLine = pInput.ElementAt(lY);
                for (int lX = 0; lX < lLine.Count(); lX++)
                {
                    for (int lSizeX = 0; lSizeX < pSize; lSizeX++)
                    {
                        for (int lSizeY = 0; lSizeY < pSize; lSizeY++)
                        {
                            string lId = string.Format("{0},{1}", lX + (lSizeX * lMaxColInput), lY + (lSizeY * lMaxRowInput));
                            int lValue = (int.Parse(lLine[lX].ToString()) + lSizeX + lSizeY);
                            if (lValue >= 10)
                            {
                                lValue -= 9;
                            }
                            this.mGraphWithValue.Add(lId, lValue);
                            this.mDistance.Add(lId, int.MaxValue);
                            this.mNodes.Add(lId);
                        }
                    }
                }
            }
            this.mDistance["0,0"] = 0;
        }

        private string FindMinimum()
        {
            int lMinimum = int.MaxValue;
            string lResultNode = "-1,-1";
            foreach (string lNode in this.mNodes)
            {
                if (this.mDistance[lNode] < lMinimum)
                {
                    lMinimum = this.mDistance[lNode];
                    lResultNode = lNode;
                }
            }
            return lResultNode;
        }

        private void UpdateDistance(string pNode1, string pNode2)
        {
            if (this.mDistance[pNode2] > (this.mDistance[pNode1] + this.mGraphWithValue[pNode1]))
            {
                this.mDistance[pNode2] = this.mDistance[pNode1] + this.mGraphWithValue[pNode1];
                this.AddPred(pNode2, pNode1);
            }
        }

        private void Djikstra()
        {
            while (this.mNodes.Any())
            {
                string lNode = this.FindMinimum();
                this.mNodes.Remove(lNode);
                Utils.GetNeighbors(lNode, this.mMaxColIndex, this.mMaxRowIndex).ForEach(pNb => this.UpdateDistance(lNode, pNb));
            }
        }

        private string Run(IEnumerable<string> pInput, int pInit)
        {
            if (pInit == 1)
            {
                this.InitializeDataPart1(pInput);
            }
            else
            {
                this.InitializeDataPart2(pInput, 5);
            }
            this.Djikstra();
            //return this.mDistance[string.Format("{0},{1}", this.mMaxColIndex, this.mMaxRowIndex)].ToString();
            return this.Result().ToString();
        }

        private void PrintMap()
        {
            StringBuilder lStr = new StringBuilder();
            for (int lY = 0; lY <= this.mMaxRowIndex; lY++)
            {
                for (int lX = 0; lX <= this.mMaxColIndex; lX++)
                {
                    lStr.Append(this.mGraphWithValue[string.Format("{0},{1}", lX, lY)]);
                }
                lStr.AppendLine();
            }
            Console.WriteLine(lStr.ToString());
        }



        private void Compate()
        {
            StringBuilder lStr = new StringBuilder();
            for (int lY = 0; lY <= this.mMaxRowIndex; lY++)
            {
                for (int lX = 0; lX <= this.mMaxColIndex; lX++)
                {
                    lStr.Append(this.mGraphWithValue[string.Format("{0},{1}", lX, lY)]);
                }
                lStr.AppendLine();
            }

            StringBuilder lStr2 = new StringBuilder();

            IEnumerable<string> lInput =  System.IO.File.ReadLines(Utils.GetFullPath(@"..\..\Data\Putpu.txt"));
            foreach (string lL in lInput)
            {
                lStr2.AppendLine(lL);
            }
            
            bool lC = lStr.ToString().Equals(lStr2.ToString());
            Console.WriteLine(lC);

        }

        private void AddPred(string pNode, string pPred)
        {
            string lOut;
            if (this.mPred.TryGetValue(pNode, out lOut))
            {
                this.mPred[pNode] = pPred;
            }
            else
            {
                this.mPred.Add(pNode, pPred);
            }
        }

        private int Result()
        {
            List<int> lResult = new List<int>();
            string lS = string.Format("{0},{1}", this.mMaxColIndex, this.mMaxRowIndex);
            string lSdeb = "0,0";
            while (!lS.Equals(lSdeb))
            {
                lResult.Insert(0, this.mGraphWithValue[lS]);
                lS = this.mPred[lS];
            }
            int lPopo = 0;
            lResult.ForEach(pL => lPopo += pL);
            return lPopo;
        }

        #endregion
    }
}
