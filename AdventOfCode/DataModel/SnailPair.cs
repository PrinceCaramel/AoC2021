using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.DataModel
{
    /// <summary>
    /// Class defining an element of a tree.
    /// </summary>
    public abstract class ATreeElement
    {
        #region Properties

        /// <summary>
        /// Returns the magnitude.
        /// </summary>
        public abstract int Magnitude
        {
            get;
        }

        /// <summary>
        /// Returns the root.
        /// </summary>
        public ATreeElement Root
        {
            get
            {
                ATreeElement lElement = this;
                while (lElement.Parent != null)
                {
                    lElement = lElement.Parent;
                }
                return lElement;
            }
        }

        /// <summary>
        /// Returns the parent.
        /// </summary>
        public ATreeElement Parent
        {
            get;
            set;
        }

        /// <summary>
        /// Returns true if is a number.
        /// </summary>
        public abstract bool IsNumber
        {
            get;
        }

        /// <summary>
        /// Gets the displayable value.
        /// </summary>
        public abstract string DisplayableValue
        {
            get;
        }

        /// <summary>
        /// The left node.
        /// </summary>
        public ATreeElement Left
        {
            get;
            protected set;
        }

        /// <summary>
        /// The right node.
        /// </summary>
        public ATreeElement Right
        {
            get;
            protected set;
        }

        /// <summary>
        /// Is left node of its parent ?
        /// </summary>
        public bool IsLeft
        {
            get
            {
                return (this.Parent != null) ? this.Parent.Left == this : false;
            }
        }

        /// <summary>
        /// Is right node of its parent?
        /// </summary>
        public bool IsRight
        {
            get
            {
                return (this.Parent != null) ? this.Parent.Right == this : false;
            }
        }

        /// <summary>
        /// Gets the depth.
        /// </summary>
        public int Depth
        {
            get
            {
                int lDepth = 0;
                ATreeElement lElement = this;
                while (lElement.Parent != null)
                {
                    lDepth++;
                    lElement = lElement.Parent;
                }
                return lDepth;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// To string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.DisplayableValue;
        }

        #endregion
    }

    /// <summary>
    /// This class defines a snailfish number.
    /// </summary>
    public class SnailPair : ATreeElement
    {
        #region Properties

        /// <summary>
        /// Is a number.
        /// </summary>
        public override bool IsNumber => false;

        /// <summary>
        /// Returns the magnitude.
        /// </summary>
        public override int Magnitude
        {
            get
            {
                return 3 * this.Left.Magnitude + 2 * this.Right.Magnitude;
            }
        }

        /// <summary>
        /// Gets the displayable value.
        /// </summary>
        public override string DisplayableValue
        {
            get
            {
                return string.Format("[{0},{1}]", this.Left.DisplayableValue, this.Right.DisplayableValue);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SnailNumber"/> class.
        /// </summary>
        public SnailPair()
        {
            this.Parent = null;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the left element.
        /// </summary>
        /// <param name="pElement"></param>
        public void SetLeft(ATreeElement pElement)
        {
            this.Left = pElement;
            this.Left.Parent = this;
        }

        /// <summary>
        /// Sets the right element.
        /// </summary>
        /// <param name="pElement"></param>
        public void SetRight(ATreeElement pElement)
        {
            this.Right = pElement;
            this.Right.Parent = this;
        }

        /// <summary>
        /// Reduces the pair.
        /// </summary>
        public void Reduce()
        {
            if (this.Parent != null)
            {
                return;
            }

            bool lCanReduce = true;
            while (lCanReduce)
            {
                SnailPair lPairToExplode = this.FindPairToExplode(this);
                if (lPairToExplode != null)
                {
                    this.Explode(lPairToExplode);
                }
                else
                {
                    SnailNumber lNumberToSplit = this.FindNumberToSplit(this);
                    if (lNumberToSplit != null)
                    {
                        this.Split(lNumberToSplit);
                    }
                    else
                    {
                        lCanReduce = false;
                    }
                }
            }
        }

        /// <summary>
        /// Split a number.
        /// </summary>
        /// <param name="pNumberToSplit"></param>
        private void Split(SnailNumber pNumberToSplit)
        {
            int lLeftValue = pNumberToSplit.Value / 2;
            int lRightValue = (int)Math.Ceiling((decimal)pNumberToSplit.Value / (decimal)2);
            SnailPair lNewPair = new SnailPair();
            SnailNumber lLeftNumber = new SnailNumber(lLeftValue);
            SnailNumber lRightNumber = new SnailNumber(lRightValue);
            lNewPair.SetLeft(lLeftNumber);
            lNewPair.SetRight(lRightNumber);
            if (pNumberToSplit.IsLeft)
            {
                (pNumberToSplit.Parent as SnailPair).SetLeft(lNewPair);
            }
            else
            {
                (pNumberToSplit.Parent as SnailPair).SetRight(lNewPair);
            }
        }

        /// <summary>
        /// Finds the number to split.
        /// </summary>
        /// <param name="pElement"></param>
        /// <returns></returns>
        private SnailNumber FindNumberToSplit(ATreeElement pElement)
        {
            SnailNumber lResult = null;
            if (pElement.IsNumber && (pElement as SnailNumber).Value >= 10)
            {
                lResult = pElement as SnailNumber;
            }
            else if (pElement.Left != null)
            {
                lResult = this.FindNumberToSplit(pElement.Left);
            }
            if (lResult == null && pElement.Right != null)
            {
                lResult = this.FindNumberToSplit(pElement.Right);
            }
            return lResult;
        }

        /// <summary>
        /// Finds the next pair to explode.
        /// </summary>
        /// <param name="pElement"></param>
        /// <returns></returns>
        private SnailPair FindPairToExplode(ATreeElement pElement)
        {
            SnailPair lResult = null;
            if (pElement.Depth == 4 && !pElement.IsNumber)
            {
                lResult = pElement as SnailPair;
            }
            else if (pElement.Left != null)
            {
                lResult = this.FindPairToExplode(pElement.Left);
            }

            if (lResult == null && pElement.Right != null)
            {
                lResult = this.FindPairToExplode(pElement.Right);
            }
            return lResult;
        }

        /// <summary>
        /// Explodes a pair.
        /// </summary>
        /// <param name="pPairToExplode"></param>
        private void Explode(SnailPair pPairToExplode)
        {
            SnailNumber lLeft = this.FindLeftSnailNumber(pPairToExplode);
            if (lLeft != null)
            {
                lLeft.Add((pPairToExplode.Left as SnailNumber).Value);
            }
            SnailNumber Right = this.FindRightSnailNumber(pPairToExplode);
            if (Right != null)
            {
                Right.Add((pPairToExplode.Right as SnailNumber).Value);
            }
            if (pPairToExplode.IsLeft)
            {
                (pPairToExplode.Parent as SnailPair).SetLeft(new SnailNumber(0));
            }
            else
            {
                (pPairToExplode.Parent as SnailPair).SetRight(new SnailNumber(0));
            }
        }

        /// <summary>
        /// Find the left number to add for an explosion.
        /// </summary>
        /// <param name="pTreeElement"></param>
        /// <returns></returns>
        private SnailNumber FindLeftSnailNumber(ATreeElement pTreeElement)
        {
            SnailNumber lResult = null;
            ATreeElement lElement = pTreeElement;
            while (lElement.Root != lElement && lElement.IsLeft)
            {
                lElement = lElement.Parent;
            }
            if (lElement.IsRight)
            {
                lElement = lElement.Parent.Left;
                while (!lElement.IsNumber)
                {
                    lElement = lElement.Right;
                }
                lResult = lElement as SnailNumber;
            }
            return lResult;
        }

        /// <summary>
        /// Find the right number to add for an explosion.
        /// </summary>
        /// <param name="pTreeElement"></param>
        /// <returns></returns>
        private SnailNumber FindRightSnailNumber(ATreeElement pTreeElement)
        {
            SnailNumber lResult = null;
            ATreeElement lElement = pTreeElement;
            while (lElement.Root != lElement && lElement.IsRight)
            {
                lElement = lElement.Parent;
            }
            if (lElement.IsLeft)
            {
                lElement = lElement.Parent.Right;
                while (!lElement.IsNumber)
                {
                    lElement = lElement.Left;
                }
                lResult = lElement as SnailNumber;
            }
            return lResult;
        }

        #endregion
    }

    /// <summary>
    /// Class the defines a snailnumber.
    /// </summary>
    public class SnailNumber : ATreeElement
    {
        #region Properties

        /// <summary>
        /// Returns the magnitude.
        /// </summary>
        public override int Magnitude => this.Value;

        /// <summary>
        /// Is number ?
        /// </summary>
        public override bool IsNumber => true;

        /// <summary>
        /// Gets the value.
        /// </summary>
        public int Value
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the displayable value.
        /// </summary>
        public override string DisplayableValue
        {
            get
            {
                return this.Value.ToString();
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SnailNumber"/> class.
        /// </summary>
        /// <param name="pValue"></param>
        public SnailNumber(int pValue)
        {
            this.Value = pValue;
            this.Parent = null;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a value.
        /// </summary>
        /// <param name="pValue"></param>
        public void Add(int pValue)
        {
            this.Value += pValue;
        }

        #endregion
    }
}
