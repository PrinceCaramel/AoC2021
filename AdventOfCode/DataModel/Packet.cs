using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.DataModel
{
    public class Packet
    {
        #region Fields

        /// <summary>
        /// Stores the remaining bits.
        /// </summary>
        private string mRemainingBits;

        /// <summary>
        /// Stores the packet index used to remove data.
        /// </summary>
        private int mPacketIndex = 0;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the subpackets.
        /// </summary>
        public List<Packet> SubPackets
        {
            get;
            private set;
        }

        /// <summary>
        /// The packet version.
        /// </summary>
        public int Version
        {
            get;
            private set;
        }

        /// <summary>
        /// The packet type.
        /// </summary>
        public int Type
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns true if this packet is an operator.
        /// </summary>
        public bool IsOperator
        {
            get
            {
                return this.Type != 4;
            }
        }

        /// <summary>
        /// Returns the remaining packets.
        /// </summary>
        public string RemainingBits
        {
            get
            {
                return this.mRemainingBits;
            }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public int Length
        {
            get
            {
                return this.mPacketIndex + this.SubPackets.Select(pPacket => pPacket.Length).Sum();
            }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public Int64 Value
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the version value.
        /// </summary>
        public int VersionSum
        {
            get
            {
                return this.Version + this.SubPackets.Select(pPacket => pPacket.VersionSum).Sum();
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Packet"/> class.
        /// </summary>
        /// <param name="pLine"></param>
        public Packet(string pLine)
        {
            this.SubPackets = new List<Packet>();
            this.ExtractVersionType(pLine);
            this.Process();
            this.SetValue();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Extract the version and type from a line.
        /// </summary>
        /// <param name="pLine"></param>
        /// <returns></returns>
        private void ExtractVersionType(string pLine)
        {
            this.Version = Convert.ToInt32(pLine.Substring(0, 3), 2);
            this.Type = Convert.ToInt32(pLine.Substring(3, 3), 2);
            this.mPacketIndex = 6;
            this.mRemainingBits = pLine;
        }

        /// <summary>
        /// Process the reading of the packets.
        /// </summary>
        private void Process()
        {
            if (!this.IsOperator)
            {
                this.ProcessLiteral();
            }
            else
            {
                this.ProcessOperator();
            }
        }

        /// <summary>
        /// Process the packet if it is an operator.
        /// </summary>
        private void ProcessOperator()
        {
            char lLengthTypeId = this.mRemainingBits.ElementAt(this.mPacketIndex);
            this.mPacketIndex++;
            if (lLengthTypeId.Equals('0'))
            {
                this.Process0Operator();
            }
            else
            {
                this.Process1Operator();
            }
        }

        /// <summary>
        /// Process packet if is operator and lengthtypeid is 1.
        /// </summary>
        private void Process1Operator()
        {
            string l15bits = this.mRemainingBits.Substring(this.mPacketIndex, 11);
            this.mPacketIndex += 11;
            int lSubPackets = Convert.ToInt32(l15bits, 2);
            string lBitsToProcess = this.mRemainingBits.Remove(0 , this.mPacketIndex);
            while (this.SubPackets.Count() != lSubPackets)
            {
                Packet lSubPacket = new Packet(lBitsToProcess);
                this.SubPackets.Add(lSubPacket);
                lBitsToProcess = lSubPacket.RemainingBits;
            }
            this.mRemainingBits = this.mRemainingBits.Remove(0, this.mPacketIndex + this.SubPackets.Select(pPacket => pPacket.Length).Sum());
        }

        /// <summary>
        /// Process packet if is operator and lengthtypeid is 0.
        /// </summary>
        private void Process0Operator()
        {
            string l15bits = this.mRemainingBits.Substring(this.mPacketIndex, 15);
            this.mPacketIndex += 15;
            int lNumberOfBits = Convert.ToInt32(l15bits, 2);
            string lBitsToProcess = this.mRemainingBits.Substring(this.mPacketIndex, lNumberOfBits);
            while (!string.IsNullOrEmpty(lBitsToProcess))
            {
                Packet lSubPacket = new Packet(lBitsToProcess);
                this.SubPackets.Add(lSubPacket);
                lBitsToProcess = lSubPacket.RemainingBits;
            }
            this.mRemainingBits = this.mRemainingBits.Remove(0, this.mPacketIndex + lNumberOfBits);
        }

        /// <summary>
        /// Process the packet if it is litteral.
        /// </summary>
        private void ProcessLiteral()
        {
            bool lIsFinished = false;
            string lResult = string.Empty;
            while (!lIsFinished)
            {
                char lFirst = this.mRemainingBits.ElementAt(this.mPacketIndex);
                this.mPacketIndex++;
                string l4bits = this.mRemainingBits.Substring(this.mPacketIndex, 4);
                this.mPacketIndex += 4;
                lResult += Convert.ToString(Convert.ToInt32(l4bits, 2), 16);
                if (lFirst.Equals('0'))
                {
                    lIsFinished = true;
                    this.mRemainingBits = this.mRemainingBits.Remove(0, this.mPacketIndex);
                }
            }
            this.Value = Convert.ToInt64(lResult, 16);
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        private void SetValue()
        {
            if (this.IsOperator)
            {
                if (this.Type == 0)
                {
                    this.Value = this.SubPackets.Select(pPacket => pPacket.Value).Sum();
                }
                else if (this.Type == 1)
                {
                    this.Value = this.SubPackets.Aggregate((Int64)1, (pAcc, pNext) => pAcc = pAcc * pNext.Value, pAcc => pAcc);
                }
                else if (this.Type == 2)
                {
                    this.Value = this.SubPackets.Select(pPacket => pPacket.Value).Min();
                }
                else if (this.Type == 3)
                {
                    this.Value = this.SubPackets.Select(pPacket => pPacket.Value).Max();
                }
                else if (this.Type == 5)
                {
                    this.Value = this.SubPackets.First().Value > this.SubPackets.Last().Value ? 1 : 0;
                }
                else if (this.Type == 6)
                {
                    this.Value = this.SubPackets.First().Value < this.SubPackets.Last().Value ? 1 : 0;
                }
                else if (this.Type == 7)
                {
                    this.Value = this.SubPackets.First().Value == this.SubPackets.Last().Value ? 1 : 0;
                }
            }
        }

        /// <summary>
        /// To string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.ToString(0);
        }

        /// <summary>
        /// To string with tabs.
        /// </summary>
        /// <param name="pTab"></param>
        /// <returns></returns>
        private string ToString(int pTab, bool pWithSubPackets = true)
        {
            int lTabCount = pTab;
            StringBuilder lStringBuilder = new StringBuilder();
            lStringBuilder.AppendLine(string.Format("{0}{1}-PACKET| Version:{2} Type:{3} Value:{4}", new string(' ', 2 * lTabCount), this.IsOperator ? "O" : "L", this.Version, this.Type, this.Value));
            if (pWithSubPackets)
            {
                lTabCount++;
                foreach (Packet lSubPacket in this.SubPackets)
                {
                    lStringBuilder.AppendLine(lSubPacket.ToString(lTabCount));
                }
            }
            return lStringBuilder.ToString();
        }

        #endregion
    }
}
