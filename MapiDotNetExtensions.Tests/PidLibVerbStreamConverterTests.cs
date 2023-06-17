using NUnit.Framework;
using System;

namespace MapiDotNetExtensions.Tests
{
    public class PidLibVerbStreamConverterTests
    {
        [TestCaseSource(nameof(TestSource))]
        public void GetFromBase64_DataConvertedSuccessfully(string data, VerbStream expectedResult)
        {
            var actualResult = PidLidVerbStreamConverter.GetFromBase64(data);
            Assert.AreEqual(expectedResult.Version, actualResult.Version);
            Assert.AreEqual(expectedResult.Count, actualResult.Count);
            Assert.AreEqual(expectedResult.Version2, actualResult.Version2);
        }

        [TestCase("")]
        [TestCase(null)]
        public void GetFromBase64_ThrowsArgumentNullException(string data)
        {
            Assert.Throws<ArgumentNullException>(() => PidLidVerbStreamConverter.GetFromBase64(data));
        }

        [Test]
        public void GetFromBytes_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => PidLidVerbStreamConverter.GetFromBytes(null));
        }

        [Test]
        public void GetFromBytes_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => PidLidVerbStreamConverter.GetFromBytes(new byte[0]));
        }

        private static TestCaseData[] TestSource = new TestCaseData[]
        {
            new TestCaseData(
                "AgEGAAAAAAAAAAVSZXBseQhJUE0uTm90ZQdNZXNzYWdlAlJFBQAAAAAAAAAAAQAAAAAAAAACAAAAZgAAAAIAAAABAAAADFJlcGx5IHRvIEFsbAhJUE0uTm90ZQdNZXNzYWdlAlJFBQAAAAAAAAAAAQAAAAAAAAACAAAAZwAAAAMAAAACAAAAB0ZvcndhcmQISVBNLk5vdGUHTWVzc2FnZQJGVwUAAAAAAAAAAAEAAAAAAAAAAgAAAGgAAAAEAAAAAwAAAA9SZXBseSB0byBGb2xkZXIISVBNLlBvc3QEUG9zdAAFAAAAAAAAAAABAAAAAAAAAAIAAABsAAAACAAAAAQAAAADWWVzCElQTS5Ob3RlAANZZXMAAAAAAAAAAAABAAAAAgAAAAIAAAABAAAA/////wQAAAACTm8ISVBNLk5vdGUAAk5vAAAAAAAAAAAAAQAAAAIAAAACAAAAAgAAAP////8EAQVSAGUAcABsAHkAAlIARQAMUgBlAHAAbAB5ACAAdABvACAAQQBsAGwAAlIARQAHRgBvAHIAdwBhAHIAZAACRgBXAA9SAGUAcABsAHkAIAB0AG8AIABGAG8AbABkAGUAcgAAA1kAZQBzAANZAGUAcwACTgBvAAJOAG8A",
                new VerbStream
                {
                    Version = 0x0102,
                    Count = 6,
                    Version2 = 0x0104
                }),
            new TestCaseData(
                "AgEHAAAAAAAAAAVSZXBseQhJUE0uTm90ZQdNZXNzYWdlAlJFBQAAAAAAAAAAAQAAAAAAAAACAAAAZgAAAAIAAAABAAAADFJlcGx5IHRvIEFsbAhJUE0uTm90ZQdNZXNzYWdlAlJFBQAAAAAAAAAAAQAAAAAAAAACAAAAZwAAAAMAAAACAAAAB0ZvcndhcmQISVBNLk5vdGUHTWVzc2FnZQJGVwUAAAAAAAAAAAEAAAAAAAAAAgAAAGgAAAAEAAAAAwAAAA9SZXBseSB0byBGb2xkZXIISVBNLlBvc3QEUG9zdAAFAAAAAAAAAAABAAAAAAAAAAIAAABsAAAACAAAAAQAAAADWWVzCElQTS5Ob3RlAANZZXMAAAAAAAAAAAABAAAAAgAAAAIAAAABAAAA/////wQAAAACTm8ISVBNLk5vdGUAAk5vAAAAAAAAAAAAAQAAAAIAAAACAAAAAgAAAP////8EAAAABU1heWJlCElQTS5Ob3RlAAVNYXliZQAAAAAAAAAAAAEAAAACAAAAAgAAAAMAAAD/////BAEFUgBlAHAAbAB5AAJSAEUADFIAZQBwAGwAeQAgAHQAbwAgAEEAbABsAAJSAEUAB0YAbwByAHcAYQByAGQAAkYAVwAPUgBlAHAAbAB5ACAAdABvACAARgBvAGwAZABlAHIAAANZAGUAcwADWQBlAHMAAk4AbwACTgBvAAVNAGEAeQBiAGUABU0AYQB5AGIAZQA=",
                new VerbStream
                {
                    Version = 0x0102,
                    Count = 7,
                    Version2 = 0x0104
                }),
            new TestCaseData(
                "AgEIAAAAAAAAAAVSZXBseQhJUE0uTm90ZQdNZXNzYWdlAlJFBQAAAAAAAAAAAQAAAAAAAAACAAAAZgAAAAIAAAABAAAADFJlcGx5IHRvIEFsbAhJUE0uTm90ZQdNZXNzYWdlAlJFBQAAAAAAAAAAAQAAAAAAAAACAAAAZwAAAAMAAAACAAAAB0ZvcndhcmQISVBNLk5vdGUHTWVzc2FnZQJGVwUAAAAAAAAAAAEAAAAAAAAAAgAAAGgAAAAEAAAAAwAAAA9SZXBseSB0byBGb2xkZXIISVBNLlBvc3QEUG9zdAAFAAAAAAAAAAABAAAAAAAAAAIAAABsAAAACAAAAAQAAAAEQmFyMQhJUE0uTm90ZQAEQmFyMQAAAAAAAAAAAAEAAAACAAAAAgAAAAEAAAD/////BAAAAARCYXIyCElQTS5Ob3RlAARCYXIyAAAAAAAAAAAAAQAAAAIAAAACAAAAAgAAAP////8EAAAABEJhcjMISVBNLk5vdGUABEJhcjMAAAAAAAAAAAABAAAAAgAAAAIAAAADAAAA/////wQAAAAEQmFyNAhJUE0uTm90ZQAEQmFyNAAAAAAAAAAAAAEAAAACAAAAAgAAAAQAAAD/////BAEFUgBlAHAAbAB5AAJSAEUADFIAZQBwAGwAeQAgAHQAbwAgAEEAbABsAAJSAEUAB0YAbwByAHcAYQByAGQAAkYAVwAPUgBlAHAAbAB5ACAAdABvACAARgBvAGwAZABlAHIAAARCAGEAcgAxAARCAGEAcgAxAARCAGEAcgAyAARCAGEAcgAyAARCAGEAcgAzAARCAGEAcgAzAARCAGEAcgA0AARCAGEAcgA0AA==",
                new VerbStream
                {
                    Version = 0x0102,
                    Count = 8,
                    Version2 = 0x0104
                })
        };
    }
}