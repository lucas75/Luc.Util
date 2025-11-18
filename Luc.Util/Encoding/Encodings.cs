namespace Luc.Util.Encoding;

/// <summary>
/// Represents the result of encoding an input to bytes with a specified bit length.
/// </summary>
public ref struct EncodingBytes
{
    public ReadOnlySpan<byte> Bytes;
    public int BitLength;

    public EncodingBytes(ReadOnlySpan<byte> bytes, int bitLength)
    {
        Bytes = bytes;
        BitLength = bitLength;
    }
}

public interface IEncodingInput
{
    EncodingBytes EncodeToBytes();
}

public interface IEncodingOutput<T> where T : IEncodingInput
{
    static abstract T DecodeFromBytes(ReadOnlySpan<byte> bytes);
}
