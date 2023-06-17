# MapiDotNetExtensions
.NET extensions for MAPI properties.

## PidLidVerbStreamConverter

Converts binary data encoded as base-64 to an object that represents the verb stream. It also converts a list of voting options to the base-64 string.

### Decode

```C#
var base64string = "AA..";
var verbStream = PidLidVerbStreamConverter.GetFromBase64(base64string);
```

```C#
var bytes = new byte[] {...};
var verbStream = PidLidVerbStreamConverter.GetFromBytes(bytes);
```

### Encode

```C#
var voteOptions = new List<string> { "Yes", "No" };
var base64string = PidLidVerbStreamConverter.ToBase64String(voteOptions);

var verbStream = new VerbStream { ... };
var base64string = PidLidVerbStreamConverter.ToBase64String(verbStream);
```

```C#
var voteOptions = new List<string> { "Yes", "No" };
var bytes = PidLidVerbStreamConverter.ToBytes(voteOptions);

var verbStream = new VerbStream { ... };
var bytes = PidLidVerbStreamConverter.ToBytes(verbStream);
```
