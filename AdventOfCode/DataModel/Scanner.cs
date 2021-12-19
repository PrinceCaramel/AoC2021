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

        private const string SCANNER = "Scanner";
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




        public bool AreOverlapping(Scanner pScanner)
        {
            // for a given rotation.
            Matrix4x4[] lRotationMatrices = Utils.RotationMatrices.ToArray();
            bool lShare12Points = false;
            for (int lIndex = 0; lIndex < lRotationMatrices.Count(); lIndex++)
            {
                lShare12Points = this.Distance(pScanner, lRotationMatrices[lIndex]).Any();
                if (lShare12Points)
                {
                    break;
                }
            }
            return lShare12Points;
        }

        /// <summary>
        /// retuns true if this scanner share 12 points according to a given rotation.
        /// </summary>
        /// <param name="pScanner"></param>
        /// <returns></returns>
        private List<Vector3> Distance(Scanner pScanner, Matrix4x4 pRotation)
        {
            List<Vector3> lResult = new List<Vector3>();
            List<Vector3> lRotatedBeacons = pScanner.RotateAllBeacons(pRotation);
            List<List<Vector3>> lVectorMatrix = new List<List<Vector3>>();
            for (int lScannerBeaconIndex = 0; lScannerBeaconIndex < lRotatedBeacons.Count(); lScannerBeaconIndex++)
            {
                List<Vector3> lToAdd = new List<Vector3>();
                for (int lThisBeaconIndex = 0; lThisBeaconIndex < this.Beacons.Count(); lThisBeaconIndex++)
                {
                    lToAdd.Add(Vector3.Subtract(this.Beacons.ElementAt(lThisBeaconIndex), lRotatedBeacons.ElementAt(lScannerBeaconIndex)));
                }
                lVectorMatrix.Add(lToAdd);
            }

            Vector3? lRelateiveDistance = this.GetRelativeDistance(lVectorMatrix);
            if (lRelateiveDistance != null)
            {
                lResult = lRotatedBeacons.Select(pVec => pVec + lRelateiveDistance.Value).ToList();
            }

            return lResult;
        }

        private Vector3? GetRelativeDistance(List<List<Vector3>> pVectorMatrix)
        {
            Vector3? lResultatFinal = null;
            List<List<Vector3>> lMatrix = pVectorMatrix.ToList();
            bool lRes = false;
            for (int i = 0; i < lMatrix.Count(); i++)
            {
                List<Vector3> lLine = lMatrix[i];
                int lFits = 0;
                foreach (Vector3 lPos in lLine)
                {
                    bool lHas = false;
                    for (int j = 0; j < lMatrix.Count(); j++)
                    {
                        lHas = false;
                        if (i!=j)
                        {
                            lHas = lMatrix[j].Any(pV => pV == lPos);
                        }
                        if (lHas)
                        {
                            lFits++;
                        }
                    }
                    if (lFits >= 11)
                    {
                        lRes = true;
                        lResultatFinal = lPos;
                        break;
                    }
                    
                }
                if (lRes)
                {
                    break;
                }
            }
            return lResultatFinal;

            //return null;
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

    public struct VectorAndRotationMatrix
    {
        public Vector3 Vector;
        public Matrix4x4 Matrix;

        public VectorAndRotationMatrix(Vector3 pVector, Matrix4x4 pMatrix)
        {
            this.Vector = pVector;
            this.Matrix = pMatrix;
        }
    }
}
