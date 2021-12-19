using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.DataModel
{
    /// <summary>
    /// Class defining a scanner with its detected beacons.
    /// </summary>
    public class Scanner
    {
        #region Fields

        /// <summary>
        /// Stores the scanner constant.
        /// </summary>
        private const string SCANNER = "Scanner";

        /// <summary>
        /// Stores the initial beacons.
        /// </summary>
        private List<Vector3> mInitialBeacons = new List<Vector3>();

        #endregion 

        #region Properties

        /// <summary>
        /// Stores the id of the scanner.
        /// </summary>
        public int Id
        {
            get;
            private set;
        }

        /// <summary>
        /// The list of beacons.
        /// </summary>
        public List<Vector3> Beacons
        {
            get
            {
                return this.mInitialBeacons;
            }
        }

        /// <summary>
        /// Relative vector from 0,0,0
        /// </summary>
        public Vector3 RelativeVectorFromZero
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Scanner"/> class.
        /// </summary>
        /// <param name="pLines"></param>
        public Scanner(IEnumerable<string> pLines)
        {
            this.InitializesScanner(pLines);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a scanner.
        /// </summary>
        /// <param name="pLines"></param>
        private void InitializesScanner(IEnumerable<string> pLines)
        {
            List<string> lLines = pLines.ToList();
            string lFirstLine = lLines.Pop<string>();
            this.Id = int.Parse(lFirstLine.Remove(0, 12).Split(' ').First());
            while (lLines.Any())
            {
                IEnumerable<int> lBeaconCoordinates = lLines.Pop<string>().Split(',').Select(pSplit => int.Parse(pSplit));
                Vector3 lBeacon = new Vector3(lBeaconCoordinates.ElementAt(0), lBeaconCoordinates.ElementAt(1), lBeaconCoordinates.ElementAt(2));
                this.mInitialBeacons.Add(lBeacon);
            }
        }

        /// <summary>
        /// Gives a list of rotated beacons.
        /// </summary>
        /// <param name="pRotationMatrix"></param>
        public List<Vector3> RotateAllBeacons(Matrix4x4 pRotationMatrix)
        {
            return this.mInitialBeacons.Select(pVect => Vector3.Transform(pVect, pRotationMatrix).Round()).ToList();
        }

        /// <summary>
        /// Returns true if the given scanner overlaps with this.
        /// </summary>
        /// <param name="pScanner"></param>
        /// <returns></returns>
        public bool AreOverlapping(Scanner pScanner)
        {
            return (this.GetRotatedBeacons(pScanner.Beacons, false).Any());
        }

        /// <summary>
        /// Gets the list of rotated beacons. Find the orientation of the scanner according to the given beacon list.
        /// </summary>
        /// <param name="pBeacons"></param>
        /// <returns></returns>
        public List<Vector3> GetRotatedBeacons(List<Vector3> pBeacons, bool pShouldComputeRelativeDistance = true)
        {
            Matrix4x4[] lRotationMatrices = Utils.RotationMatrices.ToArray();
            List<Vector3> lResult = new List<Vector3>();
            for (int lIndex = 0; lIndex < lRotationMatrices.Count(); lIndex++)
            {
                lResult = this.GetRotatedBeaconsIfOverlapWithGivenBeacons(pBeacons, lRotationMatrices[lIndex], pShouldComputeRelativeDistance);
                if (lResult.Any())
                {
                    break;
                }
            }
            return lResult;
        }

        /// <summary>
        /// returns the list of beacon coordinates if they overlapped with the given list.
        /// </summary>
        /// <param name="pScanner"></param>
        /// <returns></returns>
        private List<Vector3> GetRotatedBeaconsIfOverlapWithGivenBeacons(List<Vector3> pBeacons, Matrix4x4 pRotation, bool pShouldComputeRelativeDistance = true)
        {
            List<Vector3> lResult = new List<Vector3>();
            List<Vector3> lThisRotatedBeacons = this.RotateAllBeacons(pRotation);
            List<List<Vector3>> lVectorMatrix = new List<List<Vector3>>();
            for (int lScannerBeaconIndex = 0; lScannerBeaconIndex < pBeacons.Count(); lScannerBeaconIndex++)
            {
                List<Vector3> lToAdd = new List<Vector3>();
                for (int lThisBeaconIndex = 0; lThisBeaconIndex < this.Beacons.Count(); lThisBeaconIndex++)
                {
                    lToAdd.Add(Vector3.Subtract(pBeacons.ElementAt(lScannerBeaconIndex), lThisRotatedBeacons.ElementAt(lThisBeaconIndex)));
                }
                lVectorMatrix.Add(lToAdd);
            }

            Vector3? lRelativeVector = this.GetTranslationVector(lVectorMatrix);
            if (lRelativeVector != null)
            {
                lResult = lThisRotatedBeacons.Select(pVec => pVec + lRelativeVector.Value).ToList();
            }

            if (lResult.Any() && pShouldComputeRelativeDistance)
            {
                this.RelativeVectorFromZero = lRelativeVector.Value;
            }

            return lResult;
        }

        /// <summary>
        /// Gets the translation vector from a vector matrix, if it can be computed.
        /// Returns null if they don't overlap.
        /// </summary>
        /// <param name="pVectorMatrix"></param>
        /// <returns></returns>
        private Vector3? GetTranslationVector(List<List<Vector3>> pVectorMatrix)
        {
            Vector3? lTranslationVector = null;
            List<List<Vector3>> lMatrix = pVectorMatrix.ToList();
            bool lShouldStop = false;
            for (int lIndex = 0; lIndex < lMatrix.Count(); lIndex++)
            {
                List<Vector3> lLine = lMatrix[lIndex];
                int lVectorThatFitCount = 0;
                foreach (Vector3 lTempTranslationVector in lLine)
                {
                    for (int lJIndex = 0; lJIndex < lMatrix.Count(); lJIndex++)
                    {
                        if (lIndex != lJIndex)
                        {
                            if (lMatrix[lJIndex].Any(pVector => pVector == lTempTranslationVector))
                            {
                                lVectorThatFitCount++;
                            }
                        }
                    }
                    if (lVectorThatFitCount >= 11)
                    {
                        lShouldStop = true;
                        lTranslationVector = lTempTranslationVector;
                        break;
                    }
                }
                if (lShouldStop)
                {
                    break;
                }
            }
            return lTranslationVector;
        }

        /// <summary>
        /// Prints all beacons.
        /// </summary>
        public void PrintAllBeacons()
        {
            StringBuilder lStrBuilder = new StringBuilder();
            lStrBuilder.AppendLine(string.Format("{0} {1}", SCANNER, this.Id));
            this.Beacons.ForEach(pBeacon => lStrBuilder.AppendLine(pBeacon.Print()));
            Console.WriteLine(lStrBuilder.ToString());
        }

        #endregion
    }
}
