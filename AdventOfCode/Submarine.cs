using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    /// <summary>
    /// Class that defines the submarine.
    /// </summary>
    public class Submarine
    {
        #region Properties

        /// <summary>
        /// Gets the sonar of the submarine.
        /// </summary>
        public Sonar Sonar
        {
            get;
            private set;
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Submarine"/> class.
        /// </summary>
        public Submarine ()
        {
            this.Sonar = new Sonar();
        }

        #endregion Constructors
    }
}
