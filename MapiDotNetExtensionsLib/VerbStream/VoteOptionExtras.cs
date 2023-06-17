using System.Text;

namespace MapiDotNetExtensions
{
    public class VoteOptionExtras
    {
        /// <summary>
        /// The count of Unicode characters (not bytes) in the <see cref="DisplayName"/> field.
        /// </summary>
        public byte DisplayNameCount { get; set; }

        /// <summary>
        /// The display name of this voting option, as a Unicode string without a null terminator.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// The count of Unicode characters in the <see cref="DisplayNameRepeat"/> field.
        /// MUST have the same value as the <see cref="DisplayNameCount"/> field.
        /// </summary>
        public byte DisplayNameCountRepeat { get; set; }

        /// <summary>
        /// A duplicate instance of the display name, as a Unicode string without a null terminator. 
        /// MUST have the same value as the <see cref="DisplayName"/> field.
        /// </summary>
        public string DisplayNameRepeat { get; set; }

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
