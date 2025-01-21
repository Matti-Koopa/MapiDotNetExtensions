using System.Collections.ObjectModel;

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
        public uint Count { get; init; }

        /// <summary>
        /// Collection of vote options
        /// </summary>
        public ReadOnlyCollection<VoteOption> VoteOptions { get; init; }

        /// <summary>
        /// MUST be set to 0x0104
        /// </summary>
        public ushort Version2 { get; set; }

        /// <summary>
        /// Collection of extra vote options
        /// </summary>
        public ReadOnlyCollection<VoteOptionExtras> VoteOptionsExtras { get; init; }

        /// <summary>
        /// Creates an empty <see cref="VerbStream"/> object.
        /// </summary>
        public VerbStream() { }

        /// <summary>
        /// Creates a default <see cref="VerbStream"/> object.
        /// </summary>
        /// <param name="voteOptions">All <see cref="VerbStream.VoteOptions"/>.</param>
        public VerbStream(IEnumerable<VoteOption> voteOptions)
        {
            var voteOptionsArray = voteOptions.ToArray();
            var optionsLength = voteOptionsArray.Length;
            var voteOptionObjects = new List<VoteOption>(optionsLength);
            var voteOptionsExtras = new List<VoteOptionExtras>(optionsLength);
            for (int i = 0; i < optionsLength; i++)
            {
                var voteOption = voteOptionsArray[i];
                voteOptionObjects.Add(voteOption);
                voteOptionsExtras.Add(new VoteOptionExtras(voteOption.DisplayName));
            }

            Count = (uint)optionsLength;
            VoteOptions = voteOptionObjects.AsReadOnly();
            VoteOptionsExtras = voteOptionsExtras.AsReadOnly();
            Version = 0x0102;
            Version2 = 0x0104;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Format("Version: 0x{0:X4}, count: 0x{1:X8}, version2: 0x{2:X4}", Version, Count, Version2);
        }
    }
}