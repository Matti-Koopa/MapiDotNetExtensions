using System.Text;

namespace MapiDotNetExtensions
{
    /// <summary>
    /// Vote option extras
    /// </summary>
    public record VoteOptionExtras
    {
        /// <summary>
        /// The count of Unicode characters (not bytes) in the <see cref="DisplayName"/> field.
        /// </summary>
        public byte DisplayNameCount { get; init; }

        /// <summary>
        /// The display name of this voting option, as a Unicode string without a null terminator.
        /// </summary>
        public string DisplayName { get; init; }

        /// <summary>
        /// The count of Unicode characters in the <see cref="DisplayNameRepeat"/> field.
        /// MUST have the same value as the <see cref="DisplayNameCount"/> field.
        /// </summary>
        public byte DisplayNameCountRepeat { get; init; }

        /// <summary>
        /// A duplicate instance of the display name, as a Unicode string without a null terminator. 
        /// MUST have the same value as the <see cref="DisplayName"/> field.
        /// </summary>
        public string DisplayNameRepeat { get; init; }

        /// <summary>
        /// Creates an empty <see cref="VoteOptionExtras"/> object.
        /// </summary>
        public VoteOptionExtras() { }

        /// <summary>
        /// Creates a default <see cref="VoteOptionExtras"/> object.
        /// </summary>
        /// <param name="displayName">Friendly text of this option.</param>
        public VoteOptionExtras(string displayName)
        {
            var displayNameCount = (byte)(Encoding.Unicode.GetBytes(displayName).Length / 2);

            DisplayNameCount = displayNameCount;
            DisplayName = displayName;
            DisplayNameCountRepeat = displayNameCount;
            DisplayNameRepeat = displayName;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var sb = new StringBuilder()
                .AppendFormat("DisplayNameCount: 0x{0:X2} ({0}), ", DisplayNameCount)
                .AppendFormat("DisplayName: {0}, ", DisplayName)
                .AppendFormat("DisplayNameCountRepeat: 0x{0:X2} ({0}), ", DisplayNameCountRepeat)
                .AppendFormat("DisplayNameRepeat: {0}", DisplayNameRepeat);

            return sb.ToString();
        }
    }
}