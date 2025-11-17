using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Luc.Util;

/// <summary>
/// Provides Base36 encoding and decoding using alphabet 0-9, a-z.
/// </summary>
public static class Base36
{
  private const string Alphabet = "0123456789abcdefghijklmnopqrstuvwxyz";
  private const int Radix = 36;

  /// <summary>
  /// Encodes bytes to a Base36 string with a fixed length.
  /// </summary>
  /// <param name="bytes">The bytes to encode.</param>
  /// <param name="fixedLength">The fixed output length.</param>
  /// <returns>The Base36 string.</returns>
  public static string Encode(ReadOnlySpan<byte> bytes, int fixedLength)
  {
    if (fixedLength <= 0) throw new ArgumentOutOfRangeException(nameof(fixedLength));

    Span<char> chars = fixedLength <= 128 ? stackalloc char[fixedLength] : new char[fixedLength];
    
    var bigInt = new BigInteger(bytes, isUnsigned: true, isBigEndian: true);

    for (int i = fixedLength - 1; i >= 0; i--)
    {
      if (bigInt == BigInteger.Zero)
      {
        for (int j = i; j >= 0; j--) chars[j] = Alphabet[0];
        break;
      }

      bigInt = BigInteger.DivRem(bigInt, Radix, out var remainder);
      chars[i] = Alphabet[(int)remainder];
    }

    return new string(chars);
  }

  /// <summary>
  /// Encodes a structure to a Base36 string.
  /// </summary>
  /// <typeparam name="T">The unmanaged type to encode.</typeparam>
  /// <param name="value">The value to encode.</param>
  /// <param name="fixedLength">The fixed output length.</param>
  /// <returns>The Base36 string.</returns>
  public static string Encode<T>(T value, int fixedLength) where T : unmanaged
  {
    ReadOnlySpan<byte> bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in value), 1));
    return Encode(bytes, fixedLength);
  }

  /// <summary>
  /// Decodes a Base36 string to bytes.
  /// </summary>
  /// <param name="base36String">The Base36 string to decode.</param>
  /// <param name="outputSize">The expected output size in bytes.</param>
  /// <returns>The decoded bytes.</returns>
  /// <exception cref="FormatException">Thrown if the string contains invalid characters.</exception>
  public static byte[] DecodeToBytes(string base36String, int outputSize)
  {
    if (string.IsNullOrEmpty(base36String)) throw new ArgumentException("Base36 string cannot be null or empty.", nameof(base36String));
    if (outputSize <= 0) throw new ArgumentOutOfRangeException(nameof(outputSize));

    BigInteger bigInt = BigInteger.Zero;

    foreach (char c in base36String)
    {
      int charValue = Alphabet.IndexOf(char.ToLowerInvariant(c));
      if (charValue == -1) throw new FormatException($"Invalid Base36 character '{c}'.");
      bigInt = (bigInt * Radix) + charValue;
    }

    Span<byte> bytes = outputSize <= 128 ? stackalloc byte[outputSize] : new byte[outputSize];
    bytes.Clear();
    Span<byte> temp = outputSize <= 128 ? stackalloc byte[outputSize] : new byte[outputSize];
    temp.Clear();
    
    bigInt.TryWriteBytes(temp, out var written, isUnsigned: true, isBigEndian: true);

    if (written < outputSize)
    {
      temp.Slice(0, written).CopyTo(bytes.Slice(outputSize - written));
    }
    else
    {
      temp.CopyTo(bytes);
    }

    return bytes.ToArray();
  }

  /// <summary>
  /// Decodes a Base36 string to a structure.
  /// </summary>
  /// <typeparam name="T">The unmanaged type to decode to.</typeparam>
  /// <param name="base36String">The Base36 string to decode.</param>
  /// <returns>The decoded structure.</returns>
  public static T Decode<T>(string base36String) where T : unmanaged
  {
    int expectedSize = Unsafe.SizeOf<T>();
    byte[] bytes = DecodeToBytes(base36String, expectedSize);
    return MemoryMarshal.Read<T>(bytes);
  }

  /// <summary>
  /// Decodes a Base36 string with expected length validation.
  /// </summary>
  /// <typeparam name="T">The unmanaged type to decode to.</typeparam>
  /// <param name="base36String">The Base36 string to decode.</param>
  /// <param name="expectedLength">The expected string length.</param>
  /// <returns>The decoded structure.</returns>
  public static T Decode<T>(string base36String, int expectedLength) where T : unmanaged
  {
    if (base36String.Length != expectedLength)
      throw new ArgumentException($"Base36 string must be exactly {expectedLength} characters long.", nameof(base36String));

    return Decode<T>(base36String);
  }
}
