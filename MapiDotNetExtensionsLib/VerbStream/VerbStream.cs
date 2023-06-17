using System.Collections.Generic;

namespace MapiDotNetExtensions
{
    /// <summary>
    /// Verb stream
    /// </summary>
    public class VerbStream
    {
        /// <summary>
        /// Set to 0x0102
        /// </summary>
        public ushort Version { get; set; }

        /// <summary>
        /// Specifies the count of the <see cref="VoteOptions"/> and <see cref="VoteOptionsExtras"/> structures
        /// </summary>
        public uint Count { get; set; }

        /// <summary>
        /// Collection of vote options
        /// </summary>
        public List<VoteOption> VoteOptions { get; set; }

        /// <summary>
        /// MUST be set to 0x0104
        /// </summary>
        public ushort Version2 { get; set; }

        /// <summary>
        /// Collection of extra vote options
        /// </summary>
        public List<VoteOptionExtras> VoteOptionsExtras { get; set; }

        public override string ToString()
        {
            return string.Format("Version: 0x{0:X4}, count: 0x{1:X8}, version2: 0x{2:X4}", Version, Count, Version2);
        }
    }
}
