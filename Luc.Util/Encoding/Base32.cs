using System;
using System.Runtime.CompilerServices;

namespace Luc.Util.Encoding;

/// <summary>
/// Provides Base32 encoding and decoding using alphabet 0-9, b-z (excluding a, i, l, o).
/// Uses a bit-manipulation algorithm, not compatible with the old BigInteger-based encoding.
/// </summary>
public static class Base32
{
  private const string Alphabet = "0123456789bcdefghjkmnpqrstuvwxyz";
  private const int Radix = 32;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string Encode(IEncodingInput source)
  {
    var (Bytes, Length) = source.EncodeToBytes();
    return Encode(Bytes.Span, Length);
  }

  /// <summary>
  /// Encodes a structure to Base32.
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
    ArgumentOutOfRangeException.ThrowIfNegativeOrZero(bitLength);

    int charCount = (int)Math.Ceiling(bitLength / 5.0);
    Span<char> chars = charCount <= 128 ? stackalloc char[charCount] : new char[charCount];

    int bitIndex = 0;
    for (int i = 0; i < charCount; i++)
    {
      int value = 0;
      for (int j = 0; j < 5; j++)
      {
        if (bitIndex >= bitLength) break;
        int byteIndex = bitIndex / 8;
        int bitInByte = bitIndex % 8; // LSB first
        if ((bytes[byteIndex] & (1 << bitInByte)) != 0)
        {
          value |= (1 << j);
        }
        bitIndex++;
      }
      chars[charCount - 1 - i] = Alphabet[value];
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
    if (string.IsNullOrEmpty(str)) throw new ArgumentException("Base32 string cannot be null or empty.", nameof(str));
    int outputSize = (bitLength + 7) / 8; 
    if (outputSize <= 0) throw new ArgumentOutOfRangeException(nameof(bitLength));

    ReadOnlyMemory<byte> result;
    if (outputSize <= 128)
    {
      Span<byte> bytes = stackalloc byte[outputSize];
      bytes.Clear();
      DecodeToSpan(str, bitLength, bytes);
      result = new ReadOnlyMemory<byte>(bytes.ToArray());
    }
    else
    {
      byte[] byteArray = new byte[outputSize];
      Span<byte> bytes = byteArray;
      DecodeToSpan(str, bitLength, bytes);
      result = new ReadOnlyMemory<byte>(byteArray);
    }

    return result;

    static void DecodeToSpan(string str, int bitLength, Span<byte> bytes)
    {
      int bitIndex = 0;
      foreach (char c in str.Reverse())
      {
        int charValue = Alphabet.IndexOf(char.ToLowerInvariant(c));
        if (charValue == -1) throw new FormatException($"Invalid Base32 character '{c}'.");

        for (int j = 0; j < 5; j++)
        {
          if (bitIndex >= bitLength) break;
          int byteIndex = bitIndex / 8;
          int bitInByte = bitIndex % 8;
          if ((charValue & (1 << j)) != 0)
          {
            bytes[byteIndex] |= (byte)(1 << bitInByte);
          }
          bitIndex++;
        }
      }
    }
  }
}
