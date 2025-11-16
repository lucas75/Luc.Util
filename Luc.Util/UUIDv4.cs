using System;
using System.Runtime.InteropServices;

namespace Luc.Util;

/// <summary>
/// Represents a random UUIDv4 (RFC 4122).
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 16)]
public readonly struct UUIDv4 : UUID
{
  private readonly UUIDRecord _uuid;

  public UUIDv4(ReadOnlySpan<byte> bytes)
  {
    _uuid = new UUIDRecord(bytes);
  }

  public ReadOnlySpan<byte> Bytes => _uuid.Bytes;

  public string ToBase36() => _uuid.ToBase36();

  public string ToBase25() => _uuid.ToBase25();

  public override string ToString() => _uuid.ToString();

  public override bool Equals(object? obj)
  {
    return obj is UUIDv4 other && this == other;
  }

  public override int GetHashCode() => _uuid.GetHashCode();

  public static bool operator ==(UUIDv4 left, UUIDv4 right) => left._uuid.Equals(right._uuid);

  public static bool operator !=(UUIDv4 left, UUIDv4 right) => !left._uuid.Equals(right._uuid);

  public static UUIDv4 Create()
  {
    Span<byte> bytes = stackalloc byte[16];
    Random.Shared.NextBytes(bytes);

    // Set version to 4 (random)
    bytes[6] = (byte)((bytes[6] & 0x0F) | 0x40);
    // Set variant to RFC 4122
    bytes[8] = (byte)((bytes[8] & 0x3F) | 0x80);

    return new UUIDv4(bytes);
  }
}
