using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Luc.Util;

/// <summary>
/// Represents a Time-Ordered UUIDv7 (RFC 9562) with explicit control over memory layout.
/// This avoids the platform-specific endianness issues of System.Guid.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 16)]
public readonly struct UUIDv7 : 
  IComparable<UUIDv7>, 
  IEquatable<UUIDv7>
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

  public UUIDv7(ReadOnlySpan<byte> bytes)
  {
    if (bytes.Length != 16) throw new ArgumentException("Bytes span must be exactly 16 bytes.", nameof(bytes));

    scoped Span<byte> internalSpan = MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this), 1));
    bytes.CopyTo(internalSpan);
  }

  /// <summary>
  /// Gets a span reference to the underlying 16 bytes in RFC 9562 (big-endian) order.
  /// </summary>
  public ReadOnlySpan<byte> Bytes => MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1));



  public int CompareTo(UUIDv7 other)
  {
    return Bytes.SequenceCompareTo(other.Bytes);
  }
  public bool Equals(UUIDv7 other)
  {
    return Bytes.SequenceEqual(other.Bytes);
  }
  public override bool Equals(object? obj)
  {
    return obj is UUIDv7 other && Equals(other);
  }
  public static bool operator ==(UUIDv7 left, UUIDv7 right)
  {
    return left.Equals(right);
  }
  public static bool operator !=(UUIDv7 left, UUIDv7 right)
  {
    return !(left == right);
  }

  /// <summary>
  /// Generates a hashcode (just take the first 4 bytes from the random section)
  /// </summary>
  public override int GetHashCode()
  {
    return BitConverter.ToInt32(Bytes.Slice(8, 4));
  }

  const string hexDigits = "0123456789abcdef";

  /// <summary>
  /// Generates the standard string representation of the UUIDv7 (e.g., ffffffff-...).
  /// This implementation is entirely custom and does not use System.Guid internally.
  /// </summary>
  public override string ToString()
  {
    Span<char> chars = stackalloc char[36]; // 36 chars long (32 hex + 4 hyphens)
    ReadOnlySpan<byte> bytes = Bytes;

    // Helper to format 'count' bytes into 'count * 2' hex chars
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

    WriteHexBytes(chars: chars, charStart: 0, bytes: bytes, byteStart: 0, byteLen: 4);
    chars[8] = '-';
    WriteHexBytes(chars: chars, charStart: 9, bytes: bytes, byteStart: 4, byteLen: 2);
    chars[13] = '-';
    WriteHexBytes(chars: chars, charStart: 14, bytes: bytes, byteStart: 6, byteLen: 2);
    chars[18] = '-';
    WriteHexBytes(chars: chars, charStart: 19, bytes: bytes, byteStart: 8, byteLen: 2);
    chars[23] = '-';
    WriteHexBytes(chars: chars, charStart: 24, bytes: bytes, byteStart: 10, byteLen: 6);

    return new string(chars);
  }


  private static readonly Random rnd = Random.Shared;

  /// <summary>
  /// Create Time Ordered Uuid7 (UUIDv7)
  /// </summary>
  public static UUIDv7 Create()
  {
    Span<byte> bytes = stackalloc byte[16];

    long unixEpochMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

    // 1. Timestamp (48 bits, big-endian)
    bytes[0] = (byte)((unixEpochMs >> 40) & 0xFF);
    bytes[1] = (byte)((unixEpochMs >> 32) & 0xFF);
    bytes[2] = (byte)((unixEpochMs >> 24) & 0xFF);
    bytes[3] = (byte)((unixEpochMs >> 16) & 0xFF);
    bytes[4] = (byte)((unixEpochMs >> 8) & 0xFF);
    bytes[5] = (byte)(unixEpochMs & 0xFF);

    // 2. Clock sequence and random bytes (10 bytes total)
    rnd.NextBytes(bytes[6..]);

    // 3. Version and Variant (set over the random data)
    // Version 7 is 0111 (7) -> sets top 4 bits of byte 6
    bytes[6] = (byte)((bytes[6] & 0x0F) | 0x70);

    // Variant is 10xxxxxx (RFC 4122/9562) -> sets top 2 bits of byte 8
    bytes[8] = (byte)((bytes[8] & 0x3F) | 0x80);

    // The bytes are already in RFC 9562 (big-endian) order.
    // Use an optimized way to create the immutable struct from the span.
    return Unsafe.As<byte, UUIDv7>(ref MemoryMarshal.GetReference(bytes));
  }

  // You can also add overloads using the new Uuid7 struct for the complex generation method:
  public static UUIDv7 CreateV7(long unixEpochMs, ReadOnlySpan<byte> randomBytes, ReadOnlySpan<byte> seqBytes)
  {
    if (randomBytes.Length < 8 || seqBytes.Length < 2)
    {
      throw new ArgumentException("Random and sequence byte spans must have sufficient length.");
    }

    Span<byte> bytes = stackalloc byte[16];

    // ... [Timestamp generation logic as before] ...
    bytes[0] = (byte)((unixEpochMs >> 40) & 0xFF);
    bytes[1] = (byte)((unixEpochMs >> 32) & 0xFF);
    bytes[2] = (byte)((unixEpochMs >> 24) & 0xFF);
    bytes[3] = (byte)((unixEpochMs >> 16) & 0xFF);
    bytes[4] = (byte)((unixEpochMs >> 8) & 0xFF);
    bytes[5] = (byte)(unixEpochMs & 0xFF);

    // ... [Version/Variant logic as before] ...
    bytes[6] = (byte)((0x70) | (seqBytes[0] & 0x0F));
    bytes[7] = seqBytes[1];
    randomBytes[..8].CopyTo(bytes[8..]);
    bytes[8] = (byte)((bytes[8] & 0x3F) | 0x80);

    return Unsafe.As<byte, UUIDv7>(ref MemoryMarshal.GetReference(bytes));
  }

  public ReadOnlySpan<byte> GetRandomAndSequenceBytes()
  {
    return Bytes[6..];
  }

  /// <summary>
  /// Extract datetimeoffset
  /// </summary>  
  public DateTimeOffset GetDateTimeOffset()
  {
    ReadOnlySpan<byte> bytes = Bytes;
    // The timestamp is 48 bits (6 bytes) stored in big-endian order
    long timestampMs =
        ((long)bytes[0] << 40) |
        ((long)bytes[1] << 32) |
        ((long)bytes[2] << 24) |
        ((long)bytes[3] << 16) |
        ((long)bytes[4] << 8) |
        bytes[5];

    return DateTimeOffset.FromUnixTimeMilliseconds(timestampMs);
  }

  /// <summary>
  /// Converts the UUIDv7 to a standard .NET System.Guid structure.
  /// </summary>
  public Guid AsGuid()
  {
    return new Guid(this.Bytes, bigEndian: true);
  }

  private const string base36Digits = "0123456789abcdefghijklmnopqrstuvwxyz";

  /// <summary>
  /// Converts the UUIDv7 to a fixed-length 25-character Base36 string representation.
  /// </summary>
  public string ToBase36()
  {
    // A 128-bit number fits into ~25 base-36 characters.
    Span<char> chars = stackalloc char[25];
    Span<byte> data = stackalloc byte[16];
    Bytes.CopyTo(data);

    // This uses BigInteger internally to handle the 128-bit math needed for true base conversion.
    var bigInt = new System.Numerics.BigInteger(data, isUnsigned: true, isBigEndian: true);

    // Manual conversion loop
    for (int i = 24; i >= 0; i--)
    {
      if (bigInt == System.Numerics.BigInteger.Zero)
      {
        // Once the number is zero, fill the remaining leading characters with '0'
        for (int j = i; j >= 0; j--)
        {
          chars[j] = '0';
        }
        break;
      }

      bigInt = System.Numerics.BigInteger.DivRem(bigInt, 36, out var remainder);
      chars[i] = base36Digits[(int)remainder];
    }

    return new string(chars);
  }

  private const string base25Digits = "23456789abcdefghjkmnpqrstuvwxyz";

  /// <summary>
  /// Converts the UUIDv7 to a fixed-length 28-character Base25 string representation 
  /// using the Medo library character set.
  /// </summary>
  public string ToBase25()
  {
    const int Base = 25;
    const int FixedLength = 28; // ~27.5 chars needed for 128 bits
    Span<char> chars = stackalloc char[FixedLength];
    Span<byte> data = stackalloc byte[16];
    Bytes.CopyTo(data);

    // BigInteger handles the 128-bit math seamlessly.
    var bigInt = new BigInteger(data, isUnsigned: true, isBigEndian: true);

    for (int i = FixedLength - 1; i >= 0; i--)
    {
      if (bigInt == BigInteger.Zero)
      {
        // Fill remaining leading characters with the base25 '0' equivalent ('b')
        for (int j = i; j >= 0; j--)
        {
          chars[j] = base25Digits[0]; // The character 'b'
        }
        break;
      }

      bigInt = BigInteger.DivRem(bigInt, Base, out var remainder);
      chars[i] = base25Digits[(int)remainder];
    }

    return new string(chars);
  }

  private static readonly int[] base36CharValues = new int[128];
  private static readonly int[] base25CharValues = new int[128];

  static UUIDv7()
  {
    // Initialize all array elements to -1 (sentinel value for 'invalid character')
    Array.Fill(base36CharValues, -1);
    Array.Fill(base25CharValues, -1);

    // Populate only valid entries with their correct values
    for (int i = 0; i < base36Digits.Length; i++)
    {
      base36CharValues[base36Digits[i]] = i;
    }
    for (int i = 0; i < base25Digits.Length; i++)
    {
      base25CharValues[base25Digits[i]] = i;
    }
  }

  /// <summary>
  /// Converts a fixed-length 25-character Base36 string representation back to a UUIDv7.
  /// Uses optimized O(1) lookup table and robust error checking.
  /// </summary>
  public static UUIDv7 FromBase36(string base36String)
  {
    if (base36String.Length != 25)
    {
      throw new ArgumentException("Base36 string must be exactly 25 characters long.", nameof(base36String));
    }

    BigInteger bigInt = BigInteger.Zero;
    const int Base = 36;

    foreach (char c in base36String)
    {
      // Use the O(1) lookup table and check sentinel value (-1)
      // We also convert to lowercase as Base36 is typically case-insensitive
      int charValue = base36CharValues[char.ToLowerInvariant(c)];

      if (charValue == -1)
      {
        throw new FormatException($"Invalid Base36 character '{c}'.");
      }

      bigInt = (bigInt * Base) + charValue;
    }

    Span<byte> bytes = stackalloc byte[16];
    bigInt.TryWriteBytes(bytes, out _, isUnsigned: true, isBigEndian: true);
    return new UUIDv7(bytes);
  }

  /// <summary>
  /// Converts a fixed-length 28-character Base25 string representation back to a UUIDv7, 
  /// using the Medo library character set and optimized O(1) lookup table.
  /// </summary>
  public static UUIDv7 FromBase25(string base25String)
  {
    if (base25String.Length != 28)
    {
      throw new ArgumentException("Base25 string must be exactly 28 characters long.", nameof(base25String));
    }

    BigInteger bigInt = BigInteger.Zero;
    const int Base = 25;

    foreach (char c in base25String)
    {
      // Use the O(1) lookup table and check sentinel value (-1)
      // Ensure input is lowercase as the base25Digits const is lowercase
      int charValue = base25CharValues[char.ToLowerInvariant(c)];

      if (charValue == -1)
      {
        throw new FormatException($"Invalid Base25 character '{c}'.");
      }

      bigInt = (bigInt * Base) + charValue;
    }

    Span<byte> bytes = stackalloc byte[16];
    bigInt.TryWriteBytes(bytes, out _, isUnsigned: true, isBigEndian: true);
    return new UUIDv7(bytes);
  }
}


public static class Uuid7Extensions
{
  public static bool AsUUIDv7(this Guid guid, out UUIDv7 uuid7)
  {
    Span<byte> rfcBytes = stackalloc byte[16];
    if (!guid.TryWriteBytes(rfcBytes, bigEndian: true, out _))
    {
      uuid7 = default;
      return false;
    }

    // Verify version (0111) and variant (10xx) bits as mandated by RFC 9562

    // Version 7 check: top 4 bits of byte 6 must be 0111 (7)
    if ((rfcBytes[6] & 0xF0) != 0x70) // FIX: Use indexer [6]
    {
      uuid7 = default;
      return false;
    }

    // Variant check: top 2 bits of byte 8 must be 10xx 
    if ((rfcBytes[8] & 0xC0) != 0x80) // FIX: Use indexer [8]
    {
      uuid7 = default;
      return false;
    }

    uuid7 = new UUIDv7(rfcBytes);
    return true;
  }
}