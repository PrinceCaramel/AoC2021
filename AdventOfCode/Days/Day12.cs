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
    public class Day12 : ADay
    {
        #region Fields

        /// <summary>
        /// Stores the start cave.
        /// </summary>
        private const string START = "start";

        /// <summary>
        /// Stores the end cave.
        /// </summary>
        private const string END = "end";

        /// <summary>
        /// Stores the graph.
        /// </summary>
        private Dictionary<string, List<string>> mGraph;

        /// <summary>
        /// Stores the pathes.
        /// </summary>
        private List<List<string>> mPathes;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the path of the input.
        /// </summary>
        protected override string Path
        {
            get
            {
                return DataProvider.DAY12INPUTS_PATH;
            }
        }

        /// <summary>
        /// Gets the part1 function.
        /// </summary>
        protected override Func<IEnumerable<string>, string> Part1Function
        {
            get
            {
                return pInput => this.Compute(pInput, false);
            }
        }

        /// <summary>
        /// Gets the part2 function.
        /// </summary>
        protected override Func<IEnumerable<string>, string> Part2Function
        {
            get
            {
                return pInput => this.Compute(pInput, true);
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Day12"/> class.
        /// </summary>
        public Day12()
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
            this.mGraph = new Dictionary<string, List<string>>();
            this.mPathes = new List<List<string>>();
            foreach(string lLine in pInput)
            {
                string[] lSplit = lLine.Split('-');
                this.HandleLink(lSplit[0], lSplit[1]);
                this.HandleLink(lSplit[1], lSplit[0]);
            }
        }

        /// <summary>
        /// Adds a link to the graph if needed.
        /// </summary>
        /// <param name="pFirst"></param>
        /// <param name="pSecond"></param>
        private void HandleLink(string pFirst, string pSecond)
        {
            List<string> lValues;
            if (this.mGraph.TryGetValue(pFirst, out lValues))
            {
                lValues.Add(pSecond);
            }
            else
            {
                this.mGraph.Add(pFirst, new List<string>() { pSecond });
            }
        }

        /// <summary>
        /// Returns true if a cave is a big one.
        /// </summary>
        /// <param name="pCave"></param>
        /// <returns></returns>
        private bool IsBigCave(string pCave)
        {
            if (pCave.Equals(Day12.START) || pCave.Equals(Day12.END))
            {
                return false;
            }
            return pCave.ToUpper().Equals(pCave);
        }

        /// <summary>
        /// Computes the solution.
        /// </summary>
        /// <param name="pInput"></param>
        /// <param name="pCanVisitSmallCavesTwice"></param>
        /// <returns></returns>
        private string Compute(IEnumerable<string> pInput, bool pCanVisitSmallCavesTwice)
        {
            this.InitializeData(pInput);
            this.ComputePathes(new List<string>(), Day12.START, pCanVisitSmallCavesTwice);
            return this.mPathes.Count().ToString();
        }

        /// <summary>
        /// Computes the pathes.
        /// </summary>
        /// <param name="pCurrentPath"></param>
        /// <param name="pCurrentCave"></param>
        /// <param name="pCanVisitSmallCave"></param>
        private void ComputePathes(List<string> pCurrentPath, string pCurrentCave, bool pCanVisitSmallCave)
        {
            if (pCurrentCave.Equals(Day12.END))
            {
                pCurrentPath.Add(pCurrentCave);
                this.mPathes.Add(pCurrentPath);
            }
            else
            {
                if (this.CanCaveBeAddedInPath(pCurrentCave, pCurrentPath, pCanVisitSmallCave))
                {
                    List<string> lNewPath = pCurrentPath.ToList();
                    lNewPath.Add(pCurrentCave);
                    List<string> lPossibleCaves = this.mGraph[pCurrentCave];
                    foreach (string lCave in lPossibleCaves)
                    {
                        this.ComputePathes(lNewPath, lCave, pCanVisitSmallCave);
                    }
                }
            }
        }

        /// <summary>
        /// Can a cave be added in given path.
        /// </summary>
        /// <param name="pCave"></param>
        /// <param name="pPath"></param>
        /// <returns></returns>
        private bool CanCaveBeAddedInPath(string pCave, List<string> pPath, bool pCanVisitSmallCave)
        {
            if (!this.IsBigCave(pCave))
            {
                if (!pCanVisitSmallCave)
                {
                    return !pPath.Contains(pCave);
                }
                else
                {
                    if (this.HasVisitedSmallCaveTwice(pPath) || pCave.Equals(Day12.START) || pCave.Equals(Day12.END))
                    {
                        return !pPath.Contains(pCave);
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Returns true if a path has visited a small cave twice.
        /// </summary>
        /// <param name="pPath"></param>
        /// <returns></returns>
        private bool HasVisitedSmallCaveTwice(List<string> pPath)
        {
            List<string> lPath = pPath.ToList();
            lPath.RemoveAll(pP => pP.Equals(Day12.START) || pP.Equals(Day12.END) || this.IsBigCave(pP));
            return lPath.Distinct().Count() != lPath.Count();
        }

        /// <summary>
        /// Prints a path.
        /// </summary>
        /// <param name="pPath"></param>
        private void PrintPath(List<string> pPath)
        {
            Console.WriteLine(string.Join(" -> ", pPath));
        }

        #endregion
    }
}
