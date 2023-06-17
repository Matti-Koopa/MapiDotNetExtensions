using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapiDotNetExtensions
{
    /// <summary>
    /// Converts a base data type to <see cref="VerbStream"/> and back.
    /// </summary>
    public static class PidLidVerbStreamConverter
    {
        private const int VersionByteCount = 2;
        private const int OptionsCountByteCount = 4;
        private const int Version2ByteCount = 2;
        private const int VerbTypeByteCount = 4;
        private const int DisplayNameCountByteCount = 1;
        private const int MsgClsNameCountByteCount = 1;
        private const int Internal1StringCountByteCount = 1;
        private const int DisplayNameCountRepeatByteCount = 1;
        private const int Internal2ByteCount = 4;
        private const int Internal3ByteCount = 1;
        private const int FUseHeadersByteCount = 4;
        private const int Internal4ByteCount = 4;
        private const int SendBehaviorByteCount = 4;
        private const int Internal5ByteCount = 4;
        private const int IdByteCount = 4;
        private const int Internal6ByteCount = 4;

        private const string AnsiEncodingName = "iso-8859-1";

        /// <summary>
        /// Converts the specified string, which encodes binary data as base-64 digits, to <see cref="VerbStream"/>.
        /// </summary>
        /// <param name="data">String to convert</param>
        /// <returns>Returns <see cref="VerbStream"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static VerbStream GetFromBase64(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException(nameof(data));
            }

            var bytes = Convert.FromBase64String(data);
            return GetFromBytes(bytes);
        }

        /// <summary>
        /// Converts the bytes to <see cref="VerbStream"/>.
        /// </summary>
        /// <param name="bytes">Bytes to convert</param>
        /// <returns>Returns <see cref="VerbStream"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static VerbStream GetFromBytes(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }
            if (bytes.Length == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bytes), "Input array cannot be empty.");
            }

            var span = new ReadOnlySpan<byte>(bytes);
            var currentIndex = 0;
            var version = BitConverter.ToUInt16(span.Slice(currentIndex, VersionByteCount));
            currentIndex += VersionByteCount;

            var count = BitConverter.ToUInt32(span.Slice(currentIndex, OptionsCountByteCount));
            currentIndex += OptionsCountByteCount;

            var voteOptions = new List<VoteOption>((int)count);
            if (count > 0)
            {
                for (var i = 0; i < count; i++)
                {
                    var verbType = BitConverter.ToUInt32(span.Slice(currentIndex, VerbTypeByteCount));
                    currentIndex += VerbTypeByteCount;

                    var displayNameCount = span[currentIndex];
                    currentIndex += DisplayNameCountByteCount;

                    var displayName = Encoding.GetEncoding(AnsiEncodingName).GetString(span.Slice(currentIndex, displayNameCount));
                    currentIndex += displayNameCount;

                    var msgClsNameCount = span[currentIndex];
                    currentIndex += MsgClsNameCountByteCount;

                    var msgClsName = Encoding.GetEncoding(AnsiEncodingName).GetString(span.Slice(currentIndex, msgClsNameCount));
                    currentIndex += msgClsNameCount;

                    var internal1StringCount = span[currentIndex];
                    currentIndex += Internal1StringCountByteCount;

                    var internal1String = string.Empty;
                    if (internal1StringCount > 0)
                    {
                        internal1String = Encoding.GetEncoding(AnsiEncodingName).GetString(span.Slice(currentIndex, internal1StringCount));
                        currentIndex += internal1StringCount;
                    }

                    var displayNameCountRepeat = span[currentIndex];
                    currentIndex += DisplayNameCountRepeatByteCount;

                    var displayNameRepeat = Encoding.GetEncoding(AnsiEncodingName).GetString(span.Slice(currentIndex, displayNameCountRepeat));
                    currentIndex += displayNameCountRepeat;

                    var internal2 = BitConverter.ToUInt32(span.Slice(currentIndex, Internal2ByteCount));
                    currentIndex += Internal2ByteCount;

                    var internal3 = span[currentIndex];
                    currentIndex += Internal3ByteCount;

                    var fUseHeaders = BitConverter.ToBoolean(span.Slice(currentIndex, FUseHeadersByteCount));
                    currentIndex += FUseHeadersByteCount;

                    var internal4 = BitConverter.ToUInt32(span.Slice(currentIndex, Internal4ByteCount));
                    currentIndex += Internal4ByteCount;

                    var sendBehavior = BitConverter.ToUInt32(span.Slice(currentIndex, SendBehaviorByteCount));
                    currentIndex += SendBehaviorByteCount;

                    var internal5 = BitConverter.ToUInt32(span.Slice(currentIndex, Internal5ByteCount));
                    currentIndex += Internal5ByteCount;

                    var id = BitConverter.ToUInt32(span.Slice(currentIndex, IdByteCount));
                    currentIndex += IdByteCount;

                    var internal6 = BitConverter.ToInt32(span.Slice(currentIndex, Internal6ByteCount));
                    currentIndex += Internal6ByteCount;

                    voteOptions.Add(new VoteOption
                    {
                        VerbType = verbType,
                        DisplayNameCount = displayNameCount,
                        DisplayName = displayName,
                        MsgClsNameCount = msgClsNameCount,
                        MsgClsName = msgClsName,
                        Internal1StringCount = internal1StringCount,
                        Internal1String = internal1String,
                        DisplayNameCountRepeat = displayNameCountRepeat,
                        DisplayNameRepeat = displayNameRepeat,
                        Internal2 = internal2,
                        Internal3 = internal3,
                        fUseUSHeaders = fUseHeaders,
                        Internal4 = internal4,
                        SendBehavior = sendBehavior,
                        Internal5 = internal5,
                        ID = id,
                        Internal6 = internal6
                    });
                }
            }

            var version2 = BitConverter.ToUInt16(span.Slice(currentIndex, Version2ByteCount));
            currentIndex += Version2ByteCount;

            var voteOptionExtras = new List<VoteOptionExtras>((int)count);
            if (count > 0)
            {
                for (var i = 0; i < count; i++)
                {
                    var displayNameCount = span[currentIndex];
                    currentIndex += DisplayNameCountByteCount;

                    var displayNameByteCount = 2 * displayNameCount;
                    var displayName = Encoding.Unicode.GetString(span.Slice(currentIndex, displayNameByteCount));
                    currentIndex += displayNameByteCount;

                    var displayNameCountRepeat = span[currentIndex];
                    currentIndex += DisplayNameCountRepeatByteCount;

                    var displayNameRepeatByteCount = 2 * displayNameCountRepeat;
                    var displayNameRepeat = Encoding.Unicode.GetString(span.Slice(currentIndex, displayNameRepeatByteCount));
                    currentIndex += displayNameRepeatByteCount;

                    voteOptionExtras.Add(new VoteOptionExtras
                    {
                        DisplayNameCount = displayNameCount,
                        DisplayName = displayName,
                        DisplayNameCountRepeat = displayNameCountRepeat,
                        DisplayNameRepeat = displayNameRepeat
                    });
                }
            }

            var result = new VerbStream
            {
                Version = version,
                Count = count,
                VoteOptions = voteOptions,
                Version2 = version2,
                VoteOptionsExtras = voteOptionExtras
            };
            return result;
        }

        /// <summary>
        /// Converts a list of vote options to its equivalent string representation that is encoded with base-64 digits.
        /// </summary>
        /// <param name="voteOptions">Vote options</param>
        /// <returns>The string represenation in base-64.</returns>
        public static string ToBase64String(List<string> voteOptions)
        {
            return Convert.ToBase64String(ToBytes(voteOptions));
        }

        /// <summary>
        /// Converts a <see cref="VerbStream"/> to its equivalent string representation that is encoded with base-64 digits.
        /// </summary>
        /// <param name="verbStream"><see cref="VerbStream"/></param>
        /// <returns>The string represenation in base-64.</returns>
        public static string ToBase64String(VerbStream verbStream)
        {
            return Convert.ToBase64String(ToBytes(verbStream));
        }

        /// <summary>
        /// Converts a list of vote options to an array of bytes that represents encoded <see cref="VerbStream"/>.
        /// </summary>
        /// <param name="voteOptions">Vote options</param>
        /// <returns>The bytes representation of <see cref="VerbStream"/>.</returns>
        public static byte[] ToBytes(List<string> voteOptions)
        {
            var verbStream = new VerbStream
            {
                Version = 0x0102,
                Count = (uint)voteOptions.Count,
                VoteOptions = new List<VoteOption>(voteOptions.Count),
                Version2 = 0x0104,
                VoteOptionsExtras = new List<VoteOptionExtras>(voteOptions.Count)
            };
            for (int i = 0; i < voteOptions.Count; i++)
            {
                var voteOption = voteOptions[i];
                verbStream.VoteOptions.Add(new VoteOption
                {
                    VerbType = 4,
                    DisplayNameCount = (byte)voteOption.Length,
                    DisplayName = voteOption,
                    MsgClsNameCount = 8,
                    MsgClsName = "IPM.Note",
                    Internal1StringCount = 0,
                    DisplayNameCountRepeat = (byte)voteOption.Length,
                    DisplayNameRepeat = voteOption,
                    Internal2 = 0,
                    Internal3 = 0,
                    fUseUSHeaders = false,
                    Internal4 = 1,
                    SendBehavior = 1,
                    Internal5 = 2,
                    ID = (uint)(i + 1),
                    Internal6 = -1,
                });

                var displayNameCount = (byte)(Encoding.Unicode.GetBytes(voteOption).Length / 2);
                verbStream.VoteOptionsExtras.Add(new VoteOptionExtras
                {
                    DisplayNameCount = displayNameCount,
                    DisplayName = voteOption,
                    DisplayNameCountRepeat = displayNameCount,
                    DisplayNameRepeat = voteOption
                });
            }

            return ToBytes(verbStream);
        }

        /// <summary>
        /// Converts a <see cref="VerbStream"/> to an array of bytes that represents encoded <see cref="VerbStream"/>.
        /// </summary>
        /// <param name="verbStream"><see cref="VerbStream"/></param>
        /// <returns>The bytes representation of <see cref="VerbStream"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static byte[] ToBytes(VerbStream verbStream)
        {
            if (verbStream == null)
            {
                throw new ArgumentNullException(nameof(verbStream));
            }
            var size = CalculateByteSize(verbStream);
            Span<byte> span = stackalloc byte[size];
            ToSpan(verbStream, in span);
            return span.ToArray();
        }

        private static void ToSpan(VerbStream verbStream, in Span<byte> span)
        {
            var currentIndex = 0;
            BitConverter.TryWriteBytes(span.Slice(currentIndex, VersionByteCount), verbStream.Version);
            currentIndex += VersionByteCount;

            BitConverter.TryWriteBytes(span.Slice(currentIndex, OptionsCountByteCount), verbStream.Count);
            currentIndex += OptionsCountByteCount;

            foreach (var item in verbStream.VoteOptions)
            {
                BitConverter.TryWriteBytes(span.Slice(currentIndex, VerbTypeByteCount), item.VerbType);
                currentIndex += VerbTypeByteCount;

                span[currentIndex] = item.DisplayNameCount;
                currentIndex += DisplayNameCountByteCount;

                var bytes = Encoding.GetEncoding(AnsiEncodingName).GetBytes(item.DisplayName);
                bytes.AsSpan().CopyTo(span.Slice(currentIndex, item.DisplayNameCount));
                currentIndex += item.DisplayNameCount;

                span[currentIndex] = item.MsgClsNameCount;
                currentIndex += MsgClsNameCountByteCount;

                bytes = Encoding.GetEncoding(AnsiEncodingName).GetBytes(item.MsgClsName);
                bytes.AsSpan().CopyTo(span.Slice(currentIndex, item.MsgClsNameCount));
                currentIndex += item.MsgClsNameCount;

                span[currentIndex] = item.Internal1StringCount;
                currentIndex += Internal1StringCountByteCount;
                if (item.Internal1StringCount > 0)
                {
                    bytes = Encoding.GetEncoding(AnsiEncodingName).GetBytes(item.Internal1String);
                    bytes.AsSpan().CopyTo(span.Slice(currentIndex, item.Internal1StringCount));
                    currentIndex += item.Internal1StringCount;
                }
                span[currentIndex] = item.DisplayNameCountRepeat;
                currentIndex += DisplayNameCountRepeatByteCount;

                bytes = Encoding.GetEncoding(AnsiEncodingName).GetBytes(item.DisplayNameRepeat);
                bytes.AsSpan().CopyTo(span.Slice(currentIndex, item.DisplayNameCountRepeat));
                currentIndex += item.DisplayNameCountRepeat;

                BitConverter.TryWriteBytes(span.Slice(currentIndex, Internal2ByteCount), item.Internal2);
                currentIndex += Internal2ByteCount;

                span[currentIndex] = item.Internal3;
                currentIndex += Internal3ByteCount;

                BitConverter.TryWriteBytes(span.Slice(currentIndex, FUseHeadersByteCount), item.fUseUSHeaders ? 1 : (uint)0);
                currentIndex += FUseHeadersByteCount;

                BitConverter.TryWriteBytes(span.Slice(currentIndex, Internal4ByteCount), item.Internal4);
                currentIndex += Internal4ByteCount;

                BitConverter.TryWriteBytes(span.Slice(currentIndex, SendBehaviorByteCount), item.SendBehavior);
                currentIndex += SendBehaviorByteCount;

                BitConverter.TryWriteBytes(span.Slice(currentIndex, Internal5ByteCount), item.Internal5);
                currentIndex += Internal5ByteCount;

                BitConverter.TryWriteBytes(span.Slice(currentIndex, IdByteCount), item.ID);
                currentIndex += IdByteCount;

                BitConverter.TryWriteBytes(span.Slice(currentIndex, Internal6ByteCount), item.Internal6);
                currentIndex += Internal6ByteCount;
            }
            BitConverter.TryWriteBytes(span.Slice(currentIndex, Version2ByteCount), verbStream.Version2);
            currentIndex += Version2ByteCount;
            foreach (var item in verbStream.VoteOptionsExtras)
            {
                span[currentIndex] = item.DisplayNameCount;
                currentIndex += DisplayNameCountByteCount;

                var bytes = Encoding.Unicode.GetBytes(item.DisplayName);
                bytes.AsSpan().CopyTo(span.Slice(currentIndex, item.DisplayNameCount * 2));
                currentIndex += item.DisplayNameCount * 2;

                span[currentIndex] = item.DisplayNameCountRepeat;
                currentIndex += DisplayNameCountRepeatByteCount;

                bytes = Encoding.Unicode.GetBytes(item.DisplayNameRepeat);
                bytes.AsSpan().CopyTo(span.Slice(currentIndex, item.DisplayNameCountRepeat * 2));
                currentIndex += item.DisplayNameCountRepeat * 2;
            }
        }

        private static int CalculateByteSize(VerbStream stream)
        {
            var size = VersionByteCount + OptionsCountByteCount + Version2ByteCount;

            var voteOptionsSize = stream.VoteOptions.Sum(x => VerbTypeByteCount +
                x.DisplayNameCount + DisplayNameCountByteCount +
                DisplayNameCountRepeatByteCount + x.DisplayNameCountRepeat +
                MsgClsNameCountByteCount + x.MsgClsNameCount +
                Internal2ByteCount + Internal3ByteCount +
                FUseHeadersByteCount + Internal4ByteCount + SendBehaviorByteCount +
                Internal5ByteCount + IdByteCount + Internal6ByteCount +
                Internal1StringCountByteCount + x.Internal1StringCount);

            var voteOptionsExtrasSize = stream.VoteOptionsExtras.Sum(x => x.DisplayNameCount * 2 + DisplayNameCountByteCount +
                DisplayNameCountRepeatByteCount + x.DisplayNameCountRepeat * 2);
            return size + voteOptionsSize + voteOptionsExtrasSize;
        }
    }
}
