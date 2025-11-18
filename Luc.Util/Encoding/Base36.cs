using System;
using System.Runtime.CompilerServices;

namespace Luc.Util.Encoding;

/// <summary>
/// Provides Base36 encoding and decoding using alphabet 0-9, a-z.
/// </summary>
public static class Base36
{
  private const string Alphabet = "0123456789abcdefghijklmnopqrstuvwxyz";
  private const int Radix = 36;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string Encode(IEncodingInput source)
  {
    var (Bytes, Length) = source.EncodeToBytes();
    return Encode(Bytes.Span, Length);
  }

  /// <summary>
  /// Encodes a structure to Base36.
  /// </summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string Encode<T>(T value) where T : IEncodingOutput<T>, IEncodingInput
  {
    var (bytesMem, _) = value.EncodeToBytes();
    return Encode(bytesMem.Span);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string Encode(ReadOnlySpan<byte> bytes)
  {
    return Encode(bytes, bytes.Length * 8);
  }

  [MethodImpl()]
  public static string Encode(ReadOnlySpan<byte> bytes, int bitLength)
  {
    int charCount = (int)Math.Ceiling(bitLength / Math.Log2(Radix));
    if (charCount <= 0) throw new ArgumentOutOfRangeException(nameof(bitLength));

    Span<char> chars = charCount <= 128 ? stackalloc char[charCount] : new char[charCount];

    Span<byte> number = stackalloc byte[bytes.Length];
    bytes.CopyTo(number);

    for (int i = charCount - 1; i >= 0; i--)
    {
      int remainder = 0;
      for (int j = 0; j < number.Length; j++)
      {
        int temp = remainder * 256 + number[j];
        number[j] = (byte)(temp / Radix);
        remainder = temp % Radix;
      }
      chars[i] = Alphabet[remainder];
    }

    return new string(chars);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Decode<T>(string str) where T : IEncodingOutput<T>, IEncodingInput
  {
    var bytes = Decode(str);
    return T.DecodeFromBytes(bytes);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Decode<T>(string str, int bitLength) where T : IEncodingOutput<T>, IEncodingInput
  {
    var bytes = Decode(str, bitLength);
    return T.DecodeFromBytes(bytes);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ReadOnlyMemory<byte> Decode(string str)
  {
    int bitLength = (int)Math.Ceiling(str.Length * Math.Log2(Radix));
    return Decode(str, bitLength);
  }

  [MethodImpl()]
  public static ReadOnlyMemory<byte> Decode(string str, int bitLength)
  {
    if (string.IsNullOrEmpty(str)) throw new ArgumentException("Base36 string cannot be null or empty.", nameof(str));
    int outputSize = (bitLength + 7) / 8; // Convert bits to bytes (ceiling)
    if (outputSize <= 0) throw new ArgumentOutOfRangeException(nameof(bitLength));

    Span<byte> number = outputSize <= 128 ? stackalloc byte[outputSize] : new byte[outputSize];
    number.Clear();

    foreach (char c in str)
    {
      int charValue = Alphabet.IndexOf(char.ToLowerInvariant(c));
      if (charValue == -1) throw new FormatException($"Invalid Base36 character '{c}'.");

      int carry = charValue;
      for (int j = number.Length - 1; j >= 0; j--)
      {
        int temp = number[j] * Radix + carry;
        number[j] = (byte)(temp % 256);
        carry = temp / 256;
      }
    }

    Span<byte> resultBytes = outputSize <= 128 ? stackalloc byte[outputSize] : new byte[outputSize];
    resultBytes.Clear();

    int written = number.Length;
    while (written > 1 && number[number.Length - written] == 0) written--;

    if (written > 0)
    {
      number.Slice(number.Length - written).CopyTo(resultBytes.Slice(outputSize - written));
    }

    return new ReadOnlyMemory<byte>(resultBytes.ToArray());
  }
}
