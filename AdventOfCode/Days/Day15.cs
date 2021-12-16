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

        /// <summary>
        /// Stores the nodes.
        /// </summary>
        private List<string> mNodes;

        /// <summary>
        /// Stores the distance of the node.
        /// </summary>
        private Dictionary<string, int> mDistances;

        /// <summary>
        /// The path.
        /// </summary>
        private Dictionary<string, string> mNodeToPredecessor;

        /// <summary>
        /// Stores the max column index.
        /// </summary>
        private int mMaxColIndex;

        /// <summary>
        /// Stores the max row index.
        /// </summary>
        private int mMaxRowIndex;

        #endregion Fields

        #region Properties

        /// <summary>
        /// A flag indicating whether we want to time the process or not.
        /// </summary>
        protected override bool ShouldTimeStamp => true;

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
                return pInput => this.Run(pInput, 5);
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
        private void InitializeData(IEnumerable<string> pInput, int pSize)
        {
            this.mGraphWithValue = new Dictionary<string, int>();
            this.mDistances = new Dictionary<string, int>();
            this.mNodes = new List<string>();
            int lMaxColInput = pInput.First().Count();
            int lMaxRowInput = pInput.Count();
            this.mMaxColIndex = (lMaxColInput * pSize) - 1;
            this.mMaxRowIndex =(lMaxRowInput * pSize) - 1;
            this.mNodeToPredecessor = new Dictionary<string, string>();
            for (int lY = 0; lY <= this.mMaxRowIndex; lY++)
            {
                string lLine = pInput.ElementAt(lY% lMaxRowInput);
                int lSizeY = lY / lMaxRowInput;
                for (int lX = 0; lX <= this.mMaxColIndex; lX++)
                {
                    int lSizeX = lX / lMaxColInput;
                    string lId = this.GetId(lX, lY);
                    int lValue = (int.Parse(lLine[lX% lMaxColInput].ToString()) + lSizeX + lSizeY);
                    if (lValue >= 10)
                    {
                        lValue -= 9;
                    }
                    this.mGraphWithValue.Add(lId, lValue);
                    this.mNodes.Add(lId);
                }
            }
            this.mDistances.Add(this.GetId(0,0), 0);
        }

        /// <summary>
        /// Find the minimum.
        /// </summary>
        /// <returns></returns>
        private string FindMinimum()
        {
            int lMinimum = int.MaxValue;
            string lResultNode = this.GetId(-1,-1);
            foreach (string lNode in this.mNodes)
            {
                int lValue;
                if (this.mDistances.TryGetValue(lNode, out lValue))
                {
                    if (lValue < lMinimum)
                    {
                        lMinimum = lValue;
                        lResultNode = lNode;
                    }
                }
            }
            return lResultNode;
        }

        /// <summary>
        /// Updates the distance.
        /// </summary>
        /// <param name="pNode1"></param>
        /// <param name="pNode2"></param>
        private void UpdateDistance(string pNode1, string pNode2)
        {
            int lDistanceNode1 = this.mDistances[pNode1];
            int lDistanceNode2;
            bool lExist = true;
            if (!this.mDistances.TryGetValue(pNode2, out lDistanceNode2))
            {
                lDistanceNode2 = int.MaxValue;
                lExist = false;
            }
            int lValue = this.mGraphWithValue[pNode1];
            if (lDistanceNode2 > (lDistanceNode1 + lValue))
            {
                if (lExist)
                {
                    this.mDistances[pNode2] = lDistanceNode1 + lValue;
                }
                else
                {
                    this.mDistances.Add(pNode2, lDistanceNode1 + lValue);
                }
                this.AddPredecessor(pNode2, pNode1);
            }
        }

        /// <summary>
        /// Runs the djikstra algorithm.
        /// </summary>
        private void Dijkstra()
        {
            while (this.mNodes.Any())
            {
                string lNode = this.FindMinimum();
                this.mNodes.Remove(lNode);
                List<string> lNeighbors = Utils.GetNeighbors(lNode, this.mMaxColIndex, this.mMaxRowIndex);
                lNeighbors.ForEach(pNb => this.UpdateDistance(lNode, pNb));
            }
        }

        /// <summary>
        /// Runs the algorithm and returns the value.
        /// </summary>
        /// <param name="pInput"></param>
        /// <param name="pSize"></param>
        /// <returns></returns>
        private string Run(IEnumerable<string> pInput, int pSize)
        {
            this.InitializeData(pInput, pSize);
            this.Dijkstra();
            return this.Result().ToString();
        }

        /// <summary>
        /// Adds a predecessor to the given node.
        /// </summary>
        /// <param name="pNode"></param>
        /// <param name="pPredecessor"></param>
        private void AddPredecessor(string pNode, string pPredecessor)
        {
            string lOutput;
            if (this.mNodeToPredecessor.TryGetValue(pNode, out lOutput))
            {
                this.mNodeToPredecessor[pNode] = pPredecessor;
            }
            else
            {
                this.mNodeToPredecessor.Add(pNode, pPredecessor);
            }
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <returns></returns>
        private int Result()
        {
            Stack<int> lPathValues = new Stack<int>();
            string lEndNode = this.GetId(this.mMaxColIndex, this.mMaxRowIndex);
            string lStartNode = this.GetId(0, 0);
            while (!lEndNode.Equals(lStartNode))
            {
                lPathValues.Push(this.mGraphWithValue[lEndNode]);
                lEndNode = this.mNodeToPredecessor[lEndNode];
            }
            return lPathValues.Sum();
        }

        /// <summary>
        /// Gets an id from a string.
        /// </summary>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <returns></returns>
        private string GetId(int pX, int pY)
        {
            return string.Format("{0},{1}", pX, pY);
        }

        #endregion
    }
}
