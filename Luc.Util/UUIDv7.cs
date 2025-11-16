using System;
using System.Runtime.InteropServices;

namespace Luc.Util;

/// <summary>
/// Represents a Time-Ordered UUIDv7 (RFC 9562).
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 16)]
public readonly struct UUIDv7 : UUID
{  
  private readonly UUIDRecord _uuid;

  public UUIDv7(ReadOnlySpan<byte> bytes)
  {
    _uuid = new UUIDRecord(bytes);
  }

  public ReadOnlySpan<byte> Bytes => _uuid.Bytes;

  public string ToBase36() => _uuid.ToBase36();

  public string ToBase25() => _uuid.ToBase25();

  public override string ToString() => _uuid.ToString();

  public override bool Equals(object? obj)
  {
    return obj is UUIDv7 other && this == other;
  }

  public override int GetHashCode() => _uuid.GetHashCode();  

  public static bool operator ==(UUIDv7 left, UUIDv7 right) => left._uuid.Equals(right._uuid);

  public static bool operator !=(UUIDv7 left, UUIDv7 right) => !left._uuid.Equals(right._uuid);

  public static UUIDv7 Create()
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

    return new UUIDv7(bytes);
  }

  public static UUIDv7 CreateV7(long unixEpochMs, ReadOnlySpan<byte> randomBytes, ReadOnlySpan<byte> seqBytes)
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

    return new UUIDv7(bytes);
  }

  public DateTimeOffset GetDateTimeOffset()
  {
    ReadOnlySpan<byte> bytes = Bytes;
    long timestampMs =
        ((long)bytes[0] << 40) |
        ((long)bytes[1] << 32) |
        ((long)bytes[2] << 24) |
        ((long)bytes[3] << 16) |
        ((long)bytes[4] << 8) |
        bytes[5];

    return DateTimeOffset.FromUnixTimeMilliseconds(timestampMs);
  }
}