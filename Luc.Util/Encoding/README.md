# Encoding Directory

## General Layout of the Encoding Classes

Each Base?? class provides exactly three encoder methods and three decoder methods:

### Encoders
- `Encode(IEncodingInput source)`
- `Encode(ReadOnlySpan<byte> bytes)`
- `Encode(ReadOnlySpan<byte> bytes, int bitLength)`

### Decoders
- `Decode<T>(string str) where T : IEncodingOutput<T>, IEncodingInput`
- `Decode<T>(string str, int bitLength) where T : IEncodingOutput<T>, IEncodingInput`
- `Decode(string str)`


## IEncodingInput and IEncodingOutput
- `IEncodingInput` It requires a method to convert the type to bytes and bit length.
- `IEncodingOutput<T>` It requires a static method to reconstruct the type from bytes.





