using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Luc.Util;

/// <summary>
/// Represents a generic UUID structure with common functionality.
/// </summary>
 [StructLayout(LayoutKind.Sequential, Size = 16)]
 public readonly struct UUIDRecord : IComparable<UUIDRecord>, IEquatable<UUIDRecord>, UUID
{
  private readonly byte _byte00;
  private readonly byte _byte01;
  private readonly byte _byte02;
  private readonly byte _byte03;
  private readonly byte _byte04;
  private readonly byte _byte05;
  private readonly byte _byte06;
  private readonly byte _byte07;
  private readonly byte _byte08;
  private readonly byte _byte09;
  private readonly byte _byte10;
  private readonly byte _byte11;
  private readonly byte _byte12;
  private readonly byte _byte13;
  private readonly byte _byte14;
  private readonly byte _byte15;

  public UUIDRecord(
    ReadOnlySpan<byte> bytes
  )
  {
    if (bytes.Length != 16) 
      throw new ArgumentException("Bytes span must be exactly 16 bytes.", nameof(bytes));

    scoped Span<byte> internalSpan = MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this), 1));
    bytes.CopyTo(internalSpan);
  }

  public ReadOnlySpan<byte> Bytes => MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1));

  public int CompareTo(UUIDRecord other) => Bytes.SequenceCompareTo(other.Bytes);

  public bool Equals(UUIDRecord other) => Bytes.SequenceEqual(other.Bytes);

  public override bool Equals(object? obj) => obj is UUIDRecord other && Equals(other);

  public override int GetHashCode() => BitConverter.ToInt32(Bytes.Slice(8, 4));

  public static bool operator ==(UUIDRecord left, UUIDRecord right) => left.Equals(right);

  public static bool operator !=(UUIDRecord left, UUIDRecord right) => !(left == right);

  public override string ToString()
  {
    const string hexDigits = "0123456789abcdef";
    Span<char> chars = stackalloc char[36];
    ReadOnlySpan<byte> bytes = Bytes;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void WriteHexBytes(Span<char> chars, int charStart, ReadOnlySpan<byte> bytes, int byteStart, int byteLen)
    {
      for (int i = 0; i < byteLen; i++)
      {
        byte b = bytes[byteStart + i];
        chars[charStart + (i * 2)] = hexDigits[b >> 4];
        chars[charStart + (i * 2) + 1] = hexDigits[b & 0xF];
      }
    }

    WriteHexBytes(chars, 0, bytes, 0, 4);
    chars[8] = '-';
    WriteHexBytes(chars, 9, bytes, 4, 2);
    chars[13] = '-';
    WriteHexBytes(chars, 14, bytes, 6, 2);
    chars[18] = '-';
    WriteHexBytes(chars, 19, bytes, 8, 2);
    chars[23] = '-';
    WriteHexBytes(chars, 24, bytes, 10, 6);

    return new string(chars);
  }

  private const string base36Digits = "0123456789abcdefghijklmnopqrstuvwxyz";
  private const string base25Digits = "23456789abcdefghjkmnpqrstuvwxyz";

  public string ToBase36()
  {
    Span<char> chars = stackalloc char[25];
    Span<byte> data = stackalloc byte[16];
    Bytes.CopyTo(data);

    var bigInt = new BigInteger(data, isUnsigned: true, isBigEndian: true);

    for (int i = 24; i >= 0; i--)
    {
      if (bigInt == BigInteger.Zero)
      {
        for (int j = i; j >= 0; j--) chars[j] = '0';
        break;
      }

      bigInt = BigInteger.DivRem(bigInt, 36, out var remainder);
      chars[i] = base36Digits[(int)remainder];
    }

    return new string(chars);
  }

  public static UUIDRecord FromBase36(string base36String)
  {
    if (base36String.Length != 25) throw new ArgumentException("Base36 string must be exactly 25 characters long.", nameof(base36String));

    BigInteger bigInt = BigInteger.Zero;
    const int Base = 36;

    foreach (char c in base36String)
    {
      int charValue = base36Digits.IndexOf(char.ToLowerInvariant(c));
      if (charValue == -1) throw new FormatException($"Invalid Base36 character '{c}'.");
      bigInt = (bigInt * Base) + charValue;
    }

    Span<byte> bytes = stackalloc byte[16];
    bigInt.TryWriteBytes(bytes, out _, isUnsigned: true, isBigEndian: true);
    return new UUIDRecord(bytes);
  }

  public string ToBase25()
  {
    const int Base = 25;
    const int FixedLength = 28;
    Span<char> chars = stackalloc char[FixedLength];
    Span<byte> data = stackalloc byte[16];
    Bytes.CopyTo(data);

    var bigInt = new BigInteger(data, isUnsigned: true, isBigEndian: true);

    for (int i = FixedLength - 1; i >= 0; i--)
    {
      if (bigInt == BigInteger.Zero)
      {
        for (int j = i; j >= 0; j--) chars[j] = base25Digits[0];
        break;
      }

      bigInt = BigInteger.DivRem(bigInt, Base, out var remainder);
      chars[i] = base25Digits[(int)remainder];
    }

    return new string(chars);
  }

  public static UUIDRecord FromBase25(string base25String)
  {
    if (base25String.Length != 28) throw new ArgumentException("Base25 string must be exactly 28 characters long.", nameof(base25String));

    BigInteger bigInt = BigInteger.Zero;
    const int Base = 25;

    foreach (char c in base25String)
    {
      int charValue = base25Digits.IndexOf(char.ToLowerInvariant(c));
      if (charValue == -1) throw new FormatException($"Invalid Base25 character '{c}'.");
      bigInt = (bigInt * Base) + charValue;
    }

    Span<byte> bytes = stackalloc byte[16];
    bigInt.TryWriteBytes(bytes, out _, isUnsigned: true, isBigEndian: true);
    return new UUIDRecord(bytes);
  }
}
