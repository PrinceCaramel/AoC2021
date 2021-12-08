using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.DataModel
{
    /// <summary>
    /// Defines the segments of digit.
    /// </summary>
    [Flags]
    public enum Segments
    {
        None = 0,
        A = 1,
        B = 2,
        C = 4,
        D = 8,
        E = 16,
        F = 32,
        G = 64, 
        All = A|B|C|D|E|F|G,
    }

    /// <summary>
    /// Describes a digit signal pattern.
    /// </summary>
    public class DigitSignalPattern
    {
        #region Fields

        /// <summary>
        /// Stores the coded digits linked to the amount of segments.
        /// </summary>
        private Dictionary<int, List<Segments>> mCodedSegmentsByAmountOfSegments = new Dictionary<int, List<Segments>>();

        /// <summary>
        /// Stores the int by decoded segments.
        /// </summary>
        private Dictionary<int, Segments> mDecodedIntBySegments = new Dictionary<int, Segments>();

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the signal patterns.
        /// </summary>
        public List<string> SignalPatterns
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the output.
        /// </summary>
        public List<string> Output
        {
            get;
            private set;
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DigitSignalPattern"/> class.
        /// </summary>
        /// <param name="pLine"></param>
        public DigitSignalPattern(string pLine)
        {
            this.InitializesData(pLine);
            this.Decode();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Initializes the data.
        /// </summary>
        /// <param name="pLine"></param>
        private void InitializesData(string pLine)
        {
            this.SignalPatterns = new List<string>();
            this.Output = new List<string>();
            List<string> lSplit = pLine.Split(new char[] { ' ', '|' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            this.SignalPatterns.AddRange(lSplit.GetRange(0, 10));
            this.Output.AddRange(lSplit.GetRange(10, 4));

            this.mCodedSegmentsByAmountOfSegments = new Dictionary<int, List<Segments>>();
            for (int lIndex = 0; lIndex <= 7; lIndex++)
            {
                this.mCodedSegmentsByAmountOfSegments.Add(lIndex, this.SignalPatterns.Where(pElem => pElem.Count() == lIndex).Select(pString => this.StringToSegment(pString)).ToList());
            }
        }

        /// <summary>
        /// Count the sum of 1, 4, 7 and 9 in the output.
        /// </summary>
        /// <returns></returns>
        public int CountSumOf147And8InOutput()
        {
            return this.Output.Aggregate(0, (pAcc, pNext) => pAcc += (pNext.Count() == 2 || pNext.Count() == 3 || pNext.Count() == 4 || pNext.Count() == 7) ? 1 : 0 , pAcc => pAcc);
        }

        /// <summary>
        /// Computes the value 1.
        /// </summary>
        private void ComputeDigit1()
        {
            this.mDecodedIntBySegments.Add(1, this.mCodedSegmentsByAmountOfSegments[2].First());
        }

        /// <summary>
        /// Computes the value 7.
        /// </summary>
        private void ComputeDigit7()
        {
            this.mDecodedIntBySegments.Add(7, this.mCodedSegmentsByAmountOfSegments[3].First());
        }

        /// <summary>
        /// Computes the value 4.
        /// </summary>
        private void ComputeDigit4()
        {
            this.mDecodedIntBySegments.Add(4, this.mCodedSegmentsByAmountOfSegments[4].First());
        }

        /// <summary>
        /// Computes the value 8.
        /// </summary>
        private void ComputeDigit8()
        {
            this.mDecodedIntBySegments.Add(8, Segments.All);
        }

        /// <summary>
        /// Computes the value 3.
        /// </summary>
        private void ComputeDigit3()
        {
            this.mDecodedIntBySegments.Add(3, this.mCodedSegmentsByAmountOfSegments[5].First(pValue => pValue.HasFlag(this.mDecodedIntBySegments[1])));
        }

        /// <summary>
        /// Computes the value 6.
        /// </summary>
        private void ComputeDigit6()
        {
            this.mDecodedIntBySegments.Add(6, this.mCodedSegmentsByAmountOfSegments[6].First(pValue => pValue.HasFlag(this.mDecodedIntBySegments[1]) == false));
        }

        /// <summary>
        /// Computes the value 9.
        /// </summary>
        private void ComputeDigit9()
        {
            Segments l47 = this.mDecodedIntBySegments[4] | this.mDecodedIntBySegments[7];
            Segments l235 = this.mCodedSegmentsByAmountOfSegments[5].Aggregate(Segments.All, (pAcc, pNext) => pAcc &= pNext, pAcc => pAcc);
            this.mDecodedIntBySegments.Add(9, l47 | l235);
        }

        /// <summary>
        /// Computes the value 0.
        /// </summary>
        private void ComputeDigit0()
        {
            List<Segments> lSegments = this.mCodedSegmentsByAmountOfSegments[6].ToList();
            lSegments.Remove(this.mDecodedIntBySegments[6]);
            lSegments.Remove(this.mDecodedIntBySegments[9]);
            this.mDecodedIntBySegments.Add(0, lSegments.First());
        }

        /// <summary>
        /// Computes the value 5.
        /// </summary>
        private void ComputeDigit5()
        {
            this.mDecodedIntBySegments.Add(5, this.mCodedSegmentsByAmountOfSegments[5].First(pSeg => (pSeg | this.mDecodedIntBySegments[1]) == this.mDecodedIntBySegments[9]));
        }

        /// <summary>
        /// Computes the value 2.
        /// </summary>
        private void ComputeDigit2()
        {
            List<Segments> lSegments = this.mCodedSegmentsByAmountOfSegments[5].ToList();
            lSegments.Remove(this.mDecodedIntBySegments[3]);
            lSegments.Remove(this.mDecodedIntBySegments[5]);
            this.mDecodedIntBySegments.Add(2, lSegments.First());
        }

        /// <summary>
        /// Decodes the digits according to the signal pattern.
        /// </summary>
        private void Decode()
        {
            this.ComputeDigit1();
            this.ComputeDigit7();
            this.ComputeDigit4();
            this.ComputeDigit8();
            this.ComputeDigit3();
            this.ComputeDigit6();
            this.ComputeDigit9();
            this.ComputeDigit0();
            this.ComputeDigit5();
            this.ComputeDigit2();
        }

        /// <summary>
        /// Gets the value from a coded segment.
        /// </summary>
        /// <param name="pSegments"></param>
        /// <returns></returns>
        private int FromCodedDigitGetValue(Segments pSegments)
        {
            int lResult = -1;
            foreach (KeyValuePair<int, Segments> lKVP in this.mDecodedIntBySegments)
            {
                if (lKVP.Value == pSegments)
                {
                    lResult = lKVP.Key;
                    break;
                }
            }
            return lResult;
        }

        /// <summary>
        /// Decode the output as a int.
        /// </summary>
        /// <returns></returns>
        public int GetDecodedOutput()
        {
            return this.Output.Aggregate(0, (pResult, pDigit) => pResult = 10 * pResult + this.FromCodedDigitGetValue(this.StringToSegment(pDigit)), pResult => pResult);
        }

        /// <summary>
        /// Transforms a string into a segment.
        /// </summary>
        /// <param name="pString"></param>
        /// <returns></returns>
        private Segments StringToSegment(string pString)
        {
            Segments lResult = Segments.None;
            foreach (char lChar in pString)
            {
                if (lChar.Equals(Utils.A))
                    lResult = lResult | Segments.A;
                if (lChar.Equals(Utils.B))
                    lResult = lResult | Segments.B;
                if (lChar.Equals(Utils.C))
                    lResult = lResult | Segments.C;
                if (lChar.Equals(Utils.D))
                    lResult = lResult | Segments.D;
                if (lChar.Equals(Utils.E))
                    lResult = lResult | Segments.E;
                if (lChar.Equals(Utils.F))
                    lResult = lResult | Segments.F;
                if (lChar.Equals(Utils.G))
                    lResult = lResult | Segments.G;
            }
            return lResult;
        }

        #endregion Methods
    }
}
