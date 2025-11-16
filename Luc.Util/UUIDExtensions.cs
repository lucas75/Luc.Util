namespace Luc.Util;

public static class UUIDExtensions
{
  public static bool AsUUIDv7(this Guid guid, out UUIDv7 uuid7)
  {
    Span<byte> rfcBytes = stackalloc byte[16];
    if (!guid.TryWriteBytes(rfcBytes, bigEndian: true, out _))
    {
      uuid7 = default;
      return false;
    }

    if ((rfcBytes[6] & 0xF0) != 0x70 || (rfcBytes[8] & 0xC0) != 0x80)
    {
      uuid7 = default;
      return false;
    }

    uuid7 = new UUIDv7(rfcBytes);
    return true;
  }

  public static bool AsUUIDv4(this Guid guid, out UUIDv4 uuid4)
  {
    Span<byte> rfcBytes = stackalloc byte[16];
    if (!guid.TryWriteBytes(rfcBytes, bigEndian: true, out _))
    {
      uuid4 = default;
      return false;
    }

    // Check for version 4 and RFC 4122 variant
    if ((rfcBytes[6] & 0xF0) != 0x40 || (rfcBytes[8] & 0xC0) != 0x80)
    {
      uuid4 = default;
      return false;
    }

    uuid4 = new UUIDv4(rfcBytes);
    return true;
  }
}
