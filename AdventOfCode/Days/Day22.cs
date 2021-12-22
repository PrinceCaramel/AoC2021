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
    public class Day22 : ADay
    {
        #region Fields

        private List<Tuple<Cuboid, bool>> mInstructions;
        private Cuboid mInitialSpace;
        private List<Cuboid> mCubesOn;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the path of the input.
        /// </summary>
        protected override string Path
        {
            get
            {
                return DataProvider.DAY22INPUTS_PATH;
            }
        }

        /// <summary>
        /// Gets the part1 function.
        /// </summary>
        protected override Func<IEnumerable<string>, string> Part1Function
        {
            get
            {
                return pInput => this.Compute(pInput);
            }
        }

        /// <summary>
        /// Gets the part2 function.
        /// </summary>
        protected override Func<IEnumerable<string>, string> Part2Function
        {
            get
            {
                return pInput => this.Compute(pInput, false);
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Day22"/> class.
        /// </summary>
        public Day22()
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
            this.mInstructions = new List<Tuple<Cuboid, bool>>();
            this.mInitialSpace = new Cuboid(-50, -50, -50, 50, 50, 50);
            this.mCubesOn = new List<Cuboid>();
            foreach (string lLine in pInput)
            {
                bool lIsOn = lLine.Split(' ').First().Equals("on");
                string[] lSplit = lLine.Remove(0, lIsOn ? 3 : 4).Split(',').Select(pS => pS.Remove(0, 2)).ToArray();
                int lMinX = int.Parse(lSplit[0].Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).First());
                int lMinY = int.Parse(lSplit[1].Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).First());
                int lMinZ = int.Parse(lSplit[2].Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).First());
                int lMaxX = int.Parse(lSplit[0].Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Last());
                int lMaxY = int.Parse(lSplit[1].Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Last());
                int lMaxZ = int.Parse(lSplit[2].Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Last());
                Cuboid lCuboid = new Cuboid(lMinX, lMinY, lMinZ, lMaxX, lMaxY, lMaxZ);
                this.mInstructions.Add(new Tuple<Cuboid, bool>(lCuboid, lIsOn));
            }
        }

        /// <summary>
        /// Computes the solution.
        /// </summary>
        /// <param name="pInput"></param>
        /// <param name="pShouldConsiderInitialSpace"></param>
        /// <returns></returns>
        private string Compute(IEnumerable<string> pInput, bool pShouldConsiderInitialSpace = true)
        {
            this.InitializeData(pInput);
            List<Cuboid> lCache = new List<Cuboid>();
            foreach (Tuple<Cuboid, bool> lInstruction in this.mInstructions)
            {
                if (!pShouldConsiderInitialSpace || this.mInitialSpace.Contains(lInstruction.Item1))
                {
                    lCache.Clear();
                    foreach (Cuboid lCuboid in this.mCubesOn)
                    {
                        lCache.AddRange(lCuboid.Remove(lInstruction.Item1));
                    }
                    if (lInstruction.Item2)
                    {
                        lCache.Add(lInstruction.Item1);
                    }
                    this.mCubesOn = lCache.ToList();
                }
            }
            return this.TotalSize(this.mCubesOn).ToString();
        }

        /// <summary>
        /// Returns the total of cubes that are on.
        /// </summary>
        /// <param name="pCuboids"></param>
        /// <returns></returns>
        private UInt64 TotalSize(List<Cuboid> pCuboids)
        {
            return pCuboids.Aggregate((UInt64)0, (pAcc, pNext) => pAcc += pNext.Size, pAcc => pAcc);
        }

        #endregion
    }

    /// <summary>
    /// Struct defining a cuboid.
    /// </summary>
    public struct Cuboid
    {
        #region Properties

        public int MinX { get; }
        public int MinY { get; }
        public int MinZ { get; }
        public int MaxX { get; }
        public int MaxY { get; }
        public int MaxZ { get; }

        /// <summary>
        /// Returns true if it is valid.
        /// </summary>
        public bool IsValid { get { return this.MinX <= this.MaxX && this.MinY <= this.MaxY && this.MinZ <= this.MaxZ; } }

        /// <summary>
        /// Returns the size of the cuboid.
        /// </summary>
        public UInt64 Size
        {
            get
            {
                return (UInt64)(this.MaxX - this.MinX + 1) * (UInt64)(this.MaxY - this.MinY + 1) * (UInt64)(this.MaxZ - this.MinZ + 1);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Cuboid"/> struct.
        /// </summary>
        /// <param name="pMinX"></param>
        /// <param name="pMinY"></param>
        /// <param name="pMinZ"></param>
        /// <param name="pMaxX"></param>
        /// <param name="pMaxY"></param>
        /// <param name="pMaxZ"></param>
        public Cuboid(int pMinX, int pMinY, int pMinZ, int pMaxX, int pMaxY, int pMaxZ)
        {
            this.MinX = pMinX;
            this.MinY = pMinY;
            this.MinZ = pMinZ;
            this.MaxX = pMaxX;
            this.MaxY = pMaxY;
            this.MaxZ = pMaxZ;
        }

        #endregion

        /// <summary>
        /// Returns the cuboid that contains both of them.
        /// </summary>
        /// <param name="pCuboid"></param>
        /// <returns></returns>
        public Cuboid GetSpaceCuboid(Cuboid pCuboid)
        {
            return new Cuboid(Math.Min(this.MinX, pCuboid.MinX),
                Math.Min(this.MinY, pCuboid.MinY),
                Math.Min(this.MinZ, pCuboid.MinZ),
                Math.Max(this.MaxX, pCuboid.MaxX),
                Math.Max(this.MaxY, pCuboid.MaxY),
                Math.Max(this.MaxZ, pCuboid.MaxZ));
        }

        /// <summary>
        /// Returns true if the given cuboid is contained by this.
        /// </summary>
        /// <param name="pCuboid"></param>
        /// <returns></returns>
        public bool Contains(Cuboid pCuboid)
        {
            return pCuboid.MinX >= this.MinX &&
                pCuboid.MinY >= this.MinY &&
                pCuboid.MinZ >= this.MinZ &&
                pCuboid.MaxX <= this.MaxX &&
                pCuboid.MaxY <= this.MaxY &&
                pCuboid.MaxZ <= this.MaxZ;
        }

        /// <summary>
        /// Gets the intersection cuboid of two cuboids.
        /// </summary>
        /// <param name="pCuboid"></param>
        /// <returns></returns>
        public Cuboid GetIntersectionCuboid(Cuboid pCuboid)
        {
            int lMinX = Math.Max(this.MinX, pCuboid.MinX);
            int lMaxX = Math.Min(this.MaxX, pCuboid.MaxX);
            int lMinY = Math.Max(this.MinY, pCuboid.MinY);
            int lMaxY = Math.Min(this.MaxY, pCuboid.MaxY);
            int lMinZ = Math.Max(this.MinZ, pCuboid.MinZ);
            int lMaxZ = Math.Min(this.MaxZ, pCuboid.MaxZ);
            return new Cuboid(lMinX, lMinY, lMinZ, lMaxX, lMaxY, lMaxZ);
        }

        /// <summary>
        /// Removes the cuboid from this.
        /// </summary>
        /// <param name="pCuboid"></param>
        /// <returns></returns>
        public List<Cuboid> Remove(Cuboid pCuboid)
        {
            List<Cuboid> lResult = new List<Cuboid>();
            if (pCuboid.Contains(this))
            {
                return lResult;
            }
            Cuboid lSpaceCuboid = this.GetSpaceCuboid(pCuboid);
            Cuboid lIntersectio = this.GetIntersectionCuboid(pCuboid);
            if (lIntersectio.IsValid)
            {
                lResult = Cuboid.GetAll26CubesOutOfSpaceAndCuboid(lSpaceCuboid, lIntersectio);

                Cuboid lThis = this;
                lResult.RemoveAll(pCub => !pCub.IsValid || !lThis.Contains(pCub));
            }
            else
            {
                lResult.Add(this);
            }
            return lResult;
        }

        /// <summary>
        /// Gets all 26 cuboids out of a space and a cuboid contained by the space.
        /// </summary>
        /// <param name="pSpaceCuboid"></param>
        /// <param name="pWithinCuboid"></param>
        /// <returns></returns>
        public static List<Cuboid> GetAll26CubesOutOfSpaceAndCuboid(Cuboid pSpaceCuboid, Cuboid pWithinCuboid)
        {
            List<Cuboid> lResult = new List<Cuboid>();
            Cuboid lSpaceCuboid = pSpaceCuboid;
            Cuboid lIntersection = pWithinCuboid;
            lResult.Add(new Cuboid(lSpaceCuboid.MinX, lSpaceCuboid.MinY, lSpaceCuboid.MinZ, lIntersection.MinX - 1, lIntersection.MinY - 1, lIntersection.MinZ - 1));
            lResult.Add(new Cuboid(lIntersection.MinX, lSpaceCuboid.MinY, lSpaceCuboid.MinZ, lIntersection.MaxX, lIntersection.MinY - 1, lIntersection.MinZ - 1));
            lResult.Add(new Cuboid(lIntersection.MaxX + 1, lSpaceCuboid.MinY, lSpaceCuboid.MinZ, lSpaceCuboid.MaxX, lIntersection.MinY - 1, lIntersection.MinZ - 1));
            lResult.Add(new Cuboid(lSpaceCuboid.MinX, lIntersection.MinY, lSpaceCuboid.MinZ, lIntersection.MinX - 1, lIntersection.MaxY, lIntersection.MinZ - 1));
            lResult.Add(new Cuboid(lIntersection.MinX, lIntersection.MinY, lSpaceCuboid.MinZ, lIntersection.MaxX, lIntersection.MaxY, lIntersection.MinZ - 1));
            lResult.Add(new Cuboid(lIntersection.MaxX + 1, lIntersection.MinY, lSpaceCuboid.MinZ, lSpaceCuboid.MaxX, lIntersection.MaxY, lIntersection.MinZ - 1));
            lResult.Add(new Cuboid(lSpaceCuboid.MinX, lIntersection.MaxY + 1, lSpaceCuboid.MinZ, lIntersection.MinX - 1, lSpaceCuboid.MaxY, lIntersection.MinZ - 1));
            lResult.Add(new Cuboid(lIntersection.MinX, lIntersection.MaxY + 1, lSpaceCuboid.MinZ, lIntersection.MaxX, lSpaceCuboid.MaxY, lIntersection.MinZ - 1));
            lResult.Add(new Cuboid(lIntersection.MaxX + 1, lIntersection.MaxY + 1, lSpaceCuboid.MinZ, lSpaceCuboid.MaxX, lSpaceCuboid.MaxY, lIntersection.MinZ - 1));

            lResult.Add(new Cuboid(lSpaceCuboid.MinX, lSpaceCuboid.MinY, lIntersection.MinZ, lIntersection.MinX - 1, lIntersection.MinY - 1, lIntersection.MaxZ));
            lResult.Add(new Cuboid(lIntersection.MinX, lSpaceCuboid.MinY, lIntersection.MinZ, lIntersection.MaxX, lIntersection.MinY - 1, lIntersection.MaxZ));
            lResult.Add(new Cuboid(lIntersection.MaxX + 1, lSpaceCuboid.MinY, lIntersection.MinZ, lSpaceCuboid.MaxX, lIntersection.MinY - 1, lIntersection.MaxZ));
            lResult.Add(new Cuboid(lSpaceCuboid.MinX, lIntersection.MinY, lIntersection.MinZ, lIntersection.MinX - 1, lIntersection.MaxY, lIntersection.MaxZ));
            lResult.Add(new Cuboid(lIntersection.MaxX + 1, lIntersection.MinY, lIntersection.MinZ, lSpaceCuboid.MaxX, lIntersection.MaxY, lIntersection.MaxZ));
            lResult.Add(new Cuboid(lSpaceCuboid.MinX, lIntersection.MaxY + 1, lIntersection.MinZ, lIntersection.MinX - 1, lSpaceCuboid.MaxY, lIntersection.MaxZ));
            lResult.Add(new Cuboid(lIntersection.MinX, lIntersection.MaxY + 1, lIntersection.MinZ, lIntersection.MaxX, lSpaceCuboid.MaxY, lIntersection.MaxZ));
            lResult.Add(new Cuboid(lIntersection.MaxX + 1, lIntersection.MaxY + 1, lIntersection.MinZ, lSpaceCuboid.MaxX, lSpaceCuboid.MaxY, lIntersection.MaxZ));

            lResult.Add(new Cuboid(lSpaceCuboid.MinX, lSpaceCuboid.MinY, lIntersection.MaxZ + 1, lIntersection.MinX - 1, lIntersection.MinY - 1, lSpaceCuboid.MaxZ));
            lResult.Add(new Cuboid(lIntersection.MinX, lSpaceCuboid.MinY, lIntersection.MaxZ + 1, lIntersection.MaxX, lIntersection.MinY - 1, lSpaceCuboid.MaxZ));
            lResult.Add(new Cuboid(lIntersection.MaxX + 1, lSpaceCuboid.MinY, lIntersection.MaxZ + 1, lSpaceCuboid.MaxX, lIntersection.MinY - 1, lSpaceCuboid.MaxZ));
            lResult.Add(new Cuboid(lSpaceCuboid.MinX, lIntersection.MinY, lIntersection.MaxZ + 1, lIntersection.MinX - 1, lIntersection.MaxY, lSpaceCuboid.MaxZ));
            lResult.Add(new Cuboid(lIntersection.MinX, lIntersection.MinY, lIntersection.MaxZ + 1, lIntersection.MaxX, lIntersection.MaxY, lSpaceCuboid.MaxZ));
            lResult.Add(new Cuboid(lIntersection.MaxX + 1, lIntersection.MinY, lIntersection.MaxZ + 1, lSpaceCuboid.MaxX, lIntersection.MaxY, lSpaceCuboid.MaxZ));
            lResult.Add(new Cuboid(lSpaceCuboid.MinX, lIntersection.MaxY + 1, lIntersection.MaxZ + 1, lIntersection.MinX - 1, lSpaceCuboid.MaxY, lSpaceCuboid.MaxZ));
            lResult.Add(new Cuboid(lIntersection.MinX, lIntersection.MaxY + 1, lIntersection.MaxZ + 1, lIntersection.MaxX, lSpaceCuboid.MaxY, lSpaceCuboid.MaxZ));
            lResult.Add(new Cuboid(lIntersection.MaxX + 1, lIntersection.MaxY + 1, lIntersection.MaxZ + 1, lSpaceCuboid.MaxX, lSpaceCuboid.MaxY, lSpaceCuboid.MaxZ));

            return lResult;
        }
    }
}
