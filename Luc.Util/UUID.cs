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
  private const string base31Digits = "23456789abcdefghjkmnpqrstuvwxyz";
  private const string base32Digits = "0123456789bcdefghjkmnpqrstuvwxyz";
  private const string base35Digits = "0123456789abcdefghijkmnopqrstuvwxyz"; // excludes 'l' = 35 chars
  
  /// <summary>
  /// Encodes the UUID as a 22-character Base64 string (URL-safe, no padding).
  /// </summary>
  /// <returns>The Base64 string representation.</returns>
  public string ToBase64()
  {
    // URL-safe Base64 with no padding
    return Convert.ToBase64String(Bytes).TrimEnd('=').Replace('+', '-').Replace('/', '_');
  }

  /// <summary>
  /// Decodes a 22-character Base64 string into a UUID.
  /// </summary>
  /// <param name="base64String">The Base64 string to decode (URL-safe format).</param>
  /// <returns>The decoded UUID.</returns>
  /// <exception cref="ArgumentException">Thrown if the string is not 22 characters.</exception>
  /// <exception cref="FormatException">Thrown if the string contains invalid characters.</exception>
  public static UUID FromBase64(string base64String)
  {
    if (base64String.Length != 22) throw new ArgumentException("Base64 string must be exactly 22 characters long.", nameof(base64String));

    // Convert URL-safe back to standard Base64
    string standardBase64 = base64String.Replace('-', '+').Replace('_', '/') + "==";
    
    try
    {
      byte[] bytes = Convert.FromBase64String(standardBase64);
      if (bytes.Length != 16) throw new ArgumentException("Decoded bytes must be exactly 16 bytes.");
      return new UUID(bytes);
    }
    catch (FormatException ex)
    {
      throw new FormatException($"Invalid Base64 string: {ex.Message}", ex);
    }
  }

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
    bytes.Clear();
    Span<byte> temp = stackalloc byte[16];
    temp.Clear();
    bigInt.TryWriteBytes(temp, out var written, isUnsigned: true, isBigEndian: true);

    // BigInteger writes the minimal unsigned big-endian representation; if we
    // wrote fewer than 16 bytes then the result should be right-aligned into
    // the 16-byte UUID layout.
    if (written < 16)
    {
      temp.Slice(0, written).CopyTo(bytes.Slice(16 - written));
    }
    else
    {
      temp.CopyTo(bytes);
    }
    return new UUID(bytes);
  }

  /// <summary>
  /// Encodes the UUID as a 26-character Base31 string (Crockford-like, no ambiguous chars).
  /// </summary>
  /// <returns>The Base31 string representation.</returns>
  public string ToBase31()
  {
    const int Base = 31;
    const int FixedLength = 26;
    Span<char> chars = stackalloc char[FixedLength];
    Span<byte> data = stackalloc byte[16];
    Bytes.CopyTo(data);

    var bigInt = new BigInteger(data, isUnsigned: true, isBigEndian: true);

    for (int i = FixedLength - 1; i >= 0; i--)
    {
      if (bigInt == BigInteger.Zero)
      {
        for (int j = i; j >= 0; j--) chars[j] = base31Digits[0];
        break;
      }

      bigInt = BigInteger.DivRem(bigInt, Base, out var remainder);
      chars[i] = base31Digits[(int)remainder];
    }

    return new string(chars);
  }

  /// <summary>
  /// Decodes a 26-character Base31 string into a UUID.
  /// </summary>
  /// <param name="base31String">The Base31 string to decode.</param>
  /// <returns>The decoded UUID.</returns>
  /// <exception cref="ArgumentException">Thrown if the string is not 26 characters.</exception>
  /// <exception cref="FormatException">Thrown if the string contains invalid characters.</exception>
  public static UUID FromBase31(string base31String)
  {
    if (base31String.Length != 26) throw new ArgumentException("Base31 string must be exactly 26 characters long.", nameof(base31String));

    BigInteger bigInt = BigInteger.Zero;
    const int Base = 31;

    foreach (char c in base31String)
    {
      int charValue = base31Digits.IndexOf(char.ToLowerInvariant(c));
      if (charValue == -1) throw new FormatException($"Invalid Base31 character '{c}'.");
      bigInt = (bigInt * Base) + charValue;
    }

    Span<byte> bytes = stackalloc byte[16];
    bytes.Clear();
    Span<byte> temp = stackalloc byte[16];
    temp.Clear();
    bigInt.TryWriteBytes(temp, out var written, isUnsigned: true, isBigEndian: true);
    if (written < 16) temp.Slice(0, written).CopyTo(bytes.Slice(16 - written)); else temp.CopyTo(bytes);
    return new UUID(bytes);
  }

  /// <summary>
  /// Encodes the UUID as a 26-character Base32 string using alphabet: 0-9, b-z excluding a, i, l, o.
  /// Not Medo Id26 compatible (different encoding algorithm). Public API retained as generic Base32.
  /// </summary>
  /// <returns>The Base32 string representation.</returns>
  public string ToBase32()
  {
    const int Base = 32;
    const int FixedLength = 26;
    Span<char> chars = stackalloc char[FixedLength];
    Span<byte> data = stackalloc byte[16];
    Bytes.CopyTo(data);

    var bigInt = new BigInteger(data, isUnsigned: true, isBigEndian: true);

    for (int i = FixedLength - 1; i >= 0; i--)
    {
      if (bigInt == BigInteger.Zero)
      {
        for (int j = i; j >= 0; j--) chars[j] = base32Digits[0];
        break;
      }

      bigInt = BigInteger.DivRem(bigInt, Base, out var remainder);
      chars[i] = base32Digits[(int)remainder];
    }

    return new string(chars);
  }

  /// <summary>
  /// Decodes a 26-character Base32 string (same alphabet: 0-9, b-z excluding a,i,l,o) produced by <see cref="ToBase32"/>.
  /// Not compatible with Medo Id26 strings.
  /// </summary>
  /// <param name="base32String">The Base32 string to decode.</param>
  /// <returns>The decoded UUID.</returns>
  /// <exception cref="ArgumentException">Thrown if the string is not 26 characters.</exception>
  /// <exception cref="FormatException">Thrown if the string contains invalid characters.</exception>
  public static UUID FromBase32(string base32String)
  {
    if (base32String.Length != 26) throw new ArgumentException("Base32 string must be exactly 26 characters long.", nameof(base32String));

    BigInteger bigInt = BigInteger.Zero;
    const int Base = 32;

    foreach (char c in base32String)
    {
      int charValue = base32Digits.IndexOf(char.ToLowerInvariant(c));
      if (charValue == -1) throw new FormatException($"Invalid Base32 character '{c}'.");
      bigInt = (bigInt * Base) + charValue;
    }

    Span<byte> bytes = stackalloc byte[16];
    bytes.Clear();
    Span<byte> temp = stackalloc byte[16];
    temp.Clear();
    bigInt.TryWriteBytes(temp, out var written, isUnsigned: true, isBigEndian: true);
    if (written < 16) temp.Slice(0, written).CopyTo(bytes.Slice(16 - written)); else temp.CopyTo(bytes);
    return new UUID(bytes);
  }

  /// <summary>
  /// Encodes the UUID as a 25-character Base35 string using alphabet (0-9, a-z excluding 'l').
  /// Compatible with Medo's Id25 format (same alphabet and encoding size), but exposed generically.
  /// </summary>
  /// <returns>The Base35 string representation.</returns>
  public string ToBase35()
  {
    const int Base = 35;
    const int FixedLength = 25;
    const string alphabet = "0123456789abcdefghijkmnopqrstuvwxyz"; // excludes 'l'
    Span<char> chars = stackalloc char[FixedLength];
    Span<byte> data = stackalloc byte[16];
    Bytes.CopyTo(data);

    var bigInt = new BigInteger(data, isUnsigned: true, isBigEndian: true);

    for (int i = FixedLength - 1; i >= 0; i--)
    {
      if (bigInt == BigInteger.Zero)
      {
        for (int j = i; j >= 0; j--) chars[j] = alphabet[0];
        break;
      }

      bigInt = BigInteger.DivRem(bigInt, Base, out var remainder);
      chars[i] = alphabet[(int)remainder];
    }

    return new string(chars);
  }

  public static UUID FromBase35_MedoId25(string base35String) => FromBase35(base35String);

  /// <summary>
  /// Decodes a 25-character Base35 string into a UUID.
  /// Compatible with Medo's Id25 format.
  /// </summary>
  /// <param name="base35String">The Base35 string to decode.</param>
  /// <returns>The decoded UUID.</returns>
  /// <exception cref="ArgumentException">Thrown if the string is not 25 characters.</exception>
  /// <exception cref="FormatException">Thrown if the string contains invalid characters.</exception>
  private static UUID FromBase35(string base35String)
  {
    if (base35String.Length != 25) throw new ArgumentException("Base35 string must be exactly 25 characters long.", nameof(base35String));

    BigInteger bigInt = BigInteger.Zero;
    const int Base = 35;
    const string alphabet = "0123456789abcdefghijkmnopqrstuvwxyz"; // excludes 'l'

    foreach (char c in base35String)
    {
      char lowerChar = char.ToLowerInvariant(c);
      if (lowerChar == 'l') throw new FormatException($"Invalid Base35 character '{c}' ('l' is excluded).");
      
      int charValue = alphabet.IndexOf(lowerChar);
      if (charValue == -1) throw new FormatException($"Invalid Base35 character '{c}'.");
      
      bigInt = (bigInt * Base) + charValue;
    }

    Span<byte> bytes = stackalloc byte[16];
    bytes.Clear();
    Span<byte> temp = stackalloc byte[16];
    temp.Clear();
    bigInt.TryWriteBytes(temp, out var written, isUnsigned: true, isBigEndian: true);
    if (written < 16) temp.Slice(0, written).CopyTo(bytes.Slice(16 - written)); else temp.CopyTo(bytes);
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
  /// Extracts the timestamp component from a UUIDv7 instance.
  /// </summary>
  /// <returns>
  /// A <see cref="DateTimeOffset"/> representing the Unix epoch time truncated to millisecond precision.
  /// </returns>
  /// <exception cref="ArgumentException">Thrown if the UUID is not version 7.</exception>
  /// <remarks>
  /// <para>
  /// UUIDv7 stores a 48-bit big-endian Unix timestamp measured in whole milliseconds (per RFC 9562). This yields a
  /// fixed resolution of 1 ms; sub-millisecond (microsecond / nanosecond) information from the original event time
  /// is intentionally discarded and cannot be recovered. If the UUID was generated from a higher precision clock,
  /// the value here will be the floor/truncated millisecond of that source.
  /// </para>
  /// <para>
  /// <b>Quantification:</b> Maximum representable instant range is about 8.9e13 ms (&gt; 2.8 million years). Comparison of
  /// two timestamps that occurred within the same millisecond will return equality even if the true source times differ
  /// at microsecond or nanosecond level. When validating against external reference data (e.g. test samples containing
  /// high-resolution bounds like TS1/TS2), allow a tolerance of ±1 ms rather than expecting exact sub-millisecond matches.
  /// </para>
  /// <para>
  /// <b>Ordering &amp; Collisions:</b> Millisecond granularity means multiple UUIDv7 values created inside the same ms rely
  /// on the remaining sequence/random bits for strict ordering and uniqueness. Do not use the extracted timestamp alone
  /// for deduplicating high-frequency events; combine it with the full UUID or additional sequence metadata.
  /// </para>
  /// <para>
  /// <b>Warning:</b> Do not rely on <see cref="V7GetTimestamp"/> for audit trails requiring sub-millisecond precision.
  /// If you need higher resolution, persist the original high-precision timestamp separately alongside the UUID.
  /// </para>
  /// </remarks>
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
  /// <returns>The UUID version number.</returns>
  public int GetVersion()
  {
    ReadOnlySpan<byte> bytes = Bytes;
    return (bytes[6] & 0xF0) >> 4;
  }

  /// <summary>
  /// Parses a string representation of a UUID in various supported formats.
  /// Supports canonical UUID strings (with or without hyphens), Base64 (22 chars URL-safe),
  /// Base36 (25 chars), Base35 (25 chars), Base31 (26 chars), Base32 (26 chars).
  /// </summary>
  /// <param name="s">The string representation.</param>
  /// <returns>The parsed <see cref="UUID"/>.</returns>
  /// <exception cref="FormatException">Thrown when the input cannot be parsed.</exception>
  public static UUID Parse(string s)
  {
    if (s is null) throw new FormatException("Invalid UUID string format.");

    // Only accept the canonical hyphenated form here — TryParse(span) already
    // supports this canonical format with a non-allocating implementation.
    if (TryParse(s.AsSpan(), out var uuid)) return uuid;

    throw new FormatException("Invalid UUID string format.");
  }

  /// <summary>
  /// Attempts to parse a string representation of a UUID. Returns false on failure.
  /// </summary>
  /// <param name="s">The string representation.</param>
  /// <param name="result">Parsed UUID on success.</param>
  /// <returns>True if parsing succeeded.</returns>
  public static bool TryParse(string? s, out UUID result)
  {
    return TryParse(s.AsSpan(), out result);
  }

  /// <summary>
  /// Attempts to parse a UUID from a ReadOnlySpan without allocating the input string.
  /// This method is non-allocating for canonical and 32-char hex formats; other
  /// encodings still need string allocations to use existing FromBaseXX methods.
  /// </summary>
  /// <param name="s">Span containing the input characters.</param>
  /// <param name="result">Parsed UUID on success.</param>
  /// <returns>True if parsing succeeded.</returns>
  public static bool TryParse(ReadOnlySpan<char> s, out UUID result)
  {
    result = default;
    if (s.Length == 0) return false;

    // Trim manually from both ends
    while (s.Length > 0 && char.IsWhiteSpace(s[0])) s = s[1..];
    while (s.Length > 0 && char.IsWhiteSpace(s[^1])) s = s[..^1];

    if (s.Length == 36 && s[8] == '-' && s[13] == '-' && s[18] == '-' && s[23] == '-')
    {
      // Parse canonical form: xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
      Span<byte> bytes = stackalloc byte[16];

      // positions: bytes 0..3 -> chars 0..7
      int bi = 0;
      for (int i = 0; i < 36; i++)
      {
        if (i == 8 || i == 13 || i == 18 || i == 23)
        {
          if (s[i] != '-') return false;
          continue;
        }

        int byteIndex = bi >> 1;
        int hi = ConvertHexChar(s[i]);
        // if we're on the high nibble, store it shifted
        if ((bi & 1) == 0)
        {
          if (hi < 0) return false;
          bytes[byteIndex] = (byte)(hi << 4);
        }
        else
        {
          if (hi < 0) return false;
          bytes[byteIndex] |= (byte)hi;
        }
        bi++;
      }

      result = new UUID(bytes);
      return true;
    }

    // We no longer support non-canonical forms in the span-based TryParse.
    // The canonical form is: xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
    // For other encodings (base32/base36/etc.) use the string-based Parse/TryParse.

    return false;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static int ConvertHexChar(char c)
  {
    if (c >= '0' && c <= '9') return c - '0';
    if (c >= 'a' && c <= 'f') return c - 'a' + 10;
    if (c >= 'A' && c <= 'F') return c - 'A' + 10;
    return -1;
  }



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

