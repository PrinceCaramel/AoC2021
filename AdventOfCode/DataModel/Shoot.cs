using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.DataModel
{
    /// <summary>
    /// Defines a shoot.
    /// </summary>
    public class Shoot
    {
        #region Fields

        /// <summary>
        /// Stores a flag indicating whether the shoot has reached 0.
        /// </summary>
        private bool mHasReached0;

        #endregion

        #region Properties

        /// <summary>
        /// Initial X speed
        /// </summary>
        public int InitialXSpeed
        {
            get;
            private set;
        }

        /// <summary>
        /// Initial Y speed
        /// </summary>
        public int InitialYSpeed
        {
            get;
            private set;
        }

        /// <summary>
        /// Current X Speed.
        /// </summary>
        public int CurrentXSpeed
        {
            get;
            private set;
        }

        /// <summary>
        /// Current Y SPeed.
        /// </summary>
        public int CurrentYSpeed
        {
            get;
            private set;
        }

        /// <summary>
        /// Current X pos.
        /// </summary>
        public int CurrentX
        {
            get;
            private set;
        }

        /// <summary>
        /// Current Y pos.
        /// </summary>
        public int CurrentY
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the max y it ever reached.
        /// </summary>
        public int MaxY
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the Y speed when the probe is under 0 for the first time.
        /// </summary>
        public int YSpeedWhenFirstTimeUnder0
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Shoot"/> class.
        /// </summary>
        /// <param name="pInitialXSpeed"></param>
        /// <param name="pInitialYSpeed"></param>
        public Shoot(int pInitialXSpeed, int pInitialYSpeed)
        {
            this.InitialXSpeed = pInitialXSpeed;
            this.InitialYSpeed = pInitialYSpeed;
            this.CurrentX = 0;
            this.CurrentY = 0;
            this.MaxY = this.CurrentY;
            this.CurrentXSpeed = this.InitialXSpeed;
            this.CurrentYSpeed = this.InitialYSpeed;
            this.mHasReached0 = false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Step.
        /// </summary>
        private void Step()
        {
            this.CurrentX += this.CurrentXSpeed;
            this.CurrentY += this.CurrentYSpeed;

            if (this.CurrentXSpeed > 0)
            {
                this.CurrentXSpeed--;
            }
            else if (this.CurrentXSpeed < 0)
            {
                this.CurrentXSpeed++;
            }

            this.CurrentYSpeed--;

            if (!this.mHasReached0)
            {
                if (this.CurrentY <= 0)
                {
                    this.mHasReached0 = true;
                    this.YSpeedWhenFirstTimeUnder0 = this.CurrentYSpeed;
                }
            }

            if (this.CurrentYSpeed == 0)
            {
                this.MaxY = this.CurrentY;
            }
        }

        /// <summary>
        /// Runs towards a target area.
        /// </summary>
        /// <param name="pTargetArea"></param>
        public void Run(TargetArea pTargetArea)
        {
            bool lIsInOrOut = false;
            while (!lIsInOrOut)
            {
                lIsInOrOut = this.IsInsideArea(pTargetArea) || this.ShouldStop(pTargetArea);
                if (!lIsInOrOut)
                {
                    this.Step();
                }
            }
        }

        /// <summary>
        /// Returns true if we should stop, as it will never reach the target area.
        /// </summary>
        /// <param name="pShoot"></param>
        /// <returns></returns>
        private bool ShouldStop(TargetArea pTargetArea)
        {
            return pTargetArea.MaxX < this.CurrentX || pTargetArea.MinY > this.CurrentY || (pTargetArea.MinX > this.CurrentX && this.CurrentXSpeed == 0);
        }

        /// <summary>
        /// Returns true if a shoot is inside the target area.
        /// </summary>
        /// <returns></returns>
        public bool IsInsideArea(TargetArea pTargetArea)
        {
            return pTargetArea.MinX <= this.CurrentX && pTargetArea.MaxX >= this.CurrentX && pTargetArea.MinY <= this.CurrentY && pTargetArea.MaxY >= this.CurrentY;
        }

        /// <summary>
        /// To string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("X: {0} |Y: {1}", this.InitialXSpeed, this.InitialYSpeed);
        }

        #endregion
    }

    /// <summary>
    /// Defines the target area.
    /// </summary>
    public struct TargetArea
    {
        /// <summary>
        /// Initializes a new instance of the target area.
        /// </summary>
        /// <param name="pMinX"></param>
        /// <param name="pMaxX"></param>
        /// <param name="pMinY"></param>
        /// <param name="pMaxY"></param>
        public TargetArea(int pMinX, int pMaxX, int pMinY, int pMaxY)
        {
            this.MinX = pMinX;
            this.MaxX = pMaxX;
            this.MinY = pMinY;
            this.MaxY = pMaxY;
        }

        /// <summary>
        /// Min X
        /// </summary>
        public int MinX
        {
            get;
        }

        /// <summary>
        /// Max X
        /// </summary>
        public int MaxX
        {
            get;
        }

        /// <summary>
        /// Min Y
        /// </summary>
        public int MinY
        {
            get;
        }

        /// <summary>
        /// Max Y
        /// </summary>
        public int MaxY
        {
            get;
        }
    }
}
