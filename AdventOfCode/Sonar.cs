using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    /// <summary>
    /// The sonar
    /// </summary>
    public class Sonar
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Sonar"/> class.
        /// </summary>
        public Sonar()
        {

        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets the measurements count that are larger than the previous one.
        /// </summary>
        /// <param name="pMeasurements"></param>
        /// <returns></returns>
        public int GetMeasurementsLargerThanPrevious(IEnumerable<int> pMeasurements)
        {
            int lCounter = 0;
            int lInputCount = pMeasurements.Count();
            if (pMeasurements == null || lInputCount <= 1)
            {
                return lCounter;
            }

            for (int lIndex = 1; lIndex < lInputCount; lIndex++)
            {
                if (pMeasurements.ElementAt(lIndex - 1) < pMeasurements.ElementAt(lIndex))
                {
                    lCounter++;
                }
            }

            return lCounter;
        }

        #endregion Methods
    }
}
