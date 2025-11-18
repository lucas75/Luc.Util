namespace Luc.Util.Encoding;

public interface IEncodingInput
{
    (ReadOnlyMemory<byte> Bytes, int Length) EncodeToBytes();
}

public interface IEncodingOutput<T> where T : IEncodingInput
{
    static abstract T DecodeFromBytes(ReadOnlyMemory<byte> bytes);
}
