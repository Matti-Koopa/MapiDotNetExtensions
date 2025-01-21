using System.Text;

namespace MapiDotNetExtensions
{
    /// <summary>
    /// Vote option
    /// </summary>
    public record VoteOption
    {
        /// <summary>
        /// The verb used by this structure. 
        /// Set to 4 (0x00000004) for vote option.
        /// </summary>
        public uint VerbType { get; set; }

        /// <summary>
        /// The count of characters in the <see cref="DisplayName"/> field.
        /// </summary>
        public byte DisplayNameCount { get; init; }

        /// <summary>
        /// The localized display name of the voting option (for example, "Yes") as an ANSI string, without the null terminating character.
        /// </summary>
        public string DisplayName { get; init; }

        /// <summary>
        /// The count of characters in the <see cref="MsgClsName"/> field.
        /// Set to 8 (0x08).
        /// </summary>
        public byte MsgClsNameCount { get; set; }

        /// <summary>
        /// Set to "IPM.Note", without the null terminating character.
        /// </summary>
        public string MsgClsName { get; set; }

        /// <summary>
        /// The count of characters in the <see cref="Internal1String"/> field. 
        /// Set to 0x00 for voting options.
        /// </summary>
        public byte Internal1StringCount { get; set; }

        /// <summary>
        /// MUST NOT be present, as Internal1StringCount is 0x00 for a voting option.
        /// </summary>
        public string Internal1String { get; set; }

        /// <summary>
        /// MUST have the same value as the <see cref="DisplayNameCount"/> field.
        /// </summary>
        public byte DisplayNameCountRepeat { get; init; }

        /// <summary>
        /// MUST have the same value as the <see cref="DisplayName"/> field.
        /// </summary>
        public string DisplayNameRepeat { get; init; }

        /// <summary>
        /// Set to 0x00000000.
        /// </summary>
        public uint Internal2 { get; set; }

        /// <summary>
        /// Set to 0x00.
        /// </summary>
        public byte Internal3 { get; set; }

        /// <summary>
        /// Indicates that a U.S. style reply header is to be used in the response message (as opposed to a localized response header). 
        /// The value is set to either 0x00000001, using U.S. style reply header, or 0x00000000 otherwise.
        /// </summary>
        public bool fUseUSHeaders { get; set; }

        /// <summary>
        /// Set to 0x00000001.
        /// </summary>
        public uint Internal4 { get; set; }

        /// <summary>
        /// Indicates the behavior on send. When a user chooses a voting option, 
        /// SendBehavior specifies whether the user is to be prompted to edit the response mail or whether the client automatically sends it on behalf of the user. 
        /// The value of this field is one of the values defined in the following table.
        /// <list type="bullet">
        /// <item>0x00000001 - Automatically send the voting response message.</item>
        /// <item>0x00000002 - Prompt the user to specify whether he or she would like to automatically send or edit the voting response first.</item>
        /// </list>
        /// </summary>
        public uint SendBehavior { get; set; }

        /// <summary>
        /// Set to 0x00000002.
        /// </summary>
        public uint Internal5 { get; set; }

        /// <summary>
        /// Specifies a numeric identifier for this voting option. 
        /// The client SHOULD specify 1 for the first VoteOption structure and monotonically increase this value for each subsequent VoteOption structure.
        /// </summary>
        public uint ID { get; set; }

        /// <summary>
        /// Set to "-1" (0xFFFFFFFF).
        /// </summary>
        public int Internal6 { get; set; }

        /// <summary>
        /// Creates an empty <see cref="VoteOption"/> object.
        /// </summary>
        public VoteOption() { }

        /// <summary>
        /// Creates a default <see cref="VoteOption"/> object.
        /// </summary>
        /// <param name="displayName">Friendly text of this option.</param>
        /// <param name="index">The 0-based index of this option.</param>
        public VoteOption(string displayName, int index)
        {
            VerbType = 4;
            DisplayName = DisplayNameRepeat = displayName;
            DisplayNameCount = DisplayNameCountRepeat = (byte)DisplayName.Length;
            MsgClsNameCount = 8;
            MsgClsName = "IPM.Note";
            Internal1StringCount = 0;
            Internal2 = 0;
            Internal3 = 0;
            fUseUSHeaders = false;
            Internal4 = 1;
            SendBehavior = 1;
            Internal5 = 2;
            ID = (uint)(index + 1);
            Internal6 = -1;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var sb = new StringBuilder()
                .AppendFormat("VerbType: 0x{0:X8}, ", VerbType)
                .AppendFormat("DisplayNameCount: 0x{0:X2} ({0}), ", DisplayNameCount)
                .AppendFormat("DisplayName: {0}, ", DisplayName)
                .AppendFormat("MsgClsNameCount: 0x{0:X2} ({0}), ", MsgClsNameCount)
                .AppendFormat("MsgClsName: {0}, ", MsgClsName)
                .AppendFormat("Internal1StringCount: 0x{0:X2} ({0}), ", Internal1StringCount)
                .AppendFormat("Internal1String: {0}", Internal1String)
                .AppendFormat("DisplayNameCountRepeat: 0x{0:X2} ({0}), ", DisplayNameCountRepeat)
                .AppendFormat("DisplayNameRepeat: {0}", DisplayNameRepeat)
                .AppendFormat("Internal2: 0x{0:X8}, ", Internal2)
                .AppendFormat("Internal3: 0x{0:X2}, ", Internal3)
                .AppendFormat("fUseUSHeaders: {0}", fUseUSHeaders)
                .AppendFormat("Internal4: 0x{0:X8}, ", Internal4)
                .AppendFormat("SendBehavior: 0x{0:X8}, ", SendBehavior)
                .AppendFormat("Internal5: 0x{0:X8}, ", Internal5)
                .AppendFormat("ID: 0x{0:X8}, ", ID)
                .AppendFormat("Internal6: 0x{0:X8}, ", Internal6);

            return sb.ToString();
        }
    }
}