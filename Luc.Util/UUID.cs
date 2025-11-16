#pragma warning disable IDE1006 

using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Luc.Util;



/// <summary>
/// Represents a UUID structure with support for UUIDv4 and UUIDv7.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 16)]
public readonly struct UUID : IComparable<UUID>, IEquatable<UUID>
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

  /// <summary>
  /// Initializes a new instance of the <see cref="UUID"/> struct from a 16-byte span.
  /// </summary>
  /// <param name="bytes">A span containing exactly 16 bytes representing the UUID.</param>
  /// <exception cref="ArgumentException">Thrown if <paramref name="bytes"/> is not 16 bytes long.</exception>
  public UUID(ReadOnlySpan<byte> bytes)
  {
    if (bytes.Length != 16) 
      throw new ArgumentException("Bytes span must be exactly 16 bytes.", nameof(bytes));

    scoped Span<byte> internalSpan = MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this), 1));
    bytes.CopyTo(internalSpan);
  }

  /// <summary>
  /// Gets the raw bytes of the UUID as a read-only span.
  /// </summary>
  public ReadOnlySpan<byte> Bytes => MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1));

  /// <summary>
  /// Compares this UUID to another UUID for ordering.
  /// </summary>
  /// <param name="other">The other UUID to compare to.</param>
  /// <returns>An integer indicating the relative order.</returns>
  public int CompareTo(UUID other) => Bytes.SequenceCompareTo(other.Bytes);

  /// <summary>
  /// Determines whether this UUID is equal to another UUID.
  /// </summary>
  /// <param name="other">The other UUID to compare.</param>
  /// <returns><c>true</c> if the UUIDs are equal; otherwise, <c>false</c>.</returns>
  public bool Equals(UUID other) => Bytes.SequenceEqual(other.Bytes);

  /// <inheritdoc/>
  public override bool Equals(object? obj) => obj is UUID other && Equals(other);

  /// <inheritdoc/>
  public override int GetHashCode() => BitConverter.ToInt32(Bytes.Slice(8, 4));

  /// <summary>
  /// Determines whether two UUIDs are equal.
  /// </summary>
  public static bool operator ==(UUID left, UUID right) => left.Equals(right);

  /// <summary>
  /// Determines whether two UUIDs are not equal.
  /// </summary>
  public static bool operator !=(UUID left, UUID right) => !(left == right);

  /// <summary>
  /// Returns the canonical string representation of the UUID (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).
  /// </summary>
  /// <returns>The UUID as a string.</returns>
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

  /// <summary>
  /// Encodes the UUID as a 25-character Base36 string.
  /// </summary>
  /// <returns>The Base36 string representation.</returns>
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

  /// <summary>
  /// Decodes a 25-character Base36 string into a UUID.
  /// </summary>
  /// <param name="base36String">The Base36 string to decode.</param>
  /// <returns>The decoded UUID.</returns>
  /// <exception cref="ArgumentException">Thrown if the string is not 25 characters.</exception>
  /// <exception cref="FormatException">Thrown if the string contains invalid characters.</exception>
  public static UUID FromBase36(string base36String)
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
    return new UUID(bytes);
  }

  /// <summary>
  /// Encodes the UUID as a 28-character Base25 string (Crockford-like, no ambiguous chars).
  /// </summary>
  /// <returns>The Base25 string representation.</returns>
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

  /// <summary>
  /// Decodes a 28-character Base25 string into a UUID.
  /// </summary>
  /// <param name="base25String">The Base25 string to decode.</param>
  /// <returns>The decoded UUID.</returns>
  /// <exception cref="ArgumentException">Thrown if the string is not 28 characters.</exception>
  /// <exception cref="FormatException">Thrown if the string contains invalid characters.</exception>
  public static UUID FromBase25(string base25String)
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
    return new UUID(bytes);
  }

  // UUIDv4 static methods
  /// <summary>
  /// Generates a new random UUID (version 4).
  /// </summary>
  /// <returns>A new UUIDv4 instance.</returns>
  public static UUID NewV4()
  {
    Span<byte> bytes = stackalloc byte[16];
    Random.Shared.NextBytes(bytes);

    // Set version to 4 (random)
    bytes[6] = (byte)((bytes[6] & 0x0F) | 0x40);
    // Set variant to RFC 4122
    bytes[8] = (byte)((bytes[8] & 0x3F) | 0x80);

    return new UUID(bytes);
  }

  // UUIDv7 static methods
  /// <summary>
  /// Generates a new UUIDv7 using the current UTC timestamp and random bytes.
  /// </summary>
  /// <returns>A new UUIDv7 instance.</returns>
  public static UUID NewV7()
  {
    Span<byte> bytes = stackalloc byte[16];
    long unixEpochMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

    bytes[0] = (byte)((unixEpochMs >> 40) & 0xFF);
    bytes[1] = (byte)((unixEpochMs >> 32) & 0xFF);
    bytes[2] = (byte)((unixEpochMs >> 24) & 0xFF);
    bytes[3] = (byte)((unixEpochMs >> 16) & 0xFF);
    bytes[4] = (byte)((unixEpochMs >> 8) & 0xFF);
    bytes[5] = (byte)(unixEpochMs & 0xFF);

    Random.Shared.NextBytes(bytes[6..]);
    bytes[6] = (byte)((bytes[6] & 0x0F) | 0x70);
    bytes[8] = (byte)((bytes[8] & 0x3F) | 0x80);

    return new UUID(bytes);
  }

  /// <summary>
  /// Generates a new UUIDv7 using a specified timestamp, random bytes, and sequence bytes.
  /// </summary>
  /// <param name="unixEpochMs">Milliseconds since Unix epoch.</param>
  /// <param name="randomBytes">A span of at least 8 random bytes.</param>
  /// <param name="seqBytes">A span of at least 2 sequence bytes.</param>
  /// <returns>A new UUIDv7 instance.</returns>
  /// <exception cref="ArgumentException">Thrown if <paramref name="randomBytes"/> or <paramref name="seqBytes"/> are too short.</exception>
  public static UUID NewV7(long unixEpochMs, ReadOnlySpan<byte> randomBytes, ReadOnlySpan<byte> seqBytes)
  {
    if (randomBytes.Length < 8 || seqBytes.Length < 2)
    {
      throw new ArgumentException("Random and sequence byte spans must have sufficient length.");
    }

    Span<byte> bytes = stackalloc byte[16];

    // Timestamp (48 bits, big-endian)
    bytes[0] = (byte)((unixEpochMs >> 40) & 0xFF);
    bytes[1] = (byte)((unixEpochMs >> 32) & 0xFF);
    bytes[2] = (byte)((unixEpochMs >> 24) & 0xFF);
    bytes[3] = (byte)((unixEpochMs >> 16) & 0xFF);
    bytes[4] = (byte)((unixEpochMs >> 8) & 0xFF);
    bytes[5] = (byte)(unixEpochMs & 0xFF);

    // Sequence bytes (2 bytes)
    bytes[6] = (byte)((0x70) | (seqBytes[0] & 0x0F)); // Version 7
    bytes[7] = seqBytes[1];

    // Random bytes (8 bytes)
    randomBytes[..8].CopyTo(bytes[8..]);

    // Variant (RFC 4122/9562)
    bytes[8] = (byte)((bytes[8] & 0x3F) | 0x80);

    return new UUID(bytes);
  }

  /// <summary>
  /// Extracts the timestamp from a UUIDv7 instance.
  /// </summary>
  /// <returns>The timestamp as a <see cref="DateTimeOffset"/>.</returns>
  /// <exception cref="ArgumentException">Thrown if the UUID is not version 7.</exception>
  public DateTimeOffset V7GetTimestamp()
  {
    ReadOnlySpan<byte> bytes = Bytes;
    
    if( ((bytes[6] & 0xF0) >> 4) != 7 ) {
      throw new ArgumentException( "Not a UUIDv7 instance.");
    }

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
  /// Returns the UUID version number (for example, 4 for UUIDv4 or 7 for UUIDv7).
  /// The version is stored in the most significant 4 bits of byte 6.
  /// </summary>
  /// <summary>
  /// Returns the UUID version number (for example, 4 for UUIDv4 or 7 for UUIDv7).
  /// The version is stored in the most significant 4 bits of byte 6.
  /// </summary>
  /// <returns>The UUID version number.</returns>
  public int GetVersion()
  {
    ReadOnlySpan<byte> bytes = Bytes;
    return (bytes[6] & 0xF0) >> 4;
  }



  /// <summary>
  /// Enumeration for the UUID variant encoded in the most significant bits of byte 8.
  /// </summary>
  /// <summary>
  /// Enumeration for the UUID variant encoded in the most significant bits of byte 8.
  /// </summary>
  public enum UuidVariant
  {
    NCS,
    Rfc4122,
    Microsoft,
    Reserved
  }

  /// <summary>
  /// Returns the UUID variant encoded in the most significant bits of byte 8 as a <see cref="UuidVariant"/>.
  /// </summary>
  /// <summary>
  /// Returns the UUID variant encoded in the most significant bits of byte 8 as a <see cref="UuidVariant"/>.
  /// </summary>
  /// <returns>The UUID variant.</returns>
  public UuidVariant GetVariant()
  {
    ReadOnlySpan<byte> bytes = Bytes;
    byte variantByte = bytes[8];

    if ((variantByte & 0x80) == 0x00)
    {
        return UuidVariant.NCS;
    }
    else if ((variantByte & 0xC0) == 0x80)
    {
        return UuidVariant.Rfc4122;
    }
    else if ((variantByte & 0xE0) == 0xC0)
    {
        return UuidVariant.Microsoft;
    }
    else
    {
        return UuidVariant.Reserved;
    }
  }

  
}

